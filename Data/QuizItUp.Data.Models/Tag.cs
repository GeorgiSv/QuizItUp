namespace QuizItUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using QuizItUp.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.QuizTags = new HashSet<QuizTag>();
        }

        [Required]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<QuizTag> QuizTags { get; set; }
    }
}
