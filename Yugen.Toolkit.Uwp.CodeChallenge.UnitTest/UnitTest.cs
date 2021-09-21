using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yugen.Toolkit.Uwp.CodeChallenge.Interfaces;
using Yugen.Toolkit.Uwp.CodeChallenge.Model;
using Yugen.Toolkit.Uwp.CodeChallenge.UnitTest.Services;
using Yugen.Toolkit.Uwp.CodeChallenge.ViewModel;

namespace Yugen.Toolkit.Uwp.CodeChallenge.UnitTest
{
    [TestClass]
    public class MainPageViewModelUnitTest
    {
        private static INavigationService _navigationService;
        private static IKeyManager _keyManager;
        private static MainViewModel _mainViewModel;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            _navigationService = new TestNavigationService();
            _keyManager = new TestKeyManager();
            _mainViewModel = new MainViewModel(_navigationService, _keyManager);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _navigationService.NavigateTo(CoreConstants.PageConstants.MainPage);
        }

        [TestMethod]
        public void TestValidatePasswordAndNavigateCommand_ValidPin()
        {
            _keyManager.SetEncryptionKey("123456");

            Assert.IsTrue(_navigationService.CurrentPageKey == CoreConstants.PageConstants.MainPage);

            _mainViewModel.ValidatePasswordAndNavigateCommand.Execute("123456");

            Assert.IsTrue(_navigationService.CurrentPageKey == CoreConstants.PageConstants.ValuesPage);
        }

        [TestMethod]
        public void TestValidatePasswordAndNavigateCommand_InvalidPin()
        {
            _keyManager.SetEncryptionKey("123456");

            Assert.IsTrue(_navigationService.CurrentPageKey == CoreConstants.PageConstants.MainPage);

            _mainViewModel.ValidatePasswordAndNavigateCommand.Execute("789123");

            Assert.IsTrue(_navigationService.CurrentPageKey == CoreConstants.PageConstants.MainPage);
        }

        [TestMethod]
        public void TestValidatePasswordAndNavigateCommand_NoEncryptionKey()
        {
            _keyManager.DeleteEncryptionKey();

            Assert.IsTrue(_navigationService.CurrentPageKey == CoreConstants.PageConstants.MainPage);

            _mainViewModel.ValidatePasswordAndNavigateCommand.Execute("123456");

            Assert.IsTrue(_navigationService.CurrentPageKey == CoreConstants.PageConstants.MainPage);
        }
    }
}
