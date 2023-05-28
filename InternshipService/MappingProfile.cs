using AutoMapper;
using DataModel.Models;
using InternshipService.DTO;
using System.Runtime.CompilerServices;

namespace InternshipService
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<UserDto, User>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<BuddyDto, Buddy>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<EventDto, Event>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<InternDto, Intern>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<InternRequestDto, InternRequest>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<InternResponseDto, InternResponse>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid))
				.ForMember(x => x.Year, x => x.MapFrom(y => y.CreatedDate.Value.Year));
			CreateMap<ReviewDto, Review>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<InternshipDirectionDto, InternshipDirection>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<LinkDto, Link>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<MentorDto, Mentor>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<OrganizationAdminDto, OrganizationAdmin>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<TagDto, Tag>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<UniversityDto, University>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<UserEventDto, UserEvent>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<UserTrainingDto, UserTraining>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			CreateMap<EntityDto, Entity>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid));
			
		}
	}
}
