using MusicPlayer.Patterns;
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

        bool initFacade = false;
        private void Window_Activated(object sender, System.EventArgs e)
        {
            if (!initFacade)
            {
                initFacade = true;
                facade = new Facade();
            }
        }
    }
}
