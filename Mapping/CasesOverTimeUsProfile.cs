using AutoMapper;
using CovidDataSetsApi.DataAccessLayer;
using CovidDataSetsApi.Dto;

namespace CovidDataSetsApi.Mapping
{
    public class CasesOverTimeUsProfile : Profile
    {
        public CasesOverTimeUsProfile()
        {
            CreateMap<CovidCasesOverTimeUsa, CasesOvertimeUsDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(dest => dest.DateStamp, opts => opts.MapFrom(x => x.DateStamp))
                .ForMember(dest => dest.CountConfirmed, opts => opts.MapFrom(x => x.CountConfirmed))
                .ForMember(dest => dest.CountDeath, opts => opts.MapFrom(x => x.CountDeath))
                .ForMember(dest => dest.CountRecovered, opts => opts.MapFrom(x => x.CountRecovered));

            CreateMap<CasesOvertimeUsDto, CovidCasesOverTimeUsa>()
                .ForMember(dest => dest.Id, opts => opts.UseDestinationValue())
                .ForMember(dest => dest.DateStamp, opts => opts.MapFrom(x => x.DateStamp))
                .ForMember(dest => dest.CountConfirmed, opts => opts.MapFrom(x => x.CountConfirmed))
                .ForMember(dest => dest.CountDeath, opts => opts.MapFrom(x => x.CountDeath))
                .ForMember(dest => dest.CountRecovered, opts => opts.MapFrom(x => x.CountRecovered));

            //CreateMap<List<CovidCasesOverTimeUsa>, List<CasesOvertimeUsDto>>();
            
            //CreateMap<List<CasesOvertimeUsDto>, List<CovidCasesOverTimeUsa>>();
        }

    }
}
