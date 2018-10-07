using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DBMPlayer
{
    class Playlist
    {
        private List<string> _paths;

        public Playlist()
        {
            _paths = new List<string>();
        }


        public void Add(string path)
        {
            _paths.Add(path);
        }


        public void Remove(int index)
        {
            _paths.RemoveAt(index);
        }


        public void Remove(string path)
        {
            _paths.Remove(path);
        }


        public Playlist Shuffle()
        {
            Random rand = new Random();
            Playlist result = new Playlist();
            result._paths.AddRange(_paths);
            int count = result._paths.Count;
            for (int i = 0; i < count - 2; i++)
            {
                int j = rand.Next(i, count);
                string aux = result._paths[i];
                result._paths[i] = result._paths[j];
                result._paths[j] = aux;
            }
            return result;
        }
    }
}
