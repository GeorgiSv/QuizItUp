namespace QuizItUp.Web.ViewModels.Quizes
{
    using System.ComponentModel.DataAnnotations;

    public class BaseQuizInputModel
    {
        public string CreatorId { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(2)]
        public string Description { get; set; }

        /// <summary>
        /// Time for completing the quiz  in seconds.
        /// </summary>
        public int TimeToCompletePerQestion { get; set; }

        /// <summary>
        /// Time for completing the quiz  in minuets.
        /// </summary>
        [Required]
        [Range(1, 120)]
        public int TotalTimeToComplete { get; set; }

    }
}
