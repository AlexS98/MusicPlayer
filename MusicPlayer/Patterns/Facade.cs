using MusicPlayer.Additional;
using NAudio.Wave;
using System.IO;

namespace MusicPlayer.Patterns
{
    public class Facade
    {
        SoundSingleton singleton;
        IIterator<Song> iterator;
        ICommand command;

        public Facade(string path = @"D:\Music\Egypt Central - Discography\2008 - Egypt Central")
        {
            iterator = new Playlist(path).CreateIterator();
            singleton = SoundSingleton.GetInstance();
            command = new PlayerCommand(singleton);
        }

        public void Play(string path = @"D:\Music\Egypt Central - Discography\2008 - Egypt Central\03. Over And Under.mp3")
        {
            if (singleton.WaveOut.PlaybackState == PlaybackState.Paused)
            {
                command.Play();
            }
            else if (singleton.WaveOut.PlaybackState == PlaybackState.Playing)
            {
                command.Pause();
            }
            else 
            {
                SoundSingleton.GetInstance().StopWaveOut();
                if (File.Exists(iterator.CurrentItem().Path))
                {
                    command.Init(iterator.CurrentItem().Path);
                    command.Play();
                    string name = Path.GetFileName(iterator.CurrentItem().Path).Split('.')[0];
                }
            } 
        }

        public void Next()
        {
            SoundSingleton.GetInstance().StopWaveOut();
            if (!iterator.IsDone())
            {
                command.Init(iterator.Next().Path);
                command.Play();
            }
        }

        public void Prev()
        {
            SoundSingleton.GetInstance().StopWaveOut();
            command.Init(iterator.Prev().Path);
            command.Play();
        }
    }
}
