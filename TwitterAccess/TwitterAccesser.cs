﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TwitterAccess
{
    public class TwitterAccesser
    {
        public string OAuthConsumerSecret { get; set; }
        public string OAuthConsumerKey { get; set; }

        private HttpClientHandler handler = null;

        public TwitterAccesser()
        {
            using (StreamReader file = new StreamReader(".\\Private\\TwitterKeys.keys"))
            {
                int counter = 0;
                string ln;
                OAuthConsumerKey = file.ReadLine();
                OAuthConsumerSecret = file.ReadLine();
            }

            string proxyAddr = "";
            using (StreamReader file = new StreamReader(".\\Private\\Proxy.proxy"))
            {
                proxyAddr = file.ReadLine();
            }

            handler = new HttpClientHandler()
            {
                
                Proxy = new WebProxy(proxyAddr),
                UseProxy = true,
            };

        }

        public async Task<IEnumerable<string>> GetTweets(string userName, int count, string accessToken = null)
        {
            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserTimeline = new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&exclude_replies=1", count, userName));
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            

            var httpClient = new HttpClient(handler);
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserTimeline);
            dynamic json = JsonConvert.DeserializeObject<object>(await responseUserTimeLine.Content.ReadAsStringAsync());
            var enumerableTweets = (json as IEnumerable<dynamic>);

            if (enumerableTweets == null)
            {
                return null;
            }
            return enumerableTweets.Select(t => (string)(t["text"].ToString()));
        }

        async Task<string> GetAccessToken()
        {

            var httpClient = new HttpClient(handler);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(OAuthConsumerKey + ":" + OAuthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();
            dynamic item = JsonConvert.DeserializeObject<object>(json);
            return item["access_token"];
        }
    }
}
