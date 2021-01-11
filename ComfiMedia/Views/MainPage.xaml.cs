
using ComfiMedia.Model;
using System.Linq;
using Xamarin.Forms;

namespace ComfiMedia.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
           
        }
        //async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var media = e.CurrentSelection.FirstOrDefault() as Media;
        //    if (media == null)
        //        return;
        //    //await Navigation.PushAsync(new DetailsPage(media))

        //    await Navigation.PushAsync(new DetailsPage(media));
            
        //    ((CollectionView)sender).SelectedItem = null;

        //}
    }
}
