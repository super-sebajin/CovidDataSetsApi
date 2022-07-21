using AutoMapper;
using CovidDataSetsApi.DataAccessLayer;
using CovidDataSetsApi.Dto;

namespace CovidDataSetsApi.Mapping
{
    public class CovidDataSetsProfile : Profile
    {
        public CovidDataSetsProfile()
        {
            CreateMap<CovidDataSets, CovidDataSetsDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(dest => dest.DataSetName, opts => opts.MapFrom(x => x.DataSetName))
                .ForMember(dest => dest.DataSetPublicUrl, opts => opts.MapFrom(x => x.DataSetPublicUrl))
                .ForMember(dest => dest.DataSetPublicUrlHttpMethod, opts => opts.MapFrom(x => x.DataSetPublicUrlHttpMethod))
                .ForMember(dest => dest.DataSetProviderLongName, opts => opts.MapFrom(x => x.DataSetProviderLongName))
                .ForMember(dest => dest.DataSetProviderShortName, opts => opts.MapFrom(x => x.DataSetProviderShortName));

                
            CreateMap<CovidDataSetsDto, CovidDataSets>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(dest => dest.DataSetName, opts => opts.MapFrom(x => x.DataSetName))
                .ForMember(dest => dest.DataSetPublicUrl, opts => opts.MapFrom(x => x.DataSetPublicUrl))
                .ForMember(dest => dest.DataSetPublicUrlHttpMethod, opts => opts.MapFrom(x => x.DataSetPublicUrlHttpMethod))
                .ForMember(dest => dest.DataSetProviderLongName, opts => opts.MapFrom(x => x.DataSetProviderLongName))
                .ForMember(dest => dest.DataSetProviderShortName, opts => opts.MapFrom(x => x.DataSetProviderShortName));

        }
    }
}
