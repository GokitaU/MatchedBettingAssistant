using DevExpress.Mvvm;

namespace MatchedBettingAssistant.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                currentViewModel = value;

                this.RaisePropertyChanged(()=>this.CurrentViewModel);
            }
        }
    }
}