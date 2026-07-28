using System;
using System.Windows.Forms;

namespace LightBomb
{
    public partial class Bomb : Form
    {
        private readonly double _fadeValue;
        private readonly bool _disableKeyMsg;

        public Bomb(int fadeInterval, double fadeValue, bool disableKeyMsg)
        {
            InitializeComponent();

            timerFade.Interval = fadeInterval;
            _fadeValue = fadeValue;
            _disableKeyMsg = disableKeyMsg;

            timerFade.Start();
        }

        protected override bool ProcessKeyMessage(ref Message m)
        {
            return _disableKeyMsg ? true : base.ProcessKeyMessage(ref m);
        }

        private void FadeTick(object sender, EventArgs e)
        {
            this.Opacity -= _fadeValue;
            if (this.Opacity == 0)
            {
                timerFade.Stop();
                Application.Exit();
            }
        }
    }
}
