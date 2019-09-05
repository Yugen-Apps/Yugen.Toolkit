namespace UwpCommunity.Standard.Validation
{
    public class EmailValidationRule : RegexValidationRule
    {
        public EmailValidationRule()
        {
            this.RegexPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        }

        /// <summary>
        /// Gets the error message to display for the rule.
        /// </summary>
        public override string ErrorMessage => "The e-mail address is invalid.";
    }
}