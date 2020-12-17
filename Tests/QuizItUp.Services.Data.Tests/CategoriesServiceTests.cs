namespace QuizItUp.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Common;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Quizes;
    using Xunit;

    public class CategoriesServiceTests : BaseBLTests
    {
        private ICategoriesService Service => this.ServiceProvider.GetRequiredService<ICategoriesService>();

        [Fact]
        public async Task GetByIdSholdReturnExactCategoryCorrectly()
        {
            var category = new Category()
            {
                Title = "TestCategory",
            };

            await this.DbContext.AddAsync(category);
            await this.DbContext.SaveChangesAsync();

            var categoryFound = await this.Service.GetByIdAsync(category.Id);

            Assert.Equal(category.Id, categoryFound.Id);
        }

        //[Fact]
        //public async Task GetQuizesPeCategoryIShouldReturAllUnpublishedQuizesPerCategoryCorectly()
        //{
        //    var category = new Category()
        //    {
        //        Title = "Movies",
        //    };

        //    await this.DbContext.Categories.AddAsync(category);
        //    await this.DbContext.SaveChangesAsync();

        //    var categoryId = this.DbContext.Categories.FirstOrDefault(x => x.Title == "Movies").Id;

        //    var quiz = new Quiz()
        //    {
        //        Name = "A",
        //        Description = "A",
        //        TotalTimeToComplete = 10,
        //        CreatorId = "123",
        //        CategoryId = categoryId,
        //        IsPublished = true,
        //        Picture = new Picture() { Url = GlobalConstants.DefaultQuizPicturePath },
        //        Trophies = 10,
        //    };

        //    //var secondQuiz = new Quiz()
        //    //{
        //    //    Name = "B",
        //    //    Description = "B",
        //    //    TotalTimeToComplete = 11,
        //    //    CreatorId = "123",
        //    //    CategoryId = 23,
        //    //    IsPublished = true,
        //    //};

        //    //this.DbContext.Quizes.AddRange(new Quiz[] { quiz, secondQuiz });
        //    await this.DbContext.Quizes.AddAsync(quiz);
        //    //await this.DbContext.Quizes.AddAsync(secondQuiz);
        //    await this.DbContext.SaveChangesAsync();

        //    var quizesFound = await this.Service.GetQuizesPerCategoryIdAsync(categoryId);
        //    Assert.Single(quizesFound);
        //}
    }
}
