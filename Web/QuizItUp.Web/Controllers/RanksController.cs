namespace QuizItUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Ranks;

    [Authorize]
    public class RanksController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRanksService ranksService;
        private readonly IUsersService usersService;

        public RanksController(UserManager<ApplicationUser> userManager, IRanksService ranksService, IUsersService usersService)
        {
            this.userManager = userManager;
            this.ranksService = ranksService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> All()
        {
            var allRanksViewModel = new AllRanksViewModel()
            {
                Ranks = await this.ranksService.GetAllRanksAsync(),
            };

            return this.View(allRanksViewModel);
        }

        public async Task<IActionResult> Ranking()
        {
            var model = new UserRankingViewModel()
            {
                Users = await this.usersService.GetTopPlayers(),
            };

            return this.View(model);
        }

        public async Task<IActionResult> UpdateUsersRanks()
        {
            var result = await this.usersService.UpdateUsersRanksAsync();

            if (!result)
            {
                return this.View();
            }

            return this.Redirect("/");
        }
    }
}
