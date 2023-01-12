using Microsoft.Playwright;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TestPlaywright.ModalWindows
{
    public class NewAppointmentModalWindow
    {
        private IPage _page;

        public NewAppointmentModalWindow(IPage page) => _page = page;
        //1. Patient
        private ILocator _patientRow(string row) => _page.Locator($"[data-at='SelectPatientModalWindow'] [class*='master-row']:nth-of-type({row})");
        private ILocator _selectPatientButton => _page.Locator("[data-at='SelectButton']");
        //2.Details
        private ILocator _patientName => _page.Locator("[data-at='PatientName']");
        private ILocator _visitsDropdown => _page.Locator("[data-at='VisitsDropdown'] select");
        private ILocator _visitsValues => _page.Locator("[data-at='VisitsDropdown'] select option");
        private ILocator _providerDropDown => _page.Locator("[data-at='Provider'] select");
        private ILocator _providersValues => _page.Locator("[data-at='Provider'] select option");
        private ILocator _dateTime => _page.Locator("[data-at='DateTime'] input");
        private ILocator _timeoptions => _page.Locator("[class='k-animation-container'] [class='k-list k-list-md'] [role='option']");
        private ILocator _duration => _page.Locator("[data-at='Duration'] select");
        private ILocator _durationValues => _page.Locator("[data-at='Duration'] select option");
        private ILocator _repeat => _page.Locator("[data-at='Repeat'] select");
        private ILocator _repeatValues => _page.Locator("[data-at='Repeat'] select option");
        private ILocator _room => _page.Locator("[data-at='Room'] select");
        private ILocator _roomValues => _page.Locator("[data-at='Room'] select option");
        private ILocator _phone => _page.Locator("[data-at='Phone'] [class='input-group'] input");
        private ILocator _phoneExtention => _page.Locator("[data-at='Phone'] input[maxlength='5']");
        private ILocator _examLevelButton => _page.Locator("[data-at='ExamLevel']");
        private ILocator _codes => _page.Locator("[data-at='Code'] [class*='k-master-row']");
        private ILocator _selectButton => _page.Locator("[data-at='Select']");
        private ILocator _txButton => _page.Locator("[data-at='TX']");
        private ILocator _reasons => _page.Locator("[class*='one-cpt-code']");
        private ILocator _cptCode => _page.Locator("[class*='code-number']");
        private ILocator _notes => _page.Locator("[data-at='Notes']");
        private ILocator _setAppointmentButton => _page.Locator("[data-at='SetAppointmentButton']");
        private ILocator _openDataickerButton => _page.Locator("[aria-label='Open the date view']");
        private ILocator _monthYear => _page.Locator("[id='nav-up']");
        private ILocator _nextMonthButton => _page.Locator("[aria-label='Next']:nth-of-type(1)");
        private ILocator _prevMonthButton => _page.Locator("[aria-label='Previous']:nth-of-type(1)");
        private ILocator _dateButton => _page.Locator("[class*='k-calendar-td']");
        private ILocator _openDatePickerButton => _page.Locator("[aria-label='Open the time view'] ");





        //1. Patient 
        public async Task SelectPatient(string row) => await _patientRow(row).ClickAsync();
        public async Task ClickSelectPatientButton() => await _selectPatientButton.ClickAsync();
        //2.Details

        #region Set
        public async Task SelectVisitType(string visitType)
        {
            await _visitsDropdown.ClickAsync();
            await _visitsDropdown.SelectOptionAsync(new SelectOptionValue { Label = visitType });
        }

        public async Task ClickOpenDataickerButton() => await _openDataickerButton.ClickAsync();

        public async Task SelectDate(string day, string monthYear)
        {
            var thisMonthStringFormat = DateTime.Now.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("en-US").DateTimeFormat);

            var thisMonthDateTimeFormat = DateTime.ParseExact(thisMonthStringFormat, "MMMM yyyy",
                                          System.Globalization.CultureInfo.InvariantCulture);
            var monthYearDateTimeFormat = DateTime.ParseExact(monthYear, "MMMM yyyy", 
                                          System.Globalization.CultureInfo.InvariantCulture);
            var month = monthYear.Split()[0];

            var isBefore = DateTime.Compare(monthYearDateTimeFormat, thisMonthDateTimeFormat);

            while(await _monthYear.TextContentAsync() != monthYear)
            {
                if (isBefore < 0)
                    await _prevMonthButton.ClickAsync();
                else
                    await _nextMonthButton.ClickAsync();
            }

            await _page.Locator($"//td[contains(@class, 'k-calendar-td')] //a[contains(@title, '{month + " " + day}')]")
                       .ClickAsync();
        }

        public async Task ClickOpenTimePickerButton() => await _openDatePickerButton.ClickAsync();

        public async Task SelectTime(string time)
        {
            await _timeoptions.Filter(new() { HasTextString = time }).ClickAsync();
        }

        public async Task SetDateTime(string dateTime) => 
            await _page.EvaluateAsync($"document.querySelector(`[data-at='DateTime'] input`).value = \"{dateTime}\"");

        public async Task SelectProvider(string providerName)
        {
            await _providerDropDown.ClickAsync();
            await _providersValues.Filter(new() { HasTextString = providerName }).ClickAsync();
        }

        public async Task SelectDuration(string duration)
        {
            await _duration.ClickAsync();
            await _duration.SelectOptionAsync(new SelectOptionValue { Value = duration });
        }

        public async Task SelectRepeatTimes(string repeatTimes)
        {
            await _repeat.ClickAsync();
            await _repeatValues.Filter(new() { HasTextString = repeatTimes }).ClickAsync();
        }

        public async Task SelectRoom(string room)
        {
            await _room.ClickAsync();
            await _roomValues.Filter(new() { HasTextString = room }).ClickAsync();
        }

        public async Task SetPhoneNumber(string phoneNumber)
        {
            await _phone.ClearAsync();
            await _phone.FillAsync(phoneNumber);
        }

        public async Task SetPhoneNumberExtention(string extention)
        {
            await _phoneExtention.ClearAsync();
            await _phoneExtention.FillAsync(extention);
        }

        public async Task SetExamLevel(string code)
        {
            await _examLevelButton.ClickAsync();
            await _codes.Filter(new() { HasTextString = code }).ClickAsync();
            await _selectButton.ClickAsync();
        }

        public async Task SetTx(string code)
        {
            await _txButton.ClickAsync();
            await _codes.Filter(new() { HasTextString = code }).ClickAsync();
            await _selectButton.ClickAsync();
        }

        public async Task SetNotes(string text)
        {
            await _notes.ClearAsync();
            await _notes.FillAsync(text);
        }

        public async Task ClickSetAppointmentButton() => await _setAppointmentButton.ClickAsync();
        #endregion

        #region Get
        public async Task<string> GetPatientFullName() => await _patientName.TextContentAsync();

        public async Task<string> GetVisitType()
        {
            return await _page.EvalOnSelectorAsync<string>("[data-at='VisitsDropdown'] select", "sel => sel.options[sel.options.selectedIndex].textContent");

        }


        public async Task<string> GetVisitDate() => await _dateTime.GetAttributeAsync("value");

        public async Task<string> GetDuration()
        {
            return await _page.EvalOnSelectorAsync<string>("[data-at='Duration'] select", "sel => sel.options[sel.options.selectedIndex].textContent");

        }

        public async Task<string> GetPhone()
        {
            var text = await _phone.InputValueAsync();
            var matches = new Regex(@"(\d+)")
                .Matches(text);
            var phone = string.Join("", matches);

            return phone;
        }

        public async Task<string> GetPhoneExtention() => await _phoneExtention.InputValueAsync();

        public async Task<string> GetExamLevel()
        {
            var element = _reasons.Filter(new() { HasTextString = "minutes" });
            return await element.Locator("[class*='code-number']").TextContentAsync();
        }

        public async Task<string> GetTx()
        {
            for (int i = 0; i < await _reasons.CountAsync(); i++)
            {
                var text = await _reasons.Nth(i).TextContentAsync();
                if (!text.Contains("minutes"))
                    return await _reasons.Nth(i).Locator("[class*='code-number']").TextContentAsync();
            }
            return null;
        }

        public async Task<string> GetNotes() => await _notes.InputValueAsync();

        #endregion

        public async Task ClickPatientButon() => await _patientName.ClickAsync();

    }
}
