using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
	public interface INotification
	{

		public bool AddCheckedNotification(long[] selectedUserCheckedValues, long userid);

		public List<string> ShowSpecificUserNotification(long userId);
	}
}
