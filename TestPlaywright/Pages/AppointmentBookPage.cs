using Microsoft.Playwright;

namespace TestPlaywright.Pages
{
    public class AppointmentBookPage
    {
        private IPage _page;

        public AppointmentBookPage(IPage page) => _page = page;


        internal ILocator GetTodayGridCellsForCurrentDay(string rowNum) =>
       _page.Locator($"[class*='k-scheduler-content'] [role*='row']:nth-of-type({rowNum}) [class*='k-today']");

        private ILocator _events => _page.Locator("[class='k-event']");
        private ILocator _addNewAppointmentButton => _page.Locator("[data-at='AddNewAppointmentButton']");
        private ILocator _appointmentEvent => _page.Locator("[class='k-event']");
        private ILocator _showFullDayButton => _page.Locator("button[class*='scheduler-fullday']");
        private ILocator _eventOnClick => _page.Locator("[class='for-tooltip-onclick']");
        //event-tooltip
        private ILocator _visitType => _page.Locator("[class*='event-tooltip'] [class='visit-type']");
        private ILocator _patient => _page.Locator("[class*='event-tooltip'] [class='patient']");
        private ILocator _eventPeriod => _page.Locator("[class*='event-tooltip'] [class='event-period']");
        private ILocator _provider => _page.Locator("[class*='event-tooltip'] [class='provider']");
        private ILocator _room => _page.Locator("[class*='event-tooltip'] [class='room']");
        private ILocator _phone => _page.Locator("[class*='event-tooltip'] [class='event-phone']");
        private ILocator eventDescription => _page.Locator("[class*='event-tooltip'] [class='event-description']");

        public async Task SelectDateForAppointment(string rowNum) => await _page.Locator($"[class*='k-scheduler-content'] [role*='row']:nth-of-type({rowNum}) [class*='k-today']").First.DblClickAsync();
       
        public async Task ClickAddNewAppointmentButton() => await _addNewAppointmentButton.ClickAsync();

        public async Task ClickShowFullDayButton() => await _showFullDayButton.ClickAsync();

        public async Task SelectAppointmentByAppointmentStartTime(string dateTime)
        {
            var startDate = dateTime.Split();
            var element = _events.Filter(new() { HasText = startDate[1] });
            var onclick = element.First.Locator("[class='for-tooltip-onclick']");

            await onclick.DblClickAsync();
        }
    }
}
