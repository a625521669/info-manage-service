using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public class DepGroupController : ApiController
    {
        public ContentResult List(string owndep = null, int startIndex = 0, int count = 999, bool includeDepData = true)
        {
            var data = new DataTable();

            if (owndep == null)
                data = db.T("select * from DepGroup order by PubDate desc").ExecuteDataTable();
            else
                data = db.T("select * from DepGroup where owndep = {0} order by PubDate desc", owndep).ExecuteDataTable();

            dynamic depData = null;
            if (includeDepData)
                depData = db.T("select * from Department").ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                count = data.Count(),
                depData = depData
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpPost]
        public JsonResult Add(DepGroup group)
        {
            if (group.Name == null)
                return Json(new
                {
                    success = false,
                    reason = "名称不能为空！"
                }, JsonRequestBehavior.AllowGet);


            group.PubDate = DateTime.Now;

            db.T("insert into DepGroup(OwnDep, Leader, Member, Detail, Name, PubDate) values({0}, {1}, {2}, {3}, {4}, {5})", group.OwnDep, group.Leader, group.Member, group.Detail, group.Name, group.PubDate).Execute();

            return Json(new
            {
                success = true,
                group
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(DepGroup group)
        {

            db.T(@"update DepGroup set OwnDep = {0}, Leader = {1}, Member = {3}, Detail = {3}, Name = {3} where ID = {2}", group.OwnDep, group.Leader).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete(string id)
        {

            db.T("delete from DepGroup where ID = {0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info(string id)
        {
            var data = db.T("select * from DepGroup where ID = {0}", id).ExecuteDynamicObject();

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }
    }
}