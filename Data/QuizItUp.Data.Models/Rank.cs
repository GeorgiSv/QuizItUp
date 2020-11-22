using QuizItUp.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizItUp.Data.Models
{
    public class Rank : BaseDeletableModel<string>
    {
        public Rank()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public int TrophiesNeeded { get; set; }

        public string Color { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
