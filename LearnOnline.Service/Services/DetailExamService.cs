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
    public interface IDetailExamService
    {
        IEnumerable<DetailExam> GetAll();
        IEnumerable<DetailExam> GetAllDetailExamByExamID(int examId);
    }

    public class DetailExamService : IDetailExamService
    {
        private IDetailExamRepository _detailExamRepository;
        private IUnitOfWork _unitOfWork;

        // khoi tao service
        public DetailExamService(
            IDetailExamRepository detailExamRepository,
            IUnitOfWork unitOfWork)
        {
            this._detailExamRepository = detailExamRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<DetailExam> GetAll()
        {
            return _detailExamRepository.GetAll();
        }

        public IEnumerable<DetailExam> GetAllDetailExamByExamID(int examId)
        {
            return _detailExamRepository.GetAllDetailExamByExamID(examId);
        }
    }
}
