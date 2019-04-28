using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterAccess;

namespace TwitterStreamAPIProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        TwitterAccesser twitterAccess = new TwitterAccesser();

        //// GET: api/Tweets
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Tweets/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            var tweets = twitterAccess.GetTweets(id, 10).Result;

            string tweet_strings = "";
            foreach (var tweet in tweets)
            {
                tweet_strings += tweet.created_at + ": " + tweet.text + '\n';
            }

            return tweet_strings;
        }

        //// POST: api/Tweets
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Tweets/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
