using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AudioTools;
using AudioTools.DeviceDetection;
using AudioTools.Interface;
using AudioTools.Interface.DeviceDetection;
using AudioToolsPlayground.Commands;

namespace AudioToolsPlayground
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly IAudioDeviceDetector _deviceDetector;
        private IAudioDevice _selectedAudioDevice;
        private IEnumerable<IAudioDevice> _audioOutputDevices;

        public MainWindowViewModel()
        {
            _deviceDetector = new MmAudioDeviceDetector();
            _audioPlayer = new WasapiAudioPlayer();
            SelectedAudioDevice = _deviceDetector.GetDeviceSelectedInSystem();
        }

        #region Binded Properties

        public ICommand PlaySoundCommand => new DelegateCommand(PlaySound);

        public IEnumerable<IAudioDevice> AudioOutputDevices
        {
            get
            {
                if (_audioOutputDevices == null)
                {
                    _audioOutputDevices = _deviceDetector.GetOutputDevices();
                }

                return _audioOutputDevices;
            }
        }

        public IAudioDevice SelectedAudioDevice
        {
            get => _selectedAudioDevice;
            set {
                _selectedAudioDevice = value; 
                OnPropertyChanged(nameof(SelectedAudioDevice));
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void PlaySound()
        {
            var sampleRate = 16000;
            var frequency = 500;
            var amplitude = 0.2;
            var seconds = 1;

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

            _audioPlayer.PlaySound(raw, sampleRate, BitDepth.Of16, Channels.Mono, _selectedAudioDevice.Id);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}