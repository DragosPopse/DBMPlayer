using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio;

namespace DBMPlayer
{
    public static class Config
    {
        public static List<string> libraryFolders;

        public static WaveOutEvent outputDevice;

        static Config()
        {
            libraryFolders = new List<string>();
            outputDevice = new WaveOutEvent();
        }
    }
}
