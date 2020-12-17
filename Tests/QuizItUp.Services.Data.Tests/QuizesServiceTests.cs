namespace QuizItUp.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Common;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Quizes;
    using Xunit;

    public class QuizesServiceTests : BaseBLTests
    {
        private readonly Quiz quiz;

        public QuizesServiceTests()
        {

            this.quiz = new Quiz()
            {
                Name = "QuizTest",
                Description = "DummyDesc",
                TotalTimeToComplete = 10,
                CreatorId = "123",
            };

            var quizTag = new QuizTag()
            {
                QuizId = this.quiz.Id,
                Tag = new Tag()
                {
                    Title = "testing",
                },
            };

            this.quiz.QuizTag.Add(quizTag);

            this.DbContext.Categories.Add(new Category { Title = "Movies" });
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
            Assert.Equal(GlobalConstants.InitialQuizTrophies, quiz.Trophies);
            Assert.Equal(2, quiz.QuizTag.Count);
            Assert.Equal(result, quiz.Id);
        }

        [Fact]
        public async Task CreateQizShouldNotCreateQuizWithoudCategory()
        {
            var input = CreateDummyQuiz();
            input.Category = Categories.Games;

            var ex = await Assert.ThrowsAsync<Exception>(() => this.Service.CreateQuizAsync(input));
            Assert.Equal("Category does not exist!", ex.Message);
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
        public void GetQuizCountShoultReturnCorecQuizesCount()
        {
            var quizesCount = this.Service.GetQuizesCount();
            Assert.Equal(1, quizesCount);
        }

        [Fact]
        public async Task UpdateQuizTrophiesCorectly()
        {
            var question = new Question()
            {
                QuizId = this.quiz.Id,
                QuestionText = "QuestionText",
            };

            await this.DbContext.Questions.AddAsync(question);
            await this.DbContext.SaveChangesAsync();

            await this.Service.UpdateQuizTrophiesAsync(this.quiz.Id);
            var result = await this.DbContext.Quizes.FirstOrDefaultAsync();

            Assert.Equal(11, result.Trophies);
        }

        [Fact]
        public async Task PublishShouldPublishCorrectly()
        {
            await this.Service.PublishAsync(this.quiz.Id);
            var result = await this.DbContext.Quizes.FirstOrDefaultAsync();

            Assert.True(result.IsPublished);
        }

        [Fact]
        public async Task PublishShouldPublishOnlyUnpublishedQuizes()
        {
            await this.Service.PublishAsync(this.quiz.Id);
            var secondPublishResult = await this.Service.PublishAsync(this.quiz.Id);

            Assert.Equal(0, secondPublishResult);
        }

        [Fact]
        public async Task UnPublishShouldUnPublishOnlyPublishedQuizes()
        {
            var unpublishResult = await this.Service.UnPublishAsync(this.quiz.Id);
            var result = await this.DbContext.Quizes.FirstOrDefaultAsync();

            Assert.Equal(0, unpublishResult);
        }

        [Fact]
        public async Task UnPublishShouldUnpublishCorrectly()
        {
            await this.Service.PublishAsync(this.quiz.Id);
            await this.Service.UnPublishAsync(this.quiz.Id);
            var result = await this.DbContext.Quizes.FirstOrDefaultAsync();

            Assert.False(result.IsPublished);
        }

        [Fact]
        public async Task RemoveQuizShouldRemoveCorrectly()
        {
            var quizId = await this.Service.RemoveQuizAsync(this.quiz.Id);
            var result = await this.DbContext.Quizes.FirstOrDefaultAsync();

            Assert.Equal(this.quiz.Id, quizId);
        }

        private static QuizInputModel CreateDummyQuiz()
        {
            return new QuizInputModel()
            {
                Name = "TestQuiz",
                Category = Categories.Movies,
                Description = "Dummy",
                TotalTimeToComplete = 1,
                CreatorId = "123",
                Tags = "tag, ta",
            };
        }
    }
}
