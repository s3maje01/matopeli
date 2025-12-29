using System;
using System.Windows.Forms;

namespace SnakeGameWinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Käynnistetään ensin valikko
            Application.Run(new StartMenuForm());
        }
    }
}
