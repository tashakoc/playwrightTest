using ObjectsComparer;
using TechTalk.SpecFlow.Assist;
using TestPlaywright.Drivers;
using TestPlaywright.ModalWindows;
using TestPlaywright.ModelsUi;
using TestPlaywright.Pages;
using TestPlaywright.Pages.PatientProfile;

namespace TestPlaywright.StepDefinitions
{
    [Binding]
    public class CreateNewAppointmentStepDefinitions
    {
        private readonly Driver _driver;
        private readonly AppointmentBookPage _appointmentBookPage;
        private readonly NewAppointmentModalWindow _newAppointmentModalWindow;
        private readonly AppointmentBookTab _appointmentBookTab;
        public CreateNewAppointmentStepDefinitions(Driver driver )
        {
            _driver = driver;
            _appointmentBookPage = new AppointmentBookPage(_driver.Page);
            _newAppointmentModalWindow = new NewAppointmentModalWindow(_driver.Page);
            _appointmentBookTab = new AppointmentBookTab(_driver.Page);

        }

        [Given(@"Add New Appointment action is selected on Appointment Book tab \(Gui\)")]
        public async Task GivenAddNewAppointmentActionIsSelectedOnAppointmentBookTabGui()
        {
            await _appointmentBookPage.ClickAddNewAppointmentButton();
        }

        [When(@"I select '([^']*)' patient on SelectPatient modal window \(Gui\)")]
        public async Task WhenISelectPatientOnSelectPatientModalWindowGui(string row)
        {
            await _newAppointmentModalWindow.SelectPatient(row);
            await _newAppointmentModalWindow.ClickSelectPatientButton();
        }

        [When(@"I set appointment details on '([^']*)' modal window \(Gui\)")]
        public async Task WhenISetAppointmentDetailsOnModalWindowGui(string p0, Table table)
        {
            var appointmentData = table.CreateSet<AppointmentDetailsUiModel>();
            foreach (var data in appointmentData)
            {
                await _newAppointmentModalWindow.SelectVisitType(data.Visit);
                await _newAppointmentModalWindow.ClickOpenDataickerButton();
                await _newAppointmentModalWindow.SelectDate(data.Day, data.MonthYear);
                await _newAppointmentModalWindow.ClickOpenTimePickerButton();
                await _newAppointmentModalWindow.SelectTime(data.Time);
                await _newAppointmentModalWindow.SelectDuration(data.Duration);
                await _newAppointmentModalWindow.SetPhoneNumber(data.Phone);
                await _newAppointmentModalWindow.SetPhoneNumberExtention(data.PhoneExtention);
                await _newAppointmentModalWindow.SetExamLevel(data.ExamLevel);
                await _newAppointmentModalWindow.SetTx(data.TX);
                await _newAppointmentModalWindow.SetNotes(data.Notes);

                await _newAppointmentModalWindow.ClickSetAppointmentButton();

            }
        }

        [Then(@"Created appointment is displayed on Appointment Grid \(Gui\)")]
        public async Task ThenCreatedAppointmentIsDisplayedOnAppointmentGridGui(Table table)
        {
            var expectedAppointmentModels = table.CreateSet<AppointmentDetailsUiModel>();
            foreach (var expectedAppointmentModel in expectedAppointmentModels)
            {
                await _appointmentBookPage.ClickShowFullDayButton();
                await _appointmentBookPage.SelectAppointmentByAppointmentStartTime(expectedAppointmentModel.DateTime);
                var actualAppointmentModel = new AppointmentDetailsUiModel();
                actualAppointmentModel.Visit = await _newAppointmentModalWindow.GetVisitType();
                actualAppointmentModel.DateTime = await _newAppointmentModalWindow.GetVisitDate();
                actualAppointmentModel.Duration = await _newAppointmentModalWindow.GetDuration();
                actualAppointmentModel.Phone = await _newAppointmentModalWindow.GetPhone();
                actualAppointmentModel.PhoneExtention = await _newAppointmentModalWindow.GetPhoneExtention();
                actualAppointmentModel.TX = await _newAppointmentModalWindow.GetTx();
                actualAppointmentModel.ExamLevel = await _newAppointmentModalWindow.GetExamLevel();
                actualAppointmentModel.Notes = await _newAppointmentModalWindow.GetNotes();

                var comparer = new ObjectsComparer.Comparer<AppointmentDetailsUiModel>();
                IEnumerable<Difference> differences;

                var isEqual = comparer.Compare(expectedAppointmentModel, actualAppointmentModel, out differences);
                if(differences != null)
                    foreach (var difference in differences)
                        Console.WriteLine($"[ERROR][{Thread.CurrentThread.ManagedThreadId}][{DateTime.Now}] " +
                        $"{$"Value with key '{difference.MemberPath}' is different! EXPECTED:'{difference.Value1}' ACTUAL:'{difference.Value2}' DifferenceType:'{difference.DifferenceType}'"}");

                isEqual.Should().BeTrue();
            }
        }

        [Then(@"Created appointment is displayed on Patient Appointment Tab \(Gui\)")]
        public async Task ThenCreatedAppointmentIsDisplayedOnPatientAppointmentTabGui(Table table)
        {
            var expectedAppointmentModels = table.CreateSet<AppointmentDetailsUiModel>();
            foreach (var expectedAppointmentModel in expectedAppointmentModels)
            {
                await _newAppointmentModalWindow.ClickPatientButon();
                await _appointmentBookTab.SelectAppointmentBookTab();
                await _appointmentBookTab.SelectCreatedAppointment(expectedAppointmentModel.DateTime);
                var actualAppointmentModel = new AppointmentDetailsUiModel();
                actualAppointmentModel.Visit = await _newAppointmentModalWindow.GetVisitType();
                actualAppointmentModel.DateTime = await _newAppointmentModalWindow.GetVisitDate();
                actualAppointmentModel.Duration = await _newAppointmentModalWindow.GetDuration();
                actualAppointmentModel.Phone = await _newAppointmentModalWindow.GetPhone();
                actualAppointmentModel.PhoneExtention = await _newAppointmentModalWindow.GetPhoneExtention();
                actualAppointmentModel.TX = await _newAppointmentModalWindow.GetTx();
                actualAppointmentModel.ExamLevel = await _newAppointmentModalWindow.GetExamLevel();
                actualAppointmentModel.Notes = await _newAppointmentModalWindow.GetNotes();

                var comparer = new ObjectsComparer.Comparer<AppointmentDetailsUiModel>();
                IEnumerable<Difference> differences;

                var isEqual = comparer.Compare(expectedAppointmentModel, actualAppointmentModel, out differences);
                if (differences != null)
                    foreach (var difference in differences)
                        Console.WriteLine($"[ERROR][{Thread.CurrentThread.ManagedThreadId}][{DateTime.Now}] " +
                        $"{$"Value with key '{difference.MemberPath}' is different! EXPECTED:'{difference.Value1}' ACTUAL:'{difference.Value2}' DifferenceType:'{difference.DifferenceType}'"}");

                isEqual.Should().BeTrue();
            }
        }
    }
}
