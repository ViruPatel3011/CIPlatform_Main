using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class shareYourStoryVM
	{
		public string missionName { get; set; }
		public long storyId { get; set; }
		public long missionId { get; set; }
		public string storyTitle { get; set; }
		public string storyDescription { get; set; }
		public DateTime dateAndTime { get; set; }
		public string videoURL { get; set; }
		public List<string> imagepaths { get; set; }
	}
}
