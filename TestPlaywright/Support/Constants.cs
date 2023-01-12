using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlaywright.Support
{
    public class Constants
    {
        public static string BaseAddress = TestContext.Parameters.Get("baseAddress");
        public static string CompanyLogin = TestContext.Parameters.Get("companyLogin");
        public static string DefaultPassword = TestContext.Parameters.Get("companyPassword");

        public static readonly Dictionary<string, string> ClientPagesAndUrls = new Dictionary<string, string>
        {
            { "Login", "Account/SignIn" },
            //Pages
            {"Dashboard", "#/" },
            { "Patients", "#/patients"},
            {"Online Questionnaires", "#/online-questionnaires" },
            {"Ledger", "#/ledger" },
            {"Medical Claims", "#/claims" },
            {"Appointment Book", "#/appointment-book" },
            {"MBS Claim Tracker", "#/mbs/requests" }
        };
    }
}
