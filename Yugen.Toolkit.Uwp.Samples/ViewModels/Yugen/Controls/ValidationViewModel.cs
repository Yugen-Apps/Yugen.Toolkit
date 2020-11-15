using System.Collections.Generic;
using Yugen.Toolkit.Standard.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Controls
{
    public class ValidationViewModel : ViewModelBase
    {
        public List<string> GenderList = new List<string> { "Male", "Female" };
        private string _title = "ValidationPage";
        private string _name;

        private string _surname;

        private string _email;

        private string _mobile;

        private string _username;

        private string _password;

        private string _confirmPassword;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Surname
        {
            get => _surname;
            set => SetProperty(ref _surname, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Mobile
        {
            get => _mobile;
            set => SetProperty(ref _mobile, value);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }
    }
}