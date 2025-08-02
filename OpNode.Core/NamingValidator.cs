using OpNode.Core.Validation;

namespace OpNode.Core.Services
{
    /// <summary>
    /// Provides validation services for naming conventions and rules.
    /// </summary>
    public class NamingValidator
    {
        /// <summary>
        /// Validates that a name is not null, empty, or whitespace.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        public ValidationResult ValidateNotEmpty(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return ValidationResult.Failure("Name cannot be null, empty, or whitespace.");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// Validates that a name meets minimum length requirements.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <param name="minLength">The minimum required length.</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        public ValidationResult ValidateMinimumLength(string? name, int minLength)
        {
            if (name == null || name.Length < minLength)
            {
                return ValidationResult.Failure($"Name must be at least {minLength} characters long.");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// Validates that a name does not exceed maximum length.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <param name="maxLength">The maximum allowed length.</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        public ValidationResult ValidateMaximumLength(string? name, int maxLength)
        {
            if (name != null && name.Length > maxLength)
            {
                return ValidationResult.Failure($"Name cannot exceed {maxLength} characters.");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// Validates that a name contains only valid characters (letters, numbers, and underscores).
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        public ValidationResult ValidateCharacters(string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return ValidationResult.Failure("Name cannot be null or empty.");
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z0-9_]+$"))
            {
                return ValidationResult.Failure("Name can only contain letters, numbers, and underscores.");
            }

            return ValidationResult.Success();
        }

        /// <summary>
        /// Performs comprehensive validation on a name.
        /// </summary>
        /// <param name="name">The name to validate.</param>
        /// <param name="minLength">The minimum required length (default: 1).</param>
        /// <param name="maxLength">The maximum allowed length (default: 100).</param>
        /// <returns>A ValidationResult indicating success or failure.</returns>
        public ValidationResult ValidateName(string? name, int minLength = 1, int maxLength = 100)
        {
            var emptyResult = ValidateNotEmpty(name);
            if (!emptyResult.IsValid)
            {
                return emptyResult;
            }

            var minLengthResult = ValidateMinimumLength(name, minLength);
            if (!minLengthResult.IsValid)
            {
                return minLengthResult;
            }

            var maxLengthResult = ValidateMaximumLength(name, maxLength);
            if (!maxLengthResult.IsValid)
            {
                return maxLengthResult;
            }

            var charactersResult = ValidateCharacters(name);
            if (!charactersResult.IsValid)
            {
                return charactersResult;
            }

            return ValidationResult.Success();
        }
    }
}