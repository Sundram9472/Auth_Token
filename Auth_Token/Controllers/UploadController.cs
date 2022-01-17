using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Net;
using System.Web.Http;
using System.Threading.Tasks;
using Auth_Token.Models;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace Auth_Token.Controllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> UploadFile()
        {
            bool Result = false;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    String FileType = httpRequest.Files[file].ContentType;
                    int FileSize = httpRequest.Files[file].ContentLength;
                    var extension = FileType.Split('.').Last();
                    var finalExtension = extension.Split('/').Last().ToUpper();
                   
                    String fileNameAdd = Guid.NewGuid().ToString();
                   
                    String FileNameA = postedFile.FileName[0] + fileNameAdd;
                    if ((finalExtension=="PNG" || finalExtension == "JPG" || finalExtension == "JPEG")&& (FileSize <= 500000))
                    {
                        Result=true;
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadData/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }

                    if((finalExtension== "DOCX"|| finalExtension == "HTML" || finalExtension == "PDF" || finalExtension == "XLS" || finalExtension == "PPTX" || finalExtension == "PPT")&&(FileSize<= 2000000))
                    {
                        Result = true;
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadData/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }

                    if ((finalExtension == "MKV" || finalExtension == "MP4") && (FileSize <= 5000000))
                    {
                        Result = true;
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadData/" + postedFile.FileName);
                        postedFile.SaveAs(filePath);
                        docfiles.Add(filePath);
                    }
                }
               if(Result)
               {
                    return Request.CreateResponse(HttpStatusCode.OK, "File Uploaded");
               }
               else
               {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,"FileType Or FileSize Not Correct ");
               }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }
  

    }
}