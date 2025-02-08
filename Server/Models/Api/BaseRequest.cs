using System.ComponentModel.DataAnnotations;
using Kanboom.Utils;
namespace Kanboom.Models;

public class BaseRequest : IApiKeyHolder {
        
    [Required]
    public string ApiKey { get; set; }
}
