namespace QuizItUp.Services.Data.Contracts
{
    using QuizItUp.Web.ViewModels.Questions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IQuestionsService
    {
        Task<IList<QuestionViewModel>> GetAllQuestionsPerQuizAsync(string quizId);

        Task<QuestionViewModel> GetQuestionByQuizIdAsync(string id);

        Task<QuestionViewModel> GetQuestionByIdAsync(string id);

        Task<string> AddQuestionAsync(IndexQuestionViewModel input);
        Task<string> RemoveQuestionAsync(string questionId);

        Task<string> UpdateQuestionAsync(IndexQuestionViewModel input, string id);
    }
}
