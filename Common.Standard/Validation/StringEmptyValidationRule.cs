namespace Common.Standard.Validation
{
    public class NameValidationRule : RegexValidationRule
    {
        public NameValidationRule()
        {
            this.RegexPattern = @"^[\p{L} \.'\-]+$"; 
        }

        /// <summary>
        /// Gets the error message to display for the rule.
        /// </summary>
        public override string ErrorMessage => "The name is invalid.";
    }
}