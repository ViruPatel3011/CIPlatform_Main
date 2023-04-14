using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
	public interface IMissionAndRating
	{
		public MissionAndRatingVM GetDataForRelatedMission(int missionId,int uid);

		public string UpdateAndRate(int missionid, int rating, string Email);
		public string commentOnMission(int mId, string commentText1, string uid);

		//public bool isFavourite(int id);
		public string favouriteMission(int mId, string uid);


		//recomandation to co-worker ;
		public string userWithId(int[] ids, int missionid, string url);
		public List<User> getUsersForRecomandateToCoWorker(string uid);

		public bool alreadyApplied(int mId, string uid);


	}
}
