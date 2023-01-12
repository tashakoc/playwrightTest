using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlaywright.Drivers;
using TestPlaywright.ModalWindows;
using TestPlaywright.Pages;
using TestPlaywright.Support;

namespace TestPlaywright.StepDefinitions
{
    [Binding]
    public class BaseSteps
    {
        private readonly Driver _driver;
        private readonly LoginPage _loginPage;

        public BaseSteps(Driver driver)
        {
            _driver = driver;
            _loginPage = new LoginPage(_driver.Page);
        }

        [Given(@"Navigate to '([^']*)' \(Ui\)")]
        public void GivenNavigateToUi(string endPoint)
        {
            switch (endPoint)
            {
                case "DWWeb Client site":
                    _driver.Page.GotoAsync(Constants.BaseAddress + "/" + Constants.ClientPagesAndUrls["Login"]);
                    break;
                case "Dashboard page":
                    _driver.Page.GotoAsync(Constants.BaseAddress + "/" + Constants.ClientPagesAndUrls["Dashboard"]);
                    break;
                case "Patients page":
                    _driver.Page.GotoAsync(Constants.BaseAddress + "/" + Constants.ClientPagesAndUrls["Patients"]);
                    break;
                case "Online Questionnaires page":
                    _driver.Page.GotoAsync(Constants.BaseAddress + "/" + Constants.ClientPagesAndUrls["Online Questionnaires"]);
                    break;
                case "Ledger page":
                    _driver.Page.GotoAsync(Constants.BaseAddress + "/" + Constants.ClientPagesAndUrls["Ledger"]);
                    break;
                case "Medical Claims page":
                    _driver.Page.GotoAsync(Constants.BaseAddress + "/" + Constants.ClientPagesAndUrls["Medical Claims"]);
                    break;
                case "Appointment Book page":
                    _driver.Page.GotoAsync(Constants.BaseAddress + "/" + Constants.ClientPagesAndUrls["Appointment Book"]);
                    break;
                case "MBS Claim Tracker page":
                    _driver.Page.GotoAsync(Constants.BaseAddress + "/" + Constants.ClientPagesAndUrls["MBS Claim Tracker"]);
                    break;

            }
        }

        [Given(@"Successful authorization as a current user \(Ui\)")]
        public async Task GivenSuccessfulAuthorizationAsACurrentUserUi()
        {
            await _loginPage.SetUserName(Constants.CompanyLogin);
            await _loginPage.SetPassword(Constants.DefaultPassword);
            await _loginPage.ClickLoginButton();
        }
    }
}
