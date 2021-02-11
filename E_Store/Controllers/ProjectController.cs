using E_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Store.Controllers
{
    public class ProjectController : Controller
    {
        static BlogEntities1 blogEntities = new BlogEntities1();
        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        #region Category Related Code
        public ActionResult Category()
        {
            return View();
        }

        public JsonResult FetchCategory()
        {
            
            var list = blogEntities.Tbl_ProjectCategory.OrderBy(pc => pc.Name).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public string AddCategory(string name) {
            if (name != "")
            {
                var exist = blogEntities.Tbl_ProjectCategory.Where(pc => pc.Name.ToLower() == name.ToLower()).FirstOrDefault();
                if (exist != null)
                {
                    return "exist";
                }
                else
                {
                    Tbl_ProjectCategory category = new Tbl_ProjectCategory()
                    {
                        Name = name
                    };
                    blogEntities.Tbl_ProjectCategory.Add(category);
                    blogEntities.SaveChanges();
                    return "created";
                }
            }
            return "fail";  
        }

        public string UpdateCategory(int Id,string name)
        {
            if (Id > 0)
            {
                var exist = blogEntities.Tbl_ProjectCategory.Find(Id);
                if (exist != null)
                {
                    if (name != null)
                    {
                        exist.Name = name;
                        blogEntities.Entry(exist).State = EntityState.Modified;
                        blogEntities.SaveChanges();
                        return "updated";
                    }                   
                }
                else
                {
                    return "not exist";
                }
            }
            return "fail";
        }

        public string Delete_Catagory(int id) {
              Tbl_ProjectCategory tbl_ProjectCategory = new Tbl_ProjectCategory { Id = id };
              blogEntities.Entry(tbl_ProjectCategory).State = EntityState.Deleted;
              blogEntities.SaveChanges();
              return "deleted";
         }

        #endregion


        #region Project

        public string AddProject(string name, string desc,int cId,HttpPostedFileBase file)
        {
            if (name != null && desc != null && cId > 0 && file != null)
            {
                var relativePath = Guid.NewGuid() + "-" + file.FileName;
                var path = Server.MapPath("~/Photo/" + relativePath);
                file.SaveAs(path);
                Tbl_Projects project = new Tbl_Projects
                {
                    CategoryId = cId,
                    Description = desc,
                    P_Name = name,
                    Image = relativePath
                };
                blogEntities.Tbl_Projects.Add(project);
                blogEntities.SaveChanges();
                return "created";
            }
            return "fail";
        }

        public JsonResult FetchProject()
        {
            //var Projectslist = blogEntities.Tbl_Projects.OrderBy(P => P.Id).ToList();  
            // seelect * from Projects as P left join Category on P.CId = Category.ID 
            var Projectslist = FetchProjectWithCategory();
            return Json(Projectslist, JsonRequestBehavior.AllowGet);
        }

        public static List<ProjectWithCategory> FetchProjectWithCategory()
        {
            var Projectslist = from P in blogEntities.Tbl_Projects
                               join PC in blogEntities.Tbl_ProjectCategory
                               on P.CategoryId equals PC.Id into lj
                               from PC1 in lj.DefaultIfEmpty()
                               select new ProjectWithCategory
                               {
                                   Id = P.Id,
                                   P_Name = P.P_Name,
                                   P_Desc = P.Description,
                                   CategoryId = PC1.Id != null ? PC1.Id : 0,
                                   CategoryName = PC1.Name != null ? PC1.Name : "Not Found",
                                   Image = P.Image
                               };

            return Projectslist.ToList();
        }

        public string DeleteProject(int id)
        {
            Tbl_Projects tbl_Projects = new Tbl_Projects { Id = id };
            BlogEntities1 blog = new BlogEntities1();
            blog.Entry(tbl_Projects).State = EntityState.Deleted;
            blog.SaveChanges();
            return "deleted";
        }
        #endregion
    }
}