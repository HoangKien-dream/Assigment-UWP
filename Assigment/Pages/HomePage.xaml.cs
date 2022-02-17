using Assigment.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assigment.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private AccountService service;

        public HomePage()
        {
            this.Loaded += HomePage_Loaded;
            this.InitializeComponent();
        }

        private async void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmapImage = new BitmapImage(new Uri("https://res.cloudinary.com/hoangkien0601/image/upload/v1645023240/apple-music_a4fzru.jpg"));
            Debug.WriteLine(bitmapImage);
            avatar.ProfilePicture = bitmapImage;
        }



        private void NavigationView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItems = sender.SelectedItem as NavigationViewItem;
            switch (selectedItems.Tag)
            {
                case "profile":
                    Content.Navigate(typeof(Pages.AccountPage));
                    break;
                case "listSong":
                    Content.Navigate(typeof(Pages.ListSongPage));
                    break;
                case "mySong":
                    Content.Navigate(typeof(Pages.MySongPage));
                    break;
                case "createSong":
                    Content.Navigate(typeof(Pages.CreatedSongPage));
                    break;
                case "logOut":
                    service = new AccountService();
                    service.LogOut();
                    Frame.Navigate(typeof(Pages.LoginPage));
                    break;
            }
        }
    }
}
