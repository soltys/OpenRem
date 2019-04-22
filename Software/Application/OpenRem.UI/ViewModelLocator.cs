using CommonServiceLocator;

namespace OpenRem.UI
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindow => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
    }
}