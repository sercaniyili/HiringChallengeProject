using FluentValidation;
using HiringChallange.Application.DTOs.ShoppingList;

namespace HiringChallange.Application.Validations.ShoppingList
{
    public class UpdateShoppingListDtoValidation : AbstractValidator<UpdateShoppingListDto>
    {
        public UpdateShoppingListDtoValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(" Id boş geçilemez");
            RuleFor(x => x.Title)
              .Length(2, 30).WithMessage("Başlık geçerli uzunlukta değil")
              .NotEmpty().WithMessage("Başlık boş geçilemez");
            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("Açıklama 100 karakterden uzun olamaz");
            RuleFor(x => x.CategoryId)
               .NotEmpty().WithMessage("Kategori Id boş geçilemez");
        }
    }
}
