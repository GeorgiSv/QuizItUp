namespace QuizItUp.Web.ViewModels.Quizes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using QuizItUp.Data.Models;
    using QuizItUp.Web.ViewModels.Questions;

    public class QuizInputModel : BaseQuizInputModel
    {
        public int Trophies { get; set; }

        public IFormFile Picture { get; set; }

        public string PicturePath{ get; set; }

        public string Tags{ get; set; }

        public Categories Category { get; set; }

        public IList<QuestionInputModel> Questions { get; set; }
    }
}
