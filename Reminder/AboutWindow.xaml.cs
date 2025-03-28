using System.ComponentModel;
using System.Windows;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
            Closing += new CancelEventHandler(Window_Closing);
        }

        private void Window_Closing(object? sender, CancelEventArgs e)
        {
            e.Cancel = !MainWindow.AppExiting;
            Hide();
        }
    }
}
