namespace QuizItUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using QuizItUp.Data.Common.Models;

    public class Badge : BaseDeletableModel<string>
    {
        public Badge()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ApplicationUsers = new HashSet<ApplicationUserBadge>();
        }

        [Required]
        public string Title { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual ICollection<ApplicationUserBadge> ApplicationUsers { get; set; }
    }
}
