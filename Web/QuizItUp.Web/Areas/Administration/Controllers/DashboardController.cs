﻿namespace QuizItUp.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { };
            return this.View(viewModel);
        }
    }
}
