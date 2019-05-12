using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AudioTools;
using AudioTools.Devices;
using AudioTools.Interface;
using AudioTools.Interface.Config;
using AudioTools.Interface.DeviceDetection;
using AudioTools.Player;
using AudioToolsPlayground.Commands;

namespace AudioToolsPlayground
{
    public sealed class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly IAudioDeviceDetector _deviceDetector;
        private IAudioDevice _selectedAudioDevice;
        private IEnumerable<IAudioDevice> _audioOutputDevices;
        private double _volume;

        public MainWindowViewModel()
        {
            _deviceDetector = new MmAudioDeviceDetector();
            _audioPlayer = new WasapiAudioPlayer();
            SelectedAudioDevice = _deviceDetector.GetDeviceSelectedInSystem();
        }

        #region Binded Properties

        public ICommand PlaySoundCommand => new DelegateCommand(PlaySound);

        public IEnumerable<IAudioDevice> AudioOutputDevices => _audioOutputDevices ?? (_audioOutputDevices = _deviceDetector.GetOutputDevices());

        public IAudioDevice SelectedAudioDevice
        {
            get => _selectedAudioDevice;
            set {
                _selectedAudioDevice = value; 
                OnPropertyChanged(nameof(SelectedAudioDevice));
            }
        }

        public double Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                OnPropertyChanged(nameof(Volume));
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void PlaySound()
        {
            PlayRawSoundSample();
        }

        private void PlayRawSoundSample()
        {
            const int sampleRate = 16000;
            const int frequency = 500;
            const double amplitude = 0.2;
            const int seconds = 1;

            var raw = CreateRawSoundSample(sampleRate, seconds, frequency, amplitude);

            _audioPlayer.PlaySound(raw, new SoundConfig()
            {
                SamplingRate = sampleRate,
                BitDepth = BitDepth.Of16,
                Channels = Channels.Mono,
                Volume = (float)(this.Volume / 255d),
                AudioDeviceId = _selectedAudioDevice.Id
            });
        }

        private static byte[] CreateRawSoundSample(int sampleRate, int seconds, int frequency, double amplitude)
        {
            var raw = new byte[sampleRate * seconds * 2];

            var multiple = 2.0 * frequency / sampleRate;
            for (var n = 0; n < sampleRate * seconds; n++)
            {
                var sampleSaw = n * multiple % 2 - 1;
                var sampleValue = sampleSaw > 0 ? amplitude : -amplitude;
                var sample = (short) (sampleValue * short.MaxValue);
                var bytes = BitConverter.GetBytes(sample);
                raw[n * 2] = bytes[0];
                raw[n * 2 + 1] = bytes[1];
            }

            return raw;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}