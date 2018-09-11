using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizeComments
{
    class MyEqualityComparer : IEqualityComparer<Comment>
    {
        public bool Equals(Comment x, Comment y)
        {
            if (ReferenceEquals(x, y)) return true;
            return x != null
                && y != null
                && x.Id.Equals(y.Id)
                && x.PostId.Equals(y.PostId)
                && x.Name.Equals(y.Name)
                && x.Email.Equals(y.Email)
                && x.Body.Equals(y.Body);
        }

        public int GetHashCode(Comment obj)
        {
            int hashPostId = obj.PostId.GetHashCode();
            int hashId = obj.Id.GetHashCode();
            int hashName = obj.Name == null ? 0 : obj.Name.GetHashCode();
            int hashEmail = obj.Email == null ? 0 : obj.Email.GetHashCode();
            int hashBody = obj.Body == null ? 0 : obj.Body.GetHashCode();
            return hashPostId ^ hashId ^ hashName ^ hashEmail ^ hashBody;
        }
    }
}
