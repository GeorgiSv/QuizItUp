namespace QuizItUp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using Xunit;

    public class UsersServicesTest : BaseBLTests
    {
        private IUsersService Service => this.ServiceProvider.GetRequiredService<IUsersService>();


        [Fact]
        public async Task GetUsersCountShouldReturnCorrectCOuntOfAllRegisteredUsers()
        {
            var user = new ApplicationUser()
            {
                UserName = "TestUserName",
            };

            this.DbContext.Users.Add(user);
            await this.DbContext.SaveChangesAsync();

            var countOfUsers = this.Service.GetUsersCount();

            Assert.Equal(1, countOfUsers);
        }

        [Fact]
        public async Task GetUserRankShouldReturnCorrectRank()
        {
            var user = new ApplicationUser()
            {
                UserName = "TestUserName",
                Trophies = 50,
            };
            var firstRank = new Rank()
            {
                Name = "Pro",
                TrophiesNeeded = 60,
                IsPublished = true,
            };
            var secondRank = new Rank()
            {
                Name = "Begginer",
                TrophiesNeeded = 10,
                IsPublished = true,
            };

            this.DbContext.Users.Add(user);
            this.DbContext.Ranks.Add(firstRank);
            this.DbContext.Ranks.Add(secondRank);
            await this.DbContext.SaveChangesAsync();

            var rank = await this.Service.GetUserRankAsync(user.Trophies);

            Assert.Equal(secondRank.Name, rank.Name);
            Assert.Equal(secondRank.TrophiesNeeded, rank.TrophiesNeeded);
        }

        [Fact]
        public async Task UpdateUserRankShouldReturnFalseIfUserIsNotFound()
        {
            var rank = await this.Service.UpdateUsersRanksAsync("123");
            Assert.False(rank);
        }

        [Fact]
        public async Task UpdateUserRankShouldReturnTrueIfUserIsFoundAndTrophiesAreUpdated()
        {
            var user = new ApplicationUser()
            {
                UserName = "TestUserName",
                Trophies = 50,
            };
            var firstRank = new Rank()
            {
                Name = "Pro",
                TrophiesNeeded = 60,
            };
            var secondRank = new Rank()
            {
                Name = "Begginer",
                TrophiesNeeded = 10,
            };

            this.DbContext.Users.Add(user);
            this.DbContext.Ranks.Add(firstRank);
            this.DbContext.Ranks.Add(secondRank);
            await this.DbContext.SaveChangesAsync();

            var isSuccessfull = await this.Service.UpdateUsersRanksAsync(user.Id);

            Assert.True(isSuccessfull);
        }

        [Fact]
        public async Task UpdateUserAsyncShouldReturnFalseIfUserIsNotFound()
        {
            var isSuccessfull = await this.Service.UpdateUserAsync("Gosho", "Goshov", "123", "picPath");
            Assert.False(isSuccessfull);
        }

        [Fact]
        public async Task UpdateUserAsyncShouldUpdateNamesAndPIctureCorrectly()
        {
            var user = new ApplicationUser()
            {
                UserName = "TestUserName",
                Trophies = 50,
                Picture = new Picture()
                {
                    Url = "url",
                },
            };

            this.DbContext.Users.Add(user);
            await this.DbContext.SaveChangesAsync();

            var isSuccessfull = await this.Service.UpdateUserAsync("Gosho", "Goshov", user.Id, "picPath");

            var userFormDb = this.DbContext.Users.FirstOrDefault();
            Assert.True(isSuccessfull);
            Assert.Equal("Gosho", userFormDb.FirstName);
            Assert.Equal("Goshov", userFormDb.LastName);
            Assert.Equal("picPath", userFormDb.Picture.Url);
            Assert.Equal(user.Id, userFormDb.Id);
        }

        [Fact]
        public async Task GetUserPictureShouldReturnPictureCorrectly()
        {
            var user = new ApplicationUser()
            {
                UserName = "TestUserName",
                Trophies = 50,
                Picture = new Picture()
                {
                    Url = "url",
                },
            };

            this.DbContext.Users.Add(user);
            await this.DbContext.SaveChangesAsync();

            var ppictureUrl = await this.Service.GetUserPicture(user.Id);

            Assert.Equal(user.Picture.Url, ppictureUrl);
        }
    }
}
