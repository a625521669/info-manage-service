using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public class CourseController : ApiController
    {
        public ContentResult List(string type = null, int startIndex = 0, int count = 999, string keywords = "")
        {
            var data = new DataTable();


            if (type == null || type == "所有")
                data = db.T("select * from Course where  Title like '%" + keywords + "%' or Detail like '%" + keywords + "%' order by PubDate desc").ExecuteDataTable();
            else
                data = db.T("select * from Course where Type = {0} and (Title like '%" + keywords + "%' or Detail like '%" + keywords + "%') order by PubDate desc", type).ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                count = data.Count()
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpPost]
        public JsonResult Add(Course c)
        {
            if (c.CourseName == null)
                return Json(new
                {
                    success = false,
                    reason = "课程名称不能为空！"
                }, JsonRequestBehavior.AllowGet);


            db.T("insert into Course(CourseName, Contents, Credit, QuantityLimit, ExamTime, ExamPosition, TeacherUserName) values({...})",
                c.CourseName, c.Contents, c.Credit, c.QuantityLimit, c.ExamTime, c.ExamPosition, c.TeacherUserName).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Modify(Course c)
        {

            db.T(@"update Course set CourseName = {0}, Contents = {1}, Credit = {4}, QuantityLimit = {5}, ExamTime = {6}, ExamPosition = {7}, TeacherUserName = {8} where ID = {9} ", 
                c.CourseName, c.Contents, c.Credit, c.QuantityLimit, c.ExamTime, c.ExamPosition, c.TeacherUserName, c.ID).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete(string id)
        {

            db.T("delete from Course where ID = {0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info(string id)
        {
            var data = db.T("select * from Course where ID = {0}", id).ExecuteDynamicObject();

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }
    }
}