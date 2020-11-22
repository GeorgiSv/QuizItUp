namespace QuizItUp.Web.ViewModels.Answers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class AnswerInputModel
    {
        [Required]
        [MaxLength(300)]
        [MinLength(1)]
        public string AnswerText { get; set; }

        public int Index { get; set; }

        [Required]
        public bool IsCorrectAnswer { get; set; }

        public string QuestionId { get; set; }

        public string PictureId { get; set; }

        public string PicturePath { get; set; }
    }
}
