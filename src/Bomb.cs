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
        private readonly bool _autoRelight;

        /// <summary>
        /// 初始化闪烁窗体
        /// </summary>
        /// <param name="lightColor">闪烁颜色</param>
        /// <param name="fadeInterval">淡出间隔</param>
        /// <param name="fadeValue">淡出递减量</param>
        /// <param name="disableClose">是否禁用关闭</param>
        /// <param name="autoRelight">是否自动重启闪烁</param>
        /// <param name="bounds">窗口位置与大小，若为 null 则全屏</param>
        public Bomb(Color lightColor, int fadeInterval, double fadeValue, bool disableClose, bool autoRelight, Rectangle? bounds)
        {
            InitializeComponent();

            this.BackColor = lightColor;
            timerFade.Interval = fadeInterval;
            _fadeValue = fadeValue;
            _disableClose = disableClose;
            _autoRelight = autoRelight;

            if (bounds.HasValue)
            {
                this.Bounds = bounds.Value;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }

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
                if (_autoRelight) this.Opacity = 1;
                else
                {
                    timerFade.Stop();

                    if (_disableClose) Process.GetCurrentProcess().Kill();
                    else Application.Exit();
                }
            }
        }
    }
}
