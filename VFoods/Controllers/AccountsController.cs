using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VFoods.Models.MVC_Models;
using System.Web.Security;

namespace VFoods.Controllers
{
    
    public class AccountsController : Controller
    {
        // GET: Accounts
        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(my_login obj)
        {
            try
            {
                // TODO: Add insert logic here
                using (var context = new AquaDBEntities1())
                {
                    bool isValid = context.tbl_Login.Any(x=>x.Cred_Username==obj.Cred_Username && x.Cred_Password==obj.Cred_Password);
                    if (isValid)
                    {
                        FormsAuthentication.SetAuthCookie(obj.Cred_Username, false);
                        return RedirectToAction("Index","Home");
                    }

                    else
                    {
                        ModelState.AddModelError("", "Invalid Username and Password");
                        return View();
                    }

                }

                    
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }




    }
}
