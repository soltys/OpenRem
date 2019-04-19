using System.Linq;
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
        private readonly IDetectManager detectManager;

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
            LoadAnalyzers = new RelayCommand( async () =>
            {
                Analyzers.AddRange(await this.detectManager.GetAnalyzersAsync());
                SelectedAnalyzer = Analyzers.FirstOrDefault();
            });
            SelectFile = new RelayCommand(ShowSaveFileDialog);
            StartRecording = new RelayCommand(async () => { await this.rawFileRecorder.StartAsync(SelectedAnalyzer.Id, OutputFilename); });
            StopRecording = new RelayCommand(async () => { await this.rawFileRecorder.StopAsync(); });
        }

        private void ShowSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "raw|*.raw", Title = "Record to raw file" };
            saveFileDialog.ShowDialog();

            OutputFilename = saveFileDialog.FileName;
        }
    }
}
