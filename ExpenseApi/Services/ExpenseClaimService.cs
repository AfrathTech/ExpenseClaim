
using ExpenseApi.DataContract;
using ExpenseApi.Models;
using System.Xml.Linq;

namespace ExpenseApi.Services
{
    /// <summary>
    /// Class Used for ExpenseClaimService
    /// </summary>
    public class ExpenseClaimService : IExpenseClaimService
    {
        private const decimal SalesTaxRate = 0.1m; // 10% sales tax rate

        /// <summary>
        /// Return the  calculated sale tax object
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
     
        public ExpenseClaim ParseExpenseClaim(string text)
        {
            try
            {
                var start = text.IndexOf("<expense>", StringComparison.Ordinal);
                var endpos = text.IndexOf("</expense>", StringComparison.Ordinal);

                if (start == -1 || endpos == -1)
                {
                    throw new Exception("Invalid XML content: Opening tag without corresponding closing tag/ Message is not in Required format ");
                }

                var end = text.IndexOf("</expense>", StringComparison.Ordinal) + "</expense>".Length;


                var xmlContent = text.Substring(start, end - start);
                var xdoc = XDocument.Parse(xmlContent);

                XElement costCentreElement = xdoc!.Root!.Element("cost_centre")!;
                var totalElement = xdoc.Root.Element("total");
                var paymentMethodElement = xdoc.Root.Element("payment_method");

                if (totalElement == null)
                {
                    throw new Exception("Missing required field: <total>");
                }

                var costCentre = string.IsNullOrEmpty(costCentreElement?.Value) ? "UNKNOWN" : costCentreElement?.Value;
                var totalStr = totalElement.Value;
                var paymentMethod = paymentMethodElement?.Value;

                if (string.IsNullOrEmpty(totalStr))
                {
                    throw new Exception("Missing required fields in XML (Total is Missing)");
                }

                var total = decimal.Parse(totalStr.Replace(",", ""));
                var salesTax = total * SalesTaxRate / (1 + SalesTaxRate);
                var totalExcludingTax = total - salesTax;

                return new ExpenseClaim
                {
                    CostCentre = costCentre,
                    Total = total,
                    SalesTax = Math.Round(salesTax, 2),
                    TotalExcludingTax = Math.Round(totalExcludingTax, 2),
                    PaymentMethod = paymentMethod
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error parsing expense claim: {ex.Message}");
            }
        }
    }
}