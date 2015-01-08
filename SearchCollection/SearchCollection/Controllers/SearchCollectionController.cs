using SearchCollection.Models;
using SearchCollection.Models.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchCollection.Controllers
{
    [RequireHttps]
    public class SearchCollectionController : Controller
    {
        TopicDao topicDao = new TopicDao();
        SRUserDao userDao = new SRUserDao();
        // GET: TopicFinder
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Topics()
        {
            return Json(this.topicDao.Retrieve(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<Topic> model)
        {
            bool ret = true;
            try
            {
                foreach (var a in model)
                {
                    if (!topicDao.Create(a))
                    {
                        ret = false;
                        break;
                    }
                }
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Topic model)
        {
            try
            {
                return Json(topicDao.Update(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            try
            {
                return Json(topicDao.Delete(id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Register(string model)
        {
            try
            {
                SRUser user = Newtonsoft.Json.JsonConvert.DeserializeObject<SRUser>(model);

                bool registered = userDao.Create(user);

                if (registered)
                {
                    Session["SRUser"] = (SRUser)user;
                }

                return Json(registered, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ActionResult Logout()
        {
            Session["SRUser"] = null;
            Session.Clear();
            return RedirectToAction("index","home");
        }
    }
}