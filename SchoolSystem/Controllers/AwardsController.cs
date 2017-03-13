using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public class AwardsController : ApiController
    {
        public ContentResult List(string type = null, int startIndex = 0, int count = 999)
        {
            var data = new DataTable();

            if (type == null)
                data = db.T("select * from Awards order by PubDate desc").ExecuteDataTable();
            else
                data = db.T("select * from Awards where Type = {0} order by PubDate desc", type).ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                count = data.Count()
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpPost]
        public JsonResult Add(Awards awards)
        {
            if(awards.NO == null || !IsUserExist(awards.NO))
                return Json(new
                {
                    success = false,
                    reason = "党员编号不能为空或不存在！"
                }, JsonRequestBehavior.AllowGet);


            awards.PubDate = DateTime.Now;

            db.T("insert into Awards(Type, NO, PubDate, HasAttacher, Detail) values({0}, {1}, {2}, {3}, {4})", awards.Type, awards.NO, awards.PubDate, awards.HasAttacher, awards.Detail).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(Awards awards)
        {

            db.T(@"update Awards set Type = {0}, Detail = {1} where ID = {2} ", awards.Type, awards.Detail, awards.ID).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(string id)
        {

            db.T("delete from Awards where ID = {0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info(string id)
        {
            var data = db.T("select * from Awards where ID = {0}", id).ExecuteDynamicObject();

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }


    }
}