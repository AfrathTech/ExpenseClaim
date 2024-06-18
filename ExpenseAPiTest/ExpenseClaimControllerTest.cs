using ExpenseApi.Controllers;
using ExpenseApi.DataContract;
using ExpenseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace ExpenseAPiTest
{
    public class ExpenseClaimControllerTest
    {

        private readonly Mock<IExpenseClaimService> mockService;

        public ExpenseClaimControllerTest()
        {
            mockService = new Mock<IExpenseClaimService>();
           
        }
        /// <summary>
        /// Unit test for controller method possitive using moq
        /// </summary>

        [Fact]
        public void ParseExpense_Claim_Posstivie()
        {

            // Arrange
            string validText = "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested… <expense><cost_centre>DEV632</cost_centre><total>35,000</total><payment_method>personal card</payment_method></expense> From: William Steele Sent: Friday, 16 June 2022";

            InputRequest inputRequest = new()
            {
                Text = validText
            };

            // Act
            mockService.Setup(x => x.ParseExpenseClaim(validText)).Returns(CreateDummyResponse()).Verifiable();
            var ctrl = new ExpenseClaimController(mockService.Object);

            var reslt = ctrl.ExtractData(inputRequest);

            var result = reslt.Result as OkObjectResult;
            var model = result!.Value as ExtractAndRetrieveResponse;


            // Assert
            Assert.NotNull(model);
            Assert.Equal("Data extracted and calculated successfully", model.Message);
         
        }

        /// <summary>
        /// Unit test for controller method exception case 
        /// </summary>

        [Fact]
        public void ParseExpense_Claim_Negative()
        {

            // Arrange
            string validText = "Hi Patricia, Please create an expense claim for the below. Relevant details are marked up as requested… <expense><cost_centre>DEV632</cost_centre><total>35,000</total><payment_method>personal card</payment_method></expense> From: William Steele Sent: Friday, 16 June 2022";

            InputRequest inputRequest = new()
            {
                Text = validText
            };

            // Act
        
            var ctrl = new ExpenseClaimController(mockService.Object);

            var reslt = ctrl.ExtractData(inputRequest);

            var result = reslt.Result as OkObjectResult;
            var model = result!.Value as ExtractAndRetrieveResponse;


            // Assert
            Assert.NotNull(model);
            Assert.False(model.Success);

        }

        private ExpenseClaim CreateDummyResponse()
        {
            ExpenseClaim expenseClaim = new ExpenseClaim
            {
                CostCentre = "DEV632",
                SalesTax = 71818.20m,
                TotalExcludingTax = 7181678.20m,
                PaymentMethod = "test "
            };
            return expenseClaim;
        }
    }
}