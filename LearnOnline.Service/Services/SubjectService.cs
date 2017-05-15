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
    public interface ISubjectService
    {
        IEnumerable<Subject> GetAll();
        Subject GetById(int id);
    }

    public class SubjectService : ISubjectService
    {
        ISubjectRepository _subjectRepository;
        IUnitOfWork _unitOfWork;

        // khoi tao service
        public SubjectService(
            ISubjectRepository subjectRepository,
            IUnitOfWork unitOfWork)
        {
            this._subjectRepository = subjectRepository;
            this._unitOfWork = unitOfWork;
        }


        public IEnumerable<Subject> GetAll()
        {
            return _subjectRepository.GetAll();
        }

        public Subject GetById(int id)
        {
            return _subjectRepository.GetSingleById(id);
        }
    }
}
