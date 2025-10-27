using System.Text.Json.Serialization;

namespace Turnero.Dto
{
	public class ResponseDto<T>
	{
		public bool Success { get; set; }
		public string? Message { get; set; }
		public T? Data { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Dictionary<string, string[]>? Errors { get; set; }

	}
}
