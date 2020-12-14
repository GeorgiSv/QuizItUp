namespace QuizItUp.Web.Infrastructure.Filters
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using QuizItUp.Common;
    using QuizItUp.Services.Data.Contracts;

    public class UserValidationAttribute : IAsyncActionFilter
    {
        private readonly IQuizesService quizService;

        public UserValidationAttribute(IQuizesService quizService)
        {
            this.quizService = quizService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.ContainsKey("id"))
            {
                var quizId = context.ActionArguments["id"].ToString();
                var quiz = await this.quizService.GetQuizByIdAsync(quizId);

                if (context.HttpContext.User.Identity.Name != quiz.Creator.UserName && !context.HttpContext.User.IsInRole(GlobalConstants.AdministratorRoleName))
                {
                    var controller = (Controller)context.Controller;
                    context.Result = controller.Redirect("/");
                }
                else
                {
                    await next();
                }
            }

            // base.OnActionExecuting(context);
        }
    }
}
