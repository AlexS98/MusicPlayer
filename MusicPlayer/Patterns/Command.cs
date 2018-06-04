using NAudio.Wave;
using System;

namespace MusicPlayer.Patterns
{
    public interface ICommand
    {
        void Init(string path);
        void Play();
        void Pause();
        void Stop();
        void Dispose();
        event InitEvent NewSongInit;
    }

    public delegate void InitEvent(string name, string time);

    public class PlayerCommand : ICommand, IDisposable
    {
        SoundSingleton Singleton { get; set; }

        public event InitEvent NewSongInit;

        private PlayerCommand() { }

        public PlayerCommand(SoundSingleton singleton)
        {
            Singleton = singleton;
        }

        public void Init(string path)
        {
            Singleton.SoundWaveOut?.Dispose();
            Singleton.SoundWaveOut = new WaveOut();
            var reader = new Mp3FileReader(path);
            Singleton.SoundWaveOut.Init(reader);
            var n = path.Split('\\');
            
            NewSongInit(n[n.Length - 1], reader.TotalTime.ToString(@"mm\:ss"));
        }

        public void Pause()
        {
            Singleton.SoundWaveOut.Pause();
        }

        public void Play()
        {
            Singleton.SoundWaveOut.Play();
        }

        public void Stop()
        {
            Singleton.SoundWaveOut.Stop();
        }

        public void Dispose()
        {
            Singleton.SoundWaveOut.Dispose();
        }
    }
}
