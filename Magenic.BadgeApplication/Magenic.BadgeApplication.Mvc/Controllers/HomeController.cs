﻿using System.Web.Mvc;

namespace Magenic.BadgeApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HomeController
        : BaseController
    {
        /// <summary>
        /// Handles the /Home/Index action.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}