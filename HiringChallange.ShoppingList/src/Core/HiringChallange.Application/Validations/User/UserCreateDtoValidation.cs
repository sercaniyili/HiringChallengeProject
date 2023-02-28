using FluentValidation;
using HiringChallange.Application.DTOs.User;

namespace HiringChallange.Application.Validations.User
{
    public class UserCreateDtoValidation : AbstractValidator<UserCreateDto>
    {
        public UserCreateDtoValidation()
        {
            RuleFor(x => x.Name)
                .Length(2, 30);
            RuleFor(x => x.Surname)
                .Length(2, 30);
            RuleFor(x => x.Username)
                .Length(2, 30)
                .NotEmpty()
                .WithMessage("Kullanıcı adını lütfen kurallara uygun giriniz");
            RuleFor(x => x.Email)
                .EmailAddress()
                .Length(10, 40)
                .WithMessage("Kullanıcı adını lütfen kurallara uygun giriniz");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre boş geçilemez");
        }
    }
}
