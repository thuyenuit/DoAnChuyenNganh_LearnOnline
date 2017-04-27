using LearnOnline.Model.Models;
using LearnOnline.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LearnOnline.Web.Infrastructure.Core;
using LearnOnline.Web.Models;
using AutoMapper;
using LearnOnline.Web.Infrastructure.Extensions;

namespace LearnOnline.Web.Api
{
    [RoutePrefix("api/user")]
    public class UserController : ApiControllerBase
    {
        #region Initialize
        private IUserService _userService;
        private IUserGroupService _userGroupService;
        private IDistrictService _districtService;
        private IProvinceService _provinceService;

        public UserController(IUserService userService,
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

        [Route("getallstudent")]
        [HttpGet]
        public HttpResponseMessage GetAllStudent(HttpRequestMessage request, int idNhom, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _userService.GetAllPagingByUserGroup(1);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var resultData = from user in query.ToList()
                                 join usergroup in _userGroupService.GetAll().ToList()
                                 on user.UserGroupID equals usergroup.ID
                                 join dis in _districtService.GetAll().ToList()
                                 on user.DistrictID equals dis.ID
                                 join prov in _provinceService.GetAll().ToList()
                                 on user.ProvincesID equals prov.ID
                                 select new UserViewModel
                                 {
                                     ID = user.ID,
                                     Avatar = user.Avatar,
                                     FullName = user.FullName,
                                     Address = user.Address,
                                     NgaySinh = user.NgaySinh,
                                     Sex = user.Sex,
                                     SSN = user.SSN,
                                     Phone = user.Phone,
                                     Email = user.Email,
                                     ProvincesID = user.ProvincesID,
                                     DistrictID = user.DistrictID,
                                     UnitName = user.UnitName,
                                     Status = user.Status,
                                     UserName = user.UserName,
                                     Password = user.Password,
                                     UserGroupID = user.UserGroupID,
                                     UserGroupName = usergroup.Name,
                                     ProvincesName = prov.ProvincesName,
                                     DistrictName = dis.DistrictName,
                                 };
               
                var paginationSet = new PaginationSet<UserViewModel>()
                {
                    Items = resultData.ToList(),
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage CreateUser(HttpRequestMessage request, UserViewModel userVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newUser = new User();
                    newUser.UpdateUser(userVM);
                    _userService.Add(newUser);
                    _userService.SaveChange();
                    var responseData = Mapper.Map<User, UserViewModel>(newUser);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}
