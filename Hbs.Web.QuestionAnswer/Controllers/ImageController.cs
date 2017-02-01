using Hbs.Web.QuestionAnswer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hbs.Web.QuestionAnswer.Controllers
{
    public class ImageController : Controller
    {
        public void UploadNow(HttpPostedFileWrapper upload)
        {
            if (upload != null)
            {
                string ImageName = upload.FileName;
                string path = System.IO.Path.Combine(Server.MapPath("~/Images"), ImageName);
                upload.SaveAs(path);
            }
        }

        public ActionResult UploadPartial()
        {
            var appData = Server.MapPath("~/Images");
            var images = Directory.GetFiles(appData).Select(x => new ImagesViewModel
            {
                Url = Url.Content("/images/" + Path.GetFileName(x))
            });
            return View(images);
        }
    }
}