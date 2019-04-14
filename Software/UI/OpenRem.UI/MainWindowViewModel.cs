﻿using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OpenRem.CommonUI;
using OpenRem.Engine;
using Microsoft.Win32;

namespace OpenRem.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IRawFileRecorder rawFileRecorder;
        public RelayCommand LoadAnalyzers { get; private set; }
        public RelayCommand SelectFile { get; private set; }
        public RelayCommand StartRecording { get; private set; }
        public RelayCommand StopRecording { get; private set; }

        public UICollection<Analyzer> Analyzers { get; } = new UICollection<Analyzer>();

        private Analyzer selectedAnalyzer;
        public Analyzer SelectedAnalyzer
        {
            get => this.selectedAnalyzer;
            set => Set(() => SelectedAnalyzer, ref this.selectedAnalyzer, value);
        }

        private string outputFilename;
        private IDetectManager detectManager;

        public string OutputFilename
        {
            get => this.outputFilename;
            set => Set(() => OutputFilename, ref this.outputFilename, value);
        }

        public MainWindowViewModel(IDetectManager detectManager, IRawFileRecorder rawFileRecorder)
        {
            this.rawFileRecorder = rawFileRecorder;
            this.detectManager = detectManager;

            AddCommands();
        }

        private void AddCommands()
        {
            LoadAnalyzers = new RelayCommand(() =>
            {
                Analyzers.AddRange(detectManager.GetAnalyzers());
                SelectedAnalyzer = Analyzers.FirstOrDefault();
            });
            SelectFile = new RelayCommand(ShowSaveFileDialog);
            StartRecording = new RelayCommand(() => { this.rawFileRecorder.Start(SelectedAnalyzer.Id, OutputFilename); });
            StopRecording = new RelayCommand(() => { this.rawFileRecorder.Stop(); });
        }

        private void ShowSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "raw|*.raw", Title = "Record to raw file" };
            saveFileDialog.ShowDialog();

            OutputFilename = saveFileDialog.FileName;
        }
    }
}
