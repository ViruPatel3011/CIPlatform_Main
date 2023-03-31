using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
	public class MissionAndRatingRepository:IMissionAndRating
	{
		private readonly CiPlatformContext _ciPlatformContext;
		public MissionAndRatingRepository(CiPlatformContext ciPlatformContext)
		{
			_ciPlatformContext = ciPlatformContext;
		}


		public MissionAndRatingVM GetDataForRelatedMission(int missionId)
		{
			MissionAndRatingVM mrv = new MissionAndRatingVM();

			var missionDetail = _ciPlatformContext.Missions.Where(x => x.MissionId == missionId).FirstOrDefault();
			mrv.MissionDetail = missionDetail;

			var goalMission = _ciPlatformContext.GoalMissions.Where(x => x.MissionId == missionId).FirstOrDefault();
			mrv.GoalMission = goalMission;

			var city = _ciPlatformContext.Cities.Where(x => x.CityId == missionDetail.CityId).FirstOrDefault();
			mrv.City = city;

			var missionTheme = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == missionDetail.ThemeId).FirstOrDefault();
			mrv.MissionTheme = missionTheme;

			var cities = _ciPlatformContext.Cities.ToList();
			mrv.Cities = cities;

			var missionThemes = _ciPlatformContext.MissionThemes.ToList();
			mrv.MissionThemes = missionThemes;

			//missionRating is for rating 
			var missionRating = _ciPlatformContext.MissionRatings.Where(x => x.MissionId == missionId);

			var missionSkills = _ciPlatformContext.MissionSkills.Include(x => x.Skill).Where(x => x.MissionId == missionId).ToList();
			mrv.MissionSkills = missionSkills;

			// below line is for related mission which is below portion in related mission page
			mrv.RelatedMissions = GetRelatedMission(missionId);

			var favMission = _ciPlatformContext.FavoriteMissions.ToList();
			mrv.FavoriteMissionList = favMission;

			var userListForRecomendation = _ciPlatformContext.Users.ToList();
			mrv.UserForRecommendation = userListForRecomendation;

			var comment = _ciPlatformContext.Comments.ToList();
			mrv.comments = comment;

			var applicationForMission = _ciPlatformContext.MissionApplications.ToList();
			mrv.missionApplications = applicationForMission;

			var missionDoc = _ciPlatformContext.MissionDocuments.ToList();
			mrv.MissionDocuments = missionDoc;


			//the below code is to check that mission is already favourite by user or not
			var fav = isFavourite(missionId);
			mrv.Favourite = fav;

			try
			{
				mrv.RatedVolunteers = missionRating.Count();
				mrv.overallRating = (int)Math.Ceiling(missionRating.Average(x => x.Rating));
			}
			catch
			{
				mrv.RatedVolunteers = 0;
				mrv.overallRating = 0;
			}
			return mrv;
		}
		public IEnumerable<Mission> GetRelatedMission(int id)
		{
			var relatedmissions = new List<Mission>();
			var thismission = _ciPlatformContext.Missions.Where(m => m.MissionId.Equals(id)).FirstOrDefault();
			var relatedmissions_by_city = _ciPlatformContext.Missions.Where(m => m.MissionId != thismission.MissionId && m.CityId == thismission.CityId).Take(3).ToList();
			var relatedmissions_by_country = _ciPlatformContext.Missions.Where(m => m.MissionId != thismission.MissionId && m.CountryId == thismission.CountryId).Take(3).ToList();
			var relatedmissions_by_theme = _ciPlatformContext.Missions.Where(m => m.MissionId != thismission.MissionId && m.MissionId == thismission.MissionId).Take(3).ToList();

			if (relatedmissions_by_city.Count() > 0)
			{
				relatedmissions = relatedmissions_by_city;
			}
			else if (relatedmissions_by_country.Count() > 0)
			{
				relatedmissions = relatedmissions_by_country;
			}
			else
			{
				relatedmissions = relatedmissions_by_theme;
			}
			return relatedmissions;
		}

		//method for second way to add to favourite
		public bool isFavourite(int id)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var favourite = _ciPlatformContext.FavoriteMissions.Where(x => x.MissionId == id && x.UserId == Convert.ToInt32(uid)).FirstOrDefault();
			if (favourite == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}


		// for favourite and un favourite in related mission page 
		public string UpdateAndRate(int missionid, int rating, string Email)
		{
			var mission_rating = _ciPlatformContext.MissionRatings.Include(m => m.Mission).Include(m => m.User).ToList();
			var rate_update = mission_rating.SingleOrDefault(m => m.User.Email == Email && m.Mission.MissionId == missionid);


			if (rate_update != null)
			{
				rate_update.Rating = rating;
				_ciPlatformContext.Update(rate_update);
				rate_update.UpdatedAt = DateTime.Now;
				_ciPlatformContext.SaveChanges();

			}
			else if (rate_update == null)
			{
				var userId = _ciPlatformContext.Users.Where(u => u.Email == Email).Select(u => u.UserId).FirstOrDefault();
				var missionrating = new MissionRating
				{
					MissionId = missionid,
					UserId = userId,
					Rating = rating,
					UpdatedAt = DateTime.UtcNow,
				};

				_ciPlatformContext.Add(missionrating);

				_ciPlatformContext.SaveChanges();

			}
			return "Successfull";
		}


		// recomendation to co-worker
		public List<User> getUsersForRecomandateToCoWorker(string uid)
		{
			var userId = Convert.ToInt32(uid);
			List<User> users = _ciPlatformContext.Users.Where(x => x.UserId != userId).ToList();
			return users;
		}


		public string userWithId(int[] ids, int missionid, string url)
		{

			foreach (var id in ids)
			{

				var user = _ciPlatformContext.Users.SingleOrDefault(m => m.UserId == id);
				var resetLink = url;

				var from = new MailAddress("patelviral7180@gmail.com", "Mail From Viral Patel");

				var to = new MailAddress(user.Email);
				var subject = "Volunteer mission recommend";
				var body = $"Hi,<br /><br />Please click on the following to apply on mission:<br /><br /><a href='{resetLink}'>{resetLink}</a>";
				var message = new MailMessage(from, to)
				{
					Subject = subject,
					Body = body,
					IsBodyHtml = true
				};
				var smtpClient = new SmtpClient("smtp.gmail.com", 587)
				{
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential("patelviral7180@gmail.com", "zztpvcsykhzpxari"),
					EnableSsl = true
				};
				smtpClient.Send(message);
			}

			return "true";
		}

		//-------------------- comment ----------------------

	
		public string commentOnMission(int mId, string commentText1, string uid)
		{


			var userId = Convert.ToInt32(uid);

			if (userId != null)
			{
				var comment = new Comment()
				{
					MissionId = mId,
					UserId =userId,
					ApprovalStatus = commentText1,
				};
				if (commentText1 != null)
				{
					_ciPlatformContext.Comments.Add(comment);
					_ciPlatformContext.SaveChanges();
					return "success";
				}
			}
			return "Failure";
			//else
			//{
			//	TempData["Error"] = "Login First";
			//}

			//return RedirectToAction("RelatedMission", new { id = mId });
		}

	

		//---------------------------------- favourite--------------------------


		public string favouriteMission(int mId, string uid)
		{
			var userId = Convert.ToInt32(uid);
			if (userId != null)
			{
				var data = _ciPlatformContext.FavoriteMissions.Where(x => x.MissionId == mId && x.UserId == userId).FirstOrDefault();

				if (data != null)
				{
					_ciPlatformContext.FavoriteMissions.Remove(data);
				}
				else
				{
					FavoriteMission fm = new FavoriteMission();
					fm.UserId = userId;
					fm.MissionId = mId;
					_ciPlatformContext.Add(fm);
				}

				_ciPlatformContext.SaveChanges();
				return "Success";
			}
			return "Failure";
		}


		// applied mission
		public bool alreadyApplied(int mId, string uid)
		{
			var userId = Convert.ToInt32(uid);
			var alreadyApplied = _ciPlatformContext.MissionApplications.Where(x => x.MissionId == mId && x.UserId == userId).FirstOrDefault();
			if (alreadyApplied == null)
			{
				MissionApplication maapp = new MissionApplication()
				{
					MissionId = mId,
					UserId = Convert.ToInt32(uid),
					AppliedAt = DateTime.Now,
				};
				_ciPlatformContext.Add(maapp);
				_ciPlatformContext.SaveChanges();
				return false;
			}
			else
			{
				var deletRecord = _ciPlatformContext.MissionApplications.Where(x => x.MissionId == mId && x.UserId == userId).FirstOrDefault();
				_ciPlatformContext.Remove(deletRecord);
				_ciPlatformContext.SaveChanges();
				return true;
			}
		}


	}
}
