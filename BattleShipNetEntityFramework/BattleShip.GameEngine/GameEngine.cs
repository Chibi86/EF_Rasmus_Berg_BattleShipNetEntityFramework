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
        /// Add account - Async for no need to wait on, no risk for compromising data for only adding
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
        public static Account GetAccount(int accountId)
        {
            return (from a in _context.Accounts where a.Id == accountId select a).FirstOrDefault();
        }

        /// <summary>
        /// Login account
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <returns>Task with Account</returns>
        public static  Account Login(string userName, string password)
        {
            Account account = (from a in _context.Accounts where a.UserName == userName select a).FirstOrDefault();

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
        public static string NewGame(int accountId, bool privateGame = false)
        {
            string gameKey;

            do
            {
                gameKey = GenerateGameKey();
            } while (GameKeyExist(gameKey));

            GameBoard gameBoard = new GameBoard
            {
                Key = gameKey,
                Private = privateGame
            };

            _context.GameBoards.Add(gameBoard);
            _context.SaveChanges();

            Player player;

            NewPlayer(gameKey, accountId, out player);

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
            GameBoard game = _context.GameBoards
                .Include(g => g.Players)
                .Where(g => g.Key == gameBoardKey)
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
            string gameKeyResult = _context.GameBoards
                                    .Where(g => g.Key == gameKey)
                                    .Select(g => g.Key)
                                    .FirstOrDefault();

            return (!string.IsNullOrEmpty(gameKeyResult));
        }

        /// <summary>
        /// Get a gameboard with game key
        /// </summary>
        /// <param name="gameKey">Game key</param>
        /// <returns>GameBoard</returns>
        public static GameBoard GetGame(string gameKey)
        {
            GameBoard game = _context.GameBoards
                                // Players + Account
                                .Include(g => g.Players)
                                .ThenInclude(p => p.Account)
                                // PlayerHits + Position
                                .Include(g => g.Players)
                                .ThenInclude(p => p.AlreadyHitPositions)
                                .ThenInclude(ph => ph.Position)
                                // Boats + BoatTypes
                                .Include(g => g.Players)
                                .ThenInclude(p => p.Boats)
                                .ThenInclude(b => b.Type)
                                // BoatHits + Positions
                                .Include(g => g.Players)
                                .ThenInclude(p => p.Boats)
                                .ThenInclude(b => b.Hits)
                                .ThenInclude(bh => bh.Position)
                                // BoatPositions + Positions
                                .Include(g => g.Players)
                                .ThenInclude(p => p.Boats)
                                .ThenInclude(b => b.Positions)
                                .ThenInclude(bp => bp.Position)
                                .Where(g => g.Key == gameKey)
                                .FirstOrDefault();

            return game;
        }

        /// <summary>
        /// Get a list of all open gamebords
        /// </summary>
        /// <returns>All open gameboards</returns>
        public static List<GameBoard> GetOpenGames()
        {
            List<GameBoard> openGames = _context.GameBoards
                                            .Include(g => g.Players)
                                            .ThenInclude(p => p.Account)
                                            .Where(g => g.Private == false)
                                            .ToList();

            return openGames.FindAll(og => og.Players.Count == 1);
        }

        /// <summary>
        /// Get a list of all gameboards a account plays on
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <returns>List of GameBoards</returns>
        public static List<GameBoard> GetAccountGames(int accountId)
        {
            return _context.GameBoards
                    // Players + Accounts
                    .Include(g => g.Players)
                    .ThenInclude(p => p.Account)
                    // Boats + BoatHits
                    .Include(g => g.Players)
                    .ThenInclude(p => p.Boats)
                    .ThenInclude(b => b.Hits)
                    // BoatTypes
                    .Include(g => g.Players)
                    .ThenInclude(p => p.Boats)
                    .ThenInclude(b => b.Type)
                    .Where(g => g.Players.Any(p => p.Account.Id == accountId))
                    .ToList();
        }

        /// <summary>
        /// Make a new player to GameBoard
        /// </summary>
        /// <param name="gameBoardKey">GameBoard key</param>
        /// <param name="accountId">Account id from session</param>
        private static void NewPlayer(string gameBoardKey, int accountId, out Player player)
        {
            int countPlayers = _context.Players
                                .Where(p => p.GameBoardKey == gameBoardKey)
                                .Count();

            if (countPlayers >= 2)
            {
                throw new Exception("Only two players on a GameBoard is allowed!");
            }

            List<BoatType> boatTypes = _context.BoatTypes.ToList();

            player = new Player
            {
                GameBoardKey = gameBoardKey,
                AccountId = accountId
            };

            _context.Players.Add(player);
            _context.SaveChanges();

            List <Boat> boats = new List<Boat>
            {
                new Boat
                {
                    PlayerId = player.Id,
                    BoatTypeId = boatTypes.Find(bt => bt.Name == "Battleship").Id
                },
                new Boat
                {
                    PlayerId = player.Id,
                    BoatTypeId = boatTypes.Find(bt => bt.Name == "Cruiser").Id
                },
                new Boat
                {
                    PlayerId = player.Id,
                    BoatTypeId = boatTypes.Find(bt => bt.Name == "Destroyer").Id
                },
                new Boat {
                    PlayerId = player.Id,
                    BoatTypeId = boatTypes.Find(bt => bt.Name == "Destroyer").Id
                },
                new Boat {
                    PlayerId = player.Id,
                    BoatTypeId = boatTypes.Find(bt => bt.Name == "Submarine").Id
                },
                new Boat {
                    PlayerId = player.Id,
                    BoatTypeId = boatTypes.Find(bt => bt.Name == "Submarine").Id
                },
                new Boat  {
                    PlayerId = player.Id,
                    BoatTypeId = boatTypes.Find(bt => bt.Name == "Submarine").Id
                }
            };

            _context.Boats.AddRange(boats);
            _context.SaveChanges();

            PositionsBoats(player);
        }

        /// <summary>
        /// Positions Boats random, based on https://github.com/exceptionnotfound/BattleshipModellingPractice/
        /// </summary>
        private static void PositionsBoats(Player player)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            foreach (Boat boat in player.Boats)
            {
                // Select a random row/column combination, then select a random orientation.
                // If none of the proposed squares are occupied, place the Boat
                // Do this for all Boats

                while (!boat.Positions.Any())
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

                _context.BoatPositions.Add(boatPosition);
                _context.SaveChanges();

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
            GameBoard game = _context.GameBoards
                                .Include(g => g.Players)
                                .ThenInclude(p => p.AlreadyHitPositions)
                                .ThenInclude(ph => ph.Position)
                                .Where(g => g.Key == gameBoardKey)
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
            BoatPosition boatPosition = _context.BoatPositions
                                            .Include(bp => bp.Position)
                                            .Include(bp => bp.Boat)
                                            .Where(bp => bp.Position.X >= startX && bp.Position.X <= endX && bp.Position.Y >= startY && bp.Position.Y <= endY)
                                            .FirstOrDefault();

            return (boatPosition != null);
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
            boat = _context.BoatPositions
                    .Include(bp => bp.Position)
                    .Include(bp => bp.Boat)
                    .Where(bp => bp.Position.X == position.X && bp.Position.Y == position.Y)
                    .Select(bp => bp.Boat)
                    .FirstOrDefault();

            return (boat != null);
        }

        public static Position GetPositionOnXAndY(int x, int y)
        {
            return _context.Positions.Where(p => p.X == x && p.Y == y).FirstOrDefault();
        }

        public static List<Position> GetPositionsBetweenXAndY(int startX, int endX, int startY, int endY)
        {
            return _context.Positions.Where(p => p.X >= startX && p.X <= endX && p.Y >= startY && p.Y >= endY).ToList();
        }

        /// <summary>
        /// Remove old Gameboards
        /// </summary>
        public static void RemoveOldGameBoards()
        {
            List<GameBoard> games = _context.GameBoards
                                        // Players + PlayerHits
                                        .Include(g => g.Players)
                                        .ThenInclude(p => p.AlreadyHitPositions)
                                        // Boats + BoatHits
                                        .Include(g => g.Players)
                                        .ThenInclude(p => p.Boats)
                                        .ThenInclude(b => b.Hits)
                                        // BoatPositions
                                        .Include(g => g.Players)
                                        .ThenInclude(p => p.Boats)
                                        .ThenInclude(b => b.Positions)
                                        .Where(g => g.LastUpdate < DateTime.Now.AddMonths(-1))
                                        .ToList();

            List<GameBoard> finishGames = games.FindAll(g => g.BothPlayerHasSeenEndScreen);

            foreach(GameBoard game in finishGames)
            {
                game.TurnPlayerId = null;

                foreach (Player player in game.Players)
                {
                    _context.PlayerHits.RemoveRange(player.AlreadyHitPositions);

                    foreach (Boat boat in player.Boats)
                    {
                        _context.BoatHits.RemoveRange(boat.Hits);
                        _context.BoatPositions.RemoveRange(boat.Positions);
                    }

                    _context.Boats.RemoveRange(player.Boats);
                }
                _context.Players.RemoveRange(game.Players);
            }

            _context.SaveChanges();

            _context.GameBoards.RemoveRange(finishGames);
            _context.SaveChanges();
        }
    }
}