using MusicPlayer.Patterns;
using System;
using System.Collections.Generic;
using System.IO;

namespace MusicPlayer.Additional
{
    public class Playlist : IAggregate<Song>
    {
        private string Path { get; set; }
        public List<Song> Songs { set; get; }

        public int Count
        {
            get
            {
                return Songs.ToArray().Length;
            }
        }

        public Song this[int index]
        {
            get { return Songs.ToArray()[index]; }
            set
            {
                throw new NotImplementedException();
            }
        }

        private Playlist() { }

        public Playlist(string path)
        {
            FileInfo[] Files = new DirectoryInfo(path).GetFiles("*.mp3");
            Songs = new List<Song>();
            foreach (FileInfo file in Files)
            {
                Songs.Add(new Song(file.FullName, "mp3", file.Name, file.Directory.Name));
            }
        }

        public IIterator<Song> CreateIterator()
        {
            return new SongIterator(this);
        }
    }
}
