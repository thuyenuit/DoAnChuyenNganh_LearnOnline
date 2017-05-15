using LearnOnline.Data.Infrastructure;
using LearnOnline.Data.Repositories;
using LearnOnline.Model.Models;
using System.Collections.Generic;
using System;

namespace LearnOnline.Service.Services
{
    public interface IUserService
    {
        // dinh nghia ra cac phuong thuc can su dung
        User Add(User user);
        void Update(User user);
        void Delete(int id);
        IEnumerable<User> GetAll();
        IEnumerable<User> GetAllPagingByUserGroupStudent();
        IEnumerable<User> GetAllPagingByUserGroupTeacherAndAdmin();
        IEnumerable<User> GetAllPaging(int page, int pageSize, out int totalRow);
        IEnumerable<User> GetAllPagingByKeyWord(string keyword, int page, int pageSize, out int totalRow);
        User GetById(int id);
        bool SelectByUsername(string username);

        void SaveChange();
    }

    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        // khoi tao service
        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
        }

        public User Add(User user)
        {
            return _userRepository.Add(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<User> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _userRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<User> GetAllPagingByKeyWord(string keyword, int page, int pageSize, out int totalRow)
        {
            return _userRepository.GetByKeyword(keyword, page, pageSize, out totalRow);
        }

        public IEnumerable<User> GetAllPagingByUserGroupStudent()
        {
            return _userRepository.GetAllPagingByUserGroupStudent();
        }

        public User GetById(int id)
        {
            return _userRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public IEnumerable<User> GetAllPagingByUserGroupTeacherAndAdmin()
        {
            return _userRepository.GetAllPagingByUserGroupTeacherAndAdmin();
        }

        public bool SelectByUsername(string username)
        {
            return _userRepository.SelectByUsername(username);
        }
    }
}