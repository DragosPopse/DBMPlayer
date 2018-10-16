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
        GlobalKeyboardHook _keyHook;
        DispatcherTimer _timer;

        public MainWindow() 
        {
            InitializeComponent();
            //Visibility = Visibility.Hidden; //uncoment this if you want no interface

            _keyHook = new GlobalKeyboardHook();
            //_keyHook.AddCallback("Play", Keys.NumPad5, Play);
            _keyHook.AddCallback("Exit", Keys.NumPad2, 
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
        }


        private void UpdateKeyboardHook(object sender, EventArgs e)
        {
            _keyHook.Update();
        }
    }
}
