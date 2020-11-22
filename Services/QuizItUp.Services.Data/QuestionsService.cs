namespace QuizItUp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels.Questions;

    public class QuestionsService : IQuestionsService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepo;
        private readonly IAnswersService answerService;

        public QuestionsService(IDeletableEntityRepository<Question> questionRepo, IAnswersService answerService)
        {
            this.questionsRepo = questionRepo;
            this.answerService = answerService;
        }

        public async Task<string> AddQuestionAsync(IndexQuestionViewModel input)
        {
            var newQuestion = new Question()
            {
                QuestionText = input.QuestionInputModel.QuestionText,
                QuizId = input.QuestionInputModel.QuizId,
                PictureId = input.QuestionInputModel.PictureId,
            };

            newQuestion.Answers = this.answerService.AnswerInputsToModels(input.AnswersInputModel, newQuestion.Id);

            await this.questionsRepo.AddAsync(newQuestion);
            await this.questionsRepo.SaveChangesAsync();

            return newQuestion.Id;
        }

        public async Task<IList<QuestionViewModel>> GetAllQuestionsPerQuizAsync(string quizId)
        {
            var allQuestons = await this.questionsRepo.All()
                .Where(x => x.QuizId == quizId)
                .To<QuestionViewModel>()
                .ToListAsync();

            return allQuestons;
        }

        public async Task<QuestionViewModel> GetQuestionByQuizIdAsync(string id)
        {
            var allQuestons = await this.questionsRepo.All()
                .Where(x => x.QuizId == id)
                .To<QuestionViewModel>()
                .FirstOrDefaultAsync();

            return allQuestons;
        }

        public async Task<QuestionViewModel> GetQuestionByIdAsync(string id)
        {
            var allQuestons = await this.questionsRepo.All()
                .Where(x => x.Id == id)
                .To<QuestionViewModel>()
                .FirstOrDefaultAsync();

            return allQuestons;
        }

        public async Task<string> UpdateQuestionAsync(IndexQuestionViewModel input, string questionId)
        {
            var question = await this.questionsRepo
                .All()
                .Where(x => x.Id == questionId)
                .FirstOrDefaultAsync();
            question.QuestionText = input.QuestionInputModel.QuestionText;

            await this.answerService.UpdateAnswers(input.AnswersInputModel.ToList(), questionId);

            this.questionsRepo.Update(question);
            await this.questionsRepo.SaveChangesAsync();

            return question.QuizId;
        }

        public async Task<string> RemoveQuestionAsync(string questionId)
        {
            var question = await this.questionsRepo.All()
                .Where(x => x.Id == questionId)
                .FirstOrDefaultAsync();

            this.questionsRepo.Delete(question);
            await this.questionsRepo.SaveChangesAsync();

            return question.QuizId;
        }
    }
}
