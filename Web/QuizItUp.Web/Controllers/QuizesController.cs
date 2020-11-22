﻿namespace QuizItUp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Common;
    using QuizItUp.Data.Models;
    using QuizItUp.Services;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Quizes;
    using QuizItUp.Web.ViewModels.Results;

    public class QuizesController : Controller
    {
        private readonly IQuizesService quizService;
        private readonly IResultsService resultsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;
        private readonly IWebHostEnvironment webHostEnvironment;

        public QuizesController(
            IQuizesService quizService,
            IResultsService resultsService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary)
        {
            this.quizService = quizService;
            this.resultsService = resultsService;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.webHostEnvironment = webHostEnvironment;

        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All()
        {
            var quizViewModel = new IndexQuizViewModel()
            {
                Quizes = await this.quizService.GetAllQuizesAsync(),
            };

            return this.View(quizViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> StartCreate(QuizInputModel input)
        {
            if (!(input.Picture.FileName.EndsWith(".png")
                || input.Picture.FileName.EndsWith(".jpeg")
                || input.Picture.FileName.EndsWith(".jpg")))
            {
                this.ModelState.AddModelError("Picture", "Picture must be file with extension jpeg, jpg png");
            }

            if (input.Picture.Length > 10 * 102 * 1024)
            {
                this.ModelState.AddModelError("Picture", "Picture is too large - Max 10Mb");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var userId = this.userManager.GetUserId(this.User);
            input.CreatorId = userId;

            var picturePath = await CloudinaryService
                .UploadPicture(this.cloudinary, input.Picture, input.Name.Replace(" ", "") + this.userManager.GetUserId(this.User), GlobalConstants.CloudinaryQuizFolder);

            input.PicturePath = picturePath;

            var quizId = await this.quizService.CreateQuizAsync(input);
            if (quizId == null)
            {
                return this.View();
            }

            await this.quizService.UpdateQuizTrophiesAsync(quizId);

            return this.RedirectToAction("Create", "Questions", new { id = quizId });
        }

        public IActionResult StartCreate()
        {
            return this.View();
        }

        public async Task<IActionResult> Details(string Id)
        {
            var quiz = await this.quizService.GetQuizByIdAsync(Id);
            return this.View(quiz);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var quiz = await this.quizService.GetQuizByIdAsync(id);
            return this.View(quiz);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditQuizInputModel input, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var quizId = await this.quizService.UpdateQuizAsync(input, id);

            return this.RedirectToAction("AllQuizInfo", "Quizes", new { id = quizId });
        }

        public async Task<IActionResult> Remove(string id)
        {
            var quizId = await this.quizService.RemoveQuizAsync(id);
            return this.RedirectToAction("All", "Quizes");
        }

        public async Task<IActionResult> Publish(string id)
        {
            var categoryId = await this.quizService.PublishAsync(id);
            return this.RedirectToAction("AllQuizesPerCategory", "Categories", new { id = categoryId });
        }

        public async Task<IActionResult> UnPublish(string id)
        {
            var categoryId = await this.quizService.UnPublishAsync(id);
            return this.RedirectToAction("AllQuizesPerCategory", "Categories", new { id = categoryId });
        }

        public async Task<IActionResult> MyQuizes(string id)
        {
            return this.RedirectToAction("AllQuizesPerCategory", "Categories", new { id = id });
        }

        public async Task<IActionResult> AllQuizInfo(string id)
        {
            var model = await this.quizService.GetQuizByIdAsync(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(QuizInputModel input, string id)
        {
            var quizFromDb = await this.quizService.GetQuizByIdAsync(id);
            var userId = this.userManager.GetUserId(this.User);

            var result = this.resultsService.CheckQuizResults(input, quizFromDb);

            if (this.resultsService.UserHasPassedQuizWithId(id, userId))
            {
                quizFromDb.Trophies = -1;
            }

            var resultId = await this.resultsService.AddResult(result.Item1, quizFromDb.Id, quizFromDb.Trophies, userId, result.Item2);

            return this.RedirectToAction("QuizResult", "Results", new { resultId = resultId });
        }

        public async Task<IActionResult> Start(string id)
        {
            var model = await this.quizService.GetQuizByIdAsync(id);

            foreach (var question in model.Questions)
            {
                foreach (var answer in question.Answers)
                {
                    answer.IsCorrectAnswer = false;
                }
            }

            return this.View(model);
        }
    }
}