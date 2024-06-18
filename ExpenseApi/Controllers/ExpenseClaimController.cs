using ExpenseApi.DataContract;
using ExpenseApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpenseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseClaimController : ControllerBase
    {

       
        private readonly IExpenseClaimService _expenseClaimService;

        /// <summary>
        /// Constructor for the controller for intializing
        /// </summary>
        /// <param name="expenseClaimService"></param>
        public ExpenseClaimController(IExpenseClaimService expenseClaimService)
        {
            _expenseClaimService = expenseClaimService;
        }
      
        /// <summary>
        /// Post Method for Extract Data
        /// </summary>
        /// <param name="inputRequest"></param>
        /// <returns></returns>
        [HttpPost("extract")]
        public ActionResult<ExtractAndRetrieveResponse> ExtractData([FromBody] InputRequest inputRequest)
        {

                try
                {
                    var expenseClaimResult = _expenseClaimService.ParseExpenseClaim(inputRequest.Text.Trim());

                    var dataResponse = new ExpenseClaimResponse
                    {
                        CostCentre = expenseClaimResult.CostCentre,
                        Total = expenseClaimResult.Total,
                        SalesTax = expenseClaimResult.SalesTax,
                        TotalExcludingTax = expenseClaimResult.TotalExcludingTax,
                        PaymentMethod = expenseClaimResult.PaymentMethod
                    };

                    return Ok(new ExtractAndRetrieveResponse
                    {
                        Success = true,
                        Data = dataResponse,
                        Message = "Data extracted and calculated successfully"

                    });


                }
                catch (Exception ex)
                {
                    return Ok(new ExtractAndRetrieveResponse
                    {
                        Success = false,
                        Error = ex.Message
                    });

                }
        }
    }

    }

