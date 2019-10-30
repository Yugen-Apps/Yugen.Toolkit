// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationRule.cs" company="James Croft">
//   Copyright (c) 2015 James Croft.
// </copyright>
// <summary>
//   Defines the ValidationRule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Yugen.Toolkit.Standard.Validation
{
    /// <summary>
    /// A base class for validation rules.
    /// </summary>
    public abstract class BaseValidationRule
    {
        /// <summary>
        /// Gets the error message to display for the rule.
        /// </summary>
        public abstract string ErrorMessage { get; }

        /// <summary>
        /// Validates an object value with this rule.
        /// </summary>
        /// <param name="value">
        /// The value to validate.
        /// </param>
        /// <returns>
        /// Returns a boolean value indicating whether the object was validated with the rule.
        /// </returns>
        public abstract bool IsValid(object value);
    }
}