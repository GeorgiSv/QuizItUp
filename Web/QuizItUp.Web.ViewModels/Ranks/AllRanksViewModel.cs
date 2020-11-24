namespace QuizItUp.Web.ViewModels.Ranks
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using QuizItUp.Services.Mapping;

    public class AllRanksViewModel 
    {
        public IList<RankViewModel> Ranks { get; set; }
    }
}
