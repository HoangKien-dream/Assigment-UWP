using Assigment.Entity;
using Assigment.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
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
    public sealed partial class ListSongPage : Page
    {
        private int index;
        private MediaPlaybackList mediaPlaybackList;
        public ListSongPage()
        {
           
            this.InitializeComponent();
            Loaded += ListSongPage_Loaded;
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

        private async void ListSongPage_Loaded(object sender, RoutedEventArgs e)
        {
            SongService songService = new SongService();
            List<Song> list = await songService.ListSong();
            ObservableCollection<Song> observableSong = new ObservableCollection<Song>(list);
            ListSong.ItemsSource = observableSong;
            ListSong.SelectedIndex = index;
            var currentSong = ListSong.SelectedItem as Song;
            BitmapImage bitmapImage = new BitmapImage(new Uri(currentSong.thumbnail));
            thumbnail.ProfilePicture = bitmapImage;
            mediaPlaybackList = new MediaPlaybackList();
            foreach (var song in list)
            {
                try
                {
                    var mediaPlaybackItem = new MediaPlaybackItem(MediaSource.CreateFromUri(new Uri(song.link)));
                    mediaPlaybackList.Items.Add(mediaPlaybackItem);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            mediaPlaybackList.CurrentItemChanged += MediaPlaybackList_CurrentItemChanged;
            


            //mediaPlaybackList.MaxPlayedItemsToKeepOpen = 3;
            var mediaPlayer = new MediaPlayer(); 
            mediaPlayer.Source = mediaPlaybackList; // MediaPlayerElement < MediaPlayer < MediaPlaybackList < MediaPlaybackItem
            MyMediaPlayer.SetMediaPlayer(mediaPlayer);
        }

        private async void MediaPlaybackList_CurrentItemChanged(MediaPlaybackList sender, CurrentMediaPlaybackItemChangedEventArgs args)
        {
            Debug.WriteLine(mediaPlaybackList.CurrentItemIndex);
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                ListSong.SelectedIndex = (int)mediaPlaybackList.CurrentItemIndex;
            });
        }
        private void ListSong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSong = ListSong.SelectedItem as Song;
            index = ListSong.SelectedIndex;
            if (mediaPlaybackList != null)
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(currentSong.thumbnail));
                thumbnail.ProfilePicture = bitmapImage;
                mediaPlaybackList.MoveTo((UInt32)index);
            }
            //MyMediaPlayer.MediaPlayer.Source = MediaSource.CreateFromUri(new Uri(currentSong.link));
            //BitmapImage bitmapImage = new BitmapImage(new Uri(currentSong.thumbnail));
            //thumbnail.ProfilePicture = bitmapImage;
            //MyMediaPlayer.MediaPlayer.Play();
        }
    }
}
