using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using WPReactionTest.Classes;
using WPReactionTest.ViewModels;

namespace WPReactionTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        private DateTime g_LastHighScoreUpdate = DateTime.MinValue;


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            string strItemIndex;

            if (NavigationContext.QueryString.TryGetValue("Goto", out strItemIndex))
            {
                PanoramaSelectionChangedList removed = new PanoramaSelectionChangedList();
                removed.Add(this.MenuPanoramaItem);
                PanoramaSelectionChangedList added = new PanoramaSelectionChangedList();
                added.Add(this.HighScoresPanoramaItem);

                MainMenuPanorama_SelectionChanged(this.MainPagePanorama, new SelectionChangedEventArgs(removed, added));

                this.MainPagePanorama.DefaultItem = this.MainPagePanorama.Items[Convert.ToInt32(strItemIndex)];
            }

            base.OnNavigatedTo(e);
        }


        public MainPage()
        {
            InitializeComponent();

            DataContext = App.MainPageViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }


        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            while (NavigationService.CanGoBack)
            {
                NavigationService.RemoveBackEntry();
            }

            if (!App.MainPageViewModel.IsMenuDataLoaded)
            {
                App.MainPageViewModel.LoadMainMenuData();
            }
        }

        private void MainMenuPanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count < 1)
            {
                return;
            }

            if (!(e.AddedItems[0] is PanoramaItem))
            {
                return;
            }

            PanoramaItem selectedItem = (PanoramaItem)e.AddedItems[0];

            string strTag = (string)selectedItem.Tag;
            if (strTag.Equals("highscores"))
            {
                if (g_LastHighScoreUpdate < App.DataStore.AllHighScores.LastHighScoreUpdate)
                {
                    this.LoadingGrid.Visibility = System.Windows.Visibility.Visible;
                    this.HighScoresGrid.Visibility = System.Windows.Visibility.Collapsed;

                    App.MainPageViewModel.LoadHighscoreData();

                    g_LastHighScoreUpdate = App.DataStore.AllHighScores.LastHighScoreUpdate;

                    this.LoadingGrid.Visibility = System.Windows.Visibility.Collapsed;
                    this.HighScoresGrid.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void MenuItem_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StackPanel caller = sender as StackPanel;
            MenuItemViewModel model = caller.DataContext as MenuItemViewModel;

            NavigationService.Navigate(new Uri("/ReActionTest.xaml?DifficultyLevel=" + model.DifficultyLevel, UriKind.Relative));
        }
    }
}