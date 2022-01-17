using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.IO;
using Auth_Token.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace Auth_Token.Controllers
{
    public class FileUploadInDBApiController : ApiController
    {
        
        [HttpGet]
        [Route("api/FileUploadInDBApi/DownloadFile")]
        public HttpResponseMessage Get(int id)
        {

           byte[] fileContent = null;
            string fileType = "";
            string file_Name = "";
            using (Student_DBEntities _DB = new Student_DBEntities())
            {
                var file = _DB.Datafiles.Where(x => x.ID == id).FirstOrDefault();
                fileContent =(byte[])file.Filerecord;
                fileType = file.Filetype;
                file_Name = file.Name;
                
                if (file != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,"FileContent="+fileContent+"                FileType="+ fileType+"              FileName="+ file_Name);
                    
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "File Not Found From This:-"+id);
                }
            }
            
        }


       [HttpPost]
       [Route("api/FileUploadInDBApi/UploadDFile")]
        public async Task<HttpResponseMessage> Post()
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
                    String FileName = httpRequest.Files[file].FileName;
                    byte[] fileRcrd = new byte[FileSize];
                    var finalExtension = extension.Split('/').Last().ToUpper();
                    Stream file_Strm = httpRequest.Files[file].InputStream;
                    file_Strm.Read(fileRcrd, 0, FileSize);
                    String fileNameAdd = Guid.NewGuid().ToString();

                    String FileNameA = postedFile.FileName[0] + fileNameAdd;
                    if ((finalExtension == "PNG" || finalExtension == "JPG" || finalExtension == "JPEG") && (FileSize <= 500000))
                    {
                        Result = await DBuploadService.uploadFile(FileType, FileName, fileRcrd);
                    }

                    if ((finalExtension == "DOCX" || finalExtension == "HTML" || finalExtension == "PDF" || finalExtension == "XLS" || finalExtension == "PPTX" || finalExtension == "PPTDOCUMENT" || finalExtension == "DOCUMENT") && (FileSize <= 2000000))
                    {
                        Result = await DBuploadService.uploadFile(FileType, FileName, fileRcrd);
                    }

                    if ((finalExtension == "MKV" || finalExtension == "MP4") && (FileSize <= 5000000))
                    {
                        Result = await DBuploadService.uploadFile(FileType, FileName, fileRcrd);
                    }
                }
                if (Result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "File Uploaded");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "FileType Or FileSize Not Correct ");
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please Select 1 Minium File");
            }
        }
    }
}
