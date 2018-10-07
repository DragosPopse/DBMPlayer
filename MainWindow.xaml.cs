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

using NAudio.Wave;
using NAudio.Wave.SampleProviders;


namespace DBMPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click_Previous(object sender, RoutedEventArgs e)
        {
            
        }


        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            
        }


        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {
            var outputDevice = new WaveOutEvent();
            var audioFile = new AudioFileReader(@"D:\Music\01 Anaa (Live).mp3");
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }
    }
}
