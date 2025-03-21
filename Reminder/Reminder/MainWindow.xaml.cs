using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;
using Windows.UI.WebUI;

namespace Reminder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly AboutWindow _aboutWindow;
        private readonly CountDownWindow _countDownWindow;
        private readonly FullScreenWindow _fullScreenWindow;
        private readonly UpdateControlContentDelegate _updateContent;
        private readonly List<Window> _subWindowsList;
        private readonly GlobalVariable _gVars;
        private CancellationTokenSource? _cts;
        private const int SECONDS_PER_MINUTE = 60;
        private static bool _appExiting = false;
        private readonly String _appConfigFileName;
        private readonly int _int16ByteCounts;

        public static bool AppExiting
        {
            get { return _appExiting; }
        }

        private delegate void UpdateControlContentDelegate(String wName, string cName, String content);

        public MainWindow()
        {
            InitializeComponent();
            // initilize resources file
            _gVars = new();
            _int16ByteCounts = BitConverter.GetBytes(Int16.MaxValue).Length;
            _appConfigFileName = $"{Path.GetDirectoryName(Environment.ProcessPath)}{Path.DirectorySeparatorChar}{_gVars.ConfigDataName}";
            ReadConfigData(_appConfigFileName);
            WorkingTT.Text = Convert.ToString(_gVars.WorkingTime);
            RestingTT.Text = Convert.ToString(_gVars.RestingTime);
            AutorunCB.IsChecked = _gVars.Autostartup > 0 ? true : false;
            // initilize fields
            _aboutWindow = new();
            _countDownWindow = new CountDownWindow();
            _fullScreenWindow = new FullScreenWindow();
            _notifyIcon = new() { Text = _gVars.AppName, Icon = new(new MemoryStream(Convert.FromBase64String(_gVars.TrayIconBase64))), ContextMenuStrip = InitializeTrayIconMenu() };
            // initilize countDownWindow x,y
            Screen? srn = Screen.PrimaryScreen;
            if (srn != null)
            {
                if (_gVars.CountDownWX > 0 && _gVars.CountDownWY > 0)
                {
                    _countDownWindow.Top = _gVars.CountDownWX;
                    _countDownWindow.Left = _gVars.CountDownWY;
                }
                else
                {
                    _countDownWindow.Top = srn.Bounds.Height - _countDownWindow.Height - 50;
                    _countDownWindow.Left = srn.Bounds.Width - _countDownWindow.Width - 10;
                }
            }
            // add events to MainWindow
            Closing += new CancelEventHandler(MainWindow_Closing);
            //
            _subWindowsList = new List<Window>();
            _subWindowsList.Add(_countDownWindow);
            _subWindowsList.Add(_fullScreenWindow);
            //
            _updateContent = new UpdateControlContentDelegate(UpdateControlContent);
            // app exit event
            App.Current.SessionEnding += new SessionEndingCancelEventHandler(Application_SessionEnding);
        }

        private ContextMenuStrip InitializeTrayIconMenu()
        {
            ContextMenuStrip trayIconMenu = new();
            AddMenuItem(trayIconMenu, _gVars.Lan.Preference, new EventHandler(Preference_Click));
            AddMenuItem(trayIconMenu, _gVars.Lan.About, new EventHandler(About_Click));
            AddMenuItem(trayIconMenu, _gVars.Lan.Exit, new EventHandler(Exit_Click));
            return trayIconMenu;
        }

        private void AddMenuItem(ContextMenuStrip menu, String name, EventHandler eventHandler)
        {
            menu.Items.Add(new ToolStripMenuItem(name, null, eventHandler));
        }

        private void Preference_Click(object? sender, EventArgs e)
        {
            SwitchVisibilityMainWindow();
        }

        private void About_Click(object? sender, EventArgs e)
        {
            _aboutWindow.Show();
        }

        private void Exit_Click(object? sender, EventArgs e)
        {
            _appExiting = true;
            BeforeApplicationExit();
            App.Current.Shutdown();
        }

        private void Application_SessionEnding(object? sender, SessionEndingCancelEventArgs e)
        {
            BeforeApplicationExit();
        }

        private void BeforeApplicationExit()
        {
            // hide or close windows or components manually
            _appExiting = true;
            _gVars.CountDownWX = Convert.ToInt16(_countDownWindow.Top);
            _gVars.CountDownWY = Convert.ToInt16(_countDownWindow.Left);
            SaveConfigData(_appConfigFileName);
            _notifyIcon.Visible = false;
        }

        private void SaveConfigData(String filename)
        {
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate | FileMode.Truncate, FileAccess.Write);
            fs.WriteByte(Convert.ToByte(_gVars.WorkingTime));
            fs.WriteByte(Convert.ToByte(_gVars.RestingTime));
            fs.WriteByte(Convert.ToByte(_gVars.Autostartup));
            fs.Write(BitConverter.GetBytes(_gVars.CountDownWX), 0, _int16ByteCounts);
            fs.Write(BitConverter.GetBytes(_gVars.CountDownWY), 0, _int16ByteCounts);
            fs.Close();
        }

        private void ReadConfigData(String filename)
        {
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            try
            {
                _gVars.WorkingTime = fs.ReadByte();
                _gVars.RestingTime = fs.ReadByte();
                _gVars.Autostartup = fs.ReadByte() < 1 ? 0 : 1;
                byte[] data = new byte[_int16ByteCounts];
                fs.ReadExactly(data);
                _gVars.CountDownWX = BitConverter.ToInt16(data, 0);
                fs.ReadExactly(data);
                _gVars.CountDownWY = BitConverter.ToInt16(data, 0);
            }
            catch (Exception)
            {
                // do nothing!
            }
            finally
            {
                _gVars.WorkingTime = _gVars.WorkingTime < 0 ? 50 : _gVars.WorkingTime;
                _gVars.RestingTime = _gVars.RestingTime < 0 ? 10 : _gVars.RestingTime;
                _gVars.Autostartup = _gVars.Autostartup < 1 ? 0 : 1;
            }
            fs.Close();
        }

        private void SwitchVisibleNotifyIcon()
        {
            _notifyIcon.Visible = !_notifyIcon.Visible;
        }

        private void SwitchEnableTextBox()
        {
            WorkingTT.IsEnabled = RestingTT.IsEnabled = !WorkingTT.IsEnabled;
        }
        private void SwitchVisibilityMainWindow()
        {
            if (!_appExiting)
            {
                if (Visibility == Visibility.Visible)
                {
                    Hide();
                }
                else
                {
                    Show();
                }
                SwitchVisibleNotifyIcon();
            }
        }

        private void MainWindow_Closing(object? sender, CancelEventArgs e)
        {
            e.Cancel = true;
            SwitchVisibilityMainWindow();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                // cancel the thread
                _cts.Cancel();
                _cts.Dispose();
                // hide the subwindows
                _countDownWindow.Hide();
                _fullScreenWindow.Hide();
                StartButton.Content = _gVars.Lan.Start;
                SwitchEnableTextBox();
            }
            else
            {
                SwitchEnableTextBox();
                SwitchVisibilityMainWindow();
                StartButton.Content = _gVars.Lan.Stop;
                _gVars.WorkingTime = int.Parse(WorkingTT.Text);
                _gVars.RestingTime = int.Parse(RestingTT.Text);
                _cts = new CancellationTokenSource();
                ThreadPool.QueueUserWorkItem(new WaitCallback(CountDownWindowTask), _cts.Token);
            }
        }

        private void CountDownWindowTask(object? obj)
        {
            if (obj is null) return;
            CancellationToken ct = (CancellationToken)obj;
            while (!ct.IsCancellationRequested)
            {
                if (!ct.IsCancellationRequested) Dispatcher.BeginInvoke(_countDownWindow.Show);
                for (int i = 0; !ct.IsCancellationRequested && i < _gVars.WorkingTime * SECONDS_PER_MINUTE; ++i)
                {
                    Dispatcher.BeginInvoke(_updateContent, "CountDownW", "TimerLabel", String.Format("{0:D2} : {1:D2}", _gVars.WorkingTime - i / SECONDS_PER_MINUTE - 1, SECONDS_PER_MINUTE - i % SECONDS_PER_MINUTE - 1));
                    Thread.Sleep(1000);
                }
                Dispatcher.BeginInvoke(_countDownWindow.Hide);
                if (!ct.IsCancellationRequested) Dispatcher.BeginInvoke(_fullScreenWindow.Show);
                for (int i = 0; !ct.IsCancellationRequested && i < _gVars.RestingTime * SECONDS_PER_MINUTE; ++i)
                {
                    Dispatcher.BeginInvoke(_updateContent, "FullScreenW", "TimerLabel", String.Format("{0:D2} : {1:D2}", _gVars.RestingTime - i / SECONDS_PER_MINUTE - 1, SECONDS_PER_MINUTE - i % SECONDS_PER_MINUTE - 1));
                    Thread.Sleep(1000);
                }
                Dispatcher.BeginInvoke(_fullScreenWindow.Hide);
            }
        }

        /// <summary>
        /// Update the content
        /// </summary>
        /// <param name="wName"></param>
        /// <param name="cName"></param>
        /// <param name="content"></param>
        private void UpdateControlContent(String wName, string cName, String content)
        {
            foreach (Window w in _subWindowsList)
            {
                if (w.Name == wName)
                {
                    System.Windows.Controls.ContentControl contentControl = (System.Windows.Controls.ContentControl)w.FindName(cName);
                    contentControl.Content = content;
                }
            }
        }

        private void AutorunCB_Checked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(_gVars.RegStartAtWindowStart, true))
            {
                key?.SetValue(_gVars.SoftwareIncName, $"{Environment.ProcessPath}");
                _gVars.Autostartup = 1;
            }

        }

        private void AutorunCB_Unchecked(object sender, RoutedEventArgs e)
        {
            using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(_gVars.RegStartAtWindowStart, true))
            {
                key?.DeleteValue(_gVars.SoftwareIncName, false);
                _gVars.Autostartup = 0;
            }

        }

    }
}