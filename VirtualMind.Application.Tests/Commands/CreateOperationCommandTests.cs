using System.Linq;
using VirtualMind.Application.Commands;
using Xunit;

namespace VirtualMind.Application.Tests.Commands
{
    public class CreateOperationCommandTests
    {
        [Fact(DisplayName = "Add new VALID Operation Command")]
        [Trait("Category", "Commands")]
        public void CreateOperation_IsValid_ShouldPass()
        {
            //Arrange
            var command = new CreateOperationCommand()
            {
                CurrencyType = "USD",
                RequestedAmount = 100,
                UserId = 1
            };

            //Act
            var validation = new CreateOperationCommandValidator().Validate(command).IsValid;

            //Assert
            Assert.True(validation);
        }

        [Fact(DisplayName = "Add new INVALID Operation Command")]
        [Trait("Category", "Commands")]
        public void CreateOperation_IsNotValid_ShouldNotPass()
        {
            //Arrange
            var command = new CreateOperationCommand()
            {
                CurrencyType = "",
                RequestedAmount = 0,
                UserId = 0
            };

            //Act
            var validation = new CreateOperationCommandValidator().Validate(command);

            //Assert
            Assert.False(validation.IsValid);
            Assert.Contains("[UserId] must be greater Than 0", validation.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("[RequestedAmount] must be greater Than 0", validation.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("[CurrencyType] can't be empty", validation.Errors.Select(c => c.ErrorMessage));
            Assert.Contains("Invalid [CurrencyType]", validation.Errors.Select(c => c.ErrorMessage));
        }
    }
}
