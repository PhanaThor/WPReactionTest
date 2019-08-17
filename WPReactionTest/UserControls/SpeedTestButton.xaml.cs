using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPReactionTest.Classes;

namespace WPReactionTest.UserControls
{
    public partial class SpeedTestButton : UserControl
    {
        public event SpeedTestButtonTapped OnButtonTap;
        public event SpeedTestButtonTimeOut OnButtonTimeOut;
        public event SpeedTestButtonHidden Hidden;
        

        public SpeedTestButton()
        {
            InitializeComponent();           
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.FadeToRedEffectStoryBoard.Completed += new EventHandler(FadeToRedEffectStoryBoard_Completed);
            this.HideButtonStoryBoard.Completed += new EventHandler(HideButtonStoryBoard_Completed);
            SetButtonLayout();
        }

        private void FadeToRedEffectStoryBoard_Completed(object sender, EventArgs e)
        {
            if (OnButtonTimeOut != null)
            {
                OnButtonTimeOut(this, new SpeedTestEventArgs());
            }

            this.HideButtonStoryBoard.Begin();
        }

        private void HideButtonStoryBoard_Completed(object sender, EventArgs e)
        {
            if (Hidden != null)
            {
                Hidden(this, new SpeedTestEventArgs());
            }
        }

        private void ItemButton_Tap(object sender, GestureEventArgs e)
        {
            TimeSpan reactionTime = this.FadeToRedEffectStoryBoard.GetCurrentTime();
            this.FadeToRedEffectStoryBoard.Stop();

            if (OnButtonTap != null)
            {
                OnButtonTap(this, new SpeedTestEventArgs(reactionTime));
            }

            this.HideButtonStoryBoard.Begin();
        }


        private void SetButtonLayout()
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int randNumb = rand.Next(1, 100);

            ControlTemplate selectedTemplate;

            if (randNumb % 2 == 0)
            {
                selectedTemplate = Resources["SquareButtonTemplate"] as ControlTemplate;
            }
            else
            {
                selectedTemplate = Resources["CircleButtonTemplate"] as ControlTemplate;
            }
            
            this.ItemButton.Template = selectedTemplate;
        }
    }
}
