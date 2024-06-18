namespace ExpenseApi.Models
{
    public class ExtractAndRetrieveResponse
    {
        public string? Error { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ExpenseClaimResponse? Data { get; set; }
    }
}
