﻿namespace QuizItUp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizItUp.Data.Common.Models;

    public class Rank : BaseDeletableModel<string>
    {
        public Rank()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public int TrophiesNeeded { get; set; }

        public string Color { get; set; }

        public bool IsPublished { get; set; }

        public ICollection<ApplicationUser> ApplicationUser { get; set; }
    }
}
