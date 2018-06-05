using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Week6_Capstone.Models;
using System.Web.Mvc;


namespace Week6_Capstone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult RegisterNewUser(User newUser)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            ORM.Users.Add(newUser);
            ORM.SaveChanges();
            return View("Index");

        }

        public ActionResult SignIn(User user)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();

            List<User> users = ORM.Users.ToList();
            if (users.Where(x => x.UserName == user.UserName).ToList().Count() == 0)
            {
                ViewBag.Error = "Username does not exist. Did you want to register?";
                return View("Error");
            }

            User thisUser = ORM.Users.Find(user.UserName);

            if (thisUser.Password != user.Password)
            {
                ViewBag.Error = "Incorrect Password";
                return View("Error");
            }

            ViewBag.Message = $"Welcome {user.UserName}!";
            return RedirectToAction("Welcome");
        }

        public ActionResult Welcome()
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            ViewBag.TaskList = ORM.Tasks.ToList();
            return View();
        }

        public ActionResult Task()
        {
            return View();

        }

        public ActionResult NewTask(Task newTask)
        {

            if (ModelState.IsValid)
            {
                TaskListDBEntities ORM = new TaskListDBEntities();
                if (ORM.Tasks.ToList().Count == 0)
                {
                    newTask.TaskNumber = "1";
                }
                else
                {
                    newTask.TaskNumber = (int.Parse(ORM.Tasks.ToList().Last().TaskNumber) + 1).ToString();
                }
                ORM.Tasks.Add(newTask);
                ORM.SaveChanges();
                return RedirectToAction("Welcome");
            }
            else
            {
                return View("Error");
            }
        }
        
        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult DeleteTask(string TaskNumber)
        {
            TaskListDBEntities ORM = new TaskListDBEntities();
            Task Found = ORM.Tasks.Find(TaskNumber);


            if (Found != null)
            {
                ORM.Tasks.Remove(Found);
                ORM.SaveChanges();

                return RedirectToAction("Welcome");
            }

            else
            {
                return View("Error");
            }
        }


}

}