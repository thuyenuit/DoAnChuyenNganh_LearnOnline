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
    public interface ILevelService
    {
        IEnumerable<Level> GetAll();
        Level GetById(int id);
    }

    public class LevelService : ILevelService
    {
        ILevelRepository _levelRepository;
        IUnitOfWork _unitOfWork;

        // khoi tao service
        public LevelService(
            ILevelRepository levelRepository,
            IUnitOfWork unitOfWork)
        {
            this._levelRepository = levelRepository;
            this._unitOfWork = unitOfWork;
        }


        public IEnumerable<Level> GetAll()
        {
            return _levelRepository.GetAll();
        }

        public Level GetById(int id)
        {
            return _levelRepository.GetSingleById(id);
        }
    }
}
