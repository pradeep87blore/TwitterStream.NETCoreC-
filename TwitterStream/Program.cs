using System;
using TwitterAccess;

namespace TwitterStream
{
    class Program
    {
        static void Main(string[] args)
        {
            TwitterAccesser twitterAccess = new TwitterAccesser();

            Console.WriteLine("Welcome, the following are my 10 most recent tweets:\n\n");
            var tweets = twitterAccess.GetTweets("@pradeep87blore1", 10).Result;

            foreach (var tweet in tweets)
            {
                Console.WriteLine(tweet + "\n");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey(); // To prevent the console app from existing
        }
    }
}
