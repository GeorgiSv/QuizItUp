namespace QuizItUp.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using QuizItUp.Data.Models;
    using QuizItUp.Web.ViewModels.Quizes;

    public interface ITagsService
    {
        Task<ICollection<QuizTag>> CreateTags(string tags, string quizId, int categoryId, Quiz quiz);

        Task<IList<QuizViewModel>> GetAllWithTitle(string input);

        Task<IList<QuizTag>> GetAllQuizTags();

        Task<IList<Tag>> GetAllTags();
    }
}
