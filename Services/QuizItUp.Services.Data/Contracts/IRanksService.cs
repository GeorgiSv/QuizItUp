namespace QuizItUp.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using QuizItUp.Web.ViewModels.Ranks;

    public interface IRanksService
    {
        Task AddAsync(RankInputModel input);

        Task<string> RemoveAsync(string id);

        Task<IList<RankViewModel>> GetAllRanksAsync();

        Task<string> Publish(string id);

        Task<string> UnPublish(string id);
    }
}
