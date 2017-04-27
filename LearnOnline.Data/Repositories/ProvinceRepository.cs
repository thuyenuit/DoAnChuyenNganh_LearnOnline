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

     public interface IProvinceRepository : IRepository<Province>
    {

    }

    public class ProvinceRepository : RepositoryBase<Province>, IProvinceRepository
    {
        public ProvinceRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

    }
}
