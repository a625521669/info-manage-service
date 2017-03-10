using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class Review
    {
       public int ID { set; get; }

        public string TeacherUserName { set; get; }

        public string TeacherName { set; get; }

        public DateTime CreateOn { set; get; }

        public string Contents { set; get; }

        public string Reviews { set; get; }

        public string UserName { set; get; }

        public string Name { set; get; }
    }
}