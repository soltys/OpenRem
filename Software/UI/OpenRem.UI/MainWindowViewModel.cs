using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using OpenRem.Engine;

namespace OpenRem.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IDetectManager detectManager;

        public MainWindowViewModel(IDetectManager detectManager)
        {
            this.detectManager = detectManager;

            Analyzers = this.detectManager.GetAnalyzers();
            SelectedAnalyzer = this.Analyzers.FirstOrDefault();
        }

        public IEnumerable<Analyzer> Analyzers { get; }

        private Analyzer selectedAnalyzer;
        public Analyzer SelectedAnalyzer
        {
            get => this.selectedAnalyzer;
            set => Set(() => this.SelectedAnalyzer, ref selectedAnalyzer, value);
        }
    }
}
