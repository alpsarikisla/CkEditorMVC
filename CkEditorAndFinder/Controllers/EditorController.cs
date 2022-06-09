using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CkEditorAndFinder.Controllers
{
    public class EditorController : Controller
    {
        // GET: Editor
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string url;
            string message;
            string path = Server.MapPath("~/Images");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            FileInfo fi = new FileInfo(upload.FileName);

            string ImageName = Guid.NewGuid().ToString() + fi.Extension; /*upload.FileName;*/
            upload.SaveAs(System.IO.Path.Combine(path, ImageName));

            url = Request.Url.GetLeftPart(UriPartial.Authority) + "/Images/" + ImageName;

            // passing message success/failure
            message = "Resim Başarıyla Yüklendi";

            // since it is an ajax request it requires this string
            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\", \"" + message + "\");</script></body></html>";
            return Content(output);
        }
    }
}