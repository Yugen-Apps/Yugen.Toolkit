namespace UwpCommunity.Standard.Validation
{
    public class DecimalValidationRule : RegexValidationRule
    {
        public DecimalValidationRule()
        {
            this.RegexPattern = @"^[0-9,.]";
        }

        /// <summary>
        /// Gets the error message to display for the rule.
        /// </summary>
        public override string ErrorMessage => "The character is invalid.";
    }
}