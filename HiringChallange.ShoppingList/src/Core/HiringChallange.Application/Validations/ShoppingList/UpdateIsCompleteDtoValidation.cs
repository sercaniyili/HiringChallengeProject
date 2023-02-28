using FluentValidation;
using HiringChallange.Application.DTOs.ShoppingList;

namespace HiringChallange.Application.Validations.ShoppingList
{
    public class UpdateIsCompleteDtoValidation : AbstractValidator<UpdateIsCompleteDto>
    {
        public UpdateIsCompleteDtoValidation()
        {
            RuleFor(x => x.IsComplete)
                .NotEmpty().WithMessage("Tamamlanma alanı boş geçilemez");
            RuleFor(x => x.CompleteDate)
                .NotEmpty().WithMessage("Tamamlanma alanı boş geçilemez");
        }
    }
}
