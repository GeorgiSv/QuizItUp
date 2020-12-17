namespace QuizItUp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using Xunit;

    public class TagsServiceTests : BaseBLTests
    {
        private ITagsService Service => this.ServiceProvider.GetRequiredService<ITagsService>();

        [Fact]
        public async Task GetResultByIdShouldReturnCorrectResult()
        {
            var category = new Category()
            {
                Title = "TestCategory",
            };

            var quiz = new Quiz()
            {
                Name = "TestQuiz",
                CategoryId = 1,
            };

            this.DbContext.Categories.Add(category);
            this.DbContext.Quizes.Add(quiz);
            await this.DbContext.SaveChangesAsync();

            var quizTag = await this.Service.CreateTagsAsync("tag, newtag", quiz.Id, 1, quiz);

            Assert.Equal("tag", quizTag.FirstOrDefault().Tag.Title);
            Assert.Equal(2, quizTag.Count);
        }

        [Fact]
        public async Task GetAllWithTitleShoultReturnAllQuizesWithThatTag()
        {
            var quizTag = new QuizTag()
            {
                Tag = new Tag()
                {
                    Title = "tag",
                },
                Quiz = new Quiz()
                {
                    Name = "TestQuiz",
                    CategoryId = 1,
                    IsPublished = true,
                },
            };

            var otherQuizTag = new QuizTag()
            {
                Tag = new Tag()
                {
                    Title = "gat",
                },
                Quiz = new Quiz()
                {
                    Name = "TestQuiz1",
                    CategoryId = 1,
                    IsPublished = true,
                },
            };

            this.DbContext.QuizTag.Add(quizTag);
            this.DbContext.QuizTag.Add(otherQuizTag);
            await this.DbContext.SaveChangesAsync();

            var quizes = await this.Service.GetAllWithTitleAsync("tag");

            Assert.Single(quizes);
        }

        [Fact]
        public async Task GetAllTagsShoultReturnAllTagsCorrectly()
        {
            var tag = new Tag()
            {
                Title = "TestTag",
            };

            this.DbContext.Tags.Add(tag);
            await this.DbContext.SaveChangesAsync();

            var tags = await this.Service.GetAllTagsAsync();

            Assert.Single(tags);
            Assert.Equal(tag.Title, tags.FirstOrDefault().Title);
        }

        [Fact]
        public async Task GetAllQuizTagsShoultReturnAllQuizTagsCorrectly()
        {
            var quizTag = new QuizTag()
            {
                TagId = 1,
                QuizId = "ABC",
            };

            this.DbContext.QuizTag.Add(quizTag);
            await this.DbContext.SaveChangesAsync();

            var tags = await this.Service.GetAllQuizTagsAsync();

            Assert.Single(tags);
            Assert.Equal(1, quizTag.TagId);
            Assert.Equal("ABC", quizTag.QuizId);
        }
    }
}
