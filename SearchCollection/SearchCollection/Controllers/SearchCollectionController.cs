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
            SRUser user = (SRUser)Session["SRUser"];

            if (user != null)
            {
                if (user.Role != UserRole.ADMIN)
                {
                    return Json(this.topicDao.Retrieve().Where(a => a.CreatedBy == user.Id).ToList(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(this.topicDao.Retrieve().ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new List<Topic>() { }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Users()
        {
            SRUser user = (SRUser)Session["SRUser"];

            if (user != null)
            {
                if (user.Role == UserRole.ADMIN)
                {
                    return Json(this.userDao.Retrieve(), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new List<Topic>() { }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new List<Topic>() { }, JsonRequestBehavior.AllowGet);
            }
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
                    a.CreatedBy = ((SRUser)Session["SRUser"]).Id;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string model)
        {
            try
            {
                SRUser loginUserData = Newtonsoft.Json.JsonConvert.DeserializeObject<SRUser>(model);
                SRUser user = userDao.Login(loginUserData.Username, loginUserData.Password);

                if (user != null)
                {
                    Session["SRUser"] = user;
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
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
            return RedirectToAction("login","searchcollection");
        }

        public ActionResult SRUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(string id)
        {
            try
            {
                SRUser user = (SRUser)Session["SRUser"];

                if (user != null)
                {
                    if (user.Role == UserRole.ADMIN)
                    {
                        return Json(userDao.Update(id,ActiveStatus.INACTIVE), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Activate(string id)
        {
            try
            {
                SRUser user = (SRUser)Session["SRUser"];

                if (user != null)
                {
                    if (user.Role == UserRole.ADMIN)
                    {
                        return Json(userDao.Update(id,ActiveStatus.ACTIVE), JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(false, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                throw;
            }
        }

        public ActionResult Tester()
        {
            return View();
        }
    }
}