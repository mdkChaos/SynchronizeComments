using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizeComments
{
    class Logger
    {
        public static void ShowComments(List<Comment> comments)
        {
            foreach (Comment comment in comments)
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
