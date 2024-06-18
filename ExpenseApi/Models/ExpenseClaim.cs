namespace ExpenseApi.Models
{
    public class ExpenseClaim
    {
        public string? CostCentre { get; set; }
        public decimal Total { get; set; }
        public decimal SalesTax { get; set; }
        public decimal TotalExcludingTax { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
