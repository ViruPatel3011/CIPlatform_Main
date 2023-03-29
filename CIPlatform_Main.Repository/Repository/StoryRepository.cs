using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CIPlatform_Main.Repository.Repository
{
	public class StoryRepository:IStoryRepository
	{
		private readonly CiPlatformContext _ciPlatformContext;

		public StoryRepository(CiPlatformContext ciPlatformContext)
		{
			_ciPlatformContext = ciPlatformContext;
		}

		public storyListingVM getStoryDetail(storyListingVM svm)
		{
			storyListingVM tsm = new storyListingVM()
			{
				stories = _ciPlatformContext.Stories.ToList(),
				users = _ciPlatformContext.Users.ToList(),
				missionTheme = _ciPlatformContext.MissionThemes.ToList(),
				allCities = _ciPlatformContext.Cities.ToList(),
				allCountries = _ciPlatformContext.Countries.ToList(),
				skills = _ciPlatformContext.Skills.ToList(),
				allMissionList = _ciPlatformContext.Missions.ToList(),
			};
			return tsm;
		}

		/*public bool getDataForStoryTable(int MissionId, string StoryTitle, string StoryText, DateTime StoryDate, int userId, string[] images, string videourl)
		{
			Story str = new Story();

			str.MissionId = MissionId;
			str.Title = StoryTitle;
			str.PublishedAt = StoryDate;
			str.Description = StoryText;
			str.UserId = userId;
			str.Status = "DRAFT";

			_ciPlatformContext.Stories.Add(str);
            _ciPlatformContext.SaveChanges();


            var story = _ciPlatformContext.Stories.Where(s => s.MissionId == MissionId && s.UserId == userId).FirstOrDefault();

            foreach (var image in images)
            {
                var model1 = new StoryMedium
                {
                    StoryId = story.StoryId,
                    StoryType = "image",
                    StoryPath = image

                };
                _ciPlatformContext.Add(model1);

            }
            var model2 = new StoryMedium
            {
                StoryId = story.StoryId,
                StoryType = "video",
                StoryPath = videourl

            };
            _ciPlatformContext.StoryMedia.Add(model2);
			_ciPlatformContext.SaveChanges();
            return true;

        }*/


        public storyDetailVM GetStoryDetail(long userId, int missionId)
        {
			storyDetailVM sdv = new storyDetailVM();
            sdv.storyTitle = _ciPlatformContext.Stories.Where(x => x.UserId == userId && x.MissionId == missionId).Select(x => x.Title).FirstOrDefault();
            sdv.FirstName = _ciPlatformContext.Users.Where(x => x.UserId == userId).Select(x => x.FirstName).FirstOrDefault();
            sdv.Lastname = _ciPlatformContext.Users.Where(x => x.UserId == userId).Select(x => x.LastName).FirstOrDefault();
            sdv.WhyIVolunteered = _ciPlatformContext.Users.Where(x => x.UserId == userId).Select(x => x.WhyIVolunteer).FirstOrDefault();
            sdv.storyDescription = _ciPlatformContext.Stories.Where(x => x.UserId == userId).Select(x => x.Description).FirstOrDefault();
            sdv.CountryList = _ciPlatformContext.Countries.ToList();
            sdv.CityList = _ciPlatformContext.Cities.ToList();
            sdv.SkillList = _ciPlatformContext.Skills.ToList();
            sdv.MissionThemes = _ciPlatformContext.MissionThemes.ToList();
            sdv.MissionId = missionId;
			sdv.UserData = _ciPlatformContext.Users.ToList();
			sdv.Avatar = _ciPlatformContext.Users.Where(x => x.UserId == userId).Select(x => x.Avatar).FirstOrDefault();
            return sdv;
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

				var from = new MailAddress("patelviral0232@gmail.com", "Mail From Viral Patel");

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
					Credentials = new NetworkCredential("patelviral0232@gmail.com", "jnyrrywmzslgcfap"),
					EnableSsl = true
				};
				smtpClient.Send(message);
			}

			return "true";
		}


        //below 2 method is for edit story
        public List<Mission> getMissions(long userid)
        {
            var missionApplication = _ciPlatformContext.MissionApplications.Where(u => u.UserId == userid).Select(u => u.MissionId);
            return _ciPlatformContext.Missions.Where(u => missionApplication.Contains(u.MissionId)).OrderBy(m => m.Title).ToList();
        }
        public shareYourStoryVM getDataForShareYourStory(string missionid, string uid)
        {

            if (missionid == null)
            {

                return new shareYourStoryVM();

            }
            else
            {
                var story = _ciPlatformContext.Stories.SingleOrDefault(u => u.UserId == long.Parse(uid) && u.MissionId == long.Parse(missionid));

                if (story != null)
                {
                    var storyMedia = _ciPlatformContext.StoryMedia.Where(u => u.StoryId == story.StoryId);
                    var images = storyMedia.Where(m => m.StoryType == "Image").Select(s => s.StoryPath).ToList();
                    var video = storyMedia.SingleOrDefault(m => m.StoryType == "video");
                    var missionTitle = _ciPlatformContext.Missions.SingleOrDefault(m => m.MissionId == story.MissionId);
                    shareYourStoryVM model = new shareYourStoryVM()
                    {
                        missionName = missionTitle.Title,
                        storyId = story.StoryId,
                        missionId = story.MissionId,
                        storyTitle = story.Title,
                        storyDescription = story.Description,
                        dateAndTime = story.CreatedAt,
                        videoURL = video.StoryPath,
                        imagepaths = images,
                    };
                    return model;
                }
                var missionTitle1 = _ciPlatformContext.Missions.SingleOrDefault(m => m.MissionId == long.Parse(missionid));
                shareYourStoryVM model1 = new shareYourStoryVM()
                {
                    missionId = missionTitle1.MissionId,
                    missionName = missionTitle1.Title
                };
                return model1;  
            }
        }

        public bool getDataForStoryTable(int mid, string sTitle, string sDateAndTime, string sDesc, int userId, string[] images, string videourl)
        {
            var alreadyPosteStory = _ciPlatformContext.Stories.Where(x => x.UserId == userId && x.MissionId == mid).FirstOrDefault();
            if (alreadyPosteStory != null)
            {
                return false;
            }
            else
            {
                var model = new Story
                {
                    MissionId = mid,
                    Title = sTitle,
                    Description = sDesc,
                    Status = "DRAFT",
                    UserId = userId
                };

                _ciPlatformContext.Stories.Add(model);
                _ciPlatformContext.SaveChanges();
            }


            var story = _ciPlatformContext.Stories.Where(s => s.MissionId == mid && s.UserId == userId).FirstOrDefault();

            foreach (var image in images)
            {
                var model1 = new StoryMedium
                {
                    StoryId = story.StoryId,
                    StoryType = "image",
                    StoryPath = image

                };
                _ciPlatformContext.StoryMedia.Add(model1);

            }
            var model2 = new StoryMedium
            {
                StoryId = story.StoryId,
                StoryType = "video",
                StoryPath = videourl

            };
            _ciPlatformContext.StoryMedia.Add(model2);
            _ciPlatformContext.SaveChanges();
            return true;
        }


    }
}
