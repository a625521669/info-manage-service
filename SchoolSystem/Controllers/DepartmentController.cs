using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public class DepartmentController : ApiController
    {
        public ContentResult List(string levels = null, int startIndex = 0, int count = 999)
        {
            var data = new DataTable();

            if (levels == null)
                data = db.T("select * from Department order by PubDate desc").ExecuteDataTable();
            else
                data = db.T("select * from Department where Levels = {0} order by PubDate desc", levels).ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                count = data.Count()
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpPost]
        public JsonResult Add(Department dep)
        {
            if (dep.Name == null)
                return Json(new
                {
                    success = false,
                    reason = "名称不能为空！"
                }, JsonRequestBehavior.AllowGet);


            dep.PubDate = DateTime.Now;

            db.T("insert into Department(Name, Secretery, Levels, Leader, PubDate, Contacts, ContatctsPhone) values({0}, {1}, {2}, {3}, {4}, {5}, {6})", dep.Name, dep.Secretery, dep.Levels, dep.Leader, dep.PubDate, dep.Contacts, dep.ContatctsPhone).Execute();

            return Json(new
            {
                success = true,
                dep
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Edit(Department dep)
        {

            db.T(@"update Department set Name = {0}, Secretery = {1}, Levels = {2}, Contacts = {3}, ContatctsPhone = {4}, Leader = {6} where ID = {5} ", dep.Name, dep.Secretery, dep.Levels, dep.Contacts, dep.ContatctsPhone, dep.ID, dep.Leader).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete(string id)
        {

            db.T("delete from Department where ID = {0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info(string id)
        {
            var data = db.T("select * from Department where ID = {0}", id).ExecuteDynamicObject();

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }
    }
}