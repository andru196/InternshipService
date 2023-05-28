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
			CreateMap<UserDto, User>();
			CreateMap<BuddyDto, Buddy>();
			CreateMap<CaseChempionshipDto, CaseChempionship>();
			CreateMap<EventDto, Event>();
			CreateMap<InternCaseChempionshipResultDto, InternCaseChempionshipResult>();
			CreateMap<InternsCourseDto, InternsCourse>();
			CreateMap<InternDto, Intern>();
			CreateMap<InternsInternshipDto, InternsInternship>();
			CreateMap<InternRequestDto, InternRequest>();
			CreateMap<InternResponseDto, InternResponse>().ForMember(x => x.Guid, x => x.MapFrom(y => y.Guid))
				.ForMember(x => x.Year, x => x.MapFrom(y => y.CreatedDate.Value.Year));
			CreateMap<InternTestDto, InternTest>();
			CreateMap<ReviewDto, Review>();
			CreateMap<InternshipDirectionDto, InternshipDirection>();
			CreateMap<LinkDto, Link>();
			CreateMap<MentorDto, Mentor>();
			CreateMap<OrganizationDto, Organization>();
			CreateMap<OrganizationAdminDto, OrganizationAdmin>();
			CreateMap<TagDto, Tag>();
			CreateMap<UniversityDto, University>();
			CreateMap<UserEventDto, UserEvent>();
			CreateMap<UserTrainingDto, UserTraining>();
			CreateMap<CourseDto, Course>();
			CreateMap<EntityDto, Entity>();
			
		}
	}
}
