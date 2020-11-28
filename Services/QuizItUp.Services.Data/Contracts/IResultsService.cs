namespace QuizItUp.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using QuizItUp.Web.ViewModels.Quizes;
    using QuizItUp.Web.ViewModels.Results;

    public interface IResultsService
    {
        (bool, int) CheckQuizResults(QuizInputModel input, QuizViewModel realQuiz);

        Task<ResultViewModel> GetResultById(string id);

        Task<string> AddResult(bool isPassed, string quizId, int trophies, string userId, int correctAnswers);

        Task<bool> AddTrphiesToUser(string quizId, int trophies, string userId);

        bool UserHasPassedQuizWithId(string quizId, string userId);
    }
}
