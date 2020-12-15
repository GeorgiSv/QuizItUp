namespace QuizItUp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using QuizItUp.Data;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Data.Repositories;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Quizes;
    using Xunit;

    public class QuizesServiceTests : BaseBLTests
    {
        private readonly Quiz quiz;

        public QuizesServiceTests()
        {
            this.DbContext.Categories.Add(new Category { Title = "Movies" });
            //this.DbContext.SaveChangesAsync();

            this.quiz = new Quiz()
            {
                Name = "QuizTest",
                Description = "DummyDesc",
                TotalTimeToComplete = 10,
                CreatorId = "123",
            };

            this.DbContext.Quizes.Add(this.quiz);
            this.DbContext.SaveChangesAsync();
        }

        private IQuizesService Service => this.ServiceProvider.GetRequiredService<IQuizesService>();


        [Fact]
        public async Task CreateQizShouldBeCorrectly()
        {

            var quizInput = CreateDummyQuiz();

            var result = await this.Service.CreateQuizAsync(quizInput);
            var quiz = await this.DbContext.Quizes.FirstOrDefaultAsync(x => x.Id == result);

            Assert.Equal(quizInput.Name, quiz.Name);
            Assert.Equal(quizInput.Category.ToString(), quiz.Category.Title);
            Assert.Equal(quizInput.Description, quiz.Description);
            Assert.Equal(quizInput.TotalTimeToComplete, quiz.TotalTimeToComplete);
            Assert.Equal(result, quiz.Id);
        }

        [Fact]
        public async Task CreateQizShouldNotCreateQuizWithoudCategory()
        {
            var input = CreateDummyQuiz();
            input.Category = Categories.Games;

            var ex = Assert.ThrowsAsync<Exception>(() => this.Service.CreateQuizAsync(input));
            Assert.Equal("Category does not exist!", ex.Result.Message);
        }

        [Fact]
        public async Task UpdateQuizShouldUpdateCorectly()
        {
            var input = new EditQuizInputModel()
            {
                Name = "TestQuiz",
                Description = "Dummy",
                TotalTimeToComplete = 1,
            };

            await this.Service.UpdateQuizAsync(input, this.quiz.Id);
            var result = await this.DbContext.Quizes.FirstOrDefaultAsync();

            Assert.Equal(input.Name, result.Name);
            Assert.Equal(input.Description, result.Description);
            Assert.Equal(input.TotalTimeToComplete, result.TotalTimeToComplete);
            Assert.Equal(this.quiz.Id, result.Id);
        }

        [Fact]
        public async Task GetAllQuizesShouldReturnAllQuizes()
        {
            QuizInputModel quizInputModel = CreateDummyQuiz();
            var input = quizInputModel;

            var result = await this.Service.CreateQuizAsync(input);
            var quizesCount = this.DbContext.Quizes.Count();

            Assert.Equal(2, quizesCount);
        }

        [Fact]
        //public async Task GetAllQuizesShouldReturnAllQuizesWithCorrectValues()
        //{
        //    var result = await this.Service.GetAllQuizesAsync();

        //    Assert.Equal(this.quiz.Name, result[0].Name);
        //    Assert.Equal(this.quiz.Description, result[0].Description);
        //    Assert.Equal(this.quiz.TotalTimeToComplete, result[0].TotalTimeToComplete);
        //    Assert.Equal(this.quiz.Id, result[0].Id);
        //}

        private static QuizInputModel CreateDummyQuiz()
        {
            return new QuizInputModel()
            {
                Name = "TestQuiz",
                Category = Categories.Movies,
                Description = "Dummy",
                TotalTimeToComplete = 1,
                CreatorId = "123",
            };
        }
    }
}
