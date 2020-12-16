namespace QuizItUp.Services.Data.Tests
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
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

            this.DbContext.Add(category);

            var categoryFound = this.Service.GetById(category.Id);

            Assert.Equal(category.Id, categoryFound.Id);
        }

        [Fact]
        public async Task GetQuizesPeCategoryIShouldReturAllQuizesPerCategoryCorectly()
        {

        }
    }
}
