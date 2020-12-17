namespace QuizItUp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Answers;
    using QuizItUp.Web.ViewModels.Questions;
    using QuizItUp.Web.ViewModels.Quizes;
    using Xunit;

    public class ResultsServiceTests : BaseBLTests
    {
        private IResultsService Service => this.ServiceProvider.GetRequiredService<IResultsService>();

        [Fact]
        public async Task AddResultShouldAddCorrectlyResult()
        {
            var user = new ApplicationUser()
            {
                UserName = "test",
            };

            this.DbContext.Users.Add(user);
            await this.DbContext.SaveChangesAsync();

            var resultId = await this.Service.AddResultAsync(true, "123", 10, user.Id, 5);
            var resul = this.DbContext.Results.FirstOrDefault(x => x.Id == resultId);

            Assert.Equal(resultId, resul.Id);
            Assert.Equal("123", resul.QuizId);
            Assert.Equal(10, resul.Trophies);
            Assert.Equal(user.Id, resul.ApplicationUserId);
            Assert.Equal(5, resul.CorrectAnswers);
        }

        [Fact]
        public async Task AddTrophiesToUserShoudlAddCorrectlyTrophiesToCorrectUserAndReturnTtrue()
        {
            var user = new ApplicationUser()
            {
                UserName = "test",
            };
            this.DbContext.Users.Add(user);
            await this.DbContext.SaveChangesAsync();

            var isSuccsesfull = await this.Service.AddTrphiesToUser("123", 10, user.Id);
            var userFromDb = this.DbContext.Users.FirstOrDefault(x => x.Id == user.Id);

            Assert.True(isSuccsesfull);
            Assert.Equal(10, userFromDb.Trophies);
        }

        [Fact]
        public async Task AddTrophiesToUserShoudlNotAddTrohpiesIfTheUSerIsNotFoundAndReturnFalse()
        {
            var isSuccsesfull = await this.Service.AddTrphiesToUser("123", 10, "ABV");
            Assert.False(isSuccsesfull);
        }

        [Fact]
        public void CheckQuizResultShouldReturnTrueIFPassedAndNumberOfCorrectAnswers()
        {
            var quizViewModel = new QuizViewModel();
            quizViewModel.Questions = new List<QuestionViewModel>();

            var question = new QuestionViewModel()
            {
                QuestionText = "Question test",
            };

            var answer = new AnswerViewModel()
            {
                AnswerText = "A",
                QuestionId = question.Id,
                IsCorrectAnswer = true,
            };

            var anotherAnswer = new AnswerViewModel()
            {
                AnswerText = "B",
                QuestionId = question.Id,
                IsCorrectAnswer = false,
            };
            question.Answers = new List<AnswerViewModel>();
            question.Answers.Add(answer);
            question.Answers.Add(anotherAnswer);

            quizViewModel.Questions.Add(question);

            var questionInput = new QuestionInputModel()
            {
                QuestionText = "B??",
                QuizId = "123",
            };
            var firstNewAnswer = new AnswerInputModel()
            {
                AnswerText = "A",
                IsCorrectAnswer = true,
            };
            var secondNewAnswer = new AnswerInputModel()
            {
                AnswerText = "B",
                IsCorrectAnswer = false,
            };
            questionInput.Answers = new List<AnswerInputModel>();
            questionInput.Answers.Add(firstNewAnswer);
            questionInput.Answers.Add(secondNewAnswer);

            var quizInputModel = new QuizInputModel();
            quizInputModel.Questions = new List<QuestionInputModel>();
            quizInputModel.Questions.Add(questionInput);

            var result = this.Service.CheckQuizResults(quizInputModel, quizViewModel);
            Assert.True(result.Item1);
            Assert.Equal(1, result.Item2);
        }

        [Fact]
        public void CheckQuizResultShouldReturnFalsePassedAndNumberOfCorrectAnswers()
        {
            var quizViewModel = new QuizViewModel();
            quizViewModel.Questions = new List<QuestionViewModel>();

            var question = new QuestionViewModel()
            {
                QuestionText = "Question test",
            };

            var answer = new AnswerViewModel()
            {
                AnswerText = "A",
                QuestionId = question.Id,
                IsCorrectAnswer = false,
            };

            var anotherAnswer = new AnswerViewModel()
            {
                AnswerText = "B",
                QuestionId = question.Id,
                IsCorrectAnswer = true,
            };

            question.Answers = new List<AnswerViewModel>();
            question.Answers.Add(answer);
            question.Answers.Add(anotherAnswer);

            quizViewModel.Questions.Add(question);

            var questionInput = new QuestionInputModel()
            {
                QuestionText = "B??",
                QuizId = "123",
            };
            var firstNewAnswer = new AnswerInputModel()
            {
                AnswerText = "A",
                IsCorrectAnswer = true,
            };
            var secondNewAnswer = new AnswerInputModel()
            {
                AnswerText = "B",
                IsCorrectAnswer = false,
            };
            questionInput.Answers = new List<AnswerInputModel>();
            questionInput.Answers.Add(firstNewAnswer);
            questionInput.Answers.Add(secondNewAnswer);

            var quizInputModel = new QuizInputModel();
            quizInputModel.Questions = new List<QuestionInputModel>();
            quizInputModel.Questions.Add(questionInput);

            var result = this.Service.CheckQuizResults(quizInputModel, quizViewModel);
            Assert.False(result.Item1);
            Assert.NotEqual(1, result.Item2);
        }

        [Fact]
        public void CheckQuizResultShouldReturnFalseIfNotALlAnswersAreCorrectAndShorNumberOFCoorrectAnswers()
        {
            var quizViewModel = new QuizViewModel();
            quizViewModel.Questions = new List<QuestionViewModel>();

            var question = new QuestionViewModel()
            {
                QuestionText = "Question test",
            };

            var answer = new AnswerViewModel()
            {
                AnswerText = "A",
                QuestionId = question.Id,
                IsCorrectAnswer = false,
            };

            var anotherAnswer = new AnswerViewModel()
            {
                AnswerText = "B",
                QuestionId = question.Id,
                IsCorrectAnswer = true,
            };

            var secondQuestion = new QuestionViewModel()
            {
                QuestionText = "Question test",
            };

            var answerForSecondQuestion = new AnswerViewModel()
            {
                AnswerText = "A",
                QuestionId = question.Id,
                IsCorrectAnswer = false,
            };

            var anotherAnswerForSecondQuestion = new AnswerViewModel()
            {
                AnswerText = "B",
                QuestionId = question.Id,
                IsCorrectAnswer = true,
            };

            question.Answers = new List<AnswerViewModel>();
            secondQuestion.Answers = new List<AnswerViewModel>();
            question.Answers.Add(answer);
            question.Answers.Add(anotherAnswer);
            secondQuestion.Answers.Add(answerForSecondQuestion);
            secondQuestion.Answers.Add(anotherAnswerForSecondQuestion);

            quizViewModel.Questions.Add(question);
            quizViewModel.Questions.Add(secondQuestion);

            var questionInput = new QuestionInputModel()
            {
                QuestionText = "B??",
                QuizId = "123",
            };
            var firstNewAnswer = new AnswerInputModel()
            {
                AnswerText = "A",
                IsCorrectAnswer = false,
            };
            var secondNewAnswer = new AnswerInputModel()
            {
                AnswerText = "B",
                IsCorrectAnswer = true,
            };

            var secondQuestionInput = new QuestionInputModel()
            {
                QuestionText = "B??",
                QuizId = "123",
            };
            var firstNewAnswerForSecondQuestion = new AnswerInputModel()
            {
                AnswerText = "C",
                IsCorrectAnswer = false,
            };
            var secondNewAnswerForSecondQuestion = new AnswerInputModel()
            {
                AnswerText = "D",
                IsCorrectAnswer = false,
            };
            questionInput.Answers = new List<AnswerInputModel>();
            secondQuestionInput.Answers = new List<AnswerInputModel>();

            questionInput.Answers.Add(firstNewAnswer);
            questionInput.Answers.Add(secondNewAnswer);
            secondQuestionInput.Answers.Add(firstNewAnswerForSecondQuestion);
            secondQuestionInput.Answers.Add(secondNewAnswerForSecondQuestion);

            var quizInputModel = new QuizInputModel();
            quizInputModel.Questions = new List<QuestionInputModel>();
            quizInputModel.Questions.Add(questionInput);
            quizInputModel.Questions.Add(secondQuestionInput);

            var result = this.Service.CheckQuizResults(quizInputModel, quizViewModel);
            Assert.False(result.Item1);
            Assert.Equal(1, result.Item2);
        }

        [Fact]
        public async Task GetResultsCountShouldRetuirnCorrectResultsCount()
        {
            var result = new Result()
            {
                IsPassed = true,
                CorrectAnswers = 2,
            };

            this.DbContext.Results.Add(result);
            await this.DbContext.SaveChangesAsync();

            var resultsCount = this.Service.GetResultsCount();

            Assert.Equal(1, resultsCount);
        }

        [Fact]
        public async Task UserHasPassedQuizWithIdShouldReturnTrueIFUesrHasPassedTheQuiz()
        {
            var result = new Result()
            {
                IsPassed = true,
                CorrectAnswers = 2,
                QuizId = "123",
                ApplicationUserId = "ABC",
            };

            this.DbContext.Results.Add(result);
            await this.DbContext.SaveChangesAsync();

            var resultsCount = this.Service.UserHasPassedQuizWithId("123", "ABC");

            Assert.True(resultsCount);
        }

        [Fact]
        public async Task UserHasPassedQuizWithIdShouldReturnFalseIfUesrHasNotPassedTheQuiz()
        {
            var result = new Result()
            {
                IsPassed = false,
                CorrectAnswers = 2,
                QuizId = "123",
                ApplicationUserId = "ABC",
            };

            this.DbContext.Results.Add(result);
            await this.DbContext.SaveChangesAsync();

            var resultsCount = this.Service.UserHasPassedQuizWithId("123", "ABC");

            Assert.False(resultsCount);
        }

        //[Fact]
        //public async Task GetResultByIdShouldReturnCorrectResult()
        //{
        //    var quiz = new Quiz()
        //    {
        //        Name = "Test",
        //    };

        //    var result = new Result()
        //    {
        //        IsPassed = true,
        //        CorrectAnswers = 2,
        //        ApplicationUserId = "123",
        //        Trophies = 50,
        //        QuizId = quiz.Id,
        //    };

        //    this.DbContext.Quizes.Add(quiz);
        //    this.DbContext.Results.Add(result);
        //    await this.DbContext.SaveChangesAsync();

        //    var resultFromDb = await this.Service.GetResultById(result.Id);

        //    Assert.Equal(result.Id, resultFromDb.Id);
        //    Assert.Equal(result.IsPassed, resultFromDb.IsPassed);
        //    Assert.Equal(result.CorrectAnswers, resultFromDb.CorrectAnswers);
        //}
    }
}
