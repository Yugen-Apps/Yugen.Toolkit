using System.Collections.Generic;
using UwpCommunity.Uwp.ViewModels;

namespace UwpCommunity.Uwp.Samples.ViewModels.Controls
{
    public class ValidationViewModel : BaseViewModel
    {
        private string _title = "ValidationPage";
        public string Title
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set { Set(ref _surname, value); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { Set(ref _email, value); }
        }

        private int _mobile;
        public int Mobile
        {
            get { return _mobile; }
            set { Set(ref _mobile, value); }
        }

        public List<string> GenderList = new List<string> { "Male", "Female" };

        private string _username;
        public string Username
        {
            get { return _username; }
            set { Set(ref _username, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { Set(ref _password, value); }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set { Set(ref _confirmPassword, value); }
        }
    }
}