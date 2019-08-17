using System;

namespace WPReactionTest.Classes
{
    public delegate void SpeedTestButtonTapped(object sender, SpeedTestEventArgs e);
    public delegate void SpeedTestButtonTimeOut(object sender, SpeedTestEventArgs e);
    public delegate void SpeedTestButtonHidden(object sender, SpeedTestEventArgs e);

    public class SpeedTestEventArgs : EventArgs
    {
        private TimeSpan g_ReactionTime;


        public SpeedTestEventArgs() { }

        public SpeedTestEventArgs(TimeSpan reactionTime)
        {
            g_ReactionTime = reactionTime;
        }

        public TimeSpan GetReactionTime()
        {
            return g_ReactionTime;
        }
    }
}
