using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMPlayer
{
    class Playlist
    {
        public int CurrentIndex
        {
            set
            {
                _currentIndex = value;
            }
            get
            {
                return _currentIndex;
            }
        }

        public int CurrentOrderIndex
        {
            set
            {
                _currentOrderIndex = value;
            }
            get
            {
                return _currentOrderIndex;
            }
        }

        public bool Repeat
        {
            set
            {
                _repeat = value;
            }
            get
            {
                return _repeat;
            }
        }

        public List<MusicTrack> Tracks
        {
            get
            {
                return _tracks;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public bool UsePlayOrder
        {
            get
            {
                return _usePlayOrder;
            }
            set
            {
                _usePlayOrder = value;
            }
        }

        private List<MusicTrack> _tracks;
        private List<int> _playOrder;
        private int _currentIndex;
        private int _currentOrderIndex;
        private bool _repeat;
        private string _name;
        private bool _usePlayOrder;

        public Playlist(string name)
        {
            _tracks = new List<MusicTrack>();
            _playOrder = new List<int>();
            _currentIndex = 0;
            _currentOrderIndex = 0;
            _name = name;
        }

        public void AddTrack(MusicTrack track)
        {
            _tracks.Add(track);
            _playOrder.Add(_tracks.Count - 1);
        }

        public void RemoveTrack(MusicTrack track)
        {
            int index = _tracks.FindIndex((MusicTrack t) => { return track.Path.Equals(t.Path); });
            _playOrder.Remove(index);
        }
        
        public void Next()
        {
            if (_usePlayOrder)
            {
                _tracks[_playOrder[_currentOrderIndex]].Stop();
                _currentOrderIndex++;
            }
            else
            {
                _tracks[_currentIndex].Stop();
                _currentIndex++;
            }
        }

        public void Previous()
        {
            if (_usePlayOrder)
            {
                _tracks[_playOrder[_currentOrderIndex]].Stop();
                _currentOrderIndex--;
            }
            else
            {
                _tracks[_currentIndex].Stop();
                _currentIndex--;
            }
        }

        public void Shuffle()
        {
            _currentOrderIndex = 0;
            int n = _playOrder.Count;
            Random rand = new Random();
            
            for (int i = 0; i < n - 2; i++)
            {
                int j = rand.Next(i, n);
                int aux = _playOrder[i];
                _playOrder[i] = _playOrder[j];
                _playOrder[j] = aux;
            }
        }

        public void Play()
        {
            if (_usePlayOrder)
            {
                _tracks[_playOrder[_currentOrderIndex]].Play();
            }
            else
            {
                _tracks[_currentIndex].Play();
            }
        }

        public void Stop()
        {
            if (_usePlayOrder)
            {
                _tracks[_playOrder[_currentOrderIndex]].Stop();
            }
            else
            {
                _tracks[_currentIndex].Stop();
            }
        }

        public void Pause()
        {
            if (_usePlayOrder)
            {
                _tracks[_playOrder[_currentOrderIndex]].Pause();
            }
            else
            {
                _tracks[_currentIndex].Pause();
            }
        }
    }
}
