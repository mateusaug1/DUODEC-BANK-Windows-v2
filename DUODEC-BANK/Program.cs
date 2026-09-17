using System;
using System.Windows.Forms;
namespace DUODEC_BANK;
internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new SplashWindow());
    }
}