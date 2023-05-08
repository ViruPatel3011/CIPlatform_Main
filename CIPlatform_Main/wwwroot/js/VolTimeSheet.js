// Below javascript is for Date Volunteerd Validation in Volunteerd Hours
var TimeMissionSelect = document.getElementById("mId");
var volunteeringDateInputTime = document.getElementById("SelctedDateTime");
var hoursInputTime = document.getElementById("hours");
var minutesInput = document.getElementById("minutes");
var missionDataInput = document.getElementById("missionData");

TimeMissionSelect.addEventListener("change", function () {
	var selectedOption = this.options[this.selectedIndex];
	var startDate = selectedOption.getAttribute("data-start-date");
	var endDate = selectedOption.getAttribute("data-end-date");

	startDate = startDate.split(" ")[0].split("-").reverse().join("-");
	endDate = endDate.split(" ")[0].split("-").reverse().join("-");

	var today = new Date().toISOString().slice(0, 10);

	if (today < startDate) {
		volunteeringDateInputTime.disabled = true;
		hoursInputTime.disabled = true;
		minutesInput.disabled = true;
		missionDataInput.disabled = true;

		volunteeringDateInputTime.value = "";
		hoursInputTime.value = "";
		minutesInput.value = "";
		missionDataInput.value = "Mission not started yet";


	} else if (today > endDate) {
		volunteeringDateInputTime.disabled = true;
		hoursInputTime.disabled = true;
		minutesInput.disabled = true;
		missionDataInput.disabled = true;

		volunteeringDateInputTime.value = "";
		hoursInputTime.value = "";
		minutesInput.value = "";
		missionDataInput.value = "Mission closed !! You can't Enter TimeSheet Now";


	} else {
		volunteeringDateInputTime.disabled = false;
		hoursInputTime.disabled = false;
		minutesInput.disabled = false;
		missionDataInput.disabled = false;

		volunteeringDateInputTime.min = startDate;
		volunteeringDateInput.max = endDate;

		volunteeringDateInputTime.setCustomValidity("");
		hoursInputTime.setCustomValidity("");
		minutesInput.setCustomValidity("");
		missionDataInput.setCustomValidity("");
	}
});

volunteeringDateInputTime.addEventListener("change", function () {
	var selectedOption = TimeMissionSelect.options[TimeMissionSelect.selectedIndex];
	var startDate = new Date(selectedOption.getAttribute("data-start-date")).getTime();
	var endDate = new Date(selectedOption.getAttribute("data-end-date")).getTime();
	var selectedDateTime = new Date(this.value).getTime();
	console.log(startDate);
	console.log(endDate);
	if (selectedDateTime < startDate || selectedDateTime > endDate) {
		this.setCustomValidity("Please select a date and time between the mission start and end dates");
	} else {
		this.setCustomValidity("");
	}
});




const form = document.querySelector('form');

form.addEventListener('submit', (event) => {
	const hoursInputTime = document.getElementById('hours');
	if (hoursInputTime.disabled && !hoursInputTime.value) {
		event.preventDefault();
		alert('Please enter the hours.');
		location.reload();
	}
});


// Below javascript is for Date Volunteerd Validation in Volunteerd Goals
var GoalMissionSelect = document.getElementById("goalDateId");
var volunteeringDateInput = document.getElementById("goalSelectedDate");
var actionInput = document.getElementById("goalAction");
var goalmissionDataInput = document.getElementById("goalmissionData");

GoalMissionSelect.addEventListener("change", function () {
	var selectedOption = this.options[this.selectedIndex];
	var startDate = selectedOption.getAttribute("data-start-date");
	var endDate = selectedOption.getAttribute("data-end-date");

	startDate = startDate.split(" ")[0].split("-").reverse().join("-");
	endDate = endDate.split(" ")[0].split("-").reverse().join("-");

	var today = new Date().toISOString().slice(0, 10);

	if (today < startDate) {
		volunteeringDateInput.disabled = true;
		actionInput.disabled = true;
		goalmissionDataInput.disabled = true;

		volunteeringDateInput.value = "";
		actionInput.value = "";		
		goalmissionDataInput.value = "Mission not started yet";


	} else if (today > endDate) {
		volunteeringDateInput.disabled = true;
		actionInput.disabled = true;	
		goalmissionDataInput.disabled = true;

		volunteeringDateInput.value = "";
		actionInput.value = "";		
		goalmissionDataInput.value = "Mission closed !! You can't Enter TimeSheet Now";


	} else {
		volunteeringDateInput.disabled = false;
		actionInput.disabled = false;
		
		goalmissionDataInput.disabled = false;

		volunteeringDateInput.min = startDate;
		volunteeringDateInput.max = endDate;

		volunteeringDateInput.setCustomValidity("");
		actionInput.setCustomValidity("");
		
		goalmissionDataInput.setCustomValidity("");
	}
});

volunteeringDateInput.addEventListener("change", function () {
	var selectedOption = GoalMissionSelect.options[GoalMissionSelect.selectedIndex];
	var startDate = selectedOption.getAttribute("data-start-date");
	var endDate = selectedOption.getAttribute("data-end-date");

	startDate = new Date(startDate.split(" ")[0]).getTime();
	endDate = new Date(endDate.split(" ")[0]).getTime();
	var selectedDate = new Date(this.value).getTime();
	console.log(startDate);
	console.log(endDate);
	if (selectedDate < startDate || selectedDate > endDate) {
		this.setCustomValidity("Please select a date between the mission start and end date");
	} else {
		this.setCustomValidity("");
	}
});


const secondForm = document.getElementById('second-form');

secondForm.addEventListener('submit', (event) => {
	const goalInput = document.getElementById('goalAction');
	if (goalInput.disabled && !goalInput.value) {
		event.preventDefault();
		alert('Please enter the hours.');
	}
});