namespace QuizItUp.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using QuizItUp.Web.ViewModels.Quizes;

    public interface IQuizesService
    {
        Task<string> CreateQuizAsync(QuizInputModel input);

        Task<IList<QuizViewModel>> GetAllQuizesAsync();

        Task<IList<QuizViewModel>> GetAllQuizesWithTagAsync(string input);

        Task<IList<QuizViewModel>> GetAllQuizesWithNameAsync(string input);

        Task<IList<QuizViewModel>> GetAllQuizesByUserId(string userId);

        Task<QuizViewModel> GetQuizByIdAsync(string quizId);

        Task<int> PublishAsync(string quizId);

        Task<int> UnPublishAsync(string quizId);

        Task<string> RemoveQuizAsync(string quizId);

        Task<string> UpdateQuizAsync(EditQuizInputModel input, string quizId);

        Task UpdateQuizTrophiesAsync(string quizId);

        Task<string> GetCreatorNameAsync(string quizId);

        int GetQuizesCount();
    }
}
