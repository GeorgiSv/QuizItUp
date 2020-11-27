namespace QuizItUp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Common;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels.Quizes;

    public class QuizesService : IQuizesService
    {
        private readonly IDeletableEntityRepository<Quiz> quizesRepo;
        private readonly IDeletableEntityRepository<Category> categoriesRepo;
        private readonly IDeletableEntityRepository<Question> questionsRepo;

        public QuizesService(
            IDeletableEntityRepository<Quiz> quizesRepo,
            IDeletableEntityRepository<Category> categoriesRepo,
            IDeletableEntityRepository<Question> questionsRepo)
        {
            this.quizesRepo = quizesRepo;
            this.categoriesRepo = categoriesRepo;
            this.questionsRepo = questionsRepo;
        }

        public async Task<string> CreateQuizAsync(QuizInputModel input)
        {
            var category = this.categoriesRepo.All().Where(x => x.Title == input.Category.ToString()).FirstOrDefault();

            if (category == null)
            {
                throw new Exception("Category does not exist!");
            }

            var picture = new Picture() { Url = input.PicturePath };

            if (input.PicturePath == null)
            {
                picture.Url = GlobalConstants.DefaultPicturePath;
            }

            var quiz = new Quiz
            {
                CreatorId = input.CreatorId,
                Name = input.Name,
                Description = input.Description,
                Picture = picture,
                TotalTimeToComplete = input.TotalTimeToComplete,
                CategoryId = category.Id,
                Trophies = GlobalConstants.InitialQuizTrophies,
                Tag = new Tag() { CategoryId = category.Id, Title = input.TagTitle },
            };

            await this.quizesRepo.AddAsync(quiz);
            await this.quizesRepo.SaveChangesAsync();

            return quiz.Id;
        }

        public async Task<ICollection<QuizViewModel>> GetAllQuizesAsync()
             => await this.quizesRepo
            .All()
            .To<QuizViewModel>()
            .ToListAsync();

        public async Task<QuizViewModel> GetQuizByIdAsync(string quizId)
             => await this.quizesRepo
            .All()
            .Where(x => x.Id == quizId)
            .To<QuizViewModel>()
            .FirstOrDefaultAsync();

        public async Task<string> RemoveQuizAsync(string quizId)
        {
            var quiz = await this.quizesRepo
                .All()
                .Where(x => x.Id == quizId)
                .FirstOrDefaultAsync();

            this.quizesRepo.Delete(quiz);
            await this.quizesRepo.SaveChangesAsync();

            return quiz.Id;
        }

        public async Task<int> PublishAsync(string quizId)
        {
            var quiz = await this.quizesRepo
                .All()
                .Where(x => x.Id == quizId)
                .FirstOrDefaultAsync();

            quiz.IsPublished = true;

            this.quizesRepo.Update(quiz);
            await this.quizesRepo.SaveChangesAsync();

            return quiz.CategoryId;
        }

        public async Task<int> UnPublishAsync(string quizId)
        {
            var quiz = await this.quizesRepo
                .All()
                .Where(x => x.Id == quizId)
                .FirstOrDefaultAsync();

            quiz.IsPublished = false;

            this.quizesRepo.Update(quiz);
            await this.quizesRepo.SaveChangesAsync();

            return quiz.CategoryId;
        }

        public async Task<string> UpdateQuizAsync(EditQuizInputModel input, string quizId)
        {
            var quiz = await this.quizesRepo
                .All()
                .Where(x => x.Id == quizId)
                .FirstOrDefaultAsync();

            quiz.Name = input.Name;
            quiz.Description = input.Description;
            quiz.TotalTimeToComplete = input.TotalTimeToComplete;

            this.quizesRepo.Update(quiz);
            await this.quizesRepo.SaveChangesAsync();

            return quiz.Id;
        }

        public async Task UpdateQuizTrophiesAsync(string quizId)
        {
            var quiz = await this.quizesRepo
               .All()
               .Where(x => x.Id == quizId)
               .FirstOrDefaultAsync();

            var questions = this.questionsRepo.All().Where(x => x.QuizId == quizId).Count();
            quiz.Trophies = GlobalConstants.InitialQuizTrophies + questions;

            this.quizesRepo.Update(quiz);
            await this.quizesRepo.SaveChangesAsync();
        }

        public async Task Random(string quizId)
        {
            var quiz = await this.quizesRepo
               .All()
               .Where(x => x.Id == quizId)
               .FirstOrDefaultAsync();
        }
    }
}
