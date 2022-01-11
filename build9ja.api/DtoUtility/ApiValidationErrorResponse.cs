using System;
namespace build9ja.api.DtoUtility
{
	public class ApiValidationErrorResponse : ApiResponse
	{
		
		public ApiValidationErrorResponse() : base(400)
		{
		}

		public IEnumerable<string> Errors { get; set; }
	}
}

