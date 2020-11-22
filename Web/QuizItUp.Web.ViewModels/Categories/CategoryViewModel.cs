namespace QuizItUp.Web.ViewModels.Categories
{
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Picture Picture { get; set; }
    }
}
