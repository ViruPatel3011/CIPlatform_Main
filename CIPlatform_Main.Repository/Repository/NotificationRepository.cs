﻿using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
	public class NotificationRepository : INotification
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

			foreach (var checkId in selectedUserCheckedValues)
			{
				EnableUserPreference enableUserP = new EnableUserPreference()
				{
					NotificationId = checkId,
					UserId = userid,
					CreatedAt = DateTime.Now,
				};
				_ciPlatformContext.EnableUserPreferences.Add(enableUserP);

			}
			_ciPlatformContext.SaveChanges();
			return true;
		}


		public List<MessageTable> ShowSpecificUserNotification(long userId)
		{
			var messageList = new List<MessageTable>();
			var takeids = _ciPlatformContext.EnableUserPreferences.Where(e => e.UserId == userId).Select(e => e.NotificationId).ToList();
			if (takeids == null || !takeids.Any())
			{
				// If takeids is null or empty, retrieve all notifications for the user
				var allMessages = _ciPlatformContext.MessageTables
				.Where(m => m.ToUser == userId)
				.ToList();

				messageList.AddRange(allMessages);

			}
			else
			{
				foreach (var id in takeids)
				{
					var message = _ciPlatformContext.MessageTables.Where(m => m.NotificationId == id && m.ToUser == userId).AsQueryable();
					var messageid = message.Select(m => m.MessageId).ToList();
					foreach (var id1 in messageid)
					{
						var msg = message.FirstOrDefault(m => m.MessageId == id1);
						messageList.Add(msg);
					}
				}
			}
			return messageList;
		}

		public bool MarkAsRead(long messageid)
		{
			var isMessage = _ciPlatformContext.MessageTables.Where(msg => msg.MessageId == messageid).FirstOrDefault();
			if (isMessage != null)
			{
				isMessage.Isread = 1;
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
