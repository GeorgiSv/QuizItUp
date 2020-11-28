namespace QuizItUp.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuizItUp.Data.Models;

    public interface ITagsService
    {
        Task<ICollection<QuizTag>> CreateTags(string tags, string quizId, int categoryId);

        Task<IList<QuizTag>> GetAllWithTitle(string input);
    }
}
