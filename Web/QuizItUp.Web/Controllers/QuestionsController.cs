namespace QuizItUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Common;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Answers;
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

        public async Task<IActionResult> Create(string id)
        {
            var indexQestionodel = new IndexQuestionViewModel
            {
                QuizViewModel = await this.quizesService.GetQuizByIdAsync(id),
            };

            return this.View(indexQestionodel);
        }

        [HttpPost]
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

        public async Task<IActionResult> Remove(string id)
        {
           var quizId = await this.questionService.RemoveQuestionAsync(id);
           await this.quizesService.UpdateQuizTrophiesAsync(quizId);

           return this.RedirectToAction("AllQuizInfo", "Quizes", new { id = quizId });
        }

        public async Task<IActionResult> All(string id)
        {
            var indexQestionodel = new QuizViewModel();
            indexQestionodel.Questions = await this.questionService.GetAllQuestionsPerQuizAsync(id);

            return this.View(indexQestionodel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var indexViewModel = new IndexQuestionViewModel()
            {
                QuestionViewModel = await this.questionService.GetQuestionByIdAsync(id),
            };
            indexViewModel.QuizViewModel = await this.quizesService.GetQuizByIdAsync(indexViewModel.QuestionViewModel.QuizId);

            return this.View(indexViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IndexQuestionViewModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", "Questions", new { id = id });
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
