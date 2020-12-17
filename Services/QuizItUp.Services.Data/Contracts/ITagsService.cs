namespace QuizItUp.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuizItUp.Data.Models;
    using QuizItUp.Web.ViewModels.Quizes;

    public interface ITagsService
    {
        Task<ICollection<QuizTag>> CreateTagsAsync(string tags, string quizId, int categoryId, Quiz quiz);

        Task<IList<QuizViewModel>> GetAllWithTitleAsync(string input);

        Task<IList<QuizTag>> GetAllQuizTagsAsync();

        Task<IList<Tag>> GetAllTagsAsync();
    }
}
