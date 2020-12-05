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
        int GetUsersCountAsync();

        Task<IList<UserViewModel>> GetTopPlayers();

        Task<bool> UpdateUsersRanksAsync();

        Task<Rank> GetUserRank(string userId);

        Task<bool> UpdateUserAsync(string firstName, string lastName, string userId);
    }
}
