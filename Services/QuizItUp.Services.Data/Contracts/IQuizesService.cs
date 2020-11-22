namespace QuizItUp.Services.Data.Contracts
{
    using QuizItUp.Web.ViewModels.Quizes;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IQuizesService
    {
        Task<string> CreateQuizAsync(QuizInputModel input);

        Task<ICollection<QuizViewModel>> GetAllQuizesAsync();

        Task<QuizViewModel> GetQuizByIdAsync(string quizId);

        Task<int> PublishAsync(string quizId);

        Task<int> UnPublishAsync(string quizId);

        Task<string> RemoveQuizAsync(string quizId);

        Task<string> UpdateQuizAsync(EditQuizInputModel input, string quizId);

        Task UpdateQuizTrophiesAsync(string quizId);
    }
}
