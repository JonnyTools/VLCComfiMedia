using ComfiMedia.Model;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ComfiMedia.ViewModels
{
    
    public class MainPageViewModel : ViewModelBase
    {
        // Commands
        private DelegateCommand<Media> _openDetailsCommand;
        public DelegateCommand<Media> OpenDetailsCommand =>
            _openDetailsCommand ?? (_openDetailsCommand = new DelegateCommand<Media>(ExecuteOpenDetailsCommand));

        public Command GetMediaCommand { get; }
        // String of Media Elements 
        public ObservableCollection<Media> Playlist { get; }
        
        private readonly INavigationService _navigationService;


        public MainPageViewModel(INavigationService navigationService)     
        {
            Title = "Current Playlist";
            //SelectedMedia = new ObservableCollection<Media>();
            Playlist = new ObservableCollection<Media>();
            GetMediaCommand = new Command(async () => await GetMediaAsync());
            _navigationService = navigationService;
        }

        // Notlösung
        async void ExecuteOpenDetailsCommand(Media media)
        {
            if (IsBusy)
                return;


            try
            {
                IsBusy = true;
                var _navigationParameters = new NavigationParameters();
                _navigationParameters.Add("Title", media.Title);
                _navigationParameters.Add("URL", media.URL);
                _navigationParameters.Add("Rating", media.Rating);
                _navigationParameters.Add("Picture", media.Picture);
                _navigationParameters.Add("Description", media.Description);
                await _navigationService.NavigateAsync("DetailsPage", _navigationParameters);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get MediaDetails {ex.Message}");
                if (Application.Current?.MainPage == null)
                    return;
                await Application.Current.MainPage.DisplayAlert("Unable to get MediaDetails", ex.Message, "Mach ich später");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task GetMediaAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                string JsonMedia = null;
                var a = System.Reflection.Assembly.GetExecutingAssembly();

                // Get Data via local Json File -> Prove of Concept 
                // normaly get data from server if internet is working
                //var client = new HttpClient();
                //var JsonMedia = await client.GetStringAsync("https://myserver/mediadata.json");

                using (var resFilestream = a.GetManifestResourceStream("ComfiMedia.mediadata.json"))
                {
                    // Set Encoding to UTF 8
                    using (var reader = new System.IO.StreamReader(resFilestream,Encoding.UTF8))
                        JsonMedia = await reader.ReadToEndAsync();
                }
                var medialist = JsonConvert.DeserializeObject<List<Media>>(JsonMedia);
                Debug.WriteLine($"Json is loaded");
                Playlist.Clear();
                foreach (var media in medialist)
                    Playlist.Add(media);
                Debug.WriteLine($"Media is loaded");

            }



            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Media: {ex.Message}");
                if (Application.Current?.MainPage == null)
                    return;
                await Application.Current.MainPage.DisplayAlert("Unable to Refresh!", ex.Message, "Json File maybe corupted");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}
