using System;
using System.Web.Mvc;
using Ivony.Data;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;

namespace SchoolSystem.Controllers
{
    public class UserController : ApiController
    {

        [HttpPost]
        public JsonResult AddUser(UserProfile profile)
        {
            var maxID = db.T("select top 1(ID) from Users order by ID desc").ExecuteScalar();

            var prefix = "";

            if (profile.Pemission == "1")
            {
                prefix = "S";
                profile.Type = "学生";
            }

            if (profile.Pemission == "2")
            {
                prefix = "T";
                profile.Type = "教师";
            }


            if (profile.Pemission == "3")
            {
                prefix = "A";
                profile.Type = "管理员";
            }

            profile.UserName = prefix + "2017" + maxID.ToString();

            var userId = Guid.NewGuid().ToString();

            if (profile.CreateOn == null)
                profile.CreateOn = DateTime.Now.ToString();

            db.T("insert into UserProfile(UserID, NO, Name, Sex, Nation, BorthDate, Display, Origin, Education, Marry, OwnPart, OwnGroup, Job, PoliceNO, SoldierNO, StudentNo, CreateOn, JoinGroupDate, ApplyDate, JoinPartDate, BeRegularDate, Address, Phone, Type, UserName) values({...})", userId, profile.NO, profile.Name, profile.Sex, profile.Nation, profile.BorthDate, profile.Display, profile.Origin, profile.Education, profile.Marry, profile.OwnPart, profile.OwnGroup, profile.Job, profile.PoliceNO, profile.SoldierNO, profile.StudentNo, profile.CreateOn, profile.JoinGroupDate, profile.ApplyDate, profile.JoinPartDate, profile.BeRegularDate, profile.Address, profile.Phone, profile.Type, profile.UserName).Execute();

            AddAccount(profile.UserName, userId, "123456", int.Parse(profile.Pemission));

            return Json(new { success = true, profile }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Modify(UserProfile profile)
        {
            db.T("update UserProfile set Name = {0}, Sex = {1}, Display = {2}, BorthDate = {3}, OwnPart = {4}, Address = {5}, Phone = {6} where UserID = {7}",
                profile.Name, profile.Sex, profile.Display, profile.BorthDate, profile.OwnPart, profile.Address, profile.Phone, profile.UserID).Execute();

            return Json(new { success = true, profile }, JsonRequestBehavior.AllowGet);
        }

        private object AddAccount(string userName, string userId, string password = "123456", int pemission = 3)
        {
            if (IsUserExist(userName))
                return JObject.FromObject(new { Success = false });

            else
            {
                db.T("insert into Users(UserID, UserName, Password, Pemission, CreateOn) values({0}, {1}, {2}, {3}, {4})", userId, userName, password, pemission, DateTime.Now).Execute();

                return JObject.FromObject(new { Success = true });
            }

        }

        [HttpGet]
        public JsonResult Delete(string userName)
        {
            if (!IsUserExist(userName))
                return Json(new { success = false, reason = userName + "用户不存在!" }, JsonRequestBehavior.AllowGet);

            //if (!string.IsNullOrEmpty(userName))
            //    db.T("update Users set Disabled = 1 where UserName = {0}", userName).Execute();

            //if (!string.IsNullOrEmpty(userId))
            //    db.T("update Users set Disabled = 1 where UserId = {0}", userId).Execute();

            if (!string.IsNullOrEmpty(userName))
            {
                db.T("delete from Users where UserName = {0}", userName).Execute();
                db.T("delete from UserProfile where UserName = {0}", userName).Execute();
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SetPemission(string userName, string pemission)
        {
            db.T("update Users set Pemission = {0} where UserName = {1}", pemission, userName).Execute();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ContentResult List(int startIndex = 0, int count = 999, string keywords = "", int type = 0)
        {

            var filter = "and UserName like '%" + keywords + "%'";

            if (type != 0)
                filter += " and Pemission='" + type + "'";

            var data = db.T("select * from Users where Disabled = 0" + filter + " order by CreateOn desc").ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                total = data.Count()
            }));

            return Content(model.ToString(), "application/json");
        }

        [HttpGet]
        public ContentResult InfoList(string type = "", int startIndex = 0, int count = 999, string keywords = "")
        {
            var filter = "where (Name like '%" + keywords + "%' or UserName like '%" + keywords + "%')";

            var data = new DataTable();

            if (!string.IsNullOrEmpty(type))
                data = db.T("select * from UserProfile " + filter + " and Type = {0} order by CreateOn desc", type).ExecuteDataTable();
            else
                data = db.T("select * from UserProfile " + filter + " order by CreateOn desc").ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                total = data.Count()
            }));

            return Content(model.ToString(), "application/json");
        }

        [HttpGet]
        public ContentResult Info(string no = null, string userId = null)
        {
            dynamic data = UserInfo(no, userId);

            return Content(JsonConvert.SerializeObject(data), "application/json");
        }

        [HttpPost]
        public JsonResult ChangePassword(string userId, string oldPassword, string newPassword1, string newPassword2)
        {
            if (string.IsNullOrEmpty(oldPassword))
                return Json(new { success = false, reason = "密码不能为空!" }, JsonRequestBehavior.AllowGet);

            if (newPassword1 != newPassword2)
                return Json(new { success = false, reason = "两次输入的密码不一样!" }, JsonRequestBehavior.AllowGet);

            if (string.IsNullOrEmpty(newPassword1) || string.IsNullOrEmpty(newPassword2))
                return Json(new { success = false, reason = "新密码不能为空!" }, JsonRequestBehavior.AllowGet);

            var userInfo = db.T("select * from Users where UserID = {0} and Password = {1}", userId, oldPassword).ExecuteFirstRow();

            if (userInfo != null)
            {
                db.T("update Users set Password = {0} where UserID = {1}", newPassword1, userId).Execute();

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            else
                return Json(new { success = false, reason = "旧密码不正确!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ChangePasswordAdmin(string userName, string newPassword1, string newPassword2)
        {
            if (newPassword1 != newPassword2)
                return Json(new { success = false, reason = "两次输入的密码不一样!" }, JsonRequestBehavior.AllowGet);

            if (string.IsNullOrEmpty(newPassword1) || string.IsNullOrEmpty(newPassword2))
                return Json(new { success = false, reason = "新密码不能为空!" }, JsonRequestBehavior.AllowGet);

            if (!IsUserExist(userName))
                return Json(new { success = false, reason = "用户名不存在!" }, JsonRequestBehavior.AllowGet);

            //var userInfo = db.T("select * from Users where UserID = {0}", userId).ExecuteDynamicObject();

            //if ((int)userInfo.Pemission != 3)
            //    return Json(new { success = false, reason = "你不是管理员!" }, JsonRequestBehavior.AllowGet);

            db.T("update Users set Password = {0} where UserName = {1}", newPassword1, userName).Execute();

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Login(string userName, string password)
        {
            var userInfo = db.T("select * from Users where userName = {0} and password = {1}", userName, password).ExecuteDynamicObject();

            if (userInfo == null)
                return Json(new { success = false, reason = "用户名或密码不正确!" }, JsonRequestBehavior.AllowGet);
            else
            {

                return Json(new
                {
                    success = true,
                    userName = userInfo.UserName,
                    userId = userInfo.UserID,
                    pemission = userInfo.Pemission
                }, JsonRequestBehavior.AllowGet);
            }

        }

        //[HttpGet]
        //public JsonResult Logout()
        //{
        //    Session.RemoveAll();

        //    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public JsonResult GetLoginInfo(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return Json(new { login = false }, JsonRequestBehavior.AllowGet);

            var userInfo = db.T("select * from Users where UserID = {0}", userId).ExecuteDynamicObject();

            if (userInfo == null)
                return Json(new { login = false }, JsonRequestBehavior.AllowGet);


            else
            {
                var userProfile = db.T("select * from UserProfile where userId = {0}", userId).ExecuteDynamicObject();
                return Json(new
                {
                    login = true,
                    userName = userProfile.Name,
                    pemission = userInfo.Pemission
                }, JsonRequestBehavior.AllowGet);
            }

        }


    }



}