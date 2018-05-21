using MusicPlayer.Additional;
using NAudio.Wave;
using System.IO;

namespace MusicPlayer.Patterns
{
    public class Facade
    {
        WaveOut singleton;
        IIterator<Song> iterator;
        Invoker invoker;

        public Facade(string path = @"D:\Music\Egypt Central - Discography\2008 - Egypt Central")
        {
            iterator = new Playlist(path).CreateIterator();
            singleton = SoundSingleton.GetInstance().GetWaveOut();
            PlayerCommand command = new PlayerCommand(singleton);
            invoker = new Invoker(command);
        }

        public void Play(string path = @"D:\Music\Egypt Central - Discography\2008 - Egypt Central\03. Over And Under.mp3")
        {
            if (singleton.PlaybackState == PlaybackState.Playing || singleton.PlaybackState == PlaybackState.Stopped)
            {
                SoundSingleton.GetInstance().StopWaveOut();
                if (File.Exists(path))
                {
                    invoker.Init(path);
                    invoker.Run();
                    string name = Path.GetFileName(path).Split('.')[0];
                }
            }
            else if (singleton.PlaybackState == PlaybackState.Paused)
            {
                invoker.Run();
            }
            else
            {
                invoker.Pause();
            }
        }

        public void Next()
        {
            SoundSingleton.GetInstance().StopWaveOut();
            if (!iterator.IsDone())
            {
                invoker.Init(iterator.Next().Path);
                invoker.Run();
            }
        }

        public void Prev()
        {
            SoundSingleton.GetInstance().StopWaveOut();
            invoker.Init(iterator.Prev().Path);
            invoker.Run();
        }
    }
}
