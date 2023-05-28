using System.Numerics;

namespace InternshipService.Configs
{
	public struct Interval<T> where T: INumber<T>
	{
		public T From { get; init; }
		public T To { get; init; }
	}
	public record InternAutoCheckConfig(
		Interval<ushort> Age,
		string[] Citizen,
		ushort MinimalCourse,
		bool NeedRelevantExperiance,
		Guid FromUser,
		string SorryMsgSubject,
		string SorryMsg
		);
}
