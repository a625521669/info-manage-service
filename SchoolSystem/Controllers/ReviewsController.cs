using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public class ReviewsController : ApiController
    {
        // GET: Reviews
        public ContentResult List(string type = null, int startIndex = 0, int count = 999, string keywords = "")
        {
            var data = new DataTable();

            data = db.T("select * from Reviews where  Contents like '%" + keywords + "%' or Reviews like '%" + keywords + "%' order by CreateOn desc").ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                count = data.Count()
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpPost]
        public JsonResult Add(Review r)
        {
            if (r.Contents == null)
                return Json(new
                {
                    success = false,
                    reason = "评论内容不能为空！"
                }, JsonRequestBehavior.AllowGet);


            db.T("insert into Reviews(TeacherUserName, TeacherName, CreateOn, Contents, Reviews, UserName, Name) values({...})", r.TeacherUserName, r.TeacherName, DateTime.Now, r.Contents, r.Reviews, r.UserName, r.Name).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Modify(Review r)
        {

            //db.T(@"update News set Type = {0}, Detail = {1}, Title = {3} where ID = {2} ", news.Type, news.Detail, news.ID, news.Title).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete(string id)
        {

            db.T("delete from Reviews where ID = {0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info(string id)
        {
            var data = db.T("select * from Reviews where ID = {0}", id).ExecuteDynamicObject();

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }
    }
}