using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auth_Token.Controllers
{
    public class FileUploadDBHttpPostedFileBaseController : Controller
    {

        Datafile DB = new Datafile();
        public bool Infile(HttpPostedFileBase imgfile)
        {
            return (imgfile != null && imgfile.ContentLength > 0) ? true : false;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult fileUploadUi()
        {
            return View();
        }

        [HandleError]
        public ActionResult FileUpload(HttpPostedFileBase fileBase)
        {
            try
            {

                if (fileBase != null)
                {
                    byte[] Fileinbyte;
                    using (BinaryReader br = new BinaryReader(fileBase.InputStream))
                    {
                        Fileinbyte = br.ReadBytes(fileBase.ContentLength);
                    }

                    string fileType = fileBase.ContentType;
                    byte[] data = Fileinbyte;
                    string fileName = Path.GetFileName(fileBase.FileName);
                    int fileSize = fileBase.ContentLength;


                    DB.Filerecord = data;
                    DB.Filetype = fileType;
                    DB.Name = fileName;

                    using (Student_DBEntities _DB = new Student_DBEntities())
                    {
                        _DB.Datafiles.Add(DB);
                        _DB.SaveChanges();
                    }
                    TempData["Message"] = "File Uploaded Succesfully";
                    return RedirectToAction("Index");
                       
                }
                else
                {
                    TempData["Message"] = "File Not Uploaded";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["Message"] = "File Not Uploaded";
                return RedirectToAction("Index");
            }

        }

        public ActionResult DownloadFile()
        {

            List<string> fileNameList = new List<string>();
            List<String> fileIdList = new List<String>();
            using (Student_DBEntities _DB = new Student_DBEntities())
            {
                IEnumerable<Datafile> datafilesList = _DB.Datafiles.ToList();
                foreach (var file in datafilesList)
                {
                    fileNameList.Add(file.Name.ToString());
                    fileIdList.Add(file.ID.ToString());
                }
            }
            ViewBag.ImagesName = fileNameList;
            ViewBag.ImagesID = fileIdList;
            return View();

        }

        public FileContentResult DownloadIndingFile(int id)
        {

            byte[] fileContent = null;
            string fileType = "";
            string fileName = "";


            using (Student_DBEntities _DB = new Student_DBEntities())
            {
                var file = _DB.Datafiles.Where(x => x.ID == id).FirstOrDefault();
                if (file != null)
                {

                    fileContent = (byte[])file.Filerecord;
                    fileType = file.Filetype.ToString();
                    fileName = file.Name.ToString();
                }
            }
            return File(fileContent, fileType, fileName);
        }
    }
}