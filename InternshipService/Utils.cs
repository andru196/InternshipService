namespace InternshipService
{
	internal static class Utils
	{
		public static bool IsNull<T>(T @object, out T objectAgain)
		{
			objectAgain = @object;
			return @object == null;
		}
	}
}
