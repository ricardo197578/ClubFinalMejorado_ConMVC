using System;
using System.Windows.Forms;
using ClubMinimal.Views.Forms;

namespace ClubMinimal

{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuPrincipalForm());
        }
    }
}