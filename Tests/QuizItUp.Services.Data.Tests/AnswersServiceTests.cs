namespace QuizItUp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.ViewModels.Answers;
    using Xunit;

    public class AnswersServiceTests : BaseBLTests
    {
        private IAnswersService Service => this.ServiceProvider.GetRequiredService<IAnswersService>();

        [Fact]
        public async Task GetAllAnswersPerQuestionShouldReturnAllAnswersPerQuestionCorrectly()
        {
            var question = new Question()
            {
                QuestionText = "Question test",
            };

            var answer = new Answer()
            {
                AnswerText = "A",
                QuestionId = question.Id,
                IsCorrectAnswer = true,
            };

            var anotherAnswer = new Answer()
            {
                AnswerText = "B",
                QuestionId = question.Id,
                IsCorrectAnswer = false,
            };

            question.Answers.Add(answer);
            question.Answers.Add(anotherAnswer);

            await this.DbContext.Questions.AddAsync(question);
            await this.DbContext.SaveChangesAsync();

            var allanswersPerQuestion = await this.Service.GetAllAnswersPerQuestionAsync(question.Id);

            Assert.Equal(2, allanswersPerQuestion.Count);
            Assert.Equal(answer.AnswerText, allanswersPerQuestion[0].AnswerText);
            Assert.Equal(answer.IsCorrectAnswer, allanswersPerQuestion[0].IsCorrectAnswer);
            Assert.Equal(answer.QuestionId, allanswersPerQuestion[0].QuestionId);
            Assert.Equal(anotherAnswer.AnswerText, allanswersPerQuestion[1].AnswerText);
            Assert.Equal(anotherAnswer.IsCorrectAnswer, allanswersPerQuestion[1].IsCorrectAnswer);
            Assert.Equal(anotherAnswer.QuestionId, allanswersPerQuestion[1].QuestionId);
        }

        [Fact]
        public async Task AnswerInputsToModelsShouldCorrectlyBindProperties()
        {
            var question = new Question()
            {
                QuestionText = "Question test",
            };

            var input = new List<AnswerInputModel>();
            var answerInput = new AnswerInputModel()
            {
                AnswerText = "QQQQ",
                QuestionId = question.Id,
                IsCorrectAnswer = false,
            };

            var anotherAnswerInput = new AnswerInputModel()
            {
                AnswerText = "BBB",
                QuestionId = question.Id,
                IsCorrectAnswer = true,
            };

            input.Add(answerInput);
            input.Add(anotherAnswerInput);
            var answers = this.Service.AnswerInputsToModels(input, question.Id).ToList();

            Assert.IsType<Answer>(answers[0]);
            Assert.IsType<Answer>(answers[1]);
        }

        [Fact]
        public async Task UpdateAnswersShouldUpdateAnswerCorrectly()
        {
            var question = new Question()
            {
                QuestionText = "Question test",
            };

            var answer = new Answer()
            {
                AnswerText = "A",
                QuestionId = question.Id,
                IsCorrectAnswer = true,
            };

            var anotherAnswer = new Answer()
            {
                AnswerText = "B",
                QuestionId = question.Id,
                IsCorrectAnswer = false,
            };

            question.Answers.Add(answer);
            question.Answers.Add(anotherAnswer);

            await this.DbContext.Questions.AddAsync(question);
            await this.DbContext.SaveChangesAsync();

            var input = new List<AnswerInputModel>();

            var answerInput = new AnswerInputModel()
            {
                AnswerText = "QQQQ",
                QuestionId = question.Id,
                IsCorrectAnswer = false,
            };

            var anotherAnswerInput = new AnswerInputModel()
            {
                AnswerText = "BBB",
                QuestionId = question.Id,
                IsCorrectAnswer = true,
            };

            input.Add(answerInput);
            input.Add(anotherAnswerInput);
            await this.Service.UpdateAnswers(input, question.Id);

            var answers = this.DbContext.Answers.Where(x => x.QuestionId == question.Id).ToList();

            Assert.Equal(answerInput.AnswerText, answers[0].AnswerText);
            Assert.Equal(answerInput.IsCorrectAnswer, answers[0].IsCorrectAnswer);
            Assert.Equal(anotherAnswerInput.AnswerText, answers[1].AnswerText);
            Assert.Equal(anotherAnswerInput.IsCorrectAnswer, answers[1].IsCorrectAnswer);
        }
    }
}
