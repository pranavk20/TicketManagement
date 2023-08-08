//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TicketManagementSystem.Controllers
//{
//    public class UserController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketManagementSystem.Models;


namespace TicketManagementSystem.Controllers
{
    public class UserController : Controller
    {
        //TicketManagementSystemContext db = new TicketManagementSystemContext();

        private readonly TicketManagementSystemContext _db;

        public UserController(TicketManagementSystemContext db)
        {
            _db = db;
        }

        //GET: Register
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User u)
        {
            {
                if (ModelState.IsValid)
                {
                    // Check if the username is availa

                    // Generate salt and password hash
                    string salt = PasswordHelper.GenerateSalt();
                    string passwordHash = PasswordHelper.GeneratePasswordHash(u.Password, salt);

                    // Save user to the database
                    var user = new User
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        Password = passwordHash,
                        ConfirmPassword = passwordHash,
                        FirstName = u.FirstName,                   
                        LastName = u.LastName,
                        Email = u.Email,
                        CompanyName = u.CompanyName,
                        PhoneNumber = u.PhoneNumber,
                        Salt = salt
                    };

                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(u);
            }
        }



        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login lg)
        {
            if (ModelState.IsValid == true)
            {
                var record = _db.Users.SingleOrDefault(u => u.Email == lg.email && u.Password == lg.password);

                if (record != null && lg.email != "admin@gmail.com")
                {
                    string salt = PasswordHelper.GenerateSalt();
                    string passwordHash = PasswordHelper.GeneratePasswordHash(lg.password, salt);

                    if (passwordHash == lg.password)
                    {
                        //string Email = HttpContext.Session.GetString("email");
                        //string Name = HttpContext.Session.GetString("FirstName");
                        //ViewData["email"] = lg.email;
                        //ViewData["FirstName"] = record.FirstName;
                        // Passwords match, the user is authenticated
                        // You can store user information in session or cookie for further authentication
                        return RedirectToAction("Index", "Ticket");
                    }
                    //ViewBag.ErrorMessage = "Error: captcha is not valid.";
                    ModelState.AddModelError("InvalidCredentials", "Invalid username or password.");

                    return RedirectToAction("Index", "Task");

                }
                else if (record != null && lg.email == "admin@gmail.com")
                {
                    //string Email = HttpContext.Session.GetString("email");
                    //string Name = HttpContext.Session.GetString("FirstName");
                    //ViewData["Email"] = lg.email;
                    //ViewData["Name"] = record.FirstName;
                    return RedirectToAction("List","User");
                }


                else
                {

                    ViewBag.Message = "Incorrect Credentials";
                }
            }
            return View();


        }

        public ActionResult List()
        {

            //return View();
            string Email = HttpContext.Session.GetString("email");
            string Name = HttpContext.Session.GetString("FirstName");
            if (ViewData["email"] != null && ViewData["FirstName"].Equals("Admin"))
            {
                var obj = _db.Users.Where(u => u.UserId != 4).ToList();
                return View(obj);
            }
            else
            {
                ViewBag.Message = "Please Login First...";
                return View("Index");
            }
        }

        //    public ActionResult Details(int? id)
        //    {
        //        if (id == 4)
        //        {
        //            return Content("You are not authorized to make changes in this entry");
        //        }

        //        if (Session["Email"] != null && Session["Name"].Equals("Admin"))
        //        {
        //            var obj = db.Registers.Find(id);
        //            return View(obj);
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Please Login First...";
        //            return View("Index");
        //        }

        //    }

        //    public ActionResult Edit(int? id)
        //    {
        //        if (id == 4)
        //        {
        //            return Content("You are not authorized to make changes in this entry");
        //        }
        //        if (Session["Email"] != null && Session["Name"].Equals("Admin"))
        //        {
        //            var obj = db.Registers.Find(id);
        //            return View(obj);
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Please Login First...";
        //            return View("Index");
        //        }


        //    }
        //    [HttpPost]
        //    public ActionResult Edit(Register u)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(u).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("List");
        //        }
        //        return View(u);
        //    }

        //    public ActionResult Delete(int id)
        //    {
        //        if (id == 4)
        //        {
        //            return Content("You are not authorized to make changes in this entry");

        //        }

        //        if (Session["Email"] != null && Session["Name"].Equals("Admin"))
        //        {
        //            var obj = db.Registers.Find(id);
        //            return View(obj);
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Please Login First...";
        //            return View("Index");
        //        }

        //    }

        //    [HttpPost, ActionName("Delete")]
        //    public ActionResult DeleteConfirmed(int id)
        //    {
        //        Register record = db.Registers.Find(id);
        //        if (record != null)
        //        {
        //            db.Registers.Remove(record);
        //            db.SaveChanges();
        //        }
        //        return RedirectToAction("List");
        //    }
        //    public ActionResult Logout()
        //    {
        //        Session.Abandon();
        //        TempData["Message"] = "Logout Successfully";
        //        return RedirectToAction("Index");
        //    }
        //}
    }
}