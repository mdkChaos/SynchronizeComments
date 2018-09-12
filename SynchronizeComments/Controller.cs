using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SynchronizeComments
{
    class Controller
    {
        public List<Comment> GetCommentsFromSite()
        {
            string siteUrl = Properties.Settings.Default.SiteUrl;

            WebRequest request = WebRequest.Create(siteUrl);
            request.Credentials = CredentialCache.DefaultCredentials;
            string responseFromServer = null;

            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                responseFromServer = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Comment>>(responseFromServer);
        }

        public async Task AddCommentsToDBAsync(List<Comment> comments)
        {
            using (ModelDB db = new ModelDB())
            {
                IEnumerable<Comment> bdComments = GetCommentsFromBD();

                IEnumerable<Comment> finds = bdComments.Except(comments, new MyEqualityComparer());
                if (finds.Count() > 0 && finds != null)
                {
                    int count = 0;

                    foreach (Comment find in finds)
                    {
                        db.Comments.Add(find);
                        count++;
                    }
                    await db.SaveChangesAsync();
                    Console.WriteLine($"Добавленно {count} новых записей");
                }
                else
                {
                    Console.WriteLine("Новые записи отсутствуют");
                }
            }
        }

        public List<Comment> GetCommentsFromBD()
        {
            List<Comment> comments;
            using (ModelDB db = new ModelDB())
            {
                comments = db.Comments.ToList();
            }
            return comments;
        }
    }
}
