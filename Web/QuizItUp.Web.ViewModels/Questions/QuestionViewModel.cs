namespace QuizItUp.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using QuizItUp.Data.Models;
    using QuizItUp.Services.Mapping;
    using QuizItUp.Web.ViewModels.Answers;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string QuestionText { get; set; }

        public string QuizId { get; set; }

        public string PictureId { get; set; }

        public string PicturePath { get; set; }

        public virtual IList<AnswerViewModel> Answers { get; set; }
    }
}
