using Microsoft.Win32;
using MusicPlayer.Patterns;
using System;
using System.IO;
using System.Windows;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Facade facade;

        bool play;
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            facade.Play();
            Play.Content = play ? "►" : "||";
            play = !play;
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            facade.Prev();
            Play.Content = "||";
            play = true;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            facade.Next();
            Play.Content = "||";
            play = true;
        }

        bool initFacade;
        private void Window_Activated(object sender, EventArgs e)
        {
            if (!initFacade)
            {
                initFacade = true;
                facade = new Facade();
                facade.AddInitEvent(ShowSongName);
            }
        }

        private void AddFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = "",
                Filter = "mp3 files (*.mp3)|*.mp3",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (fileDialog.ShowDialog() == true)
            {
                facade.Dispose();
                Play.Content = "►";
                facade = new Facade(Path.GetDirectoryName(fileDialog.FileName));
                facade.AddInitEvent(ShowSongName);
            }
        }

        private void Playlist_Click(object sender, RoutedEventArgs e)
        {
            PlaylistWindow playlistWindow = new PlaylistWindow(facade.GetSongsList());
            playlistWindow.Owner = this;
            playlistWindow.Show();
        }

        private void ShowSongName(string name, string time)
        {
            SongName.Content = name;
            TotalTime.Content = time;
        }
    }
}
