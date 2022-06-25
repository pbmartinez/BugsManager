using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(g => g.Bugs, options => options.MapFrom(f => f.Bugs))
                .ReverseMap();
        }
    }
}
