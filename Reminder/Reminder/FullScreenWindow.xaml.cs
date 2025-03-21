using System.ComponentModel;
using System.Windows;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for FullScreenWindow.xaml
    /// </summary>
    public partial class FullScreenWindow : Window
    {

        public FullScreenWindow()
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
