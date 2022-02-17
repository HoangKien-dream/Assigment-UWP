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
    public sealed partial class AccountPage : Page
    {
        public AccountPage()
        {
            GetInfor();
            this.InitializeComponent();
        }
        public async void GetInfor()
        {
            AccountService accountService = new AccountService();
            var account = await accountService.ProccessLogin();
            txtName.Text = $"{account.firstName} {account.lastName}";
            BitmapImage bitmapImage = new BitmapImage(new Uri(account.avatar.ToString()));
            Debug.WriteLine(bitmapImage);
            avatar.ProfilePicture = bitmapImage;
            txtPhone.Text = account.phone;
            txtAddress.Text = account.address;
            txtEmail.Text = account.email;
            switch (account.gender)
            {
                case 1:
                    txtGender.Text = "Nam";
                    break;
                case 0:
                    txtGender.Text = "Nữ";
                    break;
            }
            dateBirthday.Text = account.birthday;
            var token = await accountService.LoadToken();
            createdAt.Text = token.created_at.ToString("yyy-MM-dd");
            if (token.status == 1)
            {
                intStatus.Text = "Đang hoạt động";
            }
        }
    }
}
