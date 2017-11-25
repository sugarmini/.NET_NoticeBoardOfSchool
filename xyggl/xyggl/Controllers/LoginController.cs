using System;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using xyggl.Models;

namespace xyggl.Controllers
{
    public class LoginController : Controller
    {
        DBEntities db = new DBEntities();
        HttpCookie nc = new HttpCookie("newcookie");

        // GET: Login
        public ActionResult Index()
        {
            HttpCookie getcook = Request.Cookies["newcookie"];
            if(getcook != null)
            {
                if (getcook.Values["remain"] != null)
                ViewBag.pwd = getcook.Values["pwd"];
                ViewBag.email = getcook.Values["email"];
                ViewBag.remain = getcook.Values["remain"];
            }
            
            return View();
        }

        public ActionResult Register()
        {
            var email = Request.Form["email"];
            var pwd = Request.Form["rpassword"];
            var res = from u in db.Users
                      where u.email == email
                      select u;
            if (res.ToList().FirstOrDefault<User>() != null){
                Response.Write("<script>alert('此账号已注册过，请直接登录');</script>");
                nc.Values["email"] = email;
                ViewBag.url = Gotomail(email);
                return View("Index");
            }
            else{
                User user = new User() { email = email,password = pwd};
                db.Users.Add(user);                
                if ( db.SaveChanges() == 1)
                    SendEmail(email, "验证邮件", "http://localhost:12112/Login/Active/" + user.Id);
                string url = Gotomail(email);
                ViewBag.url = url;
                return View("Active");
            }
        }

        public ActionResult Active(int id)
        {

            var res = from u in db.Users
                      where u.Id == id
                      select u;
            User user = res.ToList().FirstOrDefault<User>();
            if (user != null)
            {
                user.statu = 1;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public static bool SendEmail(string mailTo, string mailSubject, string mailContent)
        {
            // 设置发送方的邮件信息,例如使用网易的smtp
            string smtpServer = "smtp.126.com"; //SMTP服务器
            string mailFrom = "jxffight@126.com"; //登陆用户名
            string userPassword = "jinxiufen1997";//登陆密码

            // 邮件服务设置
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpServer; //指定SMTP服务器
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, userPassword);//用户名和密码

            // 发送邮件设置        
            MailMessage mailMessage = new MailMessage(mailFrom, mailTo); // 发送人和收件人
            mailMessage.Subject = mailSubject;//主题
            mailMessage.Body = mailContent;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Low;//优先级

            try
            {
                smtpClient.Send(mailMessage); // 发送邮件
                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
        }

        public string Gotomail(string mail)
        {
            string[] t = mail.Split('@');
            if (t[1] == "163.com"){
                return "https://mail.163.com";
            }else if (t[1] == "vip.163.com"){
                return "https://vip.163.com";
            }else if (t[1] == "126.com"){
                return "https://mail.126.com";
            }else if (t[1] == "qq.com" ||t[1] == "vip.qq.com" ||t[1] == "foxmail.com"){
                return "https://mail.qq.com";
            }else if (t[1] == "gmail.com"){
                return "https://mail.google.com";
            }else if (t[1] == "sohu.com"){
                return "https://mail.sohu.com";
            }else if (t[1] == "tom.com"){
                return "https://mail.tom.com";
            }else if (t[1] == "vip.sina.com"){
                return "https://vip.sina.com";
            }else if (t[1] == "sina.com.cn" ||t[1] == "sina.com"){
                return "https://mail.sina.com.cn";
            }else if (t[1] == "tom.com"){
                return "https://mail.tom.com";
            }else if (t[1] == "yahoo.com.cn" ||t[1] == "yahoo.cn"){
                return "https://mail.cn.yahoo.com";
            }else if (t[1] == "tom.com"){
                return "https://mail.tom.com";
            }else if (t[1] == "yeah.net"){
                return "https://www.yeah.net";
            }else if (t[1] == "21cn.com"){
                return "https://mail.21cn.com";
            }else if (t[1] == "hotmail.com"){
                return "https://www.hotmail.com";
            }else if (t[1] == "sogou.com"){
                return "https://mail.sogou.com";
            }else if (t[1] == "188.com"){
                return "https://www.188.com";
            }else if (t[1] == "139.com"){
                return "https://mail.10086.cn";
            }else if (t[1] == "189.cn"){
                return "https://webmail15.189.cn/webmail";
            }else if (t[1] == "wo.com.cn"){
                return "https://mail.wo.com.cn/smsmail";
            }else if (t[1] == "139.com"){
                return "https://mail.10086.cn";
            }else {
                return "";
            }
        }

        public ActionResult Home(int? pos)
        {
            HttpCookie getcook = Request.Cookies["newcookie"];
            int id = int.Parse(getcook.Values["id"]);
            User user = db.Users.Find(id);
            if (pos == 1)
                ViewBag.pos = "Manage";
            else
                ViewBag.pos = "Board";
            return View(user);
        }

        public ActionResult Login(User us)
        {
            var email = us.email;
            var pwd = us.password;
            nc.Values["email"] = email;
            nc.Values["remain"] = Request.Form["remain"];
            var res = from u in db.Users
                      where u.email == email && u.password == pwd
                      select u;
            var user = res.ToList().FirstOrDefault<User>();
            if (user != null)
            {               
                if (user.statu == 1){
                    
                    if (Request.Form["remain"] != null)
                    {
                        nc.Values["pwd"] = pwd;
                    }
                    
                    nc.Values["id"] = user.Id.ToString();
                    Response.Cookies.Add(nc);
                    if(user.level == 1)
                        return RedirectToAction("Index", "Manage");
                    else
                        return RedirectToAction("Index","Board");
                } else{
                    string url = Gotomail(email);
                    Response.Write( "<script>alert('账号未激活，请前往邮箱激活');</script>");
                    @ViewBag.url = Gotomail(email);
                    return View("Active");
                }
            } else{
                Response.Write("<script>alert('邮箱未注册或密码错误');</script>");
                return RedirectToAction("Index");
            }
        }

        public ActionResult Change(User info)
        {
            var name = info.name;
            var pwd= info.password;
            var email = info.email;
            User user = db.Users.Find(info.Id);
            user.name = name;           
            if(pwd != null)
                user.password = pwd;
            if (email != null){              
                var result = SendEmail(email, "验证邮件", "http://localhost:12112/Login/Active/" + user.Id);
                if (result){
                    user.email = email;
                    user.statu = 0;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    Response.Write("<script>alert('请前往新邮箱激活');</script>");
                    @ViewBag.url = Gotomail(email);
                    return View("Active");
                }
                else{
                    Response.Write( "<script>alert('邮箱错误')</script>");
                }
            }           
            return View("Home",user);
        }

        public ActionResult ChangeImg(int? id)
        {
            HttpPostedFileBase file = Request.Files["photo"];
            if (file != null)
            {
                string filePath = Path.Combine(HttpContext.Server.MapPath("~/UserImages"), "user"+id+".jpg");
                file.SaveAs(filePath);
                db.Users.Find(id).image = "/UserImages/user"+id+".jpg";
                db.SaveChanges();
            }
            return RedirectToAction("Home");
        }
    }
}