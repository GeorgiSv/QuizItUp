namespace QuizItUp.Web.ViewModels.Quizes
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class IndexQuizViewModel
    {
        public IEnumerable<QuizViewModel> Quizes { get; set; }
    }
}
