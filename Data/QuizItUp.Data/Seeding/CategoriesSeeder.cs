namespace QuizItUp.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using QuizItUp.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new Dictionary<string, string>();
            categories.Add("Movies", "https://res.cloudinary.com/dlely3oct/image/upload/v1607785078/categories/moviesCategory_dz2nwp.jpg");
            categories.Add("Games", "https://res.cloudinary.com/dlely3oct/image/upload/v1607785274/categories/games_hplxuj.jpg");
            categories.Add("Programming", "https://res.cloudinary.com/dlely3oct/image/upload/v1607785351/categories/programming_bwhvhv.jpg");
            categories.Add("History", "https://res.cloudinary.com/dlely3oct/image/upload/v1607784829/categories/sport_kneqa2.jpg");
            categories.Add("Music", "https://res.cloudinary.com/dlely3oct/image/upload/v1607785453/categories/1123676_eo6hoh.jpg");
            categories.Add("Sport", "https://res.cloudinary.com/dlely3oct/image/upload/v1607784918/categories/apple-music-note_ojhwuc.jpg");

            foreach (var category in categories)
            {
                var pic = new Picture()
                {
                    Url = category.Value,
                };
                await dbContext.Pictures.AddAsync(pic);

                await dbContext.Categories.AddAsync(new Category()
                {
                    Title = category.Key,
                    PictureId = pic.Id,
                });
            }
        }
    }
}
