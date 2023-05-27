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
			CreateMap<UserDto, User>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<BuddyDto, Buddy>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<EventDto, Event>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<InternDto, Intern>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<InternRequestDto, InternRequest>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<InternResponseDto, InternResponse>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<InternReviewDto, InternReview>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<InternshipDirectionDto, InternshipDirection>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<LinkDto, Link>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<MentorDto, Mentor>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<OrganizationAdminDto, OrganizationAdmin>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<TagDto, Tag>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<UniversityDto, University>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<UserEventDto, UserEvent>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id));
			CreateMap<UserTrainingDto, UserTraining>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Id)); 
		}
	}
}
