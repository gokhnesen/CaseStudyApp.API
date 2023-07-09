using AutoMapper;
using CaseStudyApp.API.Dtos;
using CaseStudyApp.API.Entities;

namespace CaseStudyApp.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Note, NoteDto>().ReverseMap();
        }
    }
}
