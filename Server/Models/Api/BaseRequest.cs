using System.ComponentModel.DataAnnotations;
namespace Kanboom.Models{
    public class BaseRequest {
            
        [Required]
        public string ApiKey { get; set; }
    }
}