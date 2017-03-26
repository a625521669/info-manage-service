using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace SchoolSystem.Controllers
{
    public class CourseController : ApiController
    {
        public ContentResult List(string type = "", int startIndex = 0, int count = 999, string keywords = "")
        {
            var data = new DataTable();


            if (!string.IsNullOrEmpty(keywords))
                data = db.T("select * from Course where ID={0} and Subject like '%" + type + "%' order by Time desc", keywords).ExecuteDataTable();
            else
                data = db.T("select * from Course where Subject like '%" + type + "%' order by Time desc").ExecuteDataTable();

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
        public JsonResult Add(Course c, DateTime? LeadTime = null)
        {
            if (LeadTime != null)
                c.Time = (DateTime)LeadTime;

            if (c.CourseName == null)
                return Json(new
                {
                    success = false,
                    reason = "课程名称不能为空！"
                }, JsonRequestBehavior.AllowGet);

            var teacher = db.T("select * from UserProfile where UserName={0}", c.TeacherUserName).ExecuteDynamicObject();

            c.TeacherName = (string)teacher.Name;

            db.T("insert into Course(CourseName, Contents, Credit, QuantityLimit, ExamTime, ExamPosition, TeacherUserName, Subject, Time, TeacherName) values({...})",
                c.CourseName, c.Contents, c.Credit, c.QuantityLimit, c.ExamTime, c.ExamPosition, c.TeacherUserName, c.Subject, c.Time, c.TeacherName).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Modify(Course c)
        {

            db.T(@"update Course set CourseName = {0}, Contents = {1}, Credit = {2}, QuantityLimit = {3}, ExamTime = {4}, ExamPosition = {5}, TeacherUserName = {6}, Subject = {7}, Time={8}, TeacherName={9} where ID = {10} ",
                c.CourseName, c.Contents, c.Credit, c.QuantityLimit, c.ExamTime, c.ExamPosition, c.TeacherUserName, c.Subject, c.Time, c.TeacherName, c.ID).Execute();

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

            var selectInfo = db.T("select count(*) from CourseChoose where CourseID = {0} and StudentNO = {1}", courseId, studentNo).ExecuteScalar();

            var teacherInfo = db.T("select * from UserProfile where UserName = {0}", (string)courseInfo.TeacherUserName).ExecuteDynamicObject();

            if ((int)selectInfo > 0)
                return Json(new
                {
                    success = false,
                    reason = "你已经选课",
                }, JsonRequestBehavior.AllowGet);


            if (DateTime.Now > (DateTime)courseInfo.Time)
                return Json(new
                {
                    success = false,
                    reason = "已经过了选课时间",
                }, JsonRequestBehavior.AllowGet);

            if (courseInfo.QuantityLimit != null && (int)courseInfo.QuantityLimit <= (int)selectedCount)
                return Json(new
                {
                    success = false,
                    reason = "选课人数已满",
                }, JsonRequestBehavior.AllowGet);

            db.T("insert into CourseChoose(CourseID, StudentNO, TeacherUserName, TeacherName) values({...})", courseId, studentNo, (string)teacherInfo.UserName, (string)teacherInfo.Name).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelChoose(int id)
        {
            db.T("delete from CourseChoose where ID={0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }


        public object ChooseList(int startIndex = 0, int count = 999, string studentNo = "", string teacherName = "", string teacherUserName = "", string courseID = "", int sort = 0)
        {
            var orderby = "";

            if (sort == 1)
                orderby = " order by Score asc";

            if (sort == 2)
                orderby = " order by Score desc";

            var filterList = new List<string>();

            if (!string.IsNullOrEmpty(studentNo))
                filterList.Add("StudentNO='" + studentNo + "'");

            if (!string.IsNullOrEmpty(teacherName))
                filterList.Add("TeacherName like '%" + teacherName + "%'");

            if (!string.IsNullOrEmpty(teacherUserName))
                filterList.Add("TeacherUserName = '" + teacherUserName + "'");

            if (!string.IsNullOrEmpty(courseID))
                filterList.Add("CourseID = " + courseID);

            dynamic chooseList;

            if (filterList.Count > 0)
                chooseList = db.T("select * from CourseChoose where " + string.Join(" and ", filterList) + orderby).ExecuteDynamics();
            else
                chooseList = db.T("select * from CourseChoose" + orderby).ExecuteDynamics();

            var data = new JArray();

            foreach (dynamic item in chooseList)
            {
                var courseInfo = db.T("select * from Course where ID = {0}", (int)item.CourseID).ExecuteDynamicObject();

                data.Add(JObject.FromObject(new
                {
                    CourseInfo = courseInfo,
                    ChooseInfo = item
                }));
            }

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Skip(startIndex).Take(count),
                count = data.Count
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        public object ExamModify(string ID, DateTime ExamTime, string ExamPosition)
        {
            db.T("update CourseChoose set ExamTime={1}, ExamPosition={2} where CourseID={0}", ID, ExamTime, ExamPosition).Execute();
            db.T("update Course set ExamTime={1}, ExamPosition={2} where ID={0}", ID, ExamTime, ExamPosition).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public object ScoreInput(string ids, string scores)
        {
            string[] idArray = ids.Split(',');
            string[] scoreArray = scores.Split(',');

            for (int i = 0; i < idArray.Length; i++)
                db.T("update CourseChoose set Score = {1} where ID = {0}", int.Parse(idArray[i]), int.Parse(scoreArray[i])).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}