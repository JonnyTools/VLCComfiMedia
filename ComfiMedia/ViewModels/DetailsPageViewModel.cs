using ComfiMedia.Model;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace ComfiMedia.ViewModels
{
    public class DetailsPageViewModel : ViewModelBase
    {
        //private readonly INavigationService _navigationService;
        private DelegateCommand _openPlayerCommand;
        public DelegateCommand OpenPlayerCommand=>
           _openPlayerCommand ?? (_openPlayerCommand = new DelegateCommand(ExecuteOpenPlayerCommand));
        private readonly INavigationService _navigationService;

        //public DetailsPageViewModel()
        //{
        //}
        public DetailsPageViewModel(INavigationService navigationService)
        {
            Title = "Current Playlist"; 
            _navigationService = navigationService;
        }
        Media media;
        
        public Media SelectedMedia
        {
            get => media;
            set
            {
                if (media != null)
                    return;
                SetProperty(ref media, value);

            }
        }
        async void ExecuteOpenPlayerCommand()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var CheckURL = new Uri(media.URL);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get MediaDetails {ex.Message}");
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Unable to get MediaDetails", ex.Message, "Mach ich später");
                return;
            }
            try
            {
                var _navigationParameters = new NavigationParameters();
                _navigationParameters.Add("Title", media.Title);
                _navigationParameters.Add("URL", media.URL);
                _navigationParameters.Add("Rating", media.Rating);
                _navigationParameters.Add("Picture", media.Picture);
                _navigationParameters.Add("Description", media.Description);
                await _navigationService.NavigateAsync("PlayerPage", _navigationParameters);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get MediaDetails {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Unable to get MediaDetails", ex.Message, "Mach ich später");

            }
            finally
            {
                IsBusy = false;
            }
        }
        public override void Initialize(INavigationParameters parameters)
        {
            Media media = new Media();
            media.Title = parameters.GetValue<string>("Title");
            media.Rating = parameters.GetValue<string>("Rating");
            media.URL = parameters.GetValue<string>("URL");
            media.Picture = parameters.GetValue<string>("Picture");
            media.Description = parameters.GetValue<string>("Description");

            SelectedMedia = media;

            
        }

    }

}
