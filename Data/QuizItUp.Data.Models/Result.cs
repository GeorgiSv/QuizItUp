namespace QuizItUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using QuizItUp.Data.Common.Models;

    public class Result : BaseDeletableModel<string>
    {
        public Result()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        // participant
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        public string QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }

        public int Trophies { get; set; }

        public bool IsPassed { get; set; }

        public int CorrectAnswers { get; set; }
    }
}
