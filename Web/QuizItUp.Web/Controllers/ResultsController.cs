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
    using QuizItUp.Web.ViewModels.Results;

    public class ResultsController : Controller
    {
        private readonly IResultsService resultsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ResultsController(IResultsService resultsService, UserManager<ApplicationUser> userManager)
        {
            this.resultsService = resultsService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> QuizResult(string resultId)
        {
            var userId = this.userManager.GetUserId(this.User);
            var result = await this.resultsService.GetResultById(resultId);

            if (result.CorrectAnswers > result.Quiz.Questions.Count)
            {
                result.CorrectAnswers = result.Quiz.Questions.Count;
            }

            return this.View(result);
        }
    }
}
