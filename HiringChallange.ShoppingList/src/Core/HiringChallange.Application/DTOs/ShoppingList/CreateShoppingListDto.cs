
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringChallange.Application.DTOs.ShoppingList
{
    public class CreateShoppingListDto
    {
        public string Title { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string? Description { get; set; }
        public string CategoryId { get; set; }
        public string AppUserId { get; set; }
    }
}
