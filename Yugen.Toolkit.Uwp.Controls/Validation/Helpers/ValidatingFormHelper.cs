using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Helpers;

namespace Yugen.Toolkit.Uwp.Controls.Validation.Helpers
{
    public static class ValidatingFormHelper
    {
        private static List<Control> _validatingControlsList = new List<Control>();

        public static void Init(object uiElement)
        {
            ControlHelper.GetControlList<ValidatingTextBoxUserControl>(uiElement);
            ControlHelper.GetControlList<ValidatingPasswordBoxUserControl>(uiElement, false);
            _validatingControlsList = ControlHelper.GetControlList<ValidatingComboBoxUserControl>(uiElement, false);
        }

        public static bool FormIsValid()
        {
            var isValid = true;

            foreach (var validatingTextBox in _validatingControlsList)
            {
                if (!(validatingTextBox is BaseValidatingUserControl item)) continue;
                item.ResetCustomValidation();

                if (!item.IsValid())
                    isValid = false;
            }

            return isValid;
        }
    }
}