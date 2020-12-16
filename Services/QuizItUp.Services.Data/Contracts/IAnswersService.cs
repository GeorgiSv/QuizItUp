namespace QuizItUp.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuizItUp.Data.Models;
    using QuizItUp.Web.ViewModels.Answers;

    public interface IAnswersService
    {
        Task<List<Answer>> GetAllAnswersPerQuestionAsync(string questionId);

        ICollection<Answer> AnswerInputsToModels(IEnumerable<AnswerInputModel> input, string questionId);

        Task UpdateAnswers(List<AnswerInputModel> input, string questionId);
    }
}
