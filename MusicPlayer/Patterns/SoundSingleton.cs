using NAudio.Wave;

namespace MusicPlayer.Patterns
{
    public class SoundSingleton
    {
        private WaveOut waveOut { get; set; }

        private static SoundSingleton instance;

        private SoundSingleton() { }

        public static SoundSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new SoundSingleton();
                instance.waveOut = new WaveOut();// Process.GetProcessesByName("MusicPlayer")[0].MainWindowHandle);
                instance.waveOut.DeviceNumber = 0;
                instance.waveOut.NumberOfBuffers = 1;
            }
            return instance;
        }

        public WaveOut WaveOut
        {
            set
            {
                GetInstance().waveOut = value;
            }
            get
            {
                return GetInstance().waveOut;
            }
        }

        public void StopWaveOut()
        {
            waveOut.Stop();
            waveOut = new WaveOut();
        }
    }
}
