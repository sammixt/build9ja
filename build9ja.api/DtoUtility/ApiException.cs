using System;
namespace build9ja.api.DtoUtility
{
	public class ApiException : ApiResponse
	{
		public ApiException()
		{
		}

        public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
        {
            Details = details;
        }

        public string Details { get; set; }
    }
}

