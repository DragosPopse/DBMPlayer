using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMPlayer
{
    class Playlist
    {
        public int PlayIndex
        {
            set
            {
                _playIndex = value;
            }
            get
            {
                return _playIndex;
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

        private List<MusicTrack> _tracks;
        private List<int> _playOrder;
        private int _playIndex;
        private int _currentOrderIndex;
        private bool _repeat;
        private string _name;

        public Playlist(string name)
        {
            _tracks = new List<MusicTrack>();
            _playOrder = new List<int>();
            _playIndex = 0;
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
        
        public void Next(bool usePlayOrder)
        {
            if (usePlayOrder)
            {
                _tracks[_playOrder[_currentOrderIndex]].Stop();
                _currentOrderIndex++;
            }
            else
            {
                _tracks[_playIndex].Stop();
                _playIndex++;
            }
        }

        public void Previous(bool usePlayOrder)
        {
            if (usePlayOrder)
            {
                _tracks[_playOrder[_currentOrderIndex]].Stop();
                _currentOrderIndex--;
            }
            else
            {
                _tracks[_playIndex].Stop();
                _playIndex--;
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
    }
}
