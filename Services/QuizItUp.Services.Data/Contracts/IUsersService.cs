namespace QuizItUp.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using QuizItUp.Data.Models;
    using QuizItUp.Web.ViewModels;

    public interface IUsersService
    {
        int GetUsersCount();

        Task<IList<UserViewModel>> GetTopPlayers();

        Task<bool> UpdateUsersRanksAsync(string userId);

        Task<Rank> GetUserRankAsync(int trophies);

        Task<bool> UpdateUserAsync(string firstName, string lastName, string userId, string picturePath);

        Task<string> GetUserPicture(string userId);
    }
}
