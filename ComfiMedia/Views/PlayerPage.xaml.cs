using ComfiMedia.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ComfiMedia.Views
{
    public partial class PlayerPage : ContentPage
    {
        public PlayerPage()
        {
            
                InitializeComponent();
            
        }
            void OnAppearing(object sender, System.EventArgs e)
            {
                base.OnAppearing();
                ((PlayerPageViewModel)BindingContext).OnAppearing();
            }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            ((PlayerPageViewModel)BindingContext).GoBack();
            return true;
          
        }

    }
}
