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
            var analyzers = this.detectManager.GetAnalyzers();
        }
    }
}
