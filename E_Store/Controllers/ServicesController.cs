using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Store.Models;

namespace E_Store.Controllers
{
    public class ServicesController : Controller
    {
        // GET: Services
        public ActionResult Index()
        {
            return View();
        }

        #region Region_Services
        public string AddService(string Title, string Description)
        {

            BlogEntities1 blogEntities = new BlogEntities1();
            var foo = blogEntities.Tbl_Services.Where(v => v.Service_Name == Title && v.Service_Description == Description).FirstOrDefault();
            
            if (foo != null)
            {
                return "Already exists";
            }
            else
            {
                Tbl_Services tbl_Services = new Tbl_Services();
                tbl_Services.Service_Name = Title;
                tbl_Services.Service_Description = Description;
                blogEntities.Tbl_Services.Add(tbl_Services);
                blogEntities.SaveChanges();
                return "Inserted";
            }
        }
        public JsonResult fetchData() {
            BlogEntities1 blogEntities1 = new BlogEntities1();
            var list = blogEntities1.Tbl_Services.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public string DeleteService(int id)
        {
            Tbl_Services tbl_Services = new Tbl_Services { Service_Id = id };
            BlogEntities1 blogEntities = new BlogEntities1();
            blogEntities.Entry(tbl_Services).State = EntityState.Deleted;
            blogEntities.SaveChanges();
            return "deleted";
        }
        public string UpdateService(int id, string name, string description) {
            BlogEntities1 blogEntities = new BlogEntities1();   
            var service = blogEntities.Tbl_Services.Find(id); 
            if(service != null)
            {
                service.Service_Name = name;
                service.Service_Description = description ;
            }
            blogEntities.Entry(service).State = EntityState.Modified;
            blogEntities.SaveChanges();
            return "editied";
        }

        #endregion
    }
}