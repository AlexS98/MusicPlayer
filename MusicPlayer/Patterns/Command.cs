using NAudio.Wave;

namespace MusicPlayer.Patterns
{
    public interface ICommand
    {
        void Init(string path);
        void Play();
        void Pause();
        void Stop();
        void _Dispose();
    }

    public class PlayerCommand : ICommand
    {
        SoundSingleton Singleton { get; set; }

        private PlayerCommand() { }

        public PlayerCommand(SoundSingleton singleton)
        {
            Singleton = singleton;
        }

        public void Init(string path)
        {
            Singleton.WaveOut?.Dispose();
            Singleton.WaveOut = new WaveOut();
            var reader = new Mp3FileReader(path);
            Singleton.WaveOut.Init(reader);
        }

        public void Pause()
        {
            Singleton.WaveOut.Pause();
        }

        public void Play()
        {
            Singleton.WaveOut.Play();
        }

        public void Stop()
        {
            Singleton.WaveOut.Stop();
        }

        public void _Dispose()
        {
            Singleton.WaveOut.Dispose();
        }
    }
}
