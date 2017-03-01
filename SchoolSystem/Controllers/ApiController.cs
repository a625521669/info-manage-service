using System.Web.Mvc;
using Ivony.Data;
using Ivony.Data.SqlClient;

namespace SchoolSystem.Controllers
{
    public class ApiController : Controller
    {
        protected static SqlDbExecutor db = SqlServer.FromConfiguration("connection");

        protected static bool IsUserExist(string userName)
        {
            var userInfo = db.T("select * from Users where userName = {0}", userName).ExecuteFirstRow();

            return userInfo != null;
        }

        protected static dynamic UserInfo(string no = null, string userId = null)
        {
            dynamic data = null;

            if (!string.IsNullOrEmpty(no))
                data = db.T("select * from UserProfile where NO = {0}", no).ExecuteDynamicObject();
            else
                data = db.T("select * from UserProfile where UserId = {0}", userId).ExecuteDynamicObject();

            return data;
        }
    }
}