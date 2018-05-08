using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BattleShip.SharedLibraries
{
    public static class BattleShipLibrary
    {
        /// <summary>
        /// Generate key string
        /// </summary>
        /// <param name="Length">Length on key</param>
        /// <returns>Key string</returns>
        public static string GenerateKey (int Length)
        {
            string key = Guid.NewGuid().ToString();
            key = Regex.Replace(key, @"[^0-9a-zA-Z]+", "");
            key = key.Substring(0, Length);
            return key;
        }
    }
}
