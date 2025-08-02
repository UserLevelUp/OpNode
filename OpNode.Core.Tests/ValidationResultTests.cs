using OpNode.Core.Validation;

namespace OpNode.Core.Tests;

[TestClass]
public class ValidationResultTests
{
    [TestMethod]
    public void Success_ReturnsValidResult()
    {
        // Act
        ValidationResult result = ValidationResult.Success();

        // Assert
        Assert.IsTrue(result.IsValid);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public void Failure_WithErrorMessage_ReturnsInvalidResult()
    {
        // Arrange
        string errorMessage = "Test error message";

        // Act
        ValidationResult result = ValidationResult.Failure(errorMessage);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual(errorMessage, result.ErrorMessage);
    }

    [TestMethod]
    public void Constructor_WithInitProperties_SetsCorrectValues()
    {
        // Arrange & Act
        ValidationResult validResult = new ValidationResult { IsValid = true };
        ValidationResult invalidResult = new ValidationResult { IsValid = false, ErrorMessage = "Error" };

        // Assert
        Assert.IsTrue(validResult.IsValid);
        Assert.IsNull(validResult.ErrorMessage);
        
        Assert.IsFalse(invalidResult.IsValid);
        Assert.AreEqual("Error", invalidResult.ErrorMessage);
    }
}