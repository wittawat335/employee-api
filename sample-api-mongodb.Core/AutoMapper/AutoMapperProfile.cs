using AutoMapper;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            //CreateMap<Products, ProductDTO>();
            CreateMap<ProductDTO, Products>().ReverseMap();
            CreateMap<UserDTO, Users>().ReverseMap();
            CreateMap<ApplicationUser, Users>()
                .ForMember(x => x.Id, opt => opt.MapFrom(origin => origin.Id.ToString()));
        }
    }
}
