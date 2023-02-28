namespace HiringChallange.Application.DTOs.ShoppingList
{
    public class UpdateShoppingListDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string CategoryId { get; set; }
    }
}
