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
        private IDetectManager detectManager;
        private IRawFileRecorder rawFileRecorder;


        public RelayCommand SelectFile { get; private set; }
        public RelayCommand StartRecording { get; private set; }
        public RelayCommand StopRecording { get; private set; }

        public UICollection<Analyzer> Analyzers { get; }

        private Analyzer selectedAnalyzer;
        public Analyzer SelectedAnalyzer
        {
            get => this.selectedAnalyzer;
            set => Set(() => this.SelectedAnalyzer, ref selectedAnalyzer, value);
        }

        private string outputFilename;
        public string OutputFilename
        {
            get => this.outputFilename;
            set => Set(() => this.OutputFilename, ref outputFilename, value);
        }

        public MainWindowViewModel(IDetectManager detectManager, IRawFileRecorder rawFileRecorder)
        {
            this.detectManager = detectManager;
            this.rawFileRecorder = rawFileRecorder;


            Analyzers = new UICollection<Analyzer>(this.detectManager.GetAnalyzers());
            SelectedAnalyzer = this.Analyzers.FirstOrDefault();

            AddCommands();
        }

        private void AddCommands()
        {
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
