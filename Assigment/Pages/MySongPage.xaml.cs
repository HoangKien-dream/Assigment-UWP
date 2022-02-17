using Assigment.Entity;
using Assigment.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
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
    public sealed partial class MySongPage : Page
    {
        public MySongPage()
        {
            Loaded += MySongPage_Loaded;
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            MyMediaPlayer.MediaPlayer.Pause();
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

   

        private void ListSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSong = ListSong.SelectedItem as Song;
            MyMediaPlayer.MediaPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            BitmapImage bitmapImage = new BitmapImage(new Uri(currentSong.thumbnail));
            thumbnail.ProfilePicture = bitmapImage;
            MyMediaPlayer.MediaPlayer.Play();
        }
        private async void  MySongPage_Loaded(object sender, RoutedEventArgs e)
        {
            AccountService accountService = new AccountService();
            SongService songService = new SongService();
            var token = await accountService.LoadToken();
            List<Song> list = await songService.MyListSong(token.access_token);
            ObservableCollection<Song> observableSong = new ObservableCollection<Song>(list);
            ListSong.ItemsSource = observableSong;

        }
    }
}
