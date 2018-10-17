using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Windows.Controls.Primitives;
using Microsoft.Win32;
using System.IO;


using NAudio.Wave;
using NAudio.Wave.SampleProviders;

using Notifications.Wpf;


namespace DBMPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GlobalKeyboardHook _keyHook;
        private DispatcherTimer _timer;
        private NotificationManager _notifManager;

        private List<MusicTrack> _libraryTracks;

        public MainWindow() 
        {
            InitializeComponent();
            _notifManager = new NotificationManager();
            _keyHook = new GlobalKeyboardHook();
            _libraryTracks = new List<MusicTrack>();

            //_keyHook.AddCallback("Play", Keys.NumPad5, Play);
            _keyHook.AddCallback("HideShow", Keys.NumPad2, 
                () => 
            {
                
                if (Visibility != Visibility.Hidden)
                {
                    Visibility = Visibility.Hidden;
                }
                else
                {
                    Visibility = Visibility.Visible;
                }
            });

            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(UpdateKeyboardHook);
            _timer.Interval += new TimeSpan(0, 0, 0, 0, 34);
            _timer.Start();

            AddTracksFromFolder(@"D:\Music\");
        }

        private void UpdateKeyboardHook(object sender, EventArgs e)
        {
            _keyHook.Update();
        }

        private void AddTracksFromFolder(string path)
        {
            string[] trackPaths = Directory.GetFiles(path, "*.mp3");
            for (int i = 0; i < trackPaths.Length; i++)
            {
                _libraryTracks.Add(new MusicTrack(trackPaths[i]));
                //_notifManager.ShowInfo(trackPaths[i]);
            }
        }
    }
}
