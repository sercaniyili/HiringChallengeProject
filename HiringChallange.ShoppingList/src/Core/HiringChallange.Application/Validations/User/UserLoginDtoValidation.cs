using FluentValidation;
using HiringChallange.Application.DTOs.User;

namespace HiringChallange.Application.Validations.User
{
    public class UserLoginDtoValidation : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress().MinimumLength(3).MaximumLength(30)
                .WithMessage("Kullanıcı e-posta adresinizi lütfen kurallara uygun giriniz");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre boş geçilemez");
        }

    }
}
