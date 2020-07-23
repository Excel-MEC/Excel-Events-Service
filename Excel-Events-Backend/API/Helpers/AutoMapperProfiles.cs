using API.Dtos.Bookmark;
using API.Dtos.Event;
using API.Dtos.Schedule;
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
            CreateMap<DataForAddingEventRoundDto, Schedule>();
            CreateMap<Event, EventForListViewDto>();
            CreateMap<DataForUpdatingEventDto, Event>()
                .ForMember(dest => dest.Icon, opt => opt.Ignore());
            CreateMap<Event, EventForBookmarkListViewDto>();
            CreateMap<Schedule, EventRoundForScheduleViewDto>();
        }
    }
}