using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlaywright.Pages.PatientProfile
{
    public class AppointmentBookTab
    {
        private IPage _page;

        public AppointmentBookTab(IPage page) => _page = page;

        private ILocator _appointmentBookTab => _page.Locator("[data-at='AppointmentBook'] a");
        private ILocator _gridCells => _page.Locator("[role='gridcell']");


        public async Task SelectAppointmentBookTab() => await _appointmentBookTab.ClickAsync();
        public async Task SelectCreatedAppointment(string appointmentDate)
        {
            var textTotransform = appointmentDate.Split('/');
            var day = textTotransform[1].StartsWith("0") ? textTotransform[1].Replace("0", "") : textTotransform[1];
            var result = textTotransform[0] switch
            {
                "01" => "Jan",
                "02" => "Feb",
                "03" => "Mar",
                "04" => "Apr",
                "05" => "May",
                "06" => "Jun",
                "07" => "Jul",
                "08" => "Aug",
                "09" => "Sep",
                "10" => "Oct",
                "11" => "Nov",
                "12" => "Dec",
            };

            result += $" {day}, {textTotransform[2]}";

            await _gridCells.Filter(new() { HasText = result }).ClickAsync();
        }

    }
}
