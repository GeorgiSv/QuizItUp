namespace QuizItUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using QuizItUp.Data.Common.Models;

    public class Question : BaseDeletableModel<string>
    {
        public Question()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Answers = new HashSet<Answer>();
        }

        public string QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        [Required]
        [MaxLength(150)]
        public string QuestionText { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
