using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class VolTimeSheetVM
	{

		public string? FirstName { get; set; }

		public string? LastName { get; set; }
		public long MissionId { get; set; }

		public string? MissionType { get; set; }

		public string Title { get; set; }


		[Required(ErrorMessage = "Hours Detail is Required")]
		public int hours { get; set; }

		[Required(ErrorMessage = "Minutes Detail is Required")]
		public int minutes { get; set; }	

		public long? UserId { get; set; }

		public TimeOnly? TimesheetTime { get; set; }


		[Required(ErrorMessage = "Action Field is Required")]
		public int? action { get; set; }

		[Required(ErrorMessage = "Please Select Date")]
		public DateTime volunteeringDate { get; set; }


		[Required(ErrorMessage = "Notes is Required")]
		public string? message { get; set; }
		public string? missionDetail { get; set; }


		public string Status { get; set; } = null!;

		public IEnumerable<User>? UserData { get; set; }
		public IEnumerable<Mission>? MissionListTime { get; set; }
		public IEnumerable<Mission>? MissionListGoal { get; set; }
		public IEnumerable<Timesheet>? TimeBasedSheet { get; set; }
		public IEnumerable<Timesheet>? GoalBasedSheet { get; set; }
	}
}
