using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using WPReactionTest.Classes;
using WPReactionTest.UserControls;
using WPReactionTest.ViewModels;

namespace WPReactionTest
{
    public partial class ReActionTest : PhoneApplicationPage
    {
        private SpeedTestDifficultyLevel g_DifficultyLevel;
        private SpeedTestButton g_Button;
        private int g_MaxRounds = 0;
        private List<KeyValuePair<int, Nullable<int>>> g_Results;
        private VibrateController g_VibrateController;


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string temp = String.Empty;
            if (NavigationContext.QueryString.TryGetValue("DifficultyLevel", out temp))
            {
                g_DifficultyLevel = (SpeedTestDifficultyLevel)Enum.Parse(typeof(SpeedTestDifficultyLevel), temp, true);
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Quit the test? (Test results will not be saved)", "Confirm quit?", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                e.Cancel = true;
            }
        }


        public ReActionTest()
        {
            InitializeComponent();

            g_Results = new List<KeyValuePair<int, Nullable<int>>>();
            g_VibrateController = VibrateController.Default;
        }


        private void ContentPanel_Loaded(object sender, RoutedEventArgs e)
        {
            RunSpeedTest();
        }

        private void button_OnTap(object sender, SpeedTestEventArgs e)
        {
            int index = g_Results.Count + 1;
            SuccessVibrate();
            g_Results.Add(new KeyValuePair<int, Nullable<int>>(index, (int)e.GetReactionTime().TotalMilliseconds));
        }

        private void button_OnTimeOut(object sender, SpeedTestEventArgs e)
        {
            int index = g_Results.Count + 1;
            ErrorVibrate();
            g_Results.Add(new KeyValuePair<int, Nullable<int>>(index, null));
        }

        private void button_Hidden(object sender, SpeedTestEventArgs e)
        {
            this.ContentPanel.Children.Remove(g_Button);
            g_Button = null;

            GenerateButtonAtRandomPosition();
        }


        private void RunSpeedTest()
        {
            g_MaxRounds = GetSpeedTestRoundCount();
            this.ProgressBarTestStatus.Maximum = g_MaxRounds;
            this.ProgressBarTestStatus.Value = 0;
            GenerateRandomWait();
            GenerateButtonAtRandomPosition();
        }

        private int GetSpeedTestRoundCount()
        {
            int rounds = 0;

            if (g_DifficultyLevel == SpeedTestDifficultyLevel.Easy)
            {
                rounds = 5;
            }
            else if (g_DifficultyLevel == SpeedTestDifficultyLevel.Moderate)
            {
                rounds = 10;
            }
            else if (g_DifficultyLevel == SpeedTestDifficultyLevel.Hard)
            {
                rounds = 15;
            }
            else if (g_DifficultyLevel == SpeedTestDifficultyLevel.Insane)
            {
                rounds = 20;
            }

            return rounds;
        }

        private void GenerateRandomWait()
        {
            System.Threading.Thread.Sleep(2000);
        }

        private void GenerateButtonAtRandomPosition()
        {
            if (g_Results.Count == g_MaxRounds)
            {
                ShowResults();
                return;
            }

            g_Button = new SpeedTestButton();
            g_Button.Width = 150;
            g_Button.Height = 150;
            g_Button.OnButtonTap += new SpeedTestButtonTapped(button_OnTap);
            g_Button.OnButtonTimeOut += new SpeedTestButtonTimeOut(button_OnTimeOut);
            g_Button.Hidden += new SpeedTestButtonHidden(button_Hidden);
            this.ContentPanel.Children.Add(g_Button);

            this.ProgressBarTestStatus.Value++;
        }

        private void ShowResults()
        {
            int successfull = g_Results.Where(p => p.Value.HasValue).Count();
            int unSuccessfull = g_Results.Count - successfull;

            double meanReactionTime = (int)(g_Results.Where(p => p.Value.HasValue).Sum(p => p.Value) / successfull);
            meanReactionTime += (unSuccessfull * 5000);

            HighScoreItemViewModel highScore = new HighScoreItemViewModel();
            highScore.DifficultyLevel = g_DifficultyLevel;
            highScore.Misses = unSuccessfull;
            highScore.PlayerName = "TEST";
            highScore.Result = (int)Math.Ceiling(meanReactionTime);

            App.DataStore.AllHighScores.LastHighScoreUpdate = DateTime.Now;
            App.DataStore.AllHighScores.HighScores.Add(highScore);
            App.DataStore.SaveStore();

            NavigationService.Navigate(new Uri("/MainPage.xaml?Goto=1", UriKind.Relative));
        }

        private void SuccessVibrate()
        {
            g_VibrateController.Start(TimeSpan.FromMilliseconds(150));
        }

        private void ErrorVibrate()
        {
            for (int i = 0; i < 3; i++)
            {
                g_VibrateController.Start(TimeSpan.FromMilliseconds(100));
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
