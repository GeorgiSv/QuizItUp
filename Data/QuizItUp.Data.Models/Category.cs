namespace QuizItUp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using QuizItUp.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Tags = new HashSet<Tag>();
            this.Quizes = new HashSet<Quiz>();
        }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Quiz> Quizes { get; set; }
    }
}
