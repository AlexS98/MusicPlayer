namespace MusicPlayer.Additional
{
    public class Song
    {
        public string Path { private set; get; }
        public string Format { private set; get; }
        public string Name { private set; get; }
        public string Singer { private set; get; }

        private Song() {}

        public Song(string path, string format, string name, string singer)
        {
            Path = path;
            Format = format;
            Name = name;
            Singer = singer;
        }
    }
}
