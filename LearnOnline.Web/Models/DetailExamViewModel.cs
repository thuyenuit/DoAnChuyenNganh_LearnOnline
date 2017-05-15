using LearnOnline.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearnOnline.Web.Models
{
    public class DetailExamViewModel
    {
        public int ID { get; set; }
        public int ExamID { get; set; }
        public string Question { get; set; }
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string Answer { get; set; }

    }
}