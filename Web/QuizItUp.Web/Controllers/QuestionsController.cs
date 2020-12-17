namespace QuizItUp.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Common;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.Infrastructure.Filters;
    using QuizItUp.Web.ViewModels.Questions;
    using QuizItUp.Web.ViewModels.Quizes;

    public class QuestionsController : Controller
    {
        private readonly IQuizesService quizesService;
        private readonly IQuestionsService questionService;

        public QuestionsController(IQuizesService quizesService, IQuestionsService questionService)
        {
            this.quizesService = quizesService;
            this.questionService = questionService;
        }

        [ServiceFilter(typeof(UserValidationAttribute))]
        public async Task<IActionResult> Create(string id)
        {
            var indexQestionodel = new IndexQuestionViewModel
            {
                QuizViewModel = await this.quizesService.GetQuizByIdAsync(id),
            };

            return this.View(indexQestionodel);
        }

        [HttpPost]
        [ServiceFilter(typeof(UserValidationAttribute))]
        public async Task<IActionResult> Add(IndexQuestionViewModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Create", "Questions", new { id = id });
            }

            var correctAnswersCount = input.AnswersInputModel.Where(x => x.IsCorrectAnswer == true).ToList().Count;
            if (input.AnswersInputModel.Count < 2 || (correctAnswersCount > 1 || correctAnswersCount < 1))
            {
                this.ModelState.AddModelError("AnswerInputModel", "Ivalid data!");
                return this.RedirectToAction("Create", "Questions", new { id = id });
            }

            input.QuestionInputModel.QuizId = id;
            var result = await this.questionService.AddQuestionAsync(input);

            if (result == null)
            {
                return this.View();
            }

            await this.quizesService.UpdateQuizTrophiesAsync(id);

            return this.RedirectToAction("Create", "Questions", new { id = id });
        }

        //TODO: needs validation
        public async Task<IActionResult> Remove(string id)
        {
            var question = await this.questionService.GetQuestionByIdAsync(id);
            var creatorName = await this.quizesService.GetCreatorNameAsync(question.QuizId);
            if (creatorName != this.User.Identity.Name && !this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            var quizId = await this.questionService.RemoveQuestionAsync(id);
            await this.quizesService.UpdateQuizTrophiesAsync(quizId);

            return this.RedirectToAction("AllQuizInfo", "Quizes", new { id = quizId });
        }

        [ServiceFilter(typeof(UserValidationAttribute))]
        public async Task<IActionResult> All(string id)
        {
            var indexQestionodel = new QuizViewModel();
            indexQestionodel.Questions = await this.questionService.GetAllQuestionsPerQuizAsync(id);

            return this.View(indexQestionodel);
        }

        //TODO: needs validation
        public async Task<IActionResult> Edit(string id)
        {
            var indexViewModel = new IndexQuestionViewModel()
            {
                QuestionViewModel = await this.questionService.GetQuestionByIdAsync(id),
            };
            indexViewModel.QuizViewModel = await this.quizesService.GetQuizByIdAsync(indexViewModel.QuestionViewModel.QuizId);

            var creatorName = await this.quizesService.GetCreatorNameAsync(indexViewModel.QuestionViewModel.QuizId);
            if (creatorName != this.User.Identity.Name && !this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            return this.View(indexViewModel);
        }

        //TODO: needs validation
        [HttpPost]
        public async Task<IActionResult> Edit(IndexQuestionViewModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(id);
            }

            var question = await this.questionService.GetQuestionByIdAsync(id);
            var creatorName = await this.quizesService.GetCreatorNameAsync(question.QuizId);
            if (creatorName != this.User.Identity.Name && !this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return this.Redirect("/");
            }

            var correctAnswersCount = input.AnswersInputModel.Where(x => x.IsCorrectAnswer == true).ToList().Count;
            if (input.AnswersInputModel.Count < 2 || (correctAnswersCount > 1 || correctAnswersCount < 1))
            {
                this.ModelState.AddModelError("AnswerInputModel", "Ivalid data!");
                return this.RedirectToAction("Edit", "Questions", new { id = id });
            }

            var quizId = await this.questionService.UpdateQuestionAsync(input, id);

            await this.quizesService.UpdateQuizTrophiesAsync(quizId);

            return this.RedirectToAction("AllQuizInfo", "Quizes", new { id = quizId });
        }
    }
}
