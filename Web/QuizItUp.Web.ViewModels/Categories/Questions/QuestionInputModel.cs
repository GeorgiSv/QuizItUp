namespace QuizItUp.Web.ViewModels.Questions
{
    using QuizItUp.Web.ViewModels.Answers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class QuestionInputModel
    {
        [Required]
        [MaxLength(300)]
        [MinLength(5)]
        public string QuestionText { get; set; }

        public string QuizId { get; set; }

        public string PictureId { get; set; }

        public string PicturePath { get; set; }

        public virtual IList<AnswerInputModel> Answers { get; set; }
    }
}
