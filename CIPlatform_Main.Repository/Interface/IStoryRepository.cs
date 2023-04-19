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
		public storyListingVM getStoryDetail(int pageIndex, int pageSize );

		/*public bool getDataForStoryTable(int MissionId, string StoryTitle, string StoryText, DateTime StoryDate, int userId, string[] images, string videourl);*/

        public storyDetailVM GetStoryDetail(long userId, int missionId,int count,long storyId);

		public string userWithId(int[] ids, int missionid, string url);
		public List<User> getUsersForRecomandateToCoWorker(string uid);

        public shareYourStoryVM getDataForShareYourStory(string missionid, string uid);	
        public List<Mission> getMissions(long uid);

        public bool getDataForStoryTable(int mid, string sTitle, string sDateAndTime, string sDesc, int userId, string[] images, string videourl);

		/*	public bool saveStory(shareYourStoryVM storyVM, long  userId);*/

		public void submit(long storyId);

		public void editStory(int mid, string sTitle, string sDesc, int userId, string[] images, string videourl);



	}
}
