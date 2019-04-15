using System;
using TwitterAccess;

namespace TwitterStream
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterAccesser twitterAccess = new TwitterAccesser();

            //Console.WriteLine("Welcome, the following are my 10 most recent tweets:\n\n");
            //var tweets = twitterAccess.GetTweets("@pradeep87blore1", 10).Result;

            //foreach (var tweet in tweets)
            //{
            //    Console.WriteLine(tweet + "\n");
            //}

            var userInfo = twitterAccess.GetUserInfo("pradeep87blore1"); // Screen name will not have the @
            Console.WriteLine("Profile Image URL of @pradeep87blore1 is: ", userInfo.Result.profile_image_url);
           

            Console.WriteLine("Press any key to exit");
            Console.ReadKey(); // To prevent the console app from existing
        }
    }
}
