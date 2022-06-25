using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class BugProfile : Profile
    {
        public BugProfile()
        {
            CreateMap<Bug, BugDto>().ReverseMap();
        }
    }
}
