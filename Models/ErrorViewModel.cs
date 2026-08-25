namespace ASP_Net_Core_MVC_Liberary.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Message { get; set; } = "An error occurred.";
    }
}
