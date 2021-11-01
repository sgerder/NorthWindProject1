using AutoMapper;
using EfEx.Domain;

namespace WebApplication.ViewModels.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CreateCategoryViewModel, Category>();
        }
    }
}