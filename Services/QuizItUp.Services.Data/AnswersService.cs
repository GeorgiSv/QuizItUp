namespace QuizItUp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels.Answers;

    public class AnswersService : IAnswersService
    {
        private readonly IDeletableEntityRepository<Answer> answerRepo;

        public AnswersService(IDeletableEntityRepository<Answer> answerRepo)
        {
            this.answerRepo = answerRepo;
        }

        public Task<string> AddAnswerToQuestion(AnswerInputModel input)
        {
            throw new NotImplementedException();
        }

        public ICollection<Answer> AnswerInputsToModels(IEnumerable<AnswerInputModel> input, string questionId)
        {
            var answerList = new List<Answer>();
            foreach (var answerInput in input)
            {
                var currentAnswer = new Answer()
                {
                    AnswerText = answerInput.AnswerText,
                    IsCorrectAnswer = answerInput.IsCorrectAnswer,
                    QuestionId = questionId,
                    PictureId = answerInput.PictureId,
                };

                answerList.Add(currentAnswer);
            }

            return answerList;
        }

        public async Task<List<Answer>> GetAllAnswersPerQuestionAsync(string questionId)
        {
            var answers = await this.answerRepo.All()
                .Where(x => x.QuestionId == questionId)
                .ToListAsync();

            return answers;
        }

        public AnswerViewModel GetAnswerById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAnswers(List<AnswerInputModel> input, string questionId)
        {
            var currecntAnswers = await this.GetAllAnswersPerQuestionAsync(questionId);

            for (int i = 0; i < currecntAnswers.Count(); i++)
            {
                currecntAnswers[i].AnswerText = input[i].AnswerText;
                currecntAnswers[i].IsCorrectAnswer = input[i].IsCorrectAnswer;
                currecntAnswers[i].PictureId = input[i].PictureId;

                this.answerRepo.Update(currecntAnswers[i]);
                await this.answerRepo.SaveChangesAsync();
            }
        }
    }
}
