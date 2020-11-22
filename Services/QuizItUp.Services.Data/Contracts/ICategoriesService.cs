namespace QuizItUp.Services.Data.Contracts
{
    using QuizItUp.Web.ViewModels.Categories;
    using QuizItUp.Web.ViewModels.Quizes;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        List<CategoryViewModel> GetAll();

        Task<CategoryViewModel> GetById(int id);

        Task Add(string title, string picture);

        Task<IEnumerable<QuizViewModel>> GetQuizesPerCategoryId(int categoryId);

    }
}
