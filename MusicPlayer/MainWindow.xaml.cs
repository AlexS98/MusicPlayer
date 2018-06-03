using Microsoft.Win32;
using MusicPlayer.Patterns;
using System;
using System.IO;
using System.Windows;

namespace MusicPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Music player (iterator, command, memento, facade, visitor, clientserver)
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Facade facade;

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            facade.Play();
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            facade.Prev();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            facade.Next();
        }

        bool initFacade;
        private void Window_Activated(object sender, EventArgs e)
        {
            if (!initFacade)
            {
                initFacade = true;
                facade = new Facade();
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
                facade = new Facade(Path.GetDirectoryName(fileDialog.FileName));
            }
        }

        private void Playlist_Click(object sender, RoutedEventArgs e)
        {
            PlaylistWindow playlistWindow = new PlaylistWindow();
            playlistWindow.Show();
        }
    }
}
