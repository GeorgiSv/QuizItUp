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
        //[Range(2, 40)]
        public string Description { get; set; }

        [Required]
        //[Range(2, 40)]
        public int Trophies { get; set; }

        public int TimeToCompletePerQestion { get; set; }

        /// <summary>
        /// Time for completing the quiz  in seconds
        /// </summary>
        [Required]
        [Range(60, 3600)]
        public int TotalTimeToComplete { get; set; }

    }
}
