using System;
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

            if (proxyAddr != null)
            {
                handler = new HttpClientHandler()
                {

                    Proxy = new WebProxy(proxyAddr),
                    UseProxy = true,
                };
            }
            else
            {
                handler = new HttpClientHandler();
                handler.UseProxy = false;
            }

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


        public async Task<UserObject> GetUserInfo(string userId, string screen_name = "", string accessToken = null)
        {           

            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            HttpRequestMessage requestUserProfile = null;

            if (!string.IsNullOrEmpty(screen_name))
            {
                requestUserProfile = new HttpRequestMessage(HttpMethod.Get,
                    string.Format("https://api.twitter.com/1.1/users/lookup.json?screen_name={0}", screen_name));
            }
            else
            {
                requestUserProfile = new HttpRequestMessage(HttpMethod.Get,
                    string.Format("https://api.twitter.com/1.1/users/lookup.json?user_id={0}", userId));
            }
            requestUserProfile.Headers.Add("Authorization", "Bearer " + accessToken);


            var httpClient = new HttpClient(handler);
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserProfile);
            var response = await responseUserTimeLine.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject<object>(response);
           
            UserObject userObj = new UserObject();

            userObj.user_name = json[0]["name"];
            userObj.profile_description = json[0]["description"];
            userObj.profile_image_url = json[0]["profile_image_url"];
            return userObj;
        }

        public async Task<List<string>> GetFriends(string userId, string accessToken = null)
        {

            if (accessToken == null)
            {
                accessToken = await GetAccessToken();
            }

            var requestUserFriends = new HttpRequestMessage(HttpMethod.Get,
                string.Format("https://api.twitter.com/1.1/friends/ids.json?screen_name={0}", userId));
            requestUserFriends.Headers.Add("Authorization", "Bearer " + accessToken);


            var httpClient = new HttpClient(handler);
            HttpResponseMessage responseUserTimeLine = await httpClient.SendAsync(requestUserFriends);
            var response = await responseUserTimeLine.Content.ReadAsStringAsync();
            dynamic json = JsonConvert.DeserializeObject<object>(response);

            List<string> friendIds = new List<string>();

            UserObject userObj = new UserObject();

            var friendList = json["ids"];


            string friendIdList = Convert.ToString(friendList);
            friendIdList = friendIdList.Trim('[', ']');
            friendIdList = friendIdList.Replace(",\r\n", " ");
            friendIdList = friendIdList.Replace("\r\n", " ");
            //friendIdList
            //userObj.profile_image_url = profileImageUrl; // enumerableUserProfile.Select(t => (string)(t["profile_image_url"].ToString()));
            //friendIds.Add(friendList);

            var idList = friendIdList.Split(' ');

            foreach(var id in idList)
            {
                if (String.IsNullOrEmpty(id))
                    continue;

                friendIds.Add(id);
            }
            return friendIds;
        }        
    }
}
