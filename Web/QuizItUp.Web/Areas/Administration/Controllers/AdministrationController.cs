namespace QuizItUp.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Common;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Models;
    using QuizItUp.Services.Data.Contracts;
    using QuizItUp.Web.Controllers;
    using QuizItUp.Web.ViewModels.Administration.Dashboard;
    using System.Linq;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        //private IDeletableEntityRepository<Result> resultsRepository;
        //private IDeletableEntityRepository<ApplicationUser> usersRepository;
        //private IDeletableEntityRepository<Quiz> quizRepository;

        //public AdministrationController(
        //    IDeletableEntityRepository<Result> resultsRepository,
        //    IDeletableEntityRepository<ApplicationUser> usersRepository,
        //    IDeletableEntityRepository<Quiz> quizRepository)
        //{
        //    this.resultsRepository = resultsRepository;
        //    this.usersRepository = usersRepository;
        //    this.quizRepository = quizRepository;
        //}

        public IActionResult Items()
        {
            //var model = new IndexAdminViewModel()
            //{
            //    CreatedQuizesCount = this.quizRepository.AllAsNoTracking().Count(),
            //    PlayedGamesCount = this.resultsRepository.AllAsNoTracking().Count(),
            //    UsersCount = this.usersRepository.AllAsNoTracking().Count(),
            //};

            return this.View();
        }
    }
}
