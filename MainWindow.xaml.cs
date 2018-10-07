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

using NAudio.Wave;
using NAudio.Wave.SampleProviders;


namespace DBMPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GlobalKeyboardHook _keyHook;
        DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            _keyHook = new GlobalKeyboardHook();
            _keyHook.AddCallback("Play", Keys.D1, Play);

            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(UpdateKeyboardHook);
            _timer.Interval += new TimeSpan(0, 0, 0, 0, 34);
            _timer.Start();
        }


        private void UpdateKeyboardHook(object sender, EventArgs e)
        {
            _keyHook.Update();
        }


        private void Button_Click_Previous(object sender, RoutedEventArgs e)
        {
            
        }


        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            
        }


        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {
            Play();
        }


        private void Play()
        {
            var outputDevice = new WaveOutEvent();
            var audioFile = new AudioFileReader(@"D:\Music\01 Anaa (Live).mp3");
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }
    }
}
