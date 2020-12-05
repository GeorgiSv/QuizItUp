namespace QuizItUp.Web.ViewModels.Ranks
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizItUp.Data.Models;

    public class UserRankingViewModel
    {
        public IList<UserViewModel> Users { get; set; }
    }
}
