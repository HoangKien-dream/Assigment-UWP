using Assigment.Config;
using Assigment.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Assigment.Service
{
    class SongService
    {
        

        public async Task<bool> CreateSong(CreateSong song, string token)
        {
            var jsonString = JsonConvert.SerializeObject(song);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpContent contentToSend = new StringContent(jsonString, Encoding.UTF8, ApiConfig.MediaType);
            Debug.WriteLine(contentToSend);
            var result = await httpClient.PostAsync($"{ ApiConfig.ApiDomain }{ApiConfig.CreateSongPath}", contentToSend);
            if (result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<List<Song>> MyListSong(string token)
        {
            List<Song> list = new List<Song>();
            using (HttpClient http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await http.GetAsync($"{ApiConfig.ApiDomain}{ApiConfig.MySongPath}");
                Debug.WriteLine(result);
                var content = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    list = JsonConvert.DeserializeObject<List<Song>>(content);
                    Debug.WriteLine(list.Count);
                    return list;
                }
                else
                {
                    Debug.WriteLine("Error 500");
                    return null;
                }
            }
        }

        public async Task<List<Song>> ListSong()
        {
            List<Song> list = new List<Song>();
            using (HttpClient http = new HttpClient())
            {
                var result = await http.GetAsync($"{ApiConfig.ApiDomain}{ApiConfig.SongPath}");
                Debug.WriteLine(result);
                var content = await result.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    list = JsonConvert.DeserializeObject<List<Song>>(content);
                    Debug.WriteLine(list.Count);
                    return list;
                }
                else
                {
                    Debug.WriteLine("Error 500");
                    return null;
                }
            }
        }
    }
}
