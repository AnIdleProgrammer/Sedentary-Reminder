using System.Windows;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mw = new();
            Reminder.App.Current.MainWindow = mw;
            Reminder.App.Current.MainWindow.Show();
        }
    }

}
