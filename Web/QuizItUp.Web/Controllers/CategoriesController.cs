namespace QuizItUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Categories;
    using QuizItUp.Web.ViewModels.Quizes;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Category()
        {
            var categoryViewModel = new IndexCategoryViewModel()
            {
                Categories = this.categoriesService.GetAll(),
            };

            return this.View(categoryViewModel);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var res = this.categoriesService.GetById(id);
            return this.View(res);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Add(string title, string picture)
        {
            await this.categoriesService.Add(title, picture);

            return this.Redirect("/Categories/Category");
        }

        public async Task<IActionResult> AllQuizesPerCategory(int id)
        {
            var indexQuizModel = new IndexQuizViewModel()
            {
                Quizes = await this.categoriesService.GetQuizesPerCategoryId(id),
            };

            return this.View(indexQuizModel);
        }
    }
}
