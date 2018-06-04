using MusicPlayer.Additional;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;

namespace MusicPlayer.Patterns
{
    public class Facade : IDisposable
    {
        SoundSingleton singleton;
        IIterator<Song> iterator;
        ICommand command;
        Playlist playlist;

        public Facade(string path = @"D:\Music\Egypt Central - Discography\2008 - Egypt Central")
        {
            playlist = new Playlist(path);
            iterator = playlist.CreateIterator();
            singleton = SoundSingleton.GetInstance();
            command = new PlayerCommand(singleton);
        }

        public void Play()
        {
            if (singleton.SoundWaveOut.PlaybackState == PlaybackState.Paused)
            {
                command.Play();
            }
            else if (singleton.SoundWaveOut.PlaybackState == PlaybackState.Playing)
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
            command.Init(iterator.Next().Path);
            command.Play();
        }

        public void Prev()
        {
            SoundSingleton.GetInstance().StopWaveOut();
            command.Init(iterator.Prev().Path);
            command.Play();
        }

        public Song GetCurrent()
        {
            return iterator.CurrentItem();
        }

        public List<string> GetSongsList()
        {
            var vs = new List<string>();
            foreach (var item in playlist.Songs)
            {
                vs.Add(item.Name);
            }
            return vs;
        }

        public void AddInitEvent(InitEvent myHandler)
        {
            command.NewSongInit += myHandler; 
        }

        public void Dispose()
        {
            command._Dispose();
        }
    }
}
