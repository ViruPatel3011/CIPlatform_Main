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
		public storyListingVM GetStoryDetail(int pageIndex, int pageSize );

		/*public bool getDataForStoryTable(int MissionId, string StoryTitle, string StoryText, DateTime StoryDate, int userId, string[] images, string videourl);*/

        public storyDetailVM GetStoryDetail(long userId, int missionId,int count,long storyId);

		public string UserWithId(int[] ids, int missionid, string url,long userId);
		public List<User> GetUsersForRecomandateToCoWorker(string uid);

        public shareYourStoryVM GetDataForShareYourStory(string missionid, string uid);

		// Method for Get all Mission List in Dropdown
		public List<Mission> GetMissions(long uid);

		// Method for submit new Story in Database
		public bool GetDataForStoryTable(int mid, string sTitle, string sDateAndTime, string sDesc, int userId, string[] images, string videourl);

		/*	public bool saveStory(shareYourStoryVM storyVM, long  userId);*/

		public void Submit(long storyId);


		// Method for Edit data for story
		public void EditSharedStory(int mid, string sTitle, string sDesc, int userId, string[] images, string videourl);



	}
}
