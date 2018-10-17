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
                return Tracks;
            }
        }

        private List<MusicTrack> _tracks;
        private List<int> _playOrder;
        private int _playIndex;
        private int _currentOrderIndex;
        private bool _repeat;

        public Playlist()
        {
            _tracks = new List<MusicTrack>();
            _playOrder = new List<int>();
            _playIndex = 0;
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
                _tracks[_currentOrderIndex].Stop();
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
                _tracks[_currentOrderIndex].Stop();
                _currentOrderIndex--;
            }
            else
            {
                _tracks[_playIndex].Stop();
                _playIndex--;
            }
        }
    }
}
