using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class Course
    {
        public int ID { set; get; }
        public string CourseName { set; get; }
        public string Contents { set; get; }
        public decimal Credit { set; get; }
        public int QuantityLimit { set; get; }
        public int ExamTime { set; get; }
        public DateTime Time { set; get; }
        public string ExamPosition { set; get; }
        public string TeacherUserName { set; get; }
        public string Subject { set; get; }
    }
}