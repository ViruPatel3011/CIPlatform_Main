using CIPlatform_Main.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class MissionVMAdmin
	{
		// below field is for mission page

		[Required(ErrorMessage = "MissionTitle Is Required")]
		public string title { get; set; }

		[Required(ErrorMessage = "MissionShortDescription Is Required")]
		public string? shortDescription { get; set; }

		[Required(ErrorMessage = "MissionDescription Is Required")]
		public string? description { get; set; }

		[Required(ErrorMessage = "City Is Required")]
		public long cityId { get; set; }

		[Required(ErrorMessage = "Country Is Required")]
		public long countryId { get; set; }

		[Required(ErrorMessage = "MissionTheme Is Required")]
		public long themeId { get; set; }

		[Required(ErrorMessage = "Organizatiion Name Is Required")]
		public string? organizationName { get; set; }

		[Required(ErrorMessage = "Organization Detail Is Required")]

		public string? organizationDetail { get; set; }

		[Required(ErrorMessage = "StartDate Is Required")]
		public DateTime? startDate { get; set; }

		[Required(ErrorMessage = "endDate Is Required")]

		public DateTime? endDate { get; set; }

		[Required(ErrorMessage = "Deadline Is Required")]
		public DateTime? deadline { get; set; }

		[Required(ErrorMessage = "Total Seats Is Required")]

		public long totalSeats { get; set; }

		[Required(ErrorMessage = "Availability Is Required")]
		public string? availability { get; set; }

		[Required(ErrorMessage = "Image Is Required")]
		public string? status { get; set; }

		[Required(ErrorMessage = "Status Is Required")]
		public string missionType { get; set; }

		[Required(ErrorMessage = "Please Select Images")]

		public List<IFormFile> images { get; set; }

		[Required(ErrorMessage = "Please Select Documents")]
		public List<IFormFile> Documents { get; set; }

		[Required(ErrorMessage = "Image Is Required")]

		public List<Mission> MissionList { get; set; }
		public List<MissionMedium> MissionMediaList { get; set; }
		public List<MissionTheme> MissionThemeList { get; set; }
		public List<Skill> SkillList { get; set; }
		public List<Country> CountryList{ get; set; }
		public List<City> CityList{ get; set; }
	}
}
