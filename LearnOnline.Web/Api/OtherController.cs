using AutoMapper;
using LearnOnline.Model.Models;
using LearnOnline.Service.Services;
using LearnOnline.Web.Infrastructure.Core;
using LearnOnline.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace LearnOnline.Web.Api
{
    [RoutePrefix("api/other")]
    public class OtherController : ApiControllerBase
    {
        #region Initialize
        private IUserService _userService;
        private ISubjectService _subjectService;
        private ILevelService _levelService;
        private IExamService _examService;
        private IUserGroupService _userGroupService;
        private IProvinceService _provinceService;
        private IDistrictService _districtService;

        public OtherController(IUserService userService,
                            ISubjectService subjectService,
                            ILevelService levelService,
                            IExamService examService,
                            IUserGroupService userGroupService,
                            IProvinceService provinceService,
                            IDistrictService districtService)
            : base()
        {
            this._userService = userService;
            this._subjectService = subjectService;
            this._levelService = levelService;
            this._examService = examService;
            this._userGroupService = userGroupService;
            this._provinceService = provinceService;
            this._districtService = districtService;
        }
        #endregion

        [Route("getbyprovince")]
        [HttpGet]
        public HttpResponseMessage GetByProvinceId(HttpRequestMessage request, int provinceId)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _districtService.GetByProvinceId(provinceId);
                var responseData = Mapper.Map<IEnumerable<District>, IEnumerable<DistrictViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [Route("getallprovince")]
        [HttpGet]
        public HttpResponseMessage GetAllProvince(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
           
                var model = _provinceService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Province>, IEnumerable<ProvinceViewModel>>(model);
                var response = Request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [Route("getusergroupnostudent")]
        [HttpGet]
        public HttpResponseMessage GetUserGroupNoStudent(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _userGroupService.GetUserGroupNoStudent();
                var responseData = Mapper.Map<IEnumerable<UserGroup>, IEnumerable<UserGroupViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [Route("getallsubjects")]
        [HttpGet]
        public HttpResponseMessage GetAllSubjects(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _subjectService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Subject>, IEnumerable<SubjectViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [Route("getalllevels")]
        [HttpGet]
        public HttpResponseMessage GetAllLevels(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _levelService.GetAll();
                var responseData = Mapper.Map<IEnumerable<Level>, IEnumerable<LevelViewModel>>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            };
            return CreateHttpResponse(request, func);
        }
    }
}
