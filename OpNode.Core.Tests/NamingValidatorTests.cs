using OpNode.Core.Services;
using OpNode.Core.Validation;

namespace OpNode.Core.Tests;

[TestClass]
public class NamingValidatorTests
{
    private NamingValidator _validator = null!;

    [TestInitialize]
    public void Setup()
    {
        _validator = new NamingValidator();
    }

    [TestMethod]
    public void ValidateNotEmpty_WithValidName_ReturnsSuccess()
    {
        // Arrange
        string validName = "ValidName";

        // Act
        ValidationResult result = _validator.ValidateNotEmpty(validName);

        // Assert
        Assert.IsTrue(result.IsValid);
        Assert.IsNull(result.ErrorMessage);
    }

    [TestMethod]
    public void ValidateNotEmpty_WithNullName_ReturnsFailure()
    {
        // Arrange
        string? nullName = null;

        // Act
        ValidationResult result = _validator.ValidateNotEmpty(nullName);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Name cannot be null, empty, or whitespace.", result.ErrorMessage);
    }

    [TestMethod]
    public void ValidateNotEmpty_WithEmptyName_ReturnsFailure()
    {
        // Arrange
        string emptyName = "";

        // Act
        ValidationResult result = _validator.ValidateNotEmpty(emptyName);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Name cannot be null, empty, or whitespace.", result.ErrorMessage);
    }

    [TestMethod]
    public void ValidateNotEmpty_WithWhitespaceName_ReturnsFailure()
    {
        // Arrange
        string whitespaceName = "   ";

        // Act
        ValidationResult result = _validator.ValidateNotEmpty(whitespaceName);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Name cannot be null, empty, or whitespace.", result.ErrorMessage);
    }

    [TestMethod]
    public void ValidateMinimumLength_WithValidLength_ReturnsSuccess()
    {
        // Arrange
        string validName = "ValidName";
        int minLength = 5;

        // Act
        ValidationResult result = _validator.ValidateMinimumLength(validName, minLength);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void ValidateMinimumLength_WithInvalidLength_ReturnsFailure()
    {
        // Arrange
        string shortName = "Hi";
        int minLength = 5;

        // Act
        ValidationResult result = _validator.ValidateMinimumLength(shortName, minLength);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Name must be at least 5 characters long.", result.ErrorMessage);
    }

    [TestMethod]
    public void ValidateCharacters_WithValidCharacters_ReturnsSuccess()
    {
        // Arrange
        string validName = "Valid_Name123";

        // Act
        ValidationResult result = _validator.ValidateCharacters(validName);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void ValidateCharacters_WithInvalidCharacters_ReturnsFailure()
    {
        // Arrange
        string invalidName = "Invalid-Name!";

        // Act
        ValidationResult result = _validator.ValidateCharacters(invalidName);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Name can only contain letters, numbers, and underscores.", result.ErrorMessage);
    }

    [TestMethod]
    public void ValidateName_WithValidName_ReturnsSuccess()
    {
        // Arrange
        string validName = "Valid_Name123";

        // Act
        ValidationResult result = _validator.ValidateName(validName);

        // Assert
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public void ValidateName_WithInvalidName_ReturnsFirstFailure()
    {
        // Arrange
        string invalidName = "";

        // Act
        ValidationResult result = _validator.ValidateName(invalidName);

        // Assert
        Assert.IsFalse(result.IsValid);
        Assert.AreEqual("Name cannot be null, empty, or whitespace.", result.ErrorMessage);
    }
}