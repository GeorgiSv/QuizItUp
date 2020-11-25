namespace QuizItUp.Web.ViewModels.Quizes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class EditQuizInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(2)]
        public string Description { get; set; }

        public int TimeToCompletePerQestion { get; set; }

        /// <summary>
        /// Time for completing the quiz  in seconds
        /// </summary>
        [Required]
        [Range(1, 120)]
        public int TotalTimeToComplete { get; set; }

    }
}
