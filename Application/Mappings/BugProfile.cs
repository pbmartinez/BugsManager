using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BugProfile : Profile
    {
        public BugProfile()
        {
            CreateMap<Bug, BugDto>()
                .ForMember(dest=> dest.UserFullName, option => option.MapFrom(src => 
                    src.User == null ? "": $"{src.User.Name} {src.User.SurName}"))               
                .ForMember(dest=> dest.ProjectName, option => option.MapFrom(src => 
                    src.Project == null ? "":src.Project.Name))
                .ReverseMap();
        }
    }
}
