namespace QuizItUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizItUp.Data.Common.Models;

    public class Picture : BaseDeletableModel<string>
    {
        public Picture()
        {
            this.ApplicationUsers = new HashSet<ApplicationUser>();
            this.Badges = new HashSet<Badge>();
            this.Quizes = new HashSet<Quiz>();
            this.Categories = new HashSet<Category>();
            this.Questions = new HashSet<Question>();
            this.Answers = new HashSet<Answer>();
            this.Id = Guid.NewGuid().ToString();
        }

        public string Url { get; set; }

        public string Path { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }

        public ICollection<Badge> Badges { get; set; }

        public ICollection<Quiz> Quizes { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<Answer> Answers { get; set; }

    }
}
