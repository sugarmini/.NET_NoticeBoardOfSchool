using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyggl.Models;

namespace xyggl.Controllers
{
    public class ManageController : Controller
    {
        DBEntities db = new DBEntities();
        // GET: Manage
        public ActionResult Index()
        {
            var res = from u in db.Fora
                      orderby u.time descending
                      select u;
            HttpCookie getcook = Request.Cookies["newcookie"];
            int id = int.Parse(getcook.Values["id"]);
            ViewBag.name = db.Users.Find(id).name;
            ViewBag.img = db.Users.Find(id).image;
            return View(res.ToList());
        }

        [ValidateInput(false)]
        public ActionResult Add(Forum forum)
        {
            HttpCookie getcook = Request.Cookies["newcookie"];
            forum.time = DateTime.Now;
            forum.user_id = int.Parse(getcook.Values["id"]);
            db.Fora.Add(forum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ManageForum(int? id)
        {
            Forum forum = db.Fora.Find(id);
            return View(forum);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageForum(Forum forum)
        {
            Forum f = db.Fora.Find(forum.forum_id);
            f.title = forum.title;
            f.content = forum.content;
            db.SaveChanges();
            return RedirectToAction("Detail/"+forum.forum_id+"/1","Board");
        }

        public ActionResult Delete(int? id)
        {
            Forum forum = db.Fora.Find(id);
            if(forum != null)
            {
                var res = from c in db.Comments
                          where c.forum_id == id
                          select c;
                foreach(var item in res)
                {
                    db.Comments.Remove(item);
                }
                db.Fora.Remove(forum);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CommentManage(int? id)
        {
            var res = from c in db.Comments
                      where c.forum_id == id
                      orderby c.time descending
                      select c;
            return View(res.ToList());
        }

        public ActionResult DelCom(int? id)
        {
            Comment comment = db.Comments.Find(id);
            int forum_id = comment.forum_id;
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
            }
            return RedirectToAction("CommentManage/"+forum_id) ;
        }

    }
}