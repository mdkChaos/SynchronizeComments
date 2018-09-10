using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizeComments
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller ctrl = new Controller();

            List<Comment> comments = ctrl.GetCommentsFromSite();

            ctrl.AddCommentsToDB(comments);

            Console.ReadKey();

            ctrl.ShowComments();
            
            Console.ReadKey();
        }
    }
}
