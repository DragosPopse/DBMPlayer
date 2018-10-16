using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace DBMPlayer
{
    class MusicTrack
    {
        public string Path
        {
            get
            {
                return _path;
            }
        }

        public bool IsPlaying
        {
            get
            {
                return _playing;
            }
        }

        public bool IsPaused
        {
            get
            {
                return _paused;
            }
        }

        private string _path;
        private bool _playing;
        private bool _paused;

        public MusicTrack(string path)
        {
            _playing = false;
            _paused = false;
            _path = path;
        }

        public void Play()
        {
            WaveOutEvent device = Config.outputDevice;
            if (_playing && _paused) //resume
            {
                device.Play();
                _paused = false;
            }
            else if (!_playing)
            {
                device.Stop();
                device.Init(new AudioFileReader(_path));
                device.Play();
                _playing = true;
                _paused = false;
            }
        }

        public void Pause()
        {
            WaveOutEvent device = Config.outputDevice;
            if (_playing && !_paused)
            {
                device.Pause();
                _paused = true;
            }
        }

        public void Stop()
        {
            WaveOutEvent device = Config.outputDevice;
            if (_playing)
            {
                device.Stop();
                _paused = false;
                _playing = false;
            }
        }
    }
}
