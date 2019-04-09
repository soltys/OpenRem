using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using OpenRem.Engine;
using OpenRem.Service.Interface;

namespace OpenRem.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        private IServiceWrapper serviceWrapper;

        public MainWindowViewModel(IServiceWrapper serviceWrapper)
        {
            this.serviceWrapper = serviceWrapper;

            if (!serviceWrapper.IsRunning)
            {
                serviceWrapper.StartServer();
            }

            Analyzers = serviceWrapper.GetAnalyzers();
            SelectedAnalyzer = Analyzers.FirstOrDefault();
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
