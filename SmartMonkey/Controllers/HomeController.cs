﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartMonkey.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "SmartMonkey descrição~~~~";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Página de Contato";

            return View();
        }
    }
}