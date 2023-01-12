Feature: CreateNewAppointment

Background:
	Given Navigate to 'DWWeb Client site' (Ui)
	And Successful authorization as a current user (Ui)
	And Navigate to 'Appointment Book page' (Ui)

Scenario: It is possible to create new appointment for existing patient
	Given Add New Appointment action is selected on Appointment Book tab (Gui)
	When I select '1' patient on SelectPatient modal window (Gui)
	And I set appointment details on 'New Appointment - 2. Details' modal window (Gui)
		| MonthYear     | Day | Time     | Visit                        | Duration | Phone      | PhoneExtention | ExamLevel | Tx    | Notes         |
		| December 2022 | 29  | 10:45 AM | Sleep Screening Consultation | 30       | 1234567879 | 34567          | 99205     | 00174 | AtAppointment |
	Then Created appointment is displayed on Appointment Grid (Gui)
		| DateTime            | Visit                        | Duration | Phone      | PhoneExtention | ExamLevel | Tx    | Notes         |
		| 12/28/2022 10:45 AM | Sleep Screening Consultation | 30 min   | 1234567879 | 34567          | 99205     | 00174 | AtAppointment |
	And Created appointment is displayed on Patient Appointment Tab (Gui)
		| DateTime            | Visit                        | Duration | Phone      | PhoneExtention | ExamLevel | Tx    | Notes         |
		| 12/29/2022 10:45 AM | Sleep Screening Consultation | 30 min   | 1234567879 | 34567          | 99205     | 00174 | AtAppointment |
      