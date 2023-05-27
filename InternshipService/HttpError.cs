namespace InternshipService
{
	public class HttpError
	{
		public string Message { get; set; }
		public HttpError ModelState {get;set;}
		public string ExceptionType { get; set;}
		public string StackTrace { get; set;}
		public HttpError InnerException { get; set;}
		public HttpError(Exception exception) 
		{
			Message = exception.Message;
			ExceptionType = exception.GetType().FullName;
			StackTrace = exception.StackTrace;
			if (exception.InnerException != null)
				InnerException = new HttpError(exception.InnerException);
		}
	}
}
