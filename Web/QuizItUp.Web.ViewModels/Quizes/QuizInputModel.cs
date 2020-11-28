﻿namespace QuizItUp.Web.ViewModels.Quizes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using QuizItUp.Data.Models;
    using QuizItUp.Web.ViewModels.Questions;

    public class QuizInputModel
    {
        public string CreatorId { get; set; }

        [Required]
       // [Range(2, 40)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        [MinLength(2)]
        public string Description { get; set; }

        public int Trophies { get; set; }

        public int TimeToCompletePerQestion { get; set; }

        /// <summary>
        /// Time for completing the quiz  in seconds
        /// </summary>
        [Required]
        [Range(1, 120)]
        public int TotalTimeToComplete { get; set; }

        public IFormFile Picture { get; set; }

        public string PicturePath{ get; set; }

        public string Tags{ get; set; }

        public Categories Category { get; set; }

        public IList<QuestionInputModel> Questions { get; set; }
    }
}
