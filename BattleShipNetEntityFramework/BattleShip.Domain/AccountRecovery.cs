using System;
using static BattleShip.SharedLibraries.BattleShipLibrary;

namespace BattleShip.Domain
{
    public class AccountRecovery
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public Account Account { get; set; }
        public DateTime SendDate { get; set; }

        public int AccountId { get; set; }

        public DateTime ExpireDate {
            get
            {
                return SendDate.AddHours(24);
            }
        }

        public bool Expired
        {
            get
            {
                return (ExpireDate < DateTime.Now);
            }
        }

        public AccountRecovery()
        {
            Key = GenerateKey(10);
            SendDate = DateTime.Now;
        }

        /// <summary>
        /// Deep copy AccountRecovery object
        /// </summary>
        /// <returns>Deep copy</returns>
        public AccountRecovery DeepCopy()
        {
            AccountRecovery newRecovery = (AccountRecovery)this.MemberwiseClone();

            if(newRecovery.Account != null)
            {
                newRecovery.Account.Password = null;
            }
            
            return newRecovery;
        }
    }
}
