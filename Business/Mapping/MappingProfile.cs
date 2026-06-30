using AutoMapper;
using Business.DTOs;
using Domain.Entities;

namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Category, CategoryDto>();
            CreateMap<Review, ReviewDto>();

            CreateMap<User, UserDto>();
        }
    }
}
