namespace QuizItUp.Data.Models
{
    using QuizItUp.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Tag : BaseDeletableModel<int>
    {
        public Tag()
        {
            this.Quizes = new HashSet<Quiz>();
        }

        [Required]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public ICollection<Quiz> Quizes { get; set; }
    }
}
