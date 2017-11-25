using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyggl.Models;

namespace xyggl.Controllers
{
    public class BoardController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Board
        public ActionResult Index()
        {
            List<Forum> list = db.Fora.ToList();
            HttpCookie getcook = Request.Cookies["newcookie"];
            int id = int.Parse(getcook.Values["id"]);
            ViewBag.name = db.Users.Find(id).name;
            ViewBag.img = db.Users.Find(id).image;
            return View(list);
        }

        public ActionResult Detail(int? id,int? pos)
        {
            Forum forum = db.Fora.Find(id);
            Comment(id);
            if(pos == 1)
                ViewBag.pos = "Manage";
            else
                ViewBag.pos = "Board";
            return View(forum);
        }

        public PartialViewResult Comment(int? id)
        {            
            var res = from co in db.Comments
                              where co.forum_id == id && co.comment_id == null
                              select co;
            
            return PartialView("Comment",res.ToList());
        }

        public ActionResult UserName(int? id)
        {
            User res = db.Users.Find(id);
            return PartialView("UserName",res.name);
        }

        public ActionResult Img(int? id)
        {
            User res = db.Users.Find(id);
            return PartialView("Img", res.image);
        }

        public ActionResult ReComment(int? id)
        {
           var res =  from co in db.Comments
                      where co.comment_id == id && co.comment_id != null
                      select co;
            return PartialView(res.ToList());
        }

        [ValidateInput(false)]
        public ActionResult Add(Comment comment)
        {
            HttpCookie getcook = Request.Cookies["newcookie"];
            comment.forum_id = int.Parse(Request.Form["id"]);
            comment.user_id = int.Parse(getcook.Values["id"]);
            comment.time = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Detail/"+ int.Parse(Request.Form["id"]));
        }

        [ValidateInput(false)]
        public ActionResult AddToCom(Comment comment) {
            HttpCookie getcook = Request.Cookies["newcookie"];
            comment.user_id = int.Parse(getcook.Values["id"]);
            comment.time = DateTime.Now;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("Detail/" + comment.forum_id);
        }
    }
}