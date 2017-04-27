using LearnOnline.Data.Infrastructure;
using LearnOnline.Data.Reponsitories;
using LearnOnline.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace LearnOnline.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        // định nghĩa thêm các method ngoài repository generated mà ta tự định nghĩa
        IEnumerable<User> GetByKeyword(string keyword, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<User> GetAllPagingByUserGroup(int idNhom);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<User> GetAllPagingByUserGroup(int groupId)
        {
            var query = from u in DbContext.Users
                        join p in DbContext.Provinces
                        on u.ProvincesID equals p.ID
                        where u.UserGroupID == groupId
                        select u;
            return query.ToList();
        }

        public IEnumerable<User> GetByKeyword(string keyword, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from u in DbContext.Users
                        where u.FullName.Contains(keyword)
                        || u.Address.Contains(keyword)
                        select u;
            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query;
        }
    }
}