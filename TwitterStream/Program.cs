using System;
using TwitterAccess;

namespace TwitterStream
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TwitterAccesser twitterAccess = new TwitterAccesser();

           //
            var tweets = twitterAccess.GetTweets("@pradeep87blore1", 10).Result;

            foreach (var tweet in tweets)
            {
                Console.WriteLine(tweet);
            }

            Console.ReadKey(); // To prevent the console app from existing
        }
    }
}
