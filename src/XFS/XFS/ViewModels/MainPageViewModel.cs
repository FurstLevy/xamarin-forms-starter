using Plugin.Geolocator;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace XFS.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        IPageDialogService _pageDialogService;

        public MainPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            UsarGpsCommand = new DelegateCommand(OnUsarGpsCommandExecuted, UsarGpsCommandCanExecute);
            BuscarLocalizacoesCommand = new DelegateCommand(OnBuscarLocalizacoesCommandExecuted, BuscarLocalizacoesCommandCanExecute)
                .ObservesProperty(() => EnderecoManual);
        }

        private string _enderecoGPS;

        public string EnderecoGPS
        {
            get { return _enderecoGPS; }
            set { SetProperty(ref _enderecoGPS, value); }
        }

        private string _enderecoManual;

        public string EnderecoManual
        {
            get { return _enderecoManual; }
            set { SetProperty(ref _enderecoManual, value); }
        }

        private string _enderecoManualSelecionado;

        public string EnderecoManualSelecionado
        {
            get { return _enderecoManualSelecionado; }
            set { SetProperty(ref _enderecoManualSelecionado, value); }
        }

        public DelegateCommand UsarGpsCommand { get; set; }

        private bool UsarGpsCommandCanExecute() => IsNotBusy;

        private async void OnUsarGpsCommandExecuted()
        {
            IsBusy = true;
            var enderecos = await BuscarGeolocalizacaoPeloGpsAsync();
            if (enderecos == null || enderecos.EnderecosPossiveis.Count == 0)
            {
                await _pageDialogService.DisplayAlertAsync("Alerta!", "Não foi possível obter sua localização! Verifique se seu GPS está ativo.", "Ok");
                IsBusy = false;
                return;
            }
            IsBusy = false;

            EnderecoGPS = await _pageDialogService.DisplayActionSheetAsync("Localizações:", "Cancelar", null, enderecos.EnderecosPossiveis.ToArray());
            IsBusy = false;
        }

        public DelegateCommand BuscarLocalizacoesCommand { get; set; }

        private bool BuscarLocalizacoesCommandCanExecute() => IsNotBusy && !string.IsNullOrWhiteSpace(EnderecoManual);

        private async void OnBuscarLocalizacoesCommandExecuted()
        {
            IsBusy = true;
            var enderecos = await BuscarGeolocalizacaoPeloEnderecoAsync(EnderecoManual);
            if (enderecos == null || enderecos.Count == 0)
            {
                await _pageDialogService.DisplayAlertAsync("Alerta!", "Não foi possível obter sua localização! Verifique se seu GPS está ativo.", "Ok");
                IsBusy = false;
                return;
            }

            EnderecoManualSelecionado = await _pageDialogService.DisplayActionSheetAsync("Possíveis localizações:", "Cancelar", null, enderecos.ToArray());
            IsBusy = false;
        }

        private static async Task<Geolocalizacao> BuscarGeolocalizacaoPeloGpsAsync()
        {
            try
            {
                if (!IsLocationAvailable())
                    return null;

                var locator = CrossGeolocator.Current;
                var geolocalizacao = new Geolocalizacao();
                //locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10), null);
                if (position == null) return null;

                geolocalizacao.Latitude = position.Latitude;
                geolocalizacao.Longitude = position.Longitude;

                var addressList = await locator.GetAddressesForPositionAsync(position);
                if (addressList == null || addressList.Count() == 0) return geolocalizacao;

                geolocalizacao.EnderecosPossiveis = new List<string>();
                foreach (var address in addressList)
                {
                    var enderecoCompleto = $"{(string.IsNullOrWhiteSpace(address.Thoroughfare) ? string.Empty : $"{address.Thoroughfare}, ")}" +
                        $"{(string.IsNullOrWhiteSpace(address.SubLocality) ? string.Empty : $"{address.SubLocality}, ")}" +
                        $"{(string.IsNullOrWhiteSpace(address.Locality) ? string.Empty : $"{address.Locality}, ")}";
                    if (enderecoCompleto.Length > 0)
                    {
                        enderecoCompleto = enderecoCompleto.Substring(0, enderecoCompleto.Length - 2) + ".";
                        geolocalizacao.EnderecosPossiveis.Add(enderecoCompleto);
                    }
                }

                return geolocalizacao;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static async Task<List<string>> BuscarGeolocalizacaoPeloEnderecoAsync(string endereco)
        {
            try
            {
                var enderecos = new List<string>();
                var geoCoder = new Geocoder();
                var positionsList = await geoCoder.GetPositionsForAddressAsync(endereco);
                var locator = CrossGeolocator.Current;
                foreach (var position in positionsList)
                {
                    var positionAux = new Plugin.Geolocator.Abstractions.Position();
                    positionAux.Latitude = position.Latitude;
                    positionAux.Longitude = position.Longitude;
                    var address = (await locator.GetAddressesForPositionAsync(positionAux)).FirstOrDefault();
                    if (address != null)
                    {
                        enderecos.Add($"{(string.IsNullOrWhiteSpace(address.Thoroughfare) ? string.Empty : $"{address.Thoroughfare}, ")}" +
                            $"{(string.IsNullOrWhiteSpace(address.SubLocality) ? string.Empty : $"{address.SubLocality}, ")}" +
                            $"{(string.IsNullOrWhiteSpace(address.Locality) ? string.Empty : $"{address.Locality}, ")}" +
                            $"{(string.IsNullOrWhiteSpace(address.CountryName) ? string.Empty : $"{address.CountryName}")}");
                    }
                }

                return enderecos;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }

        public class Geolocalizacao
        {
            public IList<string> EnderecosPossiveis { get; set; }
            public string EnderecoSelecionado { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
        }
    }
}
