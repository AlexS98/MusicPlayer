using NAudio.Wave;
using System;

namespace MusicPlayer.Patterns
{
    public interface ICommand
    {
        void Play();
        void Pause();
        void Stop();
        void _Dispose();
    }

    public class Invoker
    {
        PlayerCommand command;
        private Invoker() { }
        public Invoker(PlayerCommand c) => command = c;
        public void Run() => command.Play();
        public void Cancel() => command.Stop();
        public void Pause() => command.Pause();
        public void Init(string path) => command.Init(path);
    }

    public class PlayerCommand : ICommand
    {
        WaveOut WaveOut { get; set; }

        private PlayerCommand() { }

        public PlayerCommand(WaveOut wave) => WaveOut = wave;

        public void Init(string path)
        {
            var reader = new Mp3FileReader(path);
            WaveOut.NumberOfBuffers = 1;
            WaveOut.Init(reader);
        }

        public void Pause()
        {
            WaveOut.Pause();
        }

        public void Play()
        {
            WaveOut.Play();
        }

        public void Stop()
        {
            WaveOut.Stop();
        }

        public void _Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
