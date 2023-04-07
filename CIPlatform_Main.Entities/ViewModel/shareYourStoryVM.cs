

using CIPlatform_Main.Entities.Models;
using Microsoft.AspNetCore.Http;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class shareYourStoryVM
	{

		public string Avatar { get; set; }
		public long userId { get; set; }	
		public string missionName { get; set; }
		public long storyId { get; set; }
		public long missionId { get; set; }
		public string storyTitle { get; set; }
		public string storyDescription { get; set; }
		public DateTime dateAndTime { get; set; }
		public string videoURL { get; set; }

		public string storyStatus { get; set; }	
		public List<string> imagepaths { get; set; }

		public List<User> UserData { get; set; }	

		public List<IFormFile>? ImageFiles { get; set; }
	}
}
