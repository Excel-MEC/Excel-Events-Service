using API.Dtos;
using API.Dtos.Bookmark;
using API.Dtos.Event;
using API.Dtos.EventHeads;
using API.Dtos.Registration;
using API.Dtos.Schedule;
using API.Dtos.Teams;
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
            CreateMap<DataForScheduleDto, Schedule>();
            CreateMap<Event, EventForListViewDto>();
            CreateMap<Event, EventForDetailedViewDto>().
                ForMember(dest => dest.Rounds, opt => opt.MapFrom(src => src.Rounds));
            CreateMap<Schedule, EventRoundForViewDto>();
            CreateMap<DataForUpdatingEventDto, Event>()
                .ForMember(dest => dest.Icon, opt => opt.Ignore());
            CreateMap<Event, EventForBookmarkListViewDto>();
            CreateMap<Schedule, EventRoundForScheduleViewDto>();
            CreateMap<EventHead, EventHeadForViewDto>();
            CreateMap<EventHeadForViewDto, EventHead>();
            CreateMap<Schedule, ScheduleForViewDto>();
            CreateMap<Bookmark, BookmarkForViewDto>();
            CreateMap<Registration, RegistrationForViewDto>();
            CreateMap<Team, TeamForViewDto>();
            CreateMap<Registration, RegistrationWithUserViewDto>();
            CreateMap<Result, ResultForViewDto>();
        }
    }
}