using MusicPlayer.Additional;

namespace MusicPlayer.Patterns
{
    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
        int Count { get; }
        T this[int index] { get; set; }
    }

    public interface IIterator<out T>
    {
        T First();
        T Next();
        T CurrentItem();
        T Prev();
        bool IsDone();
    }

    public class SongIterator : IIterator<Song>
    {
        private readonly IAggregate<Song> _aggregate;
        private int _current;

        public SongIterator(IAggregate<Song> aggregate)
        {
            _aggregate = aggregate;
        }

        public Song First()
        {
            return _aggregate[0];
        }

        public Song Next()
        {
            if (++_current >= _aggregate.Count)
            {
                _current = 0;
            }
            return _aggregate[_current];
        }

        public Song CurrentItem()
        {
            return _aggregate[_current];
        }

        public bool IsDone()
        {
            return _current >= _aggregate.Count;
        }

        public Song Prev()
        {
            if (_current != 0)
            {
                _current -= 1;
                return _aggregate[_current];
            }
            else
            {
                _current = _aggregate.Count - 1;
                return _aggregate[_current];
            }
        }
    }
}
