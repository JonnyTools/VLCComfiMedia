using ComfiMedia.Model;
using LibVLCSharp.Shared;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;
using Media = LibVLCSharp.Shared.Media;

namespace ComfiMedia.ViewModels
{
    public class PlayerPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;


        public PlayerPageViewModel()
        {
        }
        public PlayerPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public void GoBack()
        {
            if(MediaPlayer != null)
            MediaPlayer.Dispose();
            if(LibVLC != null)
            LibVLC.Dispose();
            _navigationService.GoBackAsync();

        }
        Model.Media media;

        public Model.Media SelectedMedia
        {
            get => media;
            set
            {
                if (media == value)
                    return;
                media = value;
                OnPropertyChanged();
            }
        }
        private LibVLC _libVLC;

        /// <summary>
        /// Gets the <see cref="LibVLCSharp.Shared.LibVLC"/> instance.
        /// </summary>
        public LibVLC LibVLC
        {
            get => _libVLC;
            set
            {
                if (_libVLC == value)
                    return;

                _libVLC = value;
                OnPropertyChanged();
            }
        }
        private MediaPlayer _mediaPlayer;
        public MediaPlayer MediaPlayer
        {
            get => _mediaPlayer;
            set
            {
                if (_mediaPlayer == value)
                    return;
                _mediaPlayer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initialize LibVLC and playback when page appears
        /// </summary>
        public void OnAppearing()
        {
            if (IsBusy)
                return;
            Core.Initialize();
            LibVLC = new LibVLC();

                try
                {
                    IsBusy = true;
                    var media = new Media(LibVLC, new Uri(SelectedMedia.URL));
                    MediaPlayer = new MediaPlayer(media) { EnableHardwareDecoding = true };
                    media.Dispose();
                    MediaPlayer.Play();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unable to Play URl:{SelectedMedia.URL} {ex.Message}");
                }
                finally
                {
                    IsBusy = false;
                }
            
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Model.Media media = new Model.Media();
            media.Title = parameters.GetValue<string>("Title");
            media.Rating = parameters.GetValue<string>("Rating");
            media.URL = parameters.GetValue<string>("URL");
            media.Picture = parameters.GetValue<string>("Picture");
            media.Description = parameters.GetValue<string>("Description");

            SelectedMedia = media;

        }


    }
}
