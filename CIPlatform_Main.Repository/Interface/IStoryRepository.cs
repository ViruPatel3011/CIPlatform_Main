using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
	public interface IStoryRepository
	{
		public storyListingVM getStoryDetail(storyListingVM svm);
		public bool getDataForStoryTable(int MissionId, string StoryTitle, string StoryText, DateTime StoryDate, int userId);
	}
}
