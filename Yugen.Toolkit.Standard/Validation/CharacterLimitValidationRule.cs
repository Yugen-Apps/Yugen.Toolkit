namespace Yugen.Toolkit.Standard.Validation
{
    /// <summary>
    /// The character limit validation rule.
    /// </summary>
    public class CharacterLimitValidationRule : BaseValidationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterLimitValidationRule"/> class.
        /// </summary>
        public CharacterLimitValidationRule()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterLimitValidationRule"/> class.
        /// </summary>
        /// <param name="minLength">
        /// The min length.
        /// </param>
        /// <param name="maxLength">
        /// The max length.
        /// </param>
        public CharacterLimitValidationRule(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        /// <summary>
        /// Gets the error message to display for the rule.
        /// </summary>
        public override string ErrorMessage => $"The value must be between {MinLength} and {MaxLength} characters.";

        /// <summary>
        /// Gets or sets the min length.
        /// </summary>
        public int MinLength { get; set; }

        /// <summary>
        /// Gets or sets the max length.
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Validates an object value with this rule.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns a boolean value indicating whether the object was validated with the rule.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            var val = value.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            return (val.Length <= MaxLength || MaxLength == 0) && val.Length >= MinLength;
        }
    }
}