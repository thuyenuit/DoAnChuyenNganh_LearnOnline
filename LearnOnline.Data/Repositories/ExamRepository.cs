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
    public interface IExamRepository : IRepository<Exam>
    {
        IEnumerable<Exam> GetAllDetailByExamID(int examId);
        IEnumerable<Exam> GetAllPagingByFilter(string keyword, int subjectId, int levelId, int page, int pageSize, out int totalRow);
        void UpdateStatus(int id);
    }

    public class ExamRepository : RepositoryBase<Exam>, IExamRepository
    {
        public ExamRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Exam> GetAllDetailByExamID(int examId)
        {
            var list = GetAll();

            foreach (var item in list)
            {
                item.DetailExams = DbContext.DetailExams.ToList();
            }

            return list;
        }

        public IEnumerable<Exam> GetAllPagingByFilter(string keyword, int subjectId, int levelId, int page, int pageSize, out int totalRow)
        {
            var list = GetAll();

            if (!string.IsNullOrEmpty(keyword))
            {
                list = from a in list
                       join sub in DbContext.Subjects
                       on a.SubjectsID equals sub.ID
                       join lev in DbContext.Levels
                       on a.LevelID equals lev.ID
                       where a.UserCreate.Contains(keyword)
                       || sub.subjectName.Contains(keyword)
                       || lev.LevelName.Contains(keyword)
                       select a;
            }
            if (subjectId != -1)
            {
                list = from a in list                   
                       where a.SubjectsID == subjectId
                       select a;
            }
            if (levelId != -1)
            {
                list = from a in list
                       where a.LevelID == levelId
                       select a;
            }

            var result = from exam in list
                         where exam.Status == true
                         select exam;

            totalRow = result.Count();

            return result.OrderByDescending(x => x.ID).Skip(page * pageSize).Take(pageSize);     
        }

        public void UpdateStatus(int id)
        {
            var list = DbContext.Exams.ToList();
            var result = list.FirstOrDefault(x => x.ID == id);
            if (result != null)
            {
                result.Status = false;
            }
        }
    }
}
