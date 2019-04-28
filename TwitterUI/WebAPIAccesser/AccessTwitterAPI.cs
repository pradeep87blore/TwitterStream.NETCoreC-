using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIAccesser
{
    public class AccessTwitterAPI
    {
        static HttpClient client = new HttpClient(); // TODO: Check on introducing the proxy here

        public Task<IEnumerable<TweetObject>> GetTweets(string userName, int count)
        {

            var taskRsp = client.GetAsync("https://localhost:44361/api/tweets/@pradeep87blore1");
            var response = taskRsp.Result;
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync();
                // TODO: Getting this as a continous stream: 
                // Need to determine how to get it in TweetObject 
                /*
                 * 
                 * Id = 12, Status = RanToCompletion, Method = "{null}", Result = "Thu Apr 25 11:19:41 +0000 2019: My answer to Which lens should I buy to get close images of micro objects (e.g. an ant in full frame) from my Nikon… https://t.co/UvoM2rjiHk\nThu Apr 25 11:13:01 +0000 2019: My answer to What do you do with the photos you take with your smartphones? https://t.co/n3WwIXaxW7\nTue Apr 23 10:33:02 +0000 2019: My answer to Why a Flash diffuser is necessary and why it is useful in photo graphy? https://t.co/Rdj2W8qowh\nSun Apr 21 17:26:51 +0000 2019: Moon\n\n#sky #moon #blackwhite #bw #nikon200500 #nikon #nikond7100 @ Bangaolre https://t.co/AoH6Y0hYIT\nSun Apr 21 17:25:41 +0000 2019: Black kite\n\n#nikond7100 #nikon200500 #bird #kite #predator #raptor #scavenger #bangalore #coconuttree @ Bangaolre https://t.co/WDV76uAuaZ\nWed Apr 17 01:29:06 +0000 2019: My answer to Why are criminals more scared of Batman than they are scared of Superman? https://t.co/8oaswvGLFR\nTue Apr 16 09:43:06 +0000 2019: My answer to What are your views on \"anyone with a DSLR can become a photographer\"? https://t.co/TfPEumuwJ6\nSun Apr 14 10:18:36 +0000 2019: Black Panther 😜😜\n\n#notreally #blackcat #cat #black #bangalore #xiominote6pro #mobileclick @ Bangaolre https://t.co/JW7ft8hOjc\nFri Apr 12 09:12:27 +0000 2019: My answer to What is something unrealistic that you often see in movies that annoys the hell out of you? https://t.co/bqoAxcskaY\nWed Apr 10 10:34:57 +0000 2019: Pinarayi Vijayan: Tortured Kerala temple elephant Neelakandan must transferred to SOS Wildlife, Agra - Sign the Pet… https://t.co/eoRGn4AAnp\n" */
            }
            //return product;

            return null;
        }
    }
}
