using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ivony.Data;
using Ivony.Data.SqlClient;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public static class Common
    {

        public static dynamic Chunk(this DataTable dt, int startIndex, int count)
        {

            var jsonData = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dt));

            var list = ((JArray)jsonData).Skip(startIndex).Take(count).ToList();

            return list;
        }

        public static int Count(this DataTable dt)
        {
            var jsonData = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dt));

            return ((JArray)jsonData).Count;
        }

    }
}