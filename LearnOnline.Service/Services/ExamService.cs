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
    public interface IExamService
    {
        // dinh nghia ra cac phuong thuc can su dung
        Exam Add(Exam user);
        void Update(Exam user);
        void UpdateStatus(int id);
        void Delete(int id);
        IEnumerable<Exam> GetAll();
        IEnumerable<Exam> GetAllPaging(int page, int pageSize, out int totalRow);
        IEnumerable<Exam> GetAllDetailByExamID(int examId);
        IEnumerable<Exam> GetAllPagingByFilter(string keyword, int subjectId, int levelId, int page, int pageSize, out int totalRow);
        Exam GetById(int id);
        void SaveChange();
    }

    public class ExamService : IExamService
    {
        private IExamRepository _examRepository;
        private IUnitOfWork _unitOfWork;

        // khoi tao service
        public ExamService(
            IExamRepository examRepository,
            IUnitOfWork unitOfWork)
        {
            this._examRepository = examRepository;
            this._unitOfWork = unitOfWork;
        }

        public Exam Add(Exam exam)
        {
            return _examRepository.Add(exam);
        }

        public void Delete(int id)
        {
            _examRepository.Delete(id);
        }

        public IEnumerable<Exam> GetAll()
        {
            return _examRepository.GetAll();
        }

        public IEnumerable<Exam> GetAllDetailByExamID(int examId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exam> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _examRepository.GetMultiPaging(x => x.Status == true, out totalRow, page, pageSize);
        }

        public IEnumerable<Exam> GetAllPagingByFilter(string keyword, int subjectId, int levelId, int page, int pageSize, out int totalRow)
        {
            return _examRepository.GetAllPagingByFilter(keyword, subjectId, levelId, page, pageSize, out totalRow);
        }

        public Exam GetById(int id)
        {
            return _examRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Exam exam)
        {
            _examRepository.Update(exam);
        }

        public void UpdateStatus(int id)
        {
            _examRepository.UpdateStatus(id);
        }
    }
}
