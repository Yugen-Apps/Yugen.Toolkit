using System.Collections.Generic;

namespace Yugen.Toolkit.Standard.Validation
{
    /// <summary>
    /// A collection for containing ValidationRules.
    /// </summary>
    public class ValidationRules
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRules"/> class.
        /// </summary>
        public ValidationRules()
        {
            Rules = new List<BaseValidationRule>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRules"/> class.
        /// </summary>
        /// <param name="rules">
        /// A set of ValidationRules to initialize with.
        /// </param>
        public ValidationRules(List<BaseValidationRule> rules)
        {
            Rules = rules;
        }

        /// <summary>
        /// Gets the collection of ValidationRules.
        /// </summary>
        public List<BaseValidationRule> Rules { get; }
    }
}