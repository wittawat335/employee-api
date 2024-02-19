using AutoMapper;
using sample_api_mongodb.Core.DTOs;
using sample_api_mongodb.Core.Entities;
using System.Globalization;

namespace sample_api_mongodb.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
         

            CreateMap<UserDTO, Users>();


            CreateMap<Users, UserDTO>()
                    .ForMember(x => x.active, 
                    opt => opt.MapFrom(o => o.Active == true ? "1" : "0"));

            CreateMap<ApplicationUser, UserDTO>()
              .ForMember(x => x.active, 
              opt => opt.MapFrom(o => o.Active == true ? "1" : "0"));

            CreateMap<ApplicationUser, Users>()
                .ForMember(x => x.Id,
                opt => opt.MapFrom(origin => origin.Id.ToString()));


            CreateMap<UserDTO, ApplicationUser>()
               .ForMember(x => x.Roles, opt => opt.Ignore());

            CreateMap<Employee, EmployeeDTO>()
                .ForMember(
                _ => _.DateOfBirth,
                opt => opt.MapFrom(
                    origin => origin.DateOfBirth.ToString("yyyy-MM-dd")))
                .ForMember(
                _ => _.CreatedOn,
                opt => opt.MapFrom(
                    origin => origin.CreatedOn.ToString("yyyy-MM-dd")));

            CreateMap<EmployeeDTO, Employee>()
              .ForMember(
              _ => _.CreatedOn,
              opt => opt.MapFrom(
                  origin => DateTime.Now))
              .ForMember(
              _ => _.DateOfBirth,
              opt => opt.MapFrom(
                  origin => DateTime.Now));
        }
    }
}
