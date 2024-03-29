﻿using Dan.Main;
using Newtonsoft.Json;

namespace Dan.Models
{
    [System.Serializable]
    public struct Entry
    {
        public string Username { get; set; }
        public int Score { get; set; }
        public ulong Date { get; set; }
        public string Extra { get; set; }
        [JsonProperty] internal string UserGuid { get; set; }
        [field: System.NonSerialized] public int Rank { get; set; }
        [field: System.NonSerialized] internal string NewUsername { get; set; }
        
        /// <summary>
        /// Returns whether the entry is the current user's entry.
        /// </summary>
        public bool IsMine() => UserGuid == LeaderboardCreator.UserGuid;

        /// <summary>
        /// Returns the rank of the entry with its suffix.
        /// </summary>
        /// <returns>Rank + suffix (e.g. 1st, 2nd, 3rd, 4th, 5th, etc.).</returns>
        public string RankSuffix()
        {
            var rank = Rank;
            var lastDigit = rank % 10;
            var lastTwoDigits = rank % 100;
            var suffix = "th";
            
            if (lastDigit == 1 && lastTwoDigits != 11)
                suffix = "st";
            else if (lastDigit == 2 && lastTwoDigits != 12)
                suffix = "nd";
            else if (lastDigit == 3 && lastTwoDigits != 13)
                suffix = "rd";
            
            return $"{rank}{suffix}";
        }
    }
}