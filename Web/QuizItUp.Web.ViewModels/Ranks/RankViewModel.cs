namespace QuizItUp.Web.ViewModels.Ranks
{
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class RankViewModel : IMapFrom<Rank>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int TrophiesNeeded { get; set; }

        public string Color { get; set; }

        public bool IsPublished { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
