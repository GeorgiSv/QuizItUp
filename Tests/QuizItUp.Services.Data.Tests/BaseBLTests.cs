namespace QuizItUp.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using QuizItUp.Data;
    using QuizItUp.Data.Common.Repositories;
    using QuizItUp.Data.Repositories;
    using QuizItUp.Services.Data.Contracts;

    public abstract class BaseBLTests
    {
        protected BaseBLTests()
        {
            var services = this.SetServices();
            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddTransient<IQuizesService, QuizesService>();
            services.AddTransient<IQuestionsService, QuestionsService>();
            services.AddTransient<IAnswersService, AnswersService>();
            services.AddTransient<IResultsService, ResultsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ITagsService, TagsService>();
            services.AddTransient<IRanksService, RanksService>();
            services.AddTransient<IUsersService, UsersService>();

            //var context = new DefaultHttpContext();
            //services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }
    }
}
