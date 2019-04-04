using System.Collections.Generic;
using System.Linq;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using OpenRem.Engine.Interface;

namespace OpenRem.UI
{
    class MainWindowViewModel:ViewModelBase
    {
        private IDetectManager detectManager;

        public MainWindowViewModel()
        {
            this.detectManager = ServiceLocator.Current.GetInstance<IDetectManager>();

            Analyzers = this.detectManager.GetAnalyzers().ToArray();
            SelectedAnalyzer = this.Analyzers.FirstOrDefault();
        }

        public IEnumerable<Analyzer> Analyzers { get; }
        public Analyzer SelectedAnalyzer { get; set; }
    }
}
