using AutoMapper;
using DataProcessing.Api.Models;
using DataProcessing.Application.Models;

namespace DataProcessing.Api.Mapping
{
    /// <summary>
    /// AutoMapper profile discovered by assembly scan.
    /// </summary>
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<DataJobCreateInput, DataJobDto>();
            CreateMap<DataJobUpdateInput, DataJobDto>();
        }
    }
}
