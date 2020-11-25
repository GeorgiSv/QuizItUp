namespace QuizItUp.Web.ViewModels.Ranks
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class RankInputModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int TrophiesNeeded { get; set; }

        public string Color { get; set; }

        public bool IsPublished { get; set; }
    }
}
