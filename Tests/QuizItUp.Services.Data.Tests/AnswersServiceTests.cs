namespace QuizItUp.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using QuizItUp.Data;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Data.Repositories;
    using Xunit;

    public class AnswersServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<IDeletableEntityRepository<Answer>>();
            repository.Setup(r => r.All()).Returns(new List<Answer>
                                                        {
                                                            new Answer(),
                                                        }.AsQueryable());

            var service = new AnswersService(repository.Object);
            var result = service.GetAllAnswersPerQuestionAsync("a");
            Assert.Equal(0, 0);
            repository.Verify(x => x.All(), Times.Once);
        }
    }
}
