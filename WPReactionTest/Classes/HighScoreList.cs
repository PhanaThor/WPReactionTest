using System;
using System.Collections.Generic;
using WPReactionTest.ViewModels;

namespace WPReactionTest.Classes
{
    public class HighScoreList
    {
        public HighScoreList()
        {
            this.LastHighScoreUpdate = DateTime.MinValue;
        }

        public DateTime LastHighScoreUpdate { get; set; }

        public List<HighScoreItemViewModel> HighScores { get; set; }
    }
}
