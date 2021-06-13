using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace LichessAdvancedStats
{
    public class LichessApiWorker
    {
        public async Task<string> LoadUsersGamesPgnAsync(string userName)
        {
            var url = $"https://lichess.org/api/games/user/{userName}";
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(url);

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
