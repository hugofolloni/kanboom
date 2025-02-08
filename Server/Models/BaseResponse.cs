namespace Kanboom.Models {
    public class BaseResponse {

        public bool Success { get; set; }
        public required string Message { get; set; }
        public string? Exception { get; set;}
        public List<string>? Errors { get; set; }
    }
}