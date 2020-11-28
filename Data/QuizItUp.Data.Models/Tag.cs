namespace QuizItUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using QuizItUp.Data.Common.Models;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.QuizTag = new HashSet<QuizTag>();
        }

        [Required]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<QuizTag> QuizTag { get; set; }
    }
}
