using Prism.Commands;
using Prism.Navigation;
using System.Linq;
using Xamarin.Forms;

namespace XFS.ViewModels
{
    public class CachePageViewModel : BaseViewModel
    {
        public CachePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            AdicionarCacheCommand = new DelegateCommand(OnAdicionarCacheCommandExecuted, AdicionarCacheCommandCanExecute)
                .ObservesProperty(() => Nome)
                .ObservesProperty(() => NomeCache);
            RemoverCacheCommand = new DelegateCommand(OnRemoverCacheCommandExecuted, RemoverCacheCommandCanExecute)
                .ObservesProperty(() => NomeCache);
        }

        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { SetProperty(ref _nome, value); }
        }


        public string NomeCache
        {
            get
            {
                if (Application.Current.Properties.Any(x => x.Key == nameof(NomeCache)))
                    return Application.Current.Properties[nameof(NomeCache)].ToString();
                return string.Empty;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Application.Current.Properties.Remove(nameof(NomeCache));
                else
                    Application.Current.Properties[nameof(NomeCache)] = value;
                RaisePropertyChanged(nameof(NomeCache));
                RaisePropertyChanged(nameof(LabelNome));
            }
        }

        public string LabelNome
        {
            get { return NomeCache; }
        }


        public DelegateCommand AdicionarCacheCommand { get; set; }

        private bool AdicionarCacheCommandCanExecute() => string.IsNullOrWhiteSpace(NomeCache) && !string.IsNullOrWhiteSpace(Nome);

        private void OnAdicionarCacheCommandExecuted()
        {
            NomeCache = Nome;
        }

        public DelegateCommand RemoverCacheCommand { get; set; }

        private bool RemoverCacheCommandCanExecute() => !string.IsNullOrWhiteSpace(NomeCache);

        private void OnRemoverCacheCommandExecuted()
        {
            NomeCache = string.Empty;
        }
    }
}
