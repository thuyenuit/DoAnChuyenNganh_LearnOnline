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

    }

    public class UserGroupRepository : RepositoryBase<UserGroup>, IUserGroupRepository
    {
        public UserGroupRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }


    }
}
