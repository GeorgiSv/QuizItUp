namespace QuizItUp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepo;
        private readonly IDeletableEntityRepository<Rank> ranksRepo;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepo,
            IDeletableEntityRepository<Rank> ranksRepo)
        {
            this.usersRepo = usersRepo;
            this.ranksRepo = ranksRepo;
        }

        public int GetUsersCountAsync()
            => this.usersRepo
                 .All()
                 .Count();

        public async Task<IList<UserViewModel>> GetTopPlayers()
        {
            var resultMap = await this.usersRepo
                .All()
                .To<UserViewModel>()
                .OrderByDescending(x => x.Trophies)
                .Take(100)
                .ToListAsync();

            return resultMap;
        }

        public async Task<bool> UpdateUsersRanksAsync()
        {
            try
            {
                var allUsers = await this.usersRepo.All().ToListAsync();

                foreach (var user in allUsers)
                {
                    user.Rank = await this.GetUserRank(user.Id);

                    this.usersRepo.Update(user);
                }

                await this.usersRepo.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public async Task<Rank> GetUserRank(string userId)
        {
            var user = await this.usersRepo.All().FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
            {
                return null;
            }

            var ranks = await this.ranksRepo.All()
                .Where(x => x.TrophiesNeeded <= user.Trophies)
                .OrderByDescending(x => x.TrophiesNeeded)
                .ToListAsync();

            var rank = ranks.FirstOrDefault();

            return rank;
        }

        public async Task<bool> UpdateUserAsync(string firstName, string lastName, string userId, string picturePath)
        {
            var user = this.usersRepo.All().FirstOrDefault(x => x.Id == userId);

            if (user == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(picturePath))
            {
                user.Picture.Url = picturePath;
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            this.usersRepo.Update(user);
            await this.usersRepo.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetUserPicture(string userId)
        {
            var result = await this.usersRepo
                .All()
                .Where(x => x.Id == userId)
                .Select(x => new ApplicationUser
                {
                    Picture = x.Picture,
                    PictureId = x.PictureId,
                })
                .FirstOrDefaultAsync();

            return result.Picture.Url;
        }
    }
}
