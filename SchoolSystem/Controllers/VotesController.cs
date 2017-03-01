using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public class VotesController : ApiController
    {

        public ContentResult List(string time = "all", string type = "all", int startIndex = 0, int count = 999)
        {
            var data = new DataTable();

            var nowDate = DateTime.Now;

            if (type == "all")
            {
                if (time == "proccess")
                    data = db.T("select * from VotesList where {0} >= BeginDate and {0} <= EndDate order by PubDate desc", nowDate).ExecuteDataTable();
                else if (time == "nobegin")
                    data = db.T("select * from VotesList where {0} < BeginDate order by PubDate desc", nowDate).ExecuteDataTable();
                else if (time == "end")
                    data = db.T("select * from VotesList where {0} > EndDate order by PubDate desc", nowDate).ExecuteDataTable();
                else
                    data = db.T("select * from VotesList order by PubDate desc").ExecuteDataTable();

            }
            else
            {
                if (time == "proccess")
                    data = db.T("select * from VotesList where {0} >= BeginDate and {0} <= EndDate and Type = {1} order by PubDate desc", nowDate, type).ExecuteDataTable();
                else if (time == "nobegin")
                    data = db.T("select * from VotesList where {0} < BeginDate and Type = {1} order by PubDate desc", nowDate, type).ExecuteDataTable();
                else if (time == "end")
                    data = db.T("select * from VotesList where {0} > EndDate and Type = {1} order by PubDate desc", nowDate, type).ExecuteDataTable();
                else
                    data = db.T("select * from VotesList where Type = {0} order by PubDate desc", type).ExecuteDataTable();
            }
                

            data.Columns.Add(new DataColumn("Votes"));

            foreach (DataRow dr in data.Rows)
                dr["Votes"] = db.T("select count(*) from VotesStatic where VoteID = {0}", dr["ID"]).ExecuteScalar();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                count = data.Count()
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        [HttpPost]
        public JsonResult Add(Votes votes)
        {
            if (votes.Title == null)
                return Json(new
                {
                    success = false,
                    reason = "标题不能为空或不存在！"
                }, JsonRequestBehavior.AllowGet);

            var userInfo = UserInfo(votes.NO);

            if(userInfo == null)
                return Json(new
                {
                    success = false,
                    reason = "党员编号不存在！"
                }, JsonRequestBehavior.AllowGet);

            votes.PubDate = DateTime.Now;

            if(votes.BeginDate == null || votes.EndDate == null)
                return Json(new
                {
                    success = false,
                    reason = "请输入开始，结束时间！"
                }, JsonRequestBehavior.AllowGet);

            db.T("insert into VotesList(NO, Name, Title, Type, Detail, PubDate, BeginDate, EndDate) values({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", votes.NO, (string)userInfo.Name, votes.Title, votes.Type, votes.Detail, votes.PubDate, votes.BeginDate, votes.EndDate).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit(Votes votes)
        {
            if (votes.Title == null)
                return Json(new
                {
                    success = false,
                    reason = "标题不能为空或不存在！"
                }, JsonRequestBehavior.AllowGet);

            var userInfo = UserInfo(votes.NO);

            if (userInfo == null)
                return Json(new
                {
                    success = false,
                    reason = "党员编号不存在！"
                }, JsonRequestBehavior.AllowGet);

            db.T(@"update VotesList set Type = {0}, Detail = {1}, Title = {3}, BeginDate = {4}, EndDate = {5} where ID = {2} ", votes.Type, votes.Detail, votes.ID, votes.Title, votes.BeginDate, votes.EndDate).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult Delete(string id)
        {

            db.T("delete from VotesList where ID = {0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult Info(string id)
        {
            var data = db.T("select * from VotesList where ID = {0}", id).ExecuteDynamicObject();

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }

        public JsonResult Vote(string userId, int voteId)
        {
            var voteInfo = db.T("select * from VotesStatic where UserID = {0} and VoteID = {1}", userId, voteId).ExecuteFirstRow();

            if (voteInfo != null)
                return Json(new
                {
                    success = false,
                    reason = "你已经投过票了！"
                }, JsonRequestBehavior.AllowGet);
            else
            {
                db.T("insert into VotesStatic(UserID, VoteID) values({0}, {1})", userId, voteId).Execute();

                return Json(new
                {
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}