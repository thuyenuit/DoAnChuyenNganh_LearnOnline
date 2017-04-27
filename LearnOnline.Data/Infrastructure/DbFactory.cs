using LearnOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOnline.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private LearnOnlineDbContext dbContext;

        public LearnOnlineDbContext Init()
        {
            return dbContext ?? (dbContext = new LearnOnlineDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
