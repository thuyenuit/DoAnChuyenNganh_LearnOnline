using LearnOnline.Data.Infrastructure;
using LearnOnline.Data.Reponsitories;
using LearnOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOnline.Data.Repositories
{
    public interface ILevelRepository : IRepository<Level>
    {

    }

    public class LevelRepository : RepositoryBase<Level>, ILevelRepository
    {
        public LevelRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }


    }
}
