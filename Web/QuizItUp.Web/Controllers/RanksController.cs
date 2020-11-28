namespace QuizItUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Ranks;

    public class RanksController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRanksService ranksService;

        public RanksController(UserManager<ApplicationUser> userManager, IRanksService ranksService)
        {
            this.userManager = userManager;
            this.ranksService = ranksService;
        }

        public async Task<IActionResult> Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RankInputModel input)
        {
            await this.ranksService.AddAsync(input);
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> All()
        {
            var allRanksViewModel = new AllRanksViewModel()
            {
                Ranks = await this.ranksService.GetAllRanksAsync(),
            };

            return this.View(allRanksViewModel);
        }

        public async Task<IActionResult> Remove(string id)
        {
            await this.ranksService.RemoveAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
