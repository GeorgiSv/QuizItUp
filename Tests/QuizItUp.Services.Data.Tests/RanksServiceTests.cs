namespace QuizItUp.Services.Data.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class RanksServiceTests : BaseBLTests
    {
        private IRanksService Service => this.ServiceProvider.GetRequiredService<IRanksService>();

        [Fact]
        public async Task GetAllRanksShouldReturnAllPublishedRanks()
        {
            var rank = new Rank()
            {
                Name = "TestRank",
                IsPublished = true,
            };

            var secondRank = new Rank()
            {
                Name = "SecondTestRank",
                IsPublished = false,
            };

            this.DbContext.Ranks.Add(rank);
            this.DbContext.Ranks.Add(secondRank);
            await this.DbContext.SaveChangesAsync();

            var allRanks = await this.Service.GetAllRanksAsync();

            Assert.Single(allRanks);
            Assert.Equal(rank.Name, allRanks.FirstOrDefault().Name);
        }
    }
}
