namespace QuizItUp.Web.Controllers
{
    using System.Threading.Tasks;

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

        public async Task<IActionResult> Category()
        {
            var categoryViewModel = new IndexCategoryViewModel()
            {
                Categories = await this.categoriesService.GetAllAsync(),
            };

            return this.View(categoryViewModel);
        }

        public async Task<IActionResult> AllQuizesPerCategory(int id)
        {
            var indexQuizModel = new IndexQuizViewModel()
            {
                Quizes = await this.categoriesService.GetQuizesPerCategoryIdAsync(id),
            };

            return this.View(indexQuizModel);
        }
    }
}
