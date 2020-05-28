using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication9.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ResultMessage = TempData["ResultMessage"];
            try
            {
                string loginName = Session["LoginName"].ToString();
                ViewBag.Message = "當前登入使用者：" + loginName;
            }
            catch
            {
                TempData["ResultMessage"] = "請先登入";
                return RedirectToAction("Index", "Login");
            }
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products select s).ToList();
                return View(result);
            }
        }
        public ActionResult Details(int id)
        {
            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                var result = (from s in db.Products
                              where s.Id == id
                              select s).FirstOrDefault();

                if (result == default(Models.Product))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(result);
                }
            }
        }

        [HttpPost]  //限定使用POST
        public ActionResult AddComment(int id, string Content)
        {
            //取得目前登入使用者Id
            var userId = String.Format("{0}", Session["LoginName"]);

            var currentDateTime = DateTime.Now;

            var comment = new Models.ProductComment()
            {
                ProductId = id.ToString(),
                Content = Content,
                UserId = userId,
                CreateDate = currentDateTime.ToString()
            };

            using (Models.CartsEntities db = new Models.CartsEntities())
            {
                db.ProductComments.Add(comment);
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = id });
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
    }
}