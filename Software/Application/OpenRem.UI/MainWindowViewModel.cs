using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using Gaas.Service.Client.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OpenRem.CommonUI;
using OpenRem.Engine;
using Microsoft.Win32;
using OpenRem.Gaas.Service.Client.Interface;
using OpenRem.Common;

namespace OpenRem.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRawFileRecorder rawFileRecorder;
        public RelayCommand LoadAnalyzers { get; private set; }
        public RelayCommand SelectFile { get; private set; }
        public RelayCommand StartRecording { get; private set; }
        public RelayCommand StopRecording { get; private set; }
        public RelayCommand SendData { get; private set; }

        public UICollection<Analyzer> Analyzers { get; } = new UICollection<Analyzer>();

        private Analyzer selectedAnalyzer;

        public Analyzer SelectedAnalyzer
        {
            get => this.selectedAnalyzer;
            set => Set(() => SelectedAnalyzer, ref this.selectedAnalyzer, value);
        }

        private string outputFilename;
        private readonly IDetectManager detectManager;
        private IGraphServiceClient gsc;

        private bool send = false;
        private double offset = 0;
        private Timer timer;

        public string OutputFilename
        {
            get => this.outputFilename;
            set => Set(() => OutputFilename, ref this.outputFilename, value);
        }

        public MainWindowViewModel(IDetectManager detectManager, IRawFileRecorder rawFileRecorder, IGraphServiceClient gsc)
        {
            this.rawFileRecorder = rawFileRecorder;
            this.detectManager = detectManager;
            this.gsc = gsc;
            this.timer = new Timer(SendCallback, null, 0, 30);

            AddCommands();
        }

        private async void SendCallback(object state)
        {
            if (send)
            {
                var dataPoints = new List<DataPoint>();
                for (double i = 3; i < 99; i += 0.05)
                {
                    dataPoints.Add(new DataPoint(i + offset, Math.Log(2, i + offset)));

                }
                
                await this.gsc.DisplayDataAsync("name", dataPoints);
                offset += 0.5;
            }
        }

        private void AddCommands()
        {
            LoadAnalyzers = new RelayCommand(async () =>
            {
                Analyzers.Reset(await this.detectManager.GetAnalyzersAsync());
                SelectedAnalyzer = Analyzers.FirstOrDefault();
            });
            SelectFile = new RelayCommand(ShowSaveFileDialog);
            StartRecording = new RelayCommand(async () =>
            {
                await this.rawFileRecorder.StartAsync(SelectedAnalyzer.Id, OutputFilename);
            });
            StopRecording = new RelayCommand(async () => { await this.rawFileRecorder.StopAsync(); });
            SendData = new RelayCommand(() =>
           {
               send = !send;
           });
        }

        private void ShowSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "raw|*.raw", Title = "Record to raw file" };
            saveFileDialog.ShowDialog();

            OutputFilename = saveFileDialog.FileName;
        }
    }
}