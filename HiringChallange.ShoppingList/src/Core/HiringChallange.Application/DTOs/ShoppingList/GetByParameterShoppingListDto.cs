using HiringChallange.Domain.Entities;

namespace HiringChallange.Application.DTOs.ShoppingList
{
    public class GetByParameterShoppingListDto
    {
        public string CategoryName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Product>? Products { get; set; }
    }
}
