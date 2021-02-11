using E_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace E_Store.Controllers
{
    public class RegisterController : Controller
    {
        //db object here
        BlogEntities1 blogEntities = new BlogEntities1();
        HashCode hashCode = new HashCode();


        // GET: Register
        public ActionResult Index()
        {
            return View();
        }


        // landing method
        public string Create_New_User(string username, string email, string user_role, string password, string CPassword) {

            //Password hashing
            string Str_Pwd = hashCode.PassHash(password);

            //Empty values checking
            if (username.Equals("") || email.Equals("") || user_role.Equals("") || password.Equals("") || CPassword.Equals("") || password != CPassword) {
                return "Any Feilds Are Empty or password does not match";
            }
            
            var checkUser = blogEntities.Tbl_Users.Where(rowHere => rowHere.Email == email && rowHere.Pwd == Str_Pwd);
            if(checkUser == null) {
                
                //Add new user code here
                //Users table object creation here
                Tbl_Users Obj_tbl_Users = new Tbl_Users();

                //Fetch user role id from here
                long User_Role_Id_ = 0;
                foreach (var usertype in blogEntities.Tbl_Roles.ToList()) {
                    if ((usertype.Role_Name.Equals(user_role))) {
                        User_Role_Id_ = usertype.User_Role_Id;
                        break;
                    }
                }

                //Table row of data
                Obj_tbl_Users.Username = username;  //string
                Obj_tbl_Users.Email = email;    //string
                Obj_tbl_Users.User_Role_Id = User_Role_Id_; //long
                Obj_tbl_Users.Status_Code = "1"; //string
                Obj_tbl_Users.Pwd = Str_Pwd; //string
                Obj_tbl_Users.CPwd = Str_Pwd; //string


                blogEntities.Tbl_Users.Add(Obj_tbl_Users);
                blogEntities.SaveChanges();

                return "Registered successfully";
            }            
            return "User Already Present";
        }
    }
}
