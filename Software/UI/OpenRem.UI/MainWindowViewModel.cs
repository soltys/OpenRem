using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OpenRem.CommonUI;
using OpenRem.Engine;
using Microsoft.Win32;
using OpenRem.Service.Interface;

namespace OpenRem.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IServiceWrapper serviceWrapper;

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

        public MainWindowViewModel(IServiceWrapper serviceWrapper)
        {
            this.serviceWrapper = serviceWrapper;

            if (!serviceWrapper.IsRunning)
            {
                serviceWrapper.StartServer();
            }

            Analyzers = new UICollection<Analyzer>(serviceWrapper.GetAnalyzers());
            SelectedAnalyzer = this.Analyzers.FirstOrDefault();

            AddCommands();
        }

        private void AddCommands()
        {
            SelectFile = new RelayCommand(ShowSaveFileDialog);
            StartRecording = new RelayCommand(() => { this.serviceWrapper.StartRawFileRecorder(SelectedAnalyzer.Id, OutputFilename); });
            StopRecording = new RelayCommand(() => { this.serviceWrapper.StopRawFileRecorder(); });
        }

        private void ShowSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "raw|*.raw", Title = "Record to raw file" };
            saveFileDialog.ShowDialog();

            OutputFilename = saveFileDialog.FileName;
        }
    }
}
