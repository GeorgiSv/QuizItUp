namespace QuizItUp.Web.ViewModels.Results
{
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels.Quizes;

    public class ResultViewModel : IMapFrom<Result>
    {
        public string Id { get; set; }

        public int Trophies { get; set; }

        public QuizViewModel Quiz { get; set; }

        public bool IsPassed { get; set; }

        public int CorrectAnswers { get; set; }

        public string Passed
            => this.IsPassed ? "Passed" : "Failed";
    }
}
