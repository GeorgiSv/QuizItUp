namespace QuizItUp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using QuizItUp.Data.Common.Models;

    public class Answer : BaseDeletableModel<string>
    {
        public Answer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(150)]
        public string AnswerText { get; set; }

        [Required]
        public string QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        [Required]
        public bool IsCorrectAnswer { get; set; }
    }
}
