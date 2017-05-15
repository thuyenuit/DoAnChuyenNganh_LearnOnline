using LearnOnline.Model.Models;
using LearnOnline.Service.Services;
using LearnOnline.Web.Models;
using LearnOnline.Web.Infrastructure.Extensions;
using LearnOnline.Web.Infrastructure.Core;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using System.Web.Script.Serialization;

namespace LearnOnline.Web.Api
{
    [RoutePrefix("api/exam")]
    public class ExamController : ApiControllerBase
    {
        #region Initialize
        private IUserService _userService;
        private ISubjectService _subjectService;
        private ILevelService _levelService;
        private IExamService _examService;
        private IDetailExamService _detailExamService;

        public ExamController(IUserService userService,
                            ISubjectService subjectService,
                            ILevelService levelService,
                            IExamService examService,
                            IDetailExamService detailExamService)
            : base()
        {
            this._userService = userService;
            this._subjectService = subjectService;
            this._levelService = levelService;
            this._examService = examService;
            this._detailExamService = detailExamService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAllStudent(HttpRequestMessage request, 
            string keyword, int subjectId, int levelId, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                // var model = _examService.GetAll();
                var model = _examService.GetAllPagingByFilter(keyword, subjectId, levelId, page, pageSize, out totalRow);

               // totalRow = model.Count();
                //var query = model.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var resultData = from exam in model.ToList()
                                 join level in _levelService.GetAll().ToList()
                                 on exam.LevelID equals level.ID
                                 join sub in _subjectService.GetAll().ToList()
                                 on exam.SubjectsID equals sub.ID                  
                                 select new ExamViewModel
                                 {
                                     ID = exam.ID,
                                     SubjectsName = sub.subjectName,
                                     LevelName = level.LevelName,
                                     UserCreate = exam.UserCreate,
                                     CreateDate = exam.CreateDate,
                                     Status = exam.Status
                                 };

                var paginationSet = new PaginationSet<ExamViewModel>()
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

        [Route("import")]
        [HttpPost]
        public async Task<HttpResponseMessage> Import()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được server hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Excels");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            //do stuff with files if you wish
            if (result.FormData["subjectId"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn danh môn học.");
            }
            if (result.FormData["levelId"] == null)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bạn chưa chọn cập độ.");
            }

            //Upload files
            int addedCount = 0;

            int subjectId = 0;
            int.TryParse(result.FormData["subjectId"], out subjectId);

            int levelId = 0;
            int.TryParse(result.FormData["levelId"], out levelId);

            foreach (MultipartFileData fileData in result.FileData)
            {
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Yêu cầu không đúng định dạng");
                }
                string fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                var fullPath = Path.Combine(root, fileName);
                File.Copy(fileData.LocalFileName, fullPath, true);

                Exam exam = new Exam
                {
                    SubjectsID = subjectId,
                    LevelID = levelId,
                    CreateDate = DateTime.Now.Date,
                    UserCreate = "admin",
                    Status = true,
                };

                //insert to DB
                var listDetailExam = this.ReadProductFromExcel(fullPath, exam.ID);

                if (listDetailExam.Count > 0)
                {
                    _examService.Add(exam);

                    foreach (var item in listDetailExam)
                    {
                        exam.DetailExams.Add(item);

                        addedCount++;
                    }
                    _examService.SaveChange();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " câu hỏi.");
        }

        private List<DetailExam> ReadProductFromExcel(string fullPath, int examId)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<DetailExam> listDetailExam = new List<DetailExam>();
                DetailExamViewModel examViewModel;
                DetailExam exam;            

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    examViewModel = new DetailExamViewModel();
                    exam = new DetailExam();

                    examViewModel.ExamID = examId;
                    examViewModel.Question = workSheet.Cells[i, 1].Value.ToString();
                    examViewModel.OptionA = workSheet.Cells[i, 2].Value.ToString();
                    examViewModel.OptionB = workSheet.Cells[i, 3].Value.ToString();
                    examViewModel.OptionC = workSheet.Cells[i, 4].Value.ToString();
                    examViewModel.OptionD = workSheet.Cells[i, 5].Value.ToString();
                    examViewModel.Answer = workSheet.Cells[i, 6].Value.ToString();

                    exam.UpdateDetailExam(examViewModel);

                    listDetailExam.Add(exam);
                }
                return listDetailExam;
            }
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string jsonlistId)
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
                    var listExam = new JavaScriptSerializer().Deserialize<List<int>>(jsonlistId);

                    foreach (var item in listExam)
                    {
                        _examService.UpdateStatus(item);
                    }

                    _examService.SaveChange();

                    response = request.CreateResponse(HttpStatusCode.OK, listExam.Count());
                }
                return response;
            });    
        }

    }
}
