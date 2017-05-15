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
    public interface IUserGroupRepository : IRepository<UserGroup>
    {
        IEnumerable<UserGroup> GetUserGroupNoStudent();
    }

    public class UserGroupRepository : RepositoryBase<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<UserGroup> GetUserGroupNoStudent()
        {
            var query = from u in DbContext.UserGroups                       
                        where u.ID != 1
                        select u;
            return query.ToList();
        }       
    }
}
