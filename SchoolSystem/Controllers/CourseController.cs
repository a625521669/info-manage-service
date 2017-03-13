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


            if (!string.IsNullOrEmpty(keywords))
                data = db.T("select * from Course where ID={0} and Time > GetDate() order by Time desc", keywords).ExecuteDataTable();
            else
                data = db.T("select * from Course where Time > GetDate() order by Time desc").ExecuteDataTable();

            data.Columns.Add(new DataColumn("SelectedCount"));
            foreach (DataRow dr in data.Rows)
                dr["SelectedCount"] = db.T("select count(*) from CourseChoose where CourseID = {0}", dr["ID"].ToString()).ExecuteScalar();

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


            db.T("insert into Course(CourseName, Contents, Credit, QuantityLimit, ExamTime, ExamPosition, TeacherUserName, Subject, Time) values({...})",
                c.CourseName, c.Contents, c.Credit, c.QuantityLimit, c.ExamTime, c.ExamPosition, c.TeacherUserName, c.Subject, c.Time).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Modify(Course c)
        {

            db.T(@"update Course set CourseName = {0}, Contents = {1}, Credit = {2}, QuantityLimit = {3}, ExamTime = {4}, ExamPosition = {5}, TeacherUserName = {6}, Subject = {7}, Time={8} where ID = {9} ",
                c.CourseName, c.Contents, c.Credit, c.QuantityLimit, c.ExamTime, c.ExamPosition, c.TeacherUserName, c.Subject, c.Time, c.ID).Execute();

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

        public JsonResult Choose(string courseId, string studentNo)
        {
            var selectedCount = db.T("select count(*) from CourseChoose where CourseID={0}", courseId).ExecuteScalar();

            var courseInfo = db.T("select * from Course where ID={0}", courseId).ExecuteDynamicObject();

            if (DateTime.Now > (DateTime)courseInfo.Time)
                return Json(new
                {
                    success = false,
                    reason = "已经过了选课时间",
                }, JsonRequestBehavior.AllowGet);

            if (courseInfo.QuantityLimit != null && (int)courseInfo.QuantityLimit >= (int)selectedCount)
                return Json(new
                {
                    success = false,
                    reason = "选课人数已满",
                }, JsonRequestBehavior.AllowGet);

            db.T("insert into CourseChoose(CourseID, StudentNO) values({...})", courseId, studentNo).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}