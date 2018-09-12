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
            WebRequest request = WebRequest.Create("https://jsonplaceholder.typicode.com/comments");
            request.Credentials = CredentialCache.DefaultCredentials;
            WebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

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

        public void ShowComments()
        {
            using (ModelDB db = new ModelDB())
            {
                var comm = db.Comments;
                foreach (Comment comment in comm)
                {
                    Console.WriteLine($"PostID: {comment.PostId}");
                    Console.WriteLine($"ID: {comment.Id}");
                    Console.WriteLine($"Name: {comment.Name}");
                    Console.WriteLine($"E-Mail: {comment.Email}");
                    Console.WriteLine($"Body: {comment.Body}");
                    Console.WriteLine(new string('-', 20));
                }
            }
        }
    }
}
