﻿using CIPlatform_Main.Entities.Models;
using Microsoft.AspNetCore.Http;
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

		public long BannerId { get; set; }

		[Required(ErrorMessage = "Image Is Required")]
		public IFormFile Image { get; set; } = null!;

		[Required(ErrorMessage = "BannerText Is Required")]
		public string? Text { get; set; }

		[Required(ErrorMessage = "SortOrder Is Required")]
		public int? SortOrder { get; set; }

		[Required(ErrorMessage = "CreatedAt Is Required")]
		public DateTime CreatedAt { get; set; }

		public IFormFile bannerImage { get; set; }

		public List<Banner> BannerList { get; set; }
	}
}
