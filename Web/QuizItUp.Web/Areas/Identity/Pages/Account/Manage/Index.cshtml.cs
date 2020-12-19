namespace QuizItUp.Web.Areas.Identity.Pages.Account.Manage
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using QuizItUp.Common;
    using QuizItUp.Data.Models;
    using QuizItUp.Services;
    using QuizItUp.Services.Data.Contracts;

    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUsersService userService;
        private readonly Cloudinary cloudinary;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUsersService userService,
            Cloudinary cloudinary)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.userService = userService;
            this.cloudinary = cloudinary;
        }

        public string Username { get; set; }

        public string PictureUrl { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "FirstName")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
            [Display(Name = "LastName")]
            public string LastName { get; set; }

            [Display(Name = "Picture")]
            public IFormFile Picture { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await this._userManager.GetUserNameAsync(user);
            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            string userPicture = await this.userService.GetUserPicture(user.Id);

            this.Username = userName;
            this.PictureUrl = userPicture;

            this.Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            await this.LoadAsync(user);
            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.NotFound($"Unable to load user with ID '{this._userManager.GetUserId(this.User)}'.");
            }

            if (!this.ModelState.IsValid)
            {
                await this.LoadAsync(user);
                return this.Page();
            }

            string picturePath = string.Empty;
            if (this.Input.Picture != null)
            {
                if (!(this.Input.Picture.FileName.EndsWith(".png")
                || this.Input.Picture.FileName.EndsWith(".jpeg")
                || this.Input.Picture.FileName.EndsWith(".jpg")))
                {
                    this.ModelState.AddModelError("Picture", "Picture must be file with extension jpeg, jpg png");
                }

                if (this.Input.Picture.Length > 10 * 102 * 15024)
                {
                    this.ModelState.AddModelError("Picture", "Picture is too large - Max 10Mb");
                }

                if (!this.ModelState.IsValid)
                {
                    this.StatusMessage = "Ivalid picture";
                    return this.Page();
                }

                picturePath = await CloudinaryService
                    .UploadPicture(this.cloudinary, this.Input.Picture, user.Email, GlobalConstants.CloudinaryUsersFolder);

            }

            var phoneNumber = await this._userManager.GetPhoneNumberAsync(user);
            if (this.Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await this._userManager.SetPhoneNumberAsync(user, this.Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    this.StatusMessage = "Unexpected error when trying to set phone number.";
                    return this.RedirectToPage();
                }
            }

            await this.userService.UpdateUserAsync(this.Input.FirstName, this.Input.LastName, user.Id, picturePath);

            await this._signInManager.RefreshSignInAsync(user);
            this.StatusMessage = "Your profile has been updated";
            return this.RedirectToPage();
        }
    }
}
