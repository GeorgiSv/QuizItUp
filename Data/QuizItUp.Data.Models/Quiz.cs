namespace QuizItUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using QuizItUp.Data.Common.Models;

    public class Quiz : BaseDeletableModel<string>
    {
        public Quiz()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Questions = new HashSet<Question>();
            this.QuizTags = new HashSet<QuizTag>();
        }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        public int TimeToCompletePerQestion { get; set; }

        public int TotalTimeToComplete { get; set; }

        public bool IsPublished { get; set; }

        public int Trophies { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual ApplicationUser Creator { get; set; }

        public string BadgeId { get; set; }

        public virtual Badge Badge { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<QuizTag> QuizTags { get; set; }
    }
}
