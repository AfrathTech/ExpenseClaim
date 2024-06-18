using ExpenseApi.Models;

namespace ExpenseApi.DataContract
{
    public interface IExpenseClaimService
    {
        ExpenseClaim ParseExpenseClaim(string text);
    }
}
