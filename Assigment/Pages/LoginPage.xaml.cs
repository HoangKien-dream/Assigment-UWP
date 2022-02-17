using Assigment.Entity;
using Assigment.Service;
using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assigment.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
        private async void Login(object sender, RoutedEventArgs e)
        {
            
            AccountLogin account = new AccountLogin();
            if (string.IsNullOrEmpty(email.Text))
            {
                msgEmail.Text = "Please enter your email";
            }
            else
            {
                account.email = email.Text;
                msgEmail.Text = "";
            }
            if (string.IsNullOrEmpty(psw.Password))
            {
                msgPsw.Text = "Please enter your password ";
            }
            else
            {
                account.password = psw.Password;
                msgPsw.Text = "";
            }
            AccountService accountService = new AccountService();
          var result = await accountService.Login(account);
            ContentDialog content = new ContentDialog();
            if (result)
            {
                content.Title = "Action success !!!";
                content.Content = "Login success !!";
                Frame.Navigate(typeof(Pages.HomePage));
                
            }
            else
            {
                content.Title = "Action fails !!";
                content.Content = "Login fail !!";
            }
            content.CloseButtonText = "Okie";
            content.ShowAsync();
          
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Pages.RegisterPage));
        }
    }
}
