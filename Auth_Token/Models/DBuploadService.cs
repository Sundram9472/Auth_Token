using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Auth_Token.Controllers;
using Auth_Token.Models;


namespace Auth_Token.Models
{
    public class DBuploadService
    {
        public static async Task<bool> uploadFile(String FileType, String FileName, byte[] fileRcrd)
        {
            Datafile datafile = new Datafile();
            datafile.Filerecord = fileRcrd;
            datafile.Filetype = FileType;
            datafile.Name = FileName;
            using (Student_DBEntities _DB = new Student_DBEntities())
            {

                _DB.Datafiles.Add(datafile);
                _DB.SaveChanges();
                return true;

            }
        }

        //    public static async UploadFileBinary()
        //    {

        //    }
        
    }
}