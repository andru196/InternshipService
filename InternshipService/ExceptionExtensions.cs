using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace InternshipService
{
	public static class ExceptionExtensions
	{
		public static string PrettyMessage(this Exception exception)
		{
			switch (exception)
			{
				case SqlException ex:
					{
						var message = $"Ошибка выполнения в БД: {ex.Message}";
						return message;
					}
				case DbUpdateException ex:
					{
						Exception lowestEx = ex;
						while (lowestEx.InnerException != null)
						{
							lowestEx = lowestEx.InnerException;
						}

						var message = $"Ошибка сохранения в БД: {lowestEx.Message}";

						if (lowestEx is Microsoft.Data.SqlClient.SqlException sqlEx)
						{
							if (sqlEx.Errors.Cast<SqlError>().Any(e => e.Class == 14 && (e.Number == 2601 || e.Number == 2627)))
							{
								var regex = new Regex(@"'(UK_[\w_]+)'(.*\((.*)\))?", RegexOptions.Compiled);
								var match = regex.Match(sqlEx.Message);

								if (match.Success)
								{

									message = $"Нарушение уникальности  при сохранении данный в БД {match.Groups[1]}";
								}

								if (match.Groups[3].Success)
								{
									message += $". Значение \"{match.Groups[3]}\"";
								}
							}
							else
								message = $"Нарушение уникальности записей при сохранении данных в БД.";
						}
						return message;
					}
				default:
					return exception.Message;
			}
		}
	}
}
