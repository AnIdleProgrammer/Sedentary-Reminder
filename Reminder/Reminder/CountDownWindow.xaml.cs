using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for CountDownWindow.xaml
    /// It can be draged and moved, so it's not possible to set transparency!
    /// </summary>
    public partial class CountDownWindow : Window
    {
        private bool _dragMove = false;
        private System.Windows.Point _dPoint;
        public CountDownWindow()
        {
            InitializeComponent();
            MouseLeftButtonDown += new MouseButtonEventHandler(CountDownWindow_MouseLeftButtonDown);
            MouseLeftButtonUp += new MouseButtonEventHandler(CountDownWindow_MouseLeftButtonUp);
            MouseMove += new System.Windows.Input.MouseEventHandler(CountDownWindow_MouseMove);
            Closing += new CancelEventHandler(Window_Closing);
        }

        private void CountDownWindow_MouseMove(object? sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.MouseDevice.LeftButton == MouseButtonState.Pressed && !e.GetPosition(this).Equals(_dPoint))
            {
                _dragMove = true;
                DragMove();
            }
        }
        private void CountDownWindow_MouseLeftButtonUp(object? sender, MouseButtonEventArgs e)
        {
            if (_dragMove)
            {
                _dragMove = false;
                e.Handled = true;
            }
        }
        private void CountDownWindow_MouseLeftButtonDown(object? sender, MouseButtonEventArgs e)
        {
            _dPoint = e.GetPosition(this);
        }

        private void Window_Closing(object? sender, CancelEventArgs e)
        {
            e.Cancel = !MainWindow.AppExiting;
            Hide();
        }
    }
}
