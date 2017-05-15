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
    public interface IDistrictRepository : IRepository<District>
    {
        IEnumerable<District> GetByProvinceId(int provinceId);
    }

    public class DistrictRepository : RepositoryBase<District>, IDistrictRepository
    {
        public DistrictRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<District> GetByProvinceId(int provinceId)
        {
            var query = from u in DbContext.Districts                     
                        where u.ProvincesID == provinceId
                        select u;
            return query.ToList();
        }
    }
}
