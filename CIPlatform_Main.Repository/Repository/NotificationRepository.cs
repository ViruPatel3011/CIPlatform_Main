using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
	public class NotificationRepository:INotification
	{
		private readonly CiPlatformContext _ciPlatformContext;

		public NotificationRepository(CiPlatformContext ciPlatformContext)
		{
			_ciPlatformContext = ciPlatformContext;
		}

		public bool AddCheckedNotification(long[] selectedUserCheckedValues, long userid)
		{
			var findCheckedValues = _ciPlatformContext.EnableUserPreferences.Where(check => check.UserId == userid).ToList();
			_ciPlatformContext.EnableUserPreferences.RemoveRange(findCheckedValues);

			foreach(var checkId in selectedUserCheckedValues)
			{
				EnableUserPreference enableUserP = new EnableUserPreference() 
				{
					NotificationId=checkId,
					UserId=userid,
					CreatedAt=DateTime.Now,
				};
				_ciPlatformContext.EnableUserPreferences.Add(enableUserP);

			}
			_ciPlatformContext.SaveChanges();
			return true;
		}


		public List<string> ShowSpecificUserNotification(long userId)
		{
			var messageList = new List<string>();
			var takeids = _ciPlatformContext.EnableUserPreferences.Where(e => e.UserId == userId).Select(e => e.NotificationId).ToList();
			foreach (var id in takeids)
			{
				var message = _ciPlatformContext.MessageTables.Where(m => m.NotificationId == id && m.ToUser==userId).AsQueryable();
				var messageid = message.Select(m => m.MessageId).ToList();
				foreach (var id1 in messageid)
				{
					var msg = message.FirstOrDefault(m => m.MessageId == id1);
					messageList.Add(msg.Message);
				}
			}
			return messageList;
		}

	}
}
