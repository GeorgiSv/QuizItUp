namespace QuizItUp.Web.Controllers
{
    using System.Diagnostics;

    using QuizItUp.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;

    public class HomeController : BaseController
    {
        private readonly IDeletableEntityRepository<Category> db;

        public HomeController(IDeletableEntityRepository<Category> db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
