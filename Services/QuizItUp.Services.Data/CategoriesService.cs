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

        //public async Task Add(string title, string picture)
        //{
        //    var newPicture = new Picture()
        //    {
        //        Url = picture,
        //    };

        //    var category = new Category()
        //    {
        //        Title = title,
        //        PictureId = newPicture.Id,
        //    };

        //    await this.pictureRepo.AddAsync(newPicture);
        //    await this.pictureRepo.SaveChangesAsync();

        //    await this.categoryRepo.AddAsync(category);
        //    await this.categoryRepo.SaveChangesAsync();
        //}

        public async Task<List<CategoryViewModel>> GetAllAsync()
            => await this.categoryRepo.All()
                .To<CategoryViewModel>()
                .ToListAsync();

        public async Task<CategoryViewModel> GetByIdAsync(int id)
            => await this.categoryRepo.All()
                .Where(x => x.Id == id)
                .To<CategoryViewModel>()
                .FirstOrDefaultAsync();

        public async Task<IList<QuizViewModel>> GetQuizesPerCategoryIdAsync(int categoryId)
            => await this.quizRepo.All()
                  .Where(x => x.CategoryId == categoryId && x.IsPublished == true)
                  .To<QuizViewModel>()
                  .ToListAsync();
    }
}
