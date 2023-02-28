using AutoMapper;
using HiringChallange.Application.DTOs.Products;
using HiringChallange.Application.DTOs.ShoppingList;
using HiringChallange.Application.DTOs.User;
using HiringChallange.Domain.Entities;
using HiringChallange.Domain.Entities.Identity;

namespace HiringChallange.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            #region User

            CreateMap<AppUser, UserCreateDto>().ReverseMap();
            CreateMap<AppUser, UserLoginDto>().ReverseMap();

            #endregion


            #region ShoppingList

            CreateMap<ShoppingList, GetAllShoppingListDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
               .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products)).ReverseMap();


            CreateMap<ShoppingList, GetByParameterShoppingListDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products)).ReverseMap();


            CreateMap<ShoppingList, CreateShoppingListDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id))
            .ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.AppUser.Id)).ReverseMap();


            CreateMap<ShoppingList, UpdateShoppingListDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id)).ReverseMap();

            CreateMap<ShoppingList, UpdateIsCompleteDto>().ReverseMap();

            #endregion


            #region Product

            CreateMap<Product, AddProductDto>()
             .ForMember(dest => dest.ShoppingListId, opt => opt.MapFrom(src => src.ShoppingListId)).ReverseMap();

            #endregion

        }
    }
}
