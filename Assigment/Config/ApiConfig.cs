
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assigment.Config
{
    class ApiConfig
    {
        public static string ApiDomain = "https://music-i-like.herokuapp.com";
        public static string AccountPath = "/api/v1/accounts";
        public static string LoginPath = "/api/v1/accounts/authentication";
        public static string InformationPath = "/api/v1/accounts";
        public static string SongPath = "/api/v1/songs";
        public static string CreateSongPath = "/api/v1/songs/mine";
        public static string MySongPath = "/api/v1/songs/mine";
        public static string MediaType = "application/json";
    }
}
