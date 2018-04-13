using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BattleShip.Data;
using BattleShip.Domain;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleShip.GameEngine
{
    public static class GameEngine
    {
        private static BattleShipContext _context = new BattleShipContext();

        /// <summary>
        /// Add account
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <param name="email">Email</param>
        public static void NewAccount(string userName, string password, string email)
        {
            Account account = new Account
            {
                UserName = userName,
                Password = SecurePasswordHasher.Hash(password),
                Email = email
            };

            _context.Accounts.AddAsync(account);
            _context.SaveChanges();
        }

        /// <summary>
        /// Get account with id
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <returns>Task with Account</returns>
        public static async Task<Account> GetAccount(int accountId)
        {
            return await (from a in _context.Accounts where a.Id == accountId select a).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Login account
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <returns>Task with Account</returns>
        public static  /*Task<*/Account/*>*/ Login(string userName, string password)
        {
            Account account = /*await*/ (from a in _context.Accounts where a.UserName == userName select a).FirstOrDefault/*Async*/();

            if (account == null)
            {
                throw new Exception("No account with this username doesn't exist!");
            }

            if (!SecurePasswordHasher.Verify(password, account.Password))
            {
                throw new Exception("Wrong password or username!");
            }

            return account;
        }

        /// <summary>
        /// Make a new GameBoard
        /// </summary>
        /// <param name="accountId">Account id from session</param>
        /// <param name="privateGame">If game is private (need invite to join)</param>
        /// <returns>Task with gamebord's GameKey</returns>
        public static async Task<string> NewGame(int accountId, bool privateGame = false)
        {
            string gameKey;

            do
            {
                gameKey = GenerateGameKey();
            } while (GameKeyExist(gameKey));

            GameBoard gameBoard = new GameBoard
            {
                GameKey = gameKey,
                Private = privateGame
            };

            Player player;

            NewPlayer(gameKey, accountId, out player);

            await _context.GameBoards.AddAsync(gameBoard);
            _context.SaveChanges();

            return gameKey;
        }

        /// <summary>
        /// Generate and returns a random game key
        /// </summary>
        /// <returns>Random game key (string)</returns>
        private static string GenerateGameKey()
        {
            string gameKey = Guid.NewGuid().ToString();
            gameKey = Regex.Replace(gameKey, @"[^0-9a-zA-Z]+", "");
            gameKey = gameKey.Substring(0, 6);

            return gameKey;
        }

        /// <summary>
        /// Join new player to existing game
        /// </summary>
        /// <param name="gameBoardKey">Game key (string)</param>
        /// <param name="accountId">Account id from session</param>
        public static void JoinGame(string gameBoardKey, int accountId)
        {
            GameBoard game = (
                from g in _context.GameBoards
                join p in _context.Players
                on g.GameKey equals p.GameBoardKey
                where g.GameKey == gameBoardKey
                select g
             )
             .FirstOrDefault();

            if (game == null)
            {
                throw new KeyNotFoundException("Game doesn't exist!");
            }
            else if (game.Players.Count >= 2)
            {
                throw new Exception("Only two players on a GameBoard is allowed!");
            }

            Player player;

            NewPlayer(gameBoardKey, accountId, out player);

            game.Players.Add(player);
            game.TurnPlayer = player;

            _context.Update(game);
            _context.SaveChanges();
        }

        /// <summary>
        /// Check if GameBoard exist
        /// </summary>
        /// <param name="GameKey">Game key (string)</param>
        /// <returns>Validation result</returns>
        public static bool GameKeyExist(string gameKey)
        {
            string gameKeyResult = (from g in _context.GameBoards
                                    where g.GameKey == gameKey
                                    select g.GameKey).FirstOrDefault();

            return (!string.IsNullOrEmpty(gameKeyResult));
        }

        /// <summary>
        /// Get a gameboard with game key
        /// </summary>
        /// <param name="gameKey">Game key</param>
        /// <returns>Task with GameBoard</returns>
        public static async Task<GameBoard> GetGame(string gameKey)
        {
            return await (from g in _context.GameBoards
                          join p in _context.Players on g.GameKey equals p.GameBoardKey
                          join a in _context.Players on p.AccountId equals a.Id
                          join ph in _context.PlayerHits on p.Id equals ph.PlayerId
                          join php in _context.Positions on ph.PositionId equals php.Id
                          join pb in _context.PlayerBoats on p.Id equals pb.PlayerId
                          join b in _context.Boats on pb.BoatId equals b.Id
                          join bp in _context.BoatPositions on b.Id equals bp.BoatId
                          join bpp in _context.Positions on bp.PositionId equals bpp.Id
                          join bh in _context.BoatHits on b.Id equals bh.BoatId
                          join bhp in _context.Positions on bh.PositionId equals bhp.Id
                          where g.GameKey == gameKey
                          select g)
                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get a list of all open gamebords
        /// </summary>
        /// <returns>Task with all open gameboards</returns>
        public static async Task<List<GameBoard>> GetOpenGames()
        {
            List<GameBoard> openGames = await (from g in _context.GameBoards
                                               join p in _context.Players on g.GameKey equals p.GameBoardKey
                                               join a in _context.Accounts on p.AccountId equals a.Id
                                               where g.Private == false
                                               select g)
                                               .ToListAsync();

            return openGames.FindAll(og => og.Players.Count == 1);
        }

        /// <summary>
        /// Get a list of all gameboards a account plays on
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <returns>Task with List of GameBoards</returns>
        public static async Task<List<GameBoard>> GetAccountGames(int accountId)
        {
            return await (from g in _context.GameBoards
                          join p in _context.Players on g.GameKey equals p.GameBoardKey
                          join a in _context.Players on p.AccountId equals a.Id
                          join ph in _context.PlayerHits on p.Id equals ph.PlayerId
                          join php in _context.Positions on ph.PositionId equals php.Id
                          join pb in _context.PlayerBoats on p.Id equals pb.PlayerId
                          join b in _context.Boats on pb.BoatId equals b.Id
                          join bp in _context.BoatPositions on b.Id equals bp.BoatId
                          join bpp in _context.Positions on bp.PositionId equals bpp.Id
                          join bh in _context.BoatHits on b.Id equals bh.BoatId
                          join bhp in _context.Positions on bh.PositionId equals bhp.Id
                          where p.AccountId == accountId
                          select g)
                          .ToListAsync();
        }

        /// <summary>
        /// Make a new player to GameBoard
        /// </summary>
        /// <param name="gameBoardKey">GameBoard key</param>
        /// <param name="accountId">Account id from session</param>
        private static void NewPlayer(string gameBoardKey, int accountId, out Player player)
        {
            int countPlayers = (from p in _context.Players where p.GameBoardKey == gameBoardKey select p.Id).Count();

            if (countPlayers >= 2)
            {
                throw new Exception("Only two players on a GameBoard is allowed!");
            }

            List<BoatType> boatTypes = (from bt in _context.BoatTypes select bt).ToList();

            player = new Player
            {
                GameBoardKey = gameBoardKey,
                AccountId = accountId
            };

            List<PlayerBoat> playerBoats = new List<PlayerBoat>
            {
                new PlayerBoat {
                    Player = player,
                    Boat = new Boat {
                        Type = boatTypes.Find(bt => bt.Name == "BattleShip")
                    }
                },
                new PlayerBoat {
                    Player = player,
                    Boat = new Boat {
                        Type = boatTypes.Find(bt => bt.Name == "Cruiser")
                    }
                },
                new PlayerBoat {
                    Player = player,
                    Boat = new Boat {
                        Type = boatTypes.Find(bt => bt.Name == "Destroyer")
                    }
                },
                new PlayerBoat {
                    Player = player,
                    Boat = new Boat {
                        Type = boatTypes.Find(bt => bt.Name == "Destroyer")
                    }
                },
                new PlayerBoat {
                    Player = player,
                    Boat = new Boat {
                        Type = boatTypes.Find(bt => bt.Name == "Submarine")
                    }
                },
                new PlayerBoat {
                    Player = player,
                    Boat = new Boat {
                        Type = boatTypes.Find(bt => bt.Name == "Submarine")
                    }
                },
                new PlayerBoat {
                    Player = player,
                    Boat = new Boat  {
                        Type = boatTypes.Find(bt => bt.Name == "Submarine")
                    }
                }
            };

            player.Boats = playerBoats;

            _context.Players.AddAsync(player);
            _context.SaveChanges();

            PositionsBoats(player);
        }

        /// <summary>
        /// Positions Boats random, based on https://github.com/exceptionnotfound/BattleshipModellingPractice/
        /// </summary>
        private static void PositionsBoats(Player player)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            foreach (PlayerBoat playerBoat in player.Boats)
            {
                Boat boat = playerBoat.Boat;

                // Select a random row/column combination, then select a random orientation.
                // If none of the proposed squares are occupied, place the Boat
                // Do this for all Boats

                while (boat.Positions == null)
                {
                    int startX = rand.Next(0, 10);
                    int startY = rand.Next(0, 10);
                    int endY = startY;
                    int endX = startX;

                    int orientation = rand.Next(1, 101) % 2; //0 for Horizontal

                    // Calculate endY (Horizontal) or endX (Vertical)
                    if (orientation == 0)
                    {
                        endY += boat.Type.Size - 1;
                    }
                    else
                    {
                        endX += boat.Type.Size - 1;
                    }

                    //We cannot place Boats beyond the boundaries of the board
                    if (startX >= 0 && endX <= 9 && startY >= 0 && endY <= 9)
                    {
                        try
                        {

                            List<Position> positions = GetPositionsBetweenXAndY(startX, endX, startY, endY);

                            bool test = IsAnyBoatHere(player.Id, startX, endX, startY, endY);

                            // Check so positions is free to move boat to
                            if (!test)
                            {
                                SetBoatPositions(positions, boat);
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        private static void SetBoatPositions(List<Position> positions, Boat boat)
        {
            boat.Positions = new List<BoatPosition>();

            foreach (Position position in positions)
            {
                BoatPosition boatPosition = new BoatPosition
                {
                    BoatId = boat.Id,
                    PositionId = position.Id
                };

                boat.Positions.Add(boatPosition);
            }

            _context.Boats.Update(boat);
            _context.SaveChanges();
        }

        /// <summary>
        /// Shoot on a square in one a Player's board 
        /// </summary>
        /// <param name="playerId">Id for target player (int)</param>
        /// <param name="position">Position to hit (Position)</param>
        /// <returns>Hit result (bool)</returns>
        public static bool Shoot(int playerId, string gameBoardKey, int x, int y)
        {
            GameBoard game = (from g in _context.GameBoards
                              join p in _context.Players on g.GameKey equals p.GameBoardKey
                              join ph in _context.PlayerHits on p.Id equals ph.PlayerId
                              join php in _context.Positions on ph.PositionId equals php.Id
                              where g.GameKey == gameBoardKey
                              select g)
                          .FirstOrDefault();

            if (game == null)
            {
                throw new Exception("Game doesn't exist!");
            }

            if (playerId == game.TurnPlayerId)
            {
                throw new Exception("Not your turn!");
            }

            Player playerVictim;
            Player playerAttacker;

            // Check which player is victim and which is attacker
            if (playerId == game.Players.ElementAt(0).Id)
            {
                playerVictim = game.Players.ElementAt(0);
                playerAttacker = game.Players.ElementAt(1);
            }
            else if (playerId == game.Players.ElementAt(1).Id)
            {
                playerVictim = game.Players.ElementAt(1);
                playerAttacker = game.Players.ElementAt(0);
            }
            else
            {
                throw new KeyNotFoundException("You can only shoot on player, which playes on your gameboard!");
            }

            Position position = GetPositionOnXAndY(x, y);

            // Check so victim is not already hit on this position
            if (playerVictim.AlreadyHitPositions.Find(a => a.PositionId == position.Id) != null)
            {
                throw new Exception("Position is already hit!");
            }

            // Add a hit to victim
            PlayerHit playerHit = new PlayerHit
            {
                PositionId = position.Id,
                PlayerId = playerVictim.Id
            };

            _context.PlayerHits.Add(playerHit);

            // Update GameBoard's last update to now
            game.LastUpdate = DateTime.Now;

            Boat boat;

            bool result = IsAnyBoatHere(playerId, position, out boat);

            if (result)
            {
                BoatHit boatHit = new BoatHit
                {
                    PositionId = position.Id,
                    BoatId = boat.Id
                };

                _context.BoatHits.Add(boatHit);
            }
            else
            {
                game.TurnPlayer = playerVictim;
            }

            _context.Update(game);

            _context.SaveChanges();

            return result;
        }

        /// <summary>
        /// Check if any boat are on positions between start X to end x and start y and end y
        /// </summary>
        /// <param name="playerId">Player id which boats are checked</param>
        /// <param name="startX">Start x position</param>
        /// <param name="endX">End x position</param>
        /// <param name="startY">Start y position</param>
        /// <param name="endY">End y position</param>
        /// <returns>Result</returns>
        public static bool IsAnyBoatHere(int playerId, int startX, int endX, int startY, int endY)
        {
            List<BoatPosition> boatPositions = (from bp in _context.BoatPositions
                                                join p in _context.Positions
                                                on bp.PositionId equals p.Id
                                                where p.X >= startX && p.X <= endX && p.Y >= startY && p.Y <= endY
                                                select bp)
                                                .ToList();

            return (boatPositions.Count > 0);
        }

        /// <summary>
        /// Check if any boat are on position
        /// </summary>
        /// <param name="playerId">Player id which boats are checked</param>
        /// <param name="position">Position</param>
        /// <param name="boat">Returning boat which was on position</param>
        /// <returns></returns>
        public static bool IsAnyBoatHere(int playerId, Position position, out Boat boat)
        {
            List<BoatPosition> boatPositions = (from bp in _context.BoatPositions
                                                join p in _context.Positions
                                                on bp.PositionId equals p.Id
                                                join b in _context.Boats
                                                on bp.BoatId equals b.Id
                                                where p.X == position.X && p.Y == position.Y
                                                select bp)
                                                .ToList();
            if (boatPositions.Count > 0)
            {
                boat = boatPositions[0].Boat;
                return true;
            }
            else
            {
                boat = new Boat();
                return false;
            }
        }

        public static Position GetPositionOnXAndY(int x, int y)
        {
            return _context.Positions.Where(p => p.X == x && p.Y == y).FirstOrDefault();
        }

        public static List<Position> GetPositionsBetweenXAndY(int startX, int endX, int startY, int endY)
        {
            return _context.Positions.Where(p => p.X >= startX && p.X <= endX && p.Y >= startY && p.Y >= endY).ToList();
        }

        public static async void RemoveOldGameBoard()
        {
            List<GameBoard> gameBoards = await (from g in _context.GameBoards
                                                join p in _context.Players on g.GameKey equals p.GameBoardKey
                                                where g.LastUpdate < DateTime.Now.AddMonths(-1) && p.HaveSeenEndScreen == true
                                                select g)
                                                .ToListAsync();
            _context.GameBoards.RemoveRange(gameBoards);
        }
    }
}