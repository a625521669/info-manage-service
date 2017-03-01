using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ivony.Data;
using Ivony.Data.SqlClient;
using Newtonsoft.Json;
using SchoolSystem.Models;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IO;

namespace SchoolSystem.Controllers
{
    public class FilesController : ApiController
    {
        [HttpPost]
        public JsonResult Add(HttpPostedFileBase files, Files uploadFile)
        {
            try
            {
                var filename = files.FileName;

                if (filename.LastIndexOf(@"\", StringComparison.Ordinal) > -1)
                    filename = filename.Substring(filename.LastIndexOf(@"\", StringComparison.Ordinal) + 1);

                var fileType = filename.Substring(filename.LastIndexOf(".", StringComparison.Ordinal) + 1);

                var filePath = Server.MapPath("~/uploads/files/");

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var name = DateTime.Now.ToString("ddHHmmssfff") + "." + filename;

                files.SaveAs(filePath + name);

                uploadFile.Url = "/uploads/files/" + name;

                uploadFile.PubDate = DateTime.Now;

                db.T("insert into Files(Type, Name, Signature, PageCount, HasAttacher, Url, PubDate) values({0},{1},{2},{3},{4},{5},{6})", uploadFile.Type, uploadFile.Name, uploadFile.Signature, uploadFile.PageCount, uploadFile.HasAttacher, uploadFile.Url, uploadFile.PubDate).Execute();
            }
            catch (Exception e)
            {
                return Json(new { success = false, reason = e.ToString() }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { success = true, uploadFile }, JsonRequestBehavior.AllowGet);
        }

        public ContentResult List(string type = null, int startIndex = 0, int count = 999)
        {
            var data = new DataTable();

            if (string.IsNullOrWhiteSpace(type))
                data = db.T("select * from Files order by PubDate desc").ExecuteDataTable();
            else
                data = db.T("select * from Files where Type = {0} order by PubDate desc", type).ExecuteDataTable();

            var model = new JObject(JObject.FromObject(new
            {
                list = data.Chunk(startIndex, count),
                count = data.Count()
            }));

            return Content(JsonConvert.SerializeObject(model), "application/json");
        }

        public JsonResult Delete(string id)
        {

            db.T("delete from Files where ID = {0}", id).Execute();

            return Json(new
            {
                success = true
            }, JsonRequestBehavior.AllowGet);
        }

    }
}