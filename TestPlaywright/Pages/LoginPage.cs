using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlaywright.Pages
{
    public class LoginPage
    {
        private IPage _page;

        public LoginPage(IPage page) => _page = page;

        private ILocator _userName => _page.Locator("#Login");
        private ILocator _password => _page.Locator("#Password");
        private ILocator _signInButton => _page.Locator("button[class*='btn-lg']");

        /// <summary>
        /// Set password on login page.
        /// </summary>
        /// <param name="password"></param>
        public async Task SetPassword(string password)
        {
            await _password.TypeAsync(password);
        }

        /// <summary>
        /// Set username on login page.
        /// </summary>
        /// <param name="userName"></param>
        public async Task SetUserName(string userName)
        {
            await _userName.TypeAsync(userName);
        }

        /// <summary>
        /// Click Sign in Button on login page.
        /// </summary>
        public async Task ClickLoginButton()=> await _signInButton.ClickAsync();
    }
}
