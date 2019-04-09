using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TwitterAccess;

namespace TwitterStreamAPIProvider.Controllers
{
    [Route("api/TwitterStream")]
    [ApiController]
    public class TwitterStreamController : ControllerBase
    {
        //// GET: api/TwitterStream
        //[HttpGet("{userName}")]
        //public async Task<ActionResult<IEnumerable<string>>> GetTodoItems(string userName)
        //{
        //    TwitterAccesser tAccess = new TwitterAccesser();
        //    var tweets = tAccess.GetTweets(userName, 10);
        //    Task <ActionResult<IEnumerable<string>>> result = new Task<ActionResult<IEnumerable<string>>>();

        //    return await tweets; //_context.TodoItems.ToListAsync();
        //}

        // GET: api/TwitterStream
        [HttpGet]
        public string GetWelcomeMessage()
        {
            return "Hello!";
        }

        // GET: api/TwitterStream
        [HttpGet("{userId}")]
        public IEnumerable<string> GetTweets(string userId)
        {
            TwitterAccesser tAccess = new TwitterAccesser();
            var tweets = tAccess.GetTweets(userId, 10);
            return tweets.Result;
        }
    }
}