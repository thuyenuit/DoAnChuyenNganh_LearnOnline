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
    public interface IDistrictService
    {
        IEnumerable<District> GetAll();
        IEnumerable<District> GetByProvinceId(int provinceId);
    }

    public class DistrictService : IDistrictService
    {
        private IDistrictRepository _districtRepository;
        private IUnitOfWork _unitOfWork;

        // khoi tao service
        public DistrictService(
            IDistrictRepository districtRepository,
            IUnitOfWork unitOfWork)
        {
            this._districtRepository = districtRepository;
            this._unitOfWork = unitOfWork;
        }    

        public IEnumerable<District> GetAll()
        {
            return _districtRepository.GetAll();
        }

        public IEnumerable<District> GetByProvinceId(int provinceId)
        {
            return _districtRepository.GetByProvinceId(provinceId);
        }
    }
}
