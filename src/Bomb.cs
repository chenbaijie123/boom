using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace LightBomb
{
    public partial class Bomb : Form
    {
        private readonly double _fadeValue;
        private readonly bool _disableClose;

        public Bomb(Color lightColor, int fadeInterval, double fadeValue, bool disableClose)
        {
            InitializeComponent();

            this.BackColor = lightColor;
            timerFade.Interval = fadeInterval;
            _fadeValue = fadeValue;
            _disableClose = disableClose;

            timerFade.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = _disableClose;
        }

        private void FadeTick(object sender, EventArgs e)
        {
            this.Opacity -= _fadeValue;
            if (this.Opacity == 0)
            {
                timerFade.Stop();
                if (_disableClose) Process.GetCurrentProcess().Kill();
                else Application.Exit();
            }
        }
    }
}
