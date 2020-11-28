namespace QuizItUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizItUp.Data.Common.Models;

    public class QuizTag : BaseDeletableModel<string>
    {
        public QuizTag()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }

        public string QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
    }
}
