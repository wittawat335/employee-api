using AutoMapper;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;

namespace sample_api_mongodb.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Products, ProductDTO>();
            CreateMap<ProductDTO, Products>().ReverseMap();
            CreateMap<UserDTO, Users>();
            CreateMap<Users, UserDTO>();
            CreateMap<ApplicationUser, Users>()
                .ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()));

            CreateMap<UserDTO, ApplicationUser>()
               .ForMember(x => x.Roles, opt => opt.Ignore());
        }
    }
}
