using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace LightBomb
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int fadeInterval = 20;
            double fadeValue = 0.1;
            bool disableKeyMsg = false;

            if (args.Any(s => s == "-h" || s == "--help"))
            {
                MessageBox.Show($"""
                        LightBomb v{Assembly.GetExecutingAssembly()
                                            .GetCustomAttribute<AssemblyFileVersionAttribute>().Version}
                        © 2026 ByteBesage / 可贤

                        如参数中不包含帮助参数，
                        第一个参数为每次淡出的间隔，表示毫秒，整数。
                        第二个参数为每次淡出的量，0 至 1，双精度浮点。
                        第三个参数为是否拒绝键盘输入，开启则表示不接受任何键盘消息，布尔填整数，非零值真。

                        默认值分别为 20，0.1，0。
                        当输入无效或未指定时，则使用默认值。

                        -h | --help
                        显示这个帮助信息。

                        """, "LightBomb");
                Environment.Exit(0);
            }

            try
            {
                if (int.TryParse(args[0], out int a0))
                {
                    fadeInterval = a0;
                }

                if (double.TryParse(args[1], out double a1))
                {
                    fadeValue = a1;
                }

                if (int.TryParse(args[2], out int a2))
                {
                    disableKeyMsg = a2 != 0;
                }
            }
            catch (IndexOutOfRangeException)
            {
                ;
            }

            Application.Run(new Bomb(fadeInterval, fadeValue, disableKeyMsg));
        }
    }
}
