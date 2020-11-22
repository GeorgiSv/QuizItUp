namespace QuizItUp.Web.ViewModels.Quizes
{
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels.Questions;
    using System.Collections.Generic;

    public class QuizViewModel : IMapFrom<Quiz>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TimeToCompletePerQestion { get; set; }

        public int TotalTimeToComplete { get; set; }

        public int Trophies { get; set; }

        public bool IsPublished { get; set; }

        public ApplicationUser Creator { get; set; }

        public Badge Badge { get; set; }

        public Tag Tag { get; set; }

        public Picture Picture { get; set; }

        public Category Category { get; set; }

        public IList<QuestionViewModel> Questions { get; set; }
    }
}
