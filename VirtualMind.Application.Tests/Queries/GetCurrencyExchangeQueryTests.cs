using System.Linq;
using VirtualMind.Application.Queries;
using Xunit;

namespace VirtualMind.Application.Tests.Queries
{    
    public class GetCurrencyExchangeQueryTests
    {
        [Fact(DisplayName = "Execute VALID Query")]
        [Trait("Category", "Queries")]
        public void GetExchange_IsValid_ShouldPass()
        {
            //Arrange
            var query = new GetCurrencyExchangeQuery()
            {
                CurrencyType = "USD"
            };

            //Act
            var validation = new GetCurrencyExchangeValidator().Validate(query);

            //Assert
            Assert.True(validation.IsValid);            
        }

        [Fact(DisplayName = "Execute INVALID Query")]
        [Trait("Category", "Queries")]
        public void GetExchange_IsNotValid_ShouldNotPass()
        {
            //Arrange
            var query = new GetCurrencyExchangeQuery()
            {
                CurrencyType = null
            };

            var emptyQuery = new GetCurrencyExchangeQuery()
            {
                CurrencyType = ""
            };

            //Act
            var validation = new GetCurrencyExchangeValidator().Validate(query);
            var secondValidation = new GetCurrencyExchangeValidator().Validate(emptyQuery);

            //Assert
            Assert.False(validation.IsValid);

            Assert.Contains("[CurrencyType] can't be null!", validation.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("[CurrencyType] field is required!", validation.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Invalid [CurrencyType]!", secondValidation.Errors.Select(c => c.ErrorMessage));
        }
    }
}
