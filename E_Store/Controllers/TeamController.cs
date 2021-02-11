using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Store.Models;
namespace E_Store.Controllers
{
    public class TeamController : Controller
    {
        // GET: Team
        public ActionResult Index()
        {
            return View();
        }


        public string AddMember(string name, string job_t, string job_d, HttpPostedFileBase image, string facebook, string twitter, string email, string linkedin, string skype)
        {
            if (name != "" && job_t != "" && job_d != "" && image != null) {

                BlogEntities1 blogEntities = new BlogEntities1();
                var checkMember = blogEntities.Tbl_Team.Where(rowHere => rowHere.Email == email || rowHere.Facebook_Url == facebook || rowHere.Twitter_Url == twitter || rowHere.LinkedIn_Url == linkedin || rowHere.Skype_Url == skype).FirstOrDefault();
                if (checkMember != null) {
                    return "present";
                }
                else  {
                    var relativePath = Guid.NewGuid() + "-" + image.FileName;
                    var path = Server.MapPath("~/Photo/" + relativePath);
                    image.SaveAs(path);
                    Tbl_Team _Team = new Tbl_Team();
                    _Team.Name = name;
                    _Team.Job_Title = job_t;
                    _Team.Job_Description = job_d;
                    _Team.Image = relativePath;
                    _Team.Facebook_Url = facebook;
                    _Team.Twitter_Url = twitter;
                    _Team.Email = email;
                    _Team.LinkedIn_Url = linkedin;
                    _Team.Skype_Url = skype;

                    blogEntities.Tbl_Team.Add(_Team);
                    blogEntities.SaveChanges();
                    return "Added";
                }
            } else
            {
                return "empty";
            }
        }

        public JsonResult fetchData()
        {
            BlogEntities1 blogEntities1 = new BlogEntities1();
            var list = blogEntities1.Tbl_Team.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        public string DeleteMember(int id)
        {
            Tbl_Team tbl_Services = new Tbl_Team { id = id };
            BlogEntities1 blogEntities = new BlogEntities1();
            blogEntities.Entry(tbl_Services).State = EntityState.Deleted;
            blogEntities.SaveChanges();
            return "deleted";

        }



    }
}