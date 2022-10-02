using AutoMapper;
using DataProcessing.Application.Models;
using DataProcessing.Domain.Entities;

namespace DataProcessing.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile discovered by assembly scan.
    /// </summary>
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<DataJob, DataJobDto>().ReverseMap().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<DataJobStatus, DataJobStatusDto>().ReverseMap();
            CreateMap<Link, LinkDto>().ReverseMap();
        }
    }
}
