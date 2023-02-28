using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiringChallange.Domain.Entities;

namespace HiringChallange.Application.DTOs.ShoppingList
{
    public class GetAllShoppingListDto
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreateDate { get; set; }
        public  List<Product>? Products { get; set; }

    }
}
