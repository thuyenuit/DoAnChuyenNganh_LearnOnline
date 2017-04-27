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
    public interface IProvinceService
    {
        IEnumerable<Province> GetAll();
    }

    public class ProvinceService : IProvinceService
    {
        private IProvinceRepository _provinceRepository;
        private IUnitOfWork _unitOfWork;

        // khoi tao service
        public ProvinceService(
            IProvinceRepository provinceRepository,
            IUnitOfWork unitOfWork)
        {
            this._provinceRepository = provinceRepository;
            this._unitOfWork = unitOfWork;
        }
    
        public IEnumerable<Province> GetAll()
        {
            return _provinceRepository.GetAll();
        }
    }
}
