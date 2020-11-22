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
    using QuizItUp.Web.ViewModels.Quizes;
    using QuizItUp.Web.ViewModels.Results;

    public class ResultsService : IResultsService
    {
        private readonly IDeletableEntityRepository<Result> resultRepo;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepo;

        public ResultsService(IDeletableEntityRepository<Result> resultRepo, IDeletableEntityRepository<ApplicationUser> usersRepo)
        {
            this.resultRepo = resultRepo;
            this.usersRepo = usersRepo;
        }

        public async Task<string> AddResult(bool isPassed, string quizId, int trophies, string userId, int correctAnswers)
        {
            if (trophies >= 0)
            {
                if (isPassed)
                {
                    await this.AddTrphiesToUser(quizId, trophies, userId);
                }
                else
                {
                    trophies = 0;
                    await this.AddTrphiesToUser(quizId, trophies, userId);
                }
            }

            var result = new Result()
            {
                ApplicationUserId = userId,
                IsPassed = isPassed,
                QuizId = quizId,
                Trophies = trophies,
                CorrectAnswers = correctAnswers,
            };

            await this.resultRepo.AddAsync(result);
            await this.resultRepo.SaveChangesAsync();

            return result.Id;
        }

        public async Task<bool> AddTrphiesToUser(string quizId, int trophies, string userId)
        {
            var user = await this.usersRepo
            .All()
            .Where(x => x.Id == userId)
            .FirstOrDefaultAsync();

            user.Trophies += trophies;

            this.usersRepo.Update(user);
            await this.usersRepo.SaveChangesAsync();
            return true;
        }

        public (bool, int) CheckQuizResults(QuizInputModel input, QuizViewModel realQuiz)
        {
            var isPassed = true;
            var correctAnswers = 0;

            for (int i = 0; i < input.Questions.Count; i++)
            {
                var question = input.Questions[i];
                var userQuestion = realQuiz.Questions[i];

                for (int j = 0; j < question.Answers.Count; j++)
                {
                    var quizAnswer = question.Answers[j];
                    var userAnswer = userQuestion.Answers[j];

                    if (quizAnswer.IsCorrectAnswer != userAnswer.IsCorrectAnswer)
                    {
                        isPassed = false;
                    }
                    else if (userAnswer.IsCorrectAnswer == true)
                    {
                        correctAnswers++;
                    }
                }
            }

            return (isPassed, correctAnswers);
        }

        public async Task<ResultViewModel> GetResultById(string id)
            => await this.resultRepo
            .All()
            .Where(x => x.Id == id)
            .To<ResultViewModel>()
            .FirstOrDefaultAsync();

        public bool UserHasPassedQuizWithId(string quizId, string userId)
            => this.resultRepo
            .All()
            .Any(x => x.ApplicationUserId == userId && x.QuizId == quizId && x.IsPassed == true);
    }
}
