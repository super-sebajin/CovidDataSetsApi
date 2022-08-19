namespace CovidDataSetsApi.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {                             
            //mapping for gets
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(x => x.Username))
                .ForMember(dest => dest.IsActive, opts => opts.MapFrom(x => x.IsActive));

            //mapping for user registration and user logins
            CreateMap<UserCredentialsDto, User>();
            //.ForMember(dest => dest.Username, opts => opts.MapFrom(x => x.Username))
            //.ForMember(dest => dest.IsActive, opts => opts.MapFrom(x =>));
            CreateMap<User, UserActionResponse>();

        }

    }
}
