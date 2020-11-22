using QuizItUp.Data.Models;
using QuizItUp.Web.ViewModels.Answers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizItUp.Services.Data.Contracts
{
    public interface IAnswersService
    {
        AnswerViewModel GetAnswerById(string id);

        Task<List<Answer>> GetAllAnswersPerQuestionAsync(string questionId);

        Task<string> AddAnswerToQuestion(AnswerInputModel input);

        ICollection<Answer> AnswerInputsToModels(IEnumerable<AnswerInputModel> input, string questionId);

        Task UpdateAnswers(List<AnswerInputModel> input, string questionId);
    }
}
