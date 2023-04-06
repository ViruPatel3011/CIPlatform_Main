using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
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

		public int Hours { get; set; }	
		public int Minutes { get; set; }	

		public long? UserId { get; set; }

		public TimeOnly? TimesheetTime { get; set; }

		public int? Action { get; set; }

		public DateTime DateVolunteered { get; set; }

		public string? Notes { get; set; }

		public string Status { get; set; } = null!;

		public IEnumerable<Mission>? MissionListTime { get; set; }
		public IEnumerable<Mission>? MissionListGoal { get; set; }
		public IEnumerable<Timesheet>? TimeBasedSheet { get; set; }
		public IEnumerable<Timesheet>? GoalBasedSheet { get; set; }
	}
}
