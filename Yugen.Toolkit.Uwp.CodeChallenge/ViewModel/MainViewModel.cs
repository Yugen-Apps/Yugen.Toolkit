using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Yugen.Toolkit.Uwp.CodeChallenge.Interfaces;
using Yugen.Toolkit.Uwp.CodeChallenge.Model;

namespace Yugen.Toolkit.Uwp.CodeChallenge.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IKeyManager _keyManager;
        private string _newPasscode;
        private bool _isNewPasscodeToSetValid;
        private string _passcode;

        public MainViewModel(INavigationService navigationService, IKeyManager keyManager)
        {
            _navigationService = navigationService;
            _keyManager = keyManager;

            IsPasscodeConfigured = _keyManager.IsKeySet();
        }

        public bool IsPasscodeConfigured { get; }

        public bool IsPasscodeNotConfigured => IsPasscodeConfigured == false;

        public string NewPasscode
        {
            get => _newPasscode;

            set
            {
                if (SetProperty(ref _newPasscode, value))
                {
                    IsNewPasscodeToSetValid = IsNewPasscodeValid(_newPasscode);
                }
            }
        }

        public bool IsNewPasscodeToSetValid
        {
            get => _isNewPasscodeToSetValid;
            set => SetProperty(ref _isNewPasscodeToSetValid, value);
        }

        public string Passcode
        {
            get => _passcode;
            set => SetProperty(ref _passcode, value);
        }

        public ICommand SetPasswordAndNavigateCommand => new RelayCommand<string>(SetPasswordAndNavigate);

        public ICommand ValidatePasswordAndNavigateCommand => new RelayCommand<string>(ValidatePasscodeAndNavigate);

        private static bool IsNewPasscodeValid(string passcode)
        {
            passcode = passcode.Trim();

            // Business rule for the passcode: must be a 6-digit number.
            return passcode.Length == 6 && int.TryParse(passcode, out var number);
        }

        private void SetPasswordAndNavigate(string password)
        {
            var canNavigate = IsNewPasscodeValid(password);

            if (canNavigate)
            {
                _keyManager.SetEncryptionKey(password);
                _navigationService.NavigateTo(CoreConstants.PageConstants.ValuesPage);
            }
        }

        private void ValidatePasscodeAndNavigate(string passcode)
        {
            var savedPasscode = _keyManager.GetEncryptionKey(true);

            if (passcode == savedPasscode)
            {
                _navigationService.NavigateTo(CoreConstants.PageConstants.ValuesPage);
            }
        }
    }
}