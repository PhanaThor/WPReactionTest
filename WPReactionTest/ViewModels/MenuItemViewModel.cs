using System;
using System.ComponentModel;
using WPReactionTest.Classes;

namespace WPReactionTest.ViewModels
{
    public class MenuItemViewModel : INotifyPropertyChanged
    {
        private string g_Name;
        private string g_Description;
        private string g_ImagePath;
        private SpeedTestDifficultyLevel g_DifficultyLevel;


        public string Name
        {
            get
            {
                return g_Name;
            }
            set
            {
                if (value != g_Name)
                {
                    g_Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get
            {
                return g_Description;
            }
            set
            {
                if (value != g_Description)
                {
                    g_Description = value;
                    NotifyPropertyChanged("Description");
                }
            }
        }

        public string ImagePath
        {
            get
            {
                return g_ImagePath;
            }
            set
            {
                if (value != g_ImagePath)
                {
                    g_ImagePath = value;
                    NotifyPropertyChanged("ImagePath");
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
