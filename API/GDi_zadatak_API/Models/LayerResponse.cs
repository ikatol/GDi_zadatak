using System.Net;

namespace GDi_zadatak_API.Models {

	public record LayerResponse {
        public HttpStatusCode Code { get; init; }
        public string? Message { get; init; }
		public string? AdditionalMessage { get; init; }

		public LayerResponse(HttpStatusCode code, string? message, string? additionalMessage) {
			Code = code;
			Message = message;
			AdditionalMessage = additionalMessage;
		}
	}
	public record LayerResponse<T> : LayerResponse {
		public T? Data { get; init; }
		

		public LayerResponse(T? data, HttpStatusCode code, string? message, string? additionalMessage) 
			: base(code, message, additionalMessage) {
			Data = data;
		}
	}
}
