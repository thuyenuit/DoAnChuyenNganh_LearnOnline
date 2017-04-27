using LearnOnline.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearnOnline.Web.Api
{
    public class ExamController : ApiController
    {
        #region Initialize
        private IUserService _userService;
        private IUserGroupService _userGroupService;
        private IDistrictService _districtService;
        private IProvinceService _provinceService;

        public ExamController(IUserService userService,
                            IUserGroupService userGroupService,
                            IDistrictService districtService,
                            IProvinceService provinceService)
            : base()
        {
            this._userService = userService;
            this._userGroupService = userGroupService;
            this._districtService = districtService;
            this._provinceService = provinceService;
        }
        #endregion    
    }
}
