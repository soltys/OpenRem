using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AudioHandler;
using AudioHandler.Interface;
using AudioHandlerPlayground.Commands;

namespace AudioHandlerPlayground
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IAudioPlayer _audioPlayer;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel()
        {
            _audioPlayer = new AudioPlayer();
        }

        public ICommand PlaySoundCommand
        {
            get { return new DelegateCommand(PlaySound); }
        }

        private void PlaySound()
        {
            var sampleRate = 16000;
            var frequency = 500;
            var amplitude = 0.2;
            var seconds = 5;

            var raw = new byte[sampleRate * seconds * 2];

            var multiple = 2.0*frequency/sampleRate;
            for (int n = 0; n < sampleRate * seconds; n++)
            {
                var sampleSaw = ((n*multiple)%2) - 1;
                var sampleValue = sampleSaw > 0 ? amplitude : -amplitude;
                var sample = (short)(sampleValue * Int16.MaxValue);
                var bytes = BitConverter.GetBytes(sample);
                raw[n*2] = bytes[0];
                raw[n*2 + 1] = bytes[1];
            }
            
            _audioPlayer.PlaySound(raw, sampleRate, BitDepth.Of16, Channels.Mono);
        }
    }
}