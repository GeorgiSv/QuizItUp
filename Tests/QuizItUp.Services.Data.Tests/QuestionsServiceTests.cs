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
    using Xunit;

    public class QuestionsServiceTests : BaseBLTests
    {
        private IQuestionsService Service => this.ServiceProvider.GetRequiredService<IQuestionsService>();

        [Fact]
        public async Task AddQuestionShouldAddQuestionInQuizCorrectly()
        {
            var model = new IndexQuestionViewModel();
            model.QuestionInputModel = new QuestionInputModel()
            {
                QuestionText = "A??",
                QuizId = "123",
            };

            var firstAnswer = new AnswerInputModel()
            {
                AnswerText = "true",
                IsCorrectAnswer = true,
            };
            var secondAnswer = new AnswerInputModel()
            {
                AnswerText = "false",
                IsCorrectAnswer = false,
            };

            model.AnswersInputModel = new List<AnswerInputModel>();
            model.AnswersInputModel.Add(firstAnswer);
            model.AnswersInputModel.Add(secondAnswer);

            var questionIdResult = await this.Service.AddQuestionAsync(model);
            var questionFromDb = this.DbContext.Questions.FirstOrDefault();

            Assert.NotNull(questionFromDb);
            Assert.Equal(questionFromDb.Id, questionIdResult);
        }

        [Fact]
        public async Task UpdateQuestionShouldUpdateQuestionInQuizCorrectly()
        {
            var quizId = "123";
            var model = new IndexQuestionViewModel();
            model.QuestionInputModel = new QuestionInputModel()
            {
                QuestionText = "A??",
                QuizId = quizId,
            };


            var firstAnswer = new AnswerInputModel()
            {
                AnswerText = "true",
                IsCorrectAnswer = true,
            };
            var secondAnswer = new AnswerInputModel()
            {
                AnswerText = "false",
                IsCorrectAnswer = false,
            };
            model.AnswersInputModel = new List<AnswerInputModel>();
            model.AnswersInputModel.Add(firstAnswer);
            model.AnswersInputModel.Add(secondAnswer);

            var questionIdResult = await this.Service.AddQuestionAsync(model);

            var editModel = new IndexQuestionViewModel();
            editModel.QuestionInputModel = new QuestionInputModel()
            {
                QuestionText = "B??",
                QuizId = quizId,
            };
            var firstNewAnswer = new AnswerInputModel()
            {
                AnswerText = "false",
                IsCorrectAnswer = false,
            };
            var secondNewAnswer = new AnswerInputModel()
            {
                AnswerText = "true",
                IsCorrectAnswer = true,
            };
            editModel.AnswersInputModel = new List<AnswerInputModel>();
            editModel.AnswersInputModel.Add(firstNewAnswer);
            editModel.AnswersInputModel.Add(secondNewAnswer);

            var quizIdResult = await this.Service.UpdateQuestionAsync(editModel, questionIdResult);

            var updatedQuestion = this.DbContext.Questions.FirstOrDefault();

            Assert.Equal(quizId, quizIdResult);
            Assert.NotEqual(model.QuestionInputModel.QuestionText, updatedQuestion.QuestionText);
            Assert.NotEqual(firstAnswer.AnswerText, updatedQuestion.Answers.FirstOrDefault().AnswerText);
            Assert.NotEqual(firstAnswer.IsCorrectAnswer, updatedQuestion.Answers.FirstOrDefault().IsCorrectAnswer);
        }

        [Fact]
        public async Task RemoveQuestionShouldRemoveCorectly()
        {
            var question = new Question()
            {
                QuestionText = "A??",
                QuizId = "123",
            };

            this.DbContext.Questions.Add(question);
            await this.DbContext.SaveChangesAsync();

            var quizId = await this.Service.RemoveQuestionAsync(question.Id);

            var questionInDb = this.DbContext.Questions.FirstOrDefault();

            Assert.Null(questionInDb);
            Assert.Equal("123", quizId);
        }

        [Fact]
        public async Task GetQuestionByIdShouldRemoveCorrectQuiz()
        {
            var question = new Question()
            {
                QuestionText = "A??",
                QuizId = "123",
            };

            this.DbContext.Questions.Add(question);
            await this.DbContext.SaveChangesAsync();

            var questionViwwModel = await this.Service.GetQuestionByIdAsync(question.Id);

            Assert.Equal(question.QuizId, questionViwwModel.QuizId);
            Assert.Equal(question.QuestionText, questionViwwModel.QuestionText);
        }

        [Fact]
        public async Task GetQuestionByQuizIdShouldRemoveCorrectQuiz()
        {
            var question = new Question()
            {
                QuestionText = "A??",
                QuizId = "123",
            };

            this.DbContext.Questions.Add(question);
            await this.DbContext.SaveChangesAsync();

            var questionViwwModel = await this.Service.GetQuestionByQuizIdAsync(question.QuizId);

            Assert.Equal(question.QuizId, questionViwwModel.QuizId);
            Assert.Equal(question.QuestionText, questionViwwModel.QuestionText);
        }

        [Fact]
        public async Task GetAllQuestionsPerQuizShouldReturnONlyByQuizIdQuestions()
        {
            var question = new Question()
            {
                QuestionText = "A??",
                QuizId = "123",
            };

            var secondQuestion = new Question()
            {
                QuestionText = "B?",
                QuizId = "123",
            };

            this.DbContext.Questions.Add(question);
            this.DbContext.Questions.Add(secondQuestion);
            await this.DbContext.SaveChangesAsync();

            var questionViwwModel = await this.Service.GetAllQuestionsPerQuizAsync(question.QuizId);

            Assert.Equal(2, questionViwwModel.Count);
            Assert.Equal(question.QuestionText, questionViwwModel[0].QuestionText);
            Assert.Equal(question.QuizId, questionViwwModel[0].QuizId);
            Assert.Equal(secondQuestion.QuestionText, questionViwwModel[1].QuestionText);
            Assert.Equal(secondQuestion.QuizId, questionViwwModel[1].QuizId);
        }
    }
}
