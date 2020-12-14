namespace QuizItUp.Web.ViewModels.Questions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizItUp.Web.ViewModels.Answers;
    using QuizItUp.Web.ViewModels.Quizes;

    public class IndexQuestionViewModel
    {
        public QuizViewModel QuizViewModel { get; set; }

        public QuestionInputModel QuestionInputModel { get; set; }

        public QuestionViewModel QuestionViewModel { get; set; }

        public ICollection<AnswerInputModel> AnswersInputModel { get; set; }

        public AnswerInputModel Answer { get; set; }
    }
}
