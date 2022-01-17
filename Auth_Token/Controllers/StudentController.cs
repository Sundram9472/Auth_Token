using Auth_Token.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Auth_Token.Controllers
{
    public class StudentController : ApiController
    {
        [Authorize]
        public IEnumerable<Student_Details_Sundram> Get()
        {
            using(Student_DBEntities _DB = new Student_DBEntities())
            {
                return _DB.Student_Details_Sundram.ToList();
            }
        }
    }
}
