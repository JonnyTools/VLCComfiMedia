using ComfiMedia.ViewModels;
using ComfiMedia.Views;
using Prism;
using Prism.Ioc;

using Xamarin.Forms;

namespace ComfiMedia
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            // Main Page Navigation
            await NavigationService.NavigateAsync("MainPage");
        }
        // Views Registrieren zu NavigationService
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<DetailsPage, DetailsPageViewModel>();
            //containerRegistry.RegisterForNavigation<PlayerPage, PlayerPageViewModel>();
            containerRegistry.RegisterForNavigation<PlayerPage, PlayerPageViewModel>();
        }
    }
}
