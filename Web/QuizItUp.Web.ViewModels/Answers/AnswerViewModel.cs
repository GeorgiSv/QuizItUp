namespace QuizItUp.Web.ViewModels.Answers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizItUp.Data.Models;
    using QuizItUp.Services.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public string Id { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrectAnswer { get; set; }

        public string QuestionId { get; set; }

        public string PictureId { get; set; }

        public string PicturePath { get; set; }
    }
}
