using LearnOnline.Data.Infrastructure;
using LearnOnline.Data.Reponsitories;
using LearnOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOnline.Data.Repositories
{
    public interface IDetailExamRepository : IRepository<DetailExam>
    {
        IEnumerable<DetailExam> GetAllDetailExamByExamID(int examId);
    }

    public class DetailExamRepository : RepositoryBase<DetailExam>, IDetailExamRepository
    {
        public DetailExamRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<DetailExam> GetAllDetailExamByExamID(int examId)
        {
            IEnumerable<DetailExam> listDetail = DbContext.DetailExams;

            var list = from detail in listDetail
                       join exam in DbContext.Exams
                       on detail.ExamID equals exam.ID
                       select detail;

            return list.ToList();
        }
    }
}
