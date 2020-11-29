namespace QuizItUp.Web.ViewModels.Quizes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class EditQuizInputModel : BaseQuizInputModel
    {
        public string CurrentUserId { get; set; }
    }
}
