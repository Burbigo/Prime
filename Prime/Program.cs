using System;
using System.Windows.Forms;

namespace Prime
{
    internal static class Program
    {
        internal static bool Check;
        internal static DateTime ThisDay = DateTime.Now;
        internal static string Status;
        internal static string Login; 
      /// <summary>
        ///     Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
      {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            
        }
    }
}