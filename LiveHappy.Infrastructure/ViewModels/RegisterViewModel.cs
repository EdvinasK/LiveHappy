using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveHappy.Infrastructure.ViewModels
{
    public class RegisterViewModel
    {
        //[Required]
        //[EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Email can not be empty")
                .EmailAddress()
                .WithMessage("Email has to contain email format: sample@sample.sample");
            // https://www.apress.com/br/blog/all-blog-posts/complex-validation-rules/15067980
            // http://www.tugberkugurlu.com/archive/check-instantly-if-username-exists-asp-net-mvc-remote-validation

            //// guitar name cannot be null, empty, or whitespace and

            //// must be at least 3 but no more than 40 characters long

            //RuleFor(guitar => guitar.Name).NotEmpty().Length(3, 40);

            //// guitar must have a body

            //RuleFor(guitar => guitar.Body).NotEmpty();

            //// guitar must have a pickup installed in each slot available in the selected body

            //RuleFor(guitar => guitar.NeckPickup).NotEmpty().When(guitar => guitar.Body.AllowNeckPickup);

            //RuleFor(guitar => guitar.BridgePickup).NotEmpty().When(guitar => guitar.Body.AllowBridePickup);

            //RuleFor(guitar => guitar.MiddlePickup).NotEmpty().When(guitar => guitar.Body.AllowMiddlePickup);

            //// can't select more strings then guitar body supports

            //RuleFor(guitar => guitar.Strings)

            //    .NotNull()

            //    .Must((guitar, strings) => strings?.Count == guitar?.Body?.NumberOfStringsSupported)

            //    .WithMessage(@"The number of strings selected {0}

            //                                    does not match the number supported by the guitar body {1}",

            //    guitar => guitar?.Strings?.Count,

            //    guitar => guitar?.Body?.NumberOfStringsSupported);

            //// can't add a middle pickup if the guitar body does not support it

            //RuleFor(guitar => guitar.MiddlePickup)

            //    .Null()

            //    .When(guitar => guitar.Body.AllowMiddlePickup = false);
        }
    }
}
