using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronizeComments
{
    class ModelDB : DbContext
    {
        public ModelDB() : base("name=ModelDB")
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
    }
}
