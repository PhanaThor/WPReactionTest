using System;
using System.ComponentModel;
using WPReactionTest.Classes;

namespace WPReactionTest.ViewModels
{
    public class HighScoreItemViewModel : INotifyPropertyChanged
    {
        private string g_PlayerName;
        private int g_Result;
        private int g_Misses;
        private SpeedTestDifficultyLevel g_DifficultyLevel;


        public string PlayerName
        {
            get
            {
                return g_PlayerName;
            }
            set
            {
                if (value != g_PlayerName)
                {
                    g_PlayerName = value;
                    NotifyPropertyChanged("PlayerName");
                }
            }
        }

        public int Result
        {
            get
            {
                return g_Result;
            }
            set
            {
                if (value != g_Result)
                {
                    g_Result = value;
                    NotifyPropertyChanged("Result");
                }
            }
        }

        public int Misses
        {
            get
            {
                return g_Misses;
            }
            set
            {
                if (value != g_Misses)
                {
                    g_Misses = value;
                    NotifyPropertyChanged("Misses");
                }
            }
        }

        public SpeedTestDifficultyLevel DifficultyLevel
        {
            get
            {
                return g_DifficultyLevel;
            }
            set
            {
                if (g_DifficultyLevel != value)
                {
                    g_DifficultyLevel = value;
                    NotifyPropertyChanged("DifficultyLevel");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
