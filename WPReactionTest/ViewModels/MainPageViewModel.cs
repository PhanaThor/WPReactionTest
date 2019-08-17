using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using WPReactionTest.Classes;

namespace WPReactionTest.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public bool IsMenuDataLoaded { get; private set; }
        public ObservableCollection<MenuItemViewModel> MainMenuItems { get; private set; }
        public ObservableCollection<HighScoreItemViewModel> EasyHighScores { get; private set; }
        public ObservableCollection<HighScoreItemViewModel> ModerateHighScores { get; private set; }
        public ObservableCollection<HighScoreItemViewModel> HardHighScores { get; private set; }
        public ObservableCollection<HighScoreItemViewModel> InsaneHighScores { get; private set; }


        public MainPageViewModel()
        {
            this.MainMenuItems = new ObservableCollection<MenuItemViewModel>();
            this.EasyHighScores = new ObservableCollection<HighScoreItemViewModel>();
            this.ModerateHighScores = new ObservableCollection<HighScoreItemViewModel>();
            this.HardHighScores = new ObservableCollection<HighScoreItemViewModel>();
            this.InsaneHighScores = new ObservableCollection<HighScoreItemViewModel>();
        }

        
        public void LoadMainMenuData()
        {
            this.MainMenuItems.Add(new MenuItemViewModel() { Name = "Easy", Description = "Start an easy session of ReAction", ImagePath = "/Content/Menu.Icon.Easy.png", DifficultyLevel = SpeedTestDifficultyLevel.Easy });
            this.MainMenuItems.Add(new MenuItemViewModel() { Name = "Moderate", Description = "Start a moderate session of ReAction", ImagePath = "/Content/Menu.Icon.Moderate.png", DifficultyLevel = SpeedTestDifficultyLevel.Moderate });
            this.MainMenuItems.Add(new MenuItemViewModel() { Name = "Hard", Description = "Start a hard session of ReAction", ImagePath = "/Content/Menu.Icon.Hard.png", DifficultyLevel= SpeedTestDifficultyLevel.Hard });
            this.MainMenuItems.Add(new MenuItemViewModel() { Name = "Insane", Description = "Start an insane session of ReAction", ImagePath = "/Content/Menu.Icon.Insane.png", DifficultyLevel = SpeedTestDifficultyLevel.Insane });

            this.IsMenuDataLoaded = true;
        }

        public void LoadHighscoreData()
        {
            List<HighScoreItemViewModel> highScoreItems = App.DataStore.AllHighScores.HighScores;

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                this.EasyHighScores.Clear();
                this.ModerateHighScores.Clear();
                this.HardHighScores.Clear();
                this.InsaneHighScores.Clear();

                foreach (HighScoreItemViewModel highScore in highScoreItems)
                {
                    if (highScore.DifficultyLevel == SpeedTestDifficultyLevel.Easy)
                    {
                        this.EasyHighScores.Add(highScore);
                    }
                    else if (highScore.DifficultyLevel == SpeedTestDifficultyLevel.Moderate)
                    {
                        this.ModerateHighScores.Add(highScore);
                    }
                    else if (highScore.DifficultyLevel == SpeedTestDifficultyLevel.Hard)
                    {
                        this.HardHighScores.Add(highScore);
                    }
                    else if (highScore.DifficultyLevel == SpeedTestDifficultyLevel.Insane)
                    {
                        this.InsaneHighScores.Add(highScore);
                    }
                }
            });            
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
