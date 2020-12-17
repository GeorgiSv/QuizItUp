namespace QuizItUp.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuizItUp.Web.ViewModels.Categories;
    using QuizItUp.Web.ViewModels.Quizes;

    public interface ICategoriesService
    {
        Task<List<CategoryViewModel>> GetAllAsync();

        Task<CategoryViewModel> GetByIdAsync(int id);

        //Task Add(string title, string picture);

        Task<IList<QuizViewModel>> GetQuizesPerCategoryIdAsync(int categoryId);

    }
}
