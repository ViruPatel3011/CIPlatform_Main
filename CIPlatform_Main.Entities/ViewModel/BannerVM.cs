using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class BannerVM
	{

		[Required(ErrorMessage ="BannerText Is Required")]
		public string bannerText { get; set; }	
		public long BannerId { get; set; }

		public string Image { get; set; } = null!;

		public string? Text { get; set; }

		public int? SortOrder { get; set; }

		public DateTime CreatedAt { get; set; }



		public List<Banner> BannerList { get; set; }
	}
}
