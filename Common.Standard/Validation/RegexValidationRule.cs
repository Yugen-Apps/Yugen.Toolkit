using System.Text.RegularExpressions;

namespace Common.Standard.Validation
{
    public class RegexValidationRule : BaseValidationRule
    {
        /// <summary>
        /// Gets or sets the regex pattern.
        /// </summary>
        public string RegexPattern { get; set; }

        /// <summary>
        /// Gets the error message to display for the rule.
        /// </summary>
        public override string ErrorMessage => "The value is invalid.";

        /// <summary>
        /// Validate element against validation rules
        /// </summary>
        /// <param name="value">Value to validate</param>
        /// <returns>Is valid</returns>
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            var val = value.ToString();
            if (string.IsNullOrWhiteSpace(val))
            {
                return true;
            }

            var reg = new Regex(this.RegexPattern, RegexOptions.IgnoreCase);
            return reg.IsMatch(val);
        }
    }
}