using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolSystem.Models
{
    public class Department
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Secretery { set; get; }
        public int Levels { set; get; }
        public string Leader { set; get; }
        public DateTime PubDate { set; get; }
        public string Contacts { set; get; }
        public string ContatctsPhone { set; get; }
    }
}