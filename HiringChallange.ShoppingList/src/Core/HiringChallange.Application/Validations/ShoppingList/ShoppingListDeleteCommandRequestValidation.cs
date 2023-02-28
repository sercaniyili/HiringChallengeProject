using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Application.Features.Commands.ShoppingList.ShoppingListDelete;

namespace HiringChallange.Application.Validations.ShoppingList
{
    public class ShoppingListDeleteCommandRequestValidation : AbstractValidator<ShoppingListDeleteCommandRequest>
    {
        public ShoppingListDeleteCommandRequestValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id boş geçilemez");
        }
    }
}
