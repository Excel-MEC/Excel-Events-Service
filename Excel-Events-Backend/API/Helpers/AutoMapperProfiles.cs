using API.Dtos.Event;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            AllowNullDestinationValues = true;
            CreateMap<DataForAddingEventDto, Event>();
            CreateMap<Event, EventForListViewDto>();
            CreateMap<DataForUpdatingEventDto, Event>()
                .ForMember(dest => dest.Icon, opt => opt.Ignore());
        }
    }
}