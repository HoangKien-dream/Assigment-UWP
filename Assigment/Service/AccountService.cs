using Assigment.Config;
using Assigment.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Assigment.Service
{
    class AccountService
    {
        public async Task<bool> RegisterAsync(Account account) {
            var json = JsonConvert.SerializeObject(account);
            Debug.WriteLine(json);
            HttpClient http = new HttpClient();
            HttpContent contentToSend = new StringContent(json, Encoding.UTF8, ApiConfig.MediaType);
            var result = await http.PostAsync($"{ApiConfig.ApiDomain}{ApiConfig.AccountPath}", contentToSend);
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var content = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                Debug.WriteLine("Good");
                return true;
            }
            else
            {
                Debug.WriteLine("Error 500");
                return false;
            }
        }

        public async Task<bool> Login(AccountLogin account)
        {
           
            var json = JsonConvert.SerializeObject(account);
            Debug.WriteLine(json);
            HttpClient http = new HttpClient();
            HttpContent contentToSend = new StringContent(json, Encoding.UTF8, ApiConfig.MediaType);
            var result = await http.PostAsync($"{ApiConfig.ApiDomain}{ApiConfig.LoginPath}", contentToSend);
            Debug.WriteLine(result);
            var content = await result.Content.ReadAsStringAsync();
            Debug.WriteLine(content);
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                SaveToken(content);   
                Debug.WriteLine("Good");
                return true;
            }
            else
            {
                Debug.WriteLine("Error 500");
                return false;
            }
        }

        public async void SaveToken(string content)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile storageFile = await storageFolder.CreateFileAsync("tokenky.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, content);
        }

        public async Task<Account> ProccessLogin()
        {
            Account account;
            Token token = await LoadToken();
           if(token == null)
            {
                return null;
            }
            account = await GetInformationAsync(token.access_token);
            return account;
        }

        

        public async Task<Account> GetInformationAsync(string token)
        {
            using(HttpClient http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                var result = await http.GetAsync($"{ApiConfig.ApiDomain}{ApiConfig.InformationPath}");
                Debug.WriteLine(result);
                var content = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {

                    Account account = JsonConvert.DeserializeObject<Account>(content);
                    return account;
                }
                else
                {
                    Debug.WriteLine("Error 500");
                    return null;
                }
            }
        }

        public async void LogOut()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile manifestFile = await storageFolder.GetFileAsync("tokenky.txt");
            await manifestFile.DeleteAsync();
        }
        public async Task<Token> LoadToken()
        {
            try
            {
                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                var items = await storageFolder.GetFileAsync("tokenky.txt");
                if (items == null)
                {
                    return null;
                }
                var tokenContent = await FileIO.ReadTextAsync(items as StorageFile);
                var token = JsonConvert.DeserializeObject<Token>(tokenContent);
                return token;
            }
            catch(Exception e) {
                return null;
            }
        }

 

    }
}
