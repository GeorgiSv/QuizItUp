namespace QuizItUp.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizItUp.Data.Models;
    using QuizItUp.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Trophies { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual Rank Rank { get; set; }

        //public virtual ICollection<Quiz> Quizes { get; set; }

        //public virtual ICollection<ApplicationUserBadge> Badges { get; set; }
    }
}
