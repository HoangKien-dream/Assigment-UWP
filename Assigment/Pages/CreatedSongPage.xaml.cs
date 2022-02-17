using Assigment.Entity;
using Assigment.Service;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
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
    public sealed partial class CreatedSongPage : Windows.UI.Xaml.Controls.Page
    {
        private RawUploadResult imgUri;
        private CloudinaryDotNet.Account account;
        private Cloudinary cloudinary;
        public CreatedSongPage()
        {
            account = new CloudinaryDotNet.Account(
               "hoangkien0601",
               "182924316467176",
               "jQyE5H4PMZjQO2_dR6-o6kKC7mY");
            cloudinary = new Cloudinary(account);
            cloudinary.Api.Secure = true;
            this.InitializeComponent();
        }
        private async void Create(object sender, RoutedEventArgs e)
        {
            
            SongService songService = new SongService();
            AccountService accountService = new AccountService();
            CreateSong song = new CreateSong();
            if (string.IsNullOrEmpty(name.Text))
            {
                msgName.Text = "Name isn't empty";
            }
            else
            {
                song.name = name.Text;
                msgName.Text = "";
            }
            if (string.IsNullOrEmpty(singer.Text))
            {
                msgSinger.Text = "Singer isn't empty";
            }
            else
            {
                song.singer = singer.Text;
                msgSinger.Text = "";
            }
            if (string.IsNullOrEmpty(author.Text))
            {
                msgAuthor.Text = "Author isn't empty";
            }
            else
            {
                song.author = author.Text;
                msgAuthor.Text = "";
            }
            if (string.IsNullOrEmpty(link.Text))
            {
                msgLink.Text = "Link isn't empty";
            }
            else
            {
                song.link = link.Text;
                msgLink.Text = "";
            }
            if(imgUri == null){
                msgThumbnail.Text = "Thumbnail isn't empty";
            }
            else
            {
                Debug.WriteLine(imgUri.Uri.ToString());
                song.thumbnail = imgUri.Uri.ToString();
                msgThumbnail.Text = "";
            }
            if (string.IsNullOrEmpty(description.Text))
            {
                msgDescription.Text = "Description isn't empty";
            }
            else
            {
                song.description = description.Text;
                msgDescription.Text = "";
            }
            var token = await accountService.LoadToken();
            var result = await songService.CreateSong(song,token.access_token);
            ContentDialog contentDialog = new ContentDialog();
            if (result)
            {
                contentDialog.Title = "Action Success !!";
                contentDialog.Content = "Create song success !!!";
            }
            else
            {
                contentDialog.Title = "Action Fails !!";
                contentDialog.Content = "Create song Fails !!!";
            }
            contentDialog.CloseButtonText = "Okie";
            contentDialog.ShowAsync();
        }
   

        private async void HandleOpenFile(object sender, RoutedEventArgs e)
        {
            
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                Debug.WriteLine(file.Name);
                Debug.WriteLine(file.Path);
                BitmapImage bitmapImage = new BitmapImage(new Uri(file.Path));
                Debug.WriteLine(bitmapImage);
                //previewImg.ProfilePicture = bitmapImage;
                IRandomAccessStream fileStream = await file.OpenReadAsync();
                Debug.WriteLine(fileStream);
                await bitmapImage.SetSourceAsync(fileStream);
                previewImage.ProfilePicture = bitmapImage;
                ImageUploadParams imageUpload = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, await file.OpenStreamForReadAsync()),

                };
                imgUri = await cloudinary.UploadAsync(imageUpload);
            }
            else
            {
                link.Text = "Operation cancelled.";
            }
        }
    }
}
