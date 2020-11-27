namespace QuizItUp.Web.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using QuizItUp.Data.Models;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> CreateRole()
        {
            var result = this.roleManager.CreateAsync(new ApplicationRole("User"));

            if (!result.IsCompletedSuccessfully)
            {
                return this.BadRequest();
            }

            return this.Json("Ok");
        }
    }
}
