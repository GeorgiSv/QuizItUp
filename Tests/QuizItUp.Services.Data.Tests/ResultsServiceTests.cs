namespace QuizItUp.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using Xunit;

    public class ResultsServiceTests : BaseBLTests
    {
        private IResultsService Service => this.ServiceProvider.GetRequiredService<IResultsService>();

        [Fact]
        public async Task AddResultShouldAddCorrectlyResult()
        {
            var user = new ApplicationUser()
            {
                UserName = "test",
            };

            this.DbContext.Users.Add(user);
            await this.DbContext.SaveChangesAsync();

            var resultId = await this.Service.AddResultAsync(true, "123", 10, user.Id, 5);
            var resul = this.DbContext.Results.FirstOrDefault(x => x.Id == resultId);

            Assert.Equal(resultId, resul.Id);
            Assert.Equal("123", resul.QuizId);
            Assert.Equal(10, resul.Trophies);
            Assert.Equal(user.Id, resul.ApplicationUserId);
            Assert.Equal(5, resul.CorrectAnswers);
        }

        [Fact]
        public async Task AddTrophiesToUserShoudlAddCorrectlyTrophiesToCorrectUser()
        {
            //var user = new ApplicationUser()
            //{
            //    UserName = "test",
            //};

            //this.DbContext.Users.Add(user);
            //await this.DbContext.SaveChangesAsync();

            //var resultId = await this.Service.AddResultAsync(true, "123", 10, user.Id, 5);
            //var resul = this.DbContext.Results.FirstOrDefault(x => x.Id == resultId);

            //Assert.Equal(resultId, resul.Id);
            //Assert.Equal("123", resul.QuizId);
            //Assert.Equal(10, resul.Trophies);
            //Assert.Equal(user.Id, resul.ApplicationUserId);
            //Assert.Equal(5, resul.CorrectAnswers);
        }


        //AddTrphiesToUser
        //CheckQuizResults
        //GetResultById
        //UserHasPassedQuizWithId
        //GetResultsCount
    }
}
