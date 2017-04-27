using LearnOnline.Data.Infrastructure;
using LearnOnline.Data.Repositories;
using LearnOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOnline.Service.Services
{
    public interface IUserGroupService
    {
        IEnumerable<UserGroup> GetAll();
        UserGroup GetById(int id);
    }

    public class UserGroupService : IUserGroupService
    {
        IUserGroupRepository _userGroupRepository;
        IUnitOfWork _unitOfWork;

        // khoi tao service
        public UserGroupService(
            IUserGroupRepository userGroupRepository,
            IUnitOfWork unitOfWork)
        {
            this._userGroupRepository = userGroupRepository;
            this._unitOfWork = unitOfWork;
        }

       
        public IEnumerable<UserGroup> GetAll()
        {
            return _userGroupRepository.GetAll();
        }     

        public UserGroup GetById(int id)
        {
            return _userGroupRepository.GetSingleById(id);
        }     
    }
}
