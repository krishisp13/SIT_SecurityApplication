﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("SubmitRequest", "Request");
        }

        public ActionResult Confirmation()
        {
            return View();
        }
    }
}