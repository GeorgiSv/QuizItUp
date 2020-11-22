namespace QuizItUp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels;
    using QuizItUp.Web.ViewModels.Categories;
    using QuizItUp.Web.ViewModels.Quizes;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepo;
        private readonly IDeletableEntityRepository<Picture> pictureRepo;
        private readonly IDeletableEntityRepository<Quiz> quizRepo;

        public CategoriesService(IDeletableEntityRepository<Category> categoryDb, IDeletableEntityRepository<Picture> pictureDb, IDeletableEntityRepository<Quiz> quizDb)
        {
            this.categoryRepo = categoryDb;
            this.pictureRepo = pictureDb;
            this.quizRepo = quizDb;
        }

        public async Task Add(string title, string picture)
        {
            var newPicture = new Picture()
            {
                Url = picture,
            };

            var category = new Category()
            {
                Title = title,
                PictureId = newPicture.Id,
            };

            await this.pictureRepo.AddAsync(newPicture);
            await this.pictureRepo.SaveChangesAsync();

            await this.categoryRepo.AddAsync(category);
            await this.categoryRepo.SaveChangesAsync();
        }

        public List<CategoryViewModel> GetAll()
        {
            return this.categoryRepo.All()
                .To<CategoryViewModel>()
                .ToList();
        }

        public async Task<CategoryViewModel> GetById(int id)
        {
            return await this.categoryRepo.All()
                .Where(x => x.Id == id)
                .To<CategoryViewModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<QuizViewModel>> GetQuizesPerCategoryId(int categoryId)
        {
            var quizes = await this.quizRepo.All()
                .Where(x => x.CategoryId == categoryId && x.IsPublished == true)
                .To<QuizViewModel>()
                .ToListAsync();

            return quizes;
        }
    }
}
