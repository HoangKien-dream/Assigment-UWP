using Assigment.Entity;
using Assigment.Service;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class RegisterPage : Windows.UI.Xaml.Controls.Page
    {
        private int checkedGender;
        private RawUploadResult imgUri;
        private DateTime date;
        private static string ApiUrl = "https://music-i-like.herokuapp.com/api/v1/accounts";
        private CloudinaryDotNet.Account account1;
        private Cloudinary cloudinary;
        private DateTime CurrentDate = DateTime.Now;


        public RegisterPage()
        {
            account1 = new CloudinaryDotNet.Account(
            "hoangkien0601",
            "182924316467176",
            "jQyE5H4PMZjQO2_dR6-o6kKC7mY");
            cloudinary = new Cloudinary(account1);
            cloudinary.Api.Secure = true;
            this.InitializeComponent();
            this.InitializeComponent();
            
        }

        private async void Register(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            AccountService accountService = new AccountService();
            var account = new Entity.Account();
            if (string.IsNullOrEmpty(firstName.Text))
            {
                msgFirstName.Text = "First Name isn't empty";
            }
            else
            {
                account.firstName = firstName.Text;
                msgFirstName.Text = "";
            }
            if (string.IsNullOrEmpty(lastName.Text))
            {
                msgLastName.Text = "Last Name isn't empty";
            }
            else
            {
                account.lastName = lastName.Text;
                msgLastName.Text = "";
            }
            if (string.IsNullOrEmpty(psw.Password))
            {
                msgPwd.Text = "Password isn't empty";
                msgConfirmPwd.Text = "Password isn't match";
            }
            else
            {
                msgPwd.Text = " ";
                if (psw.Password.Equals(confirmPwd.Password))
                {
                    msgConfirmPwd.Text = " ";
                    account.password = psw.Password;
                }
                else
                {
                    msgConfirmPwd.Text = "Password isn't match";
                }
            }
            if (string.IsNullOrEmpty(address.Text))
            {
                msgAddress.Text = " Address isn't empty";
            }
            else
            {
                account.address = address.Text;
                msgAddress.Text = " ";
            }
            if (string.IsNullOrEmpty(phone.Text))
            {
                msgPhonel.Text = " Phone isn't empty";
            }
            else
            {
                account.phone = phone.Text;
                msgPhonel.Text = " ";
            }
            if (checkedGender == 1 || checkedGender == 0)
            {
                account.gender = checkedGender;
                msgGender.Text = " ";
            }
            else
            {
                msgGender.Text = "Please choose your gender";
            }
            if (string.IsNullOrEmpty(email.Text))
            {
                msgEmail.Text = " Email isn't empty";
            }
            else
            {
                account.email = email.Text;
                msgEmail.Text = " ";
            }
            if (date == new DateTime())
            {
                msgBirthday.Text = "Please chose your birthday";
            }
            else if (date > CurrentDate)
            {
                msgBirthday.Text = "Are u juskidding me :)))";
            }
            else
            {
                account.birthday = date.ToString("yyyy-MM-dd");
                msgBirthday.Text = " ";
            }
            if (imgUri == null)
            {
                msgAvatar.Text = "Avatar isn't empty";
            }
            else
            {
                account.avatar = imgUri.Uri.ToString();
                msgAvatar.Text = "";
            }

            var result = await accountService.RegisterAsync(account);
            ContentDialog content = new ContentDialog();
            if (result)
            {
                content.Title = "Action success !!!";
                content.Content = "Register success !!";
                rootFrame.Navigate(typeof(Pages.LoginPage));
            }
            else
            {
                content.Title = "Action fails !!";
                content.Content = "Register fail !!";
            }
            content.CloseButtonText = "Okie";
            content.ShowAsync();

        }

        private void CheckedDate(DatePicker sender, DatePickerSelectedValueChangedEventArgs args)
        {
            date = new DateTime(args.NewDate.Value.Year, args.NewDate.Value.Month, args.NewDate.Value.Day);
        }

        private void GenderChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            switch (radio.Tag)
            {
                case "Male":
                    checkedGender = 1;
                    break;
                case "Female":
                    checkedGender = 0;
                    break;
            }
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
               imgUri =  await cloudinary.UploadAsync(imageUpload);
               
            }
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(Pages.LoginPage));
        }
    }
}
