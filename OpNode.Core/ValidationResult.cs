namespace OpNode.Core.Validation
{
    /// <summary>
    /// Represents the result of a validation operation.
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Gets a value indicating whether the validation was successful.
        /// </summary>
        public bool IsValid { get; init; }

        /// <summary>
        /// Gets the error message if validation failed, otherwise null or empty.
        /// </summary>
        public string? ErrorMessage { get; init; }

        /// <summary>
        /// Creates a successful validation result.
        /// </summary>
        public static ValidationResult Success() => new ValidationResult { IsValid = true };

        /// <summary>
        /// Creates a failed validation result with an error message.
        /// </summary>
        public static ValidationResult Failure(string errorMessage) => 
            new ValidationResult { IsValid = false, ErrorMessage = errorMessage };
    }
}
