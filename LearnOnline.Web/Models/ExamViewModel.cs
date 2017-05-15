using LearnOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnOnline.Web.Models
{
    public class ExamViewModel
    {
        public int ID { get; set; }
        public int SubjectsID { get; set; }
        public int LevelID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UserCreate { get; set; }
        public bool? Status { get; set; }

        public IEnumerable<DetailExam> detailExam { get; set; }

        public string SubjectsName { get; set; }
        public string LevelName { get; set; }
    }
}