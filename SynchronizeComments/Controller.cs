﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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

        public void AddCommentsToDB(List<Comment> comments)
        {
            using (ModelDB db = new ModelDB())
            {
                int count = 0;
                foreach (Comment comment in comments)
                {
                    Comment find = db.Comments.Find(comment.Id);
                    if (find == null)
                    {
                        db.Comments.Add(comment);
                        count++;
                    }
                }
                if (count > 0)
                {
                    db.SaveChanges();
                    Console.WriteLine($"Добавленно {count} новых записей");
                }
                else
                {
                    Console.WriteLine("Новые записи отсутствуют");
                }
            }
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