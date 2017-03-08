using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public class NewsController : ApiController
    {
        public ContentResult List(string type = null, int startIndex = 0, int count = 999, string keywords = "")
        {
            var data = new DataTable();


            if (type == null || type == "所有")
                data = db.T("select * from News where  Title like '%" + keywords + "%' or Detail like '%" + keywords + "%' order by PubDate desc").ExecuteDataTable();
            else
                data = db.T("select * from News where Type = {0} and (Title like '%" + keywords + "%' or Detail like '%" + keywords + "%') order by PubDate desc", type).ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                count = data.Count()
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpPost]
        public JsonResult Add(News news)
        {
            if (news.Title == null)
                return Json(new
                {
                    success = false,
                    reason = "标题不能为空或不存在！"
                }, JsonRequestBehavior.AllowGet);


            news.PubDate = DateTime.Now;

            db.T("insert into News(Title, Type, Detail, PubDate, Author) values({0}, {1}, {2}, {3}, {4})", news.Title, news.Type, news.Detail, news.PubDate, news.Author).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Modify(News news)
        {

            db.T(@"update News set Type = {0}, Detail = {1}, Title = {3} where ID = {2} ", news.Type, news.Detail, news.ID, news.Title).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete(string id)
        {

            db.T("delete from News where ID = {0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info(string id)
        {
            var data = db.T("select * from News where ID = {0}", id).ExecuteDynamicObject();

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }
    }
}