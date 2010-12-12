using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using StedySoft.SenseSDK;
using GMap.NET;
using System.Xml.Serialization;	 //For serialization of an object to an XML Document file.

namespace HelperLib
{
	[Serializable]
	public class Task
	{
		public enum ActionTypes { NULL = 0, SMS = 1, NOTIFICATION = 2, APP = 3 };
		
		private int internalIdentifier = 0;
		private String subject = null;
		private String notes = null;
		private PointLatLng? locationCoord = null;
		private String locationAddress = null;
		private String monitorStartTimeStr = null;
		private String monitorEndTimeStr = null;
		private Time monitorStartTime = null;
		private Time monitorEndTime = null;
		private ActionTypes actionType = ActionTypes.NULL;
		private String application = null;
		private String smsRecipient = null;
		private String smsBody = null;

		public Task()
		{
		}

		[XmlIgnore]
		public int InternalIdentifier
		{
			get { return internalIdentifier; }
			set { internalIdentifier = value; }
		}

		[XmlAttribute]
		public System.String Subject
		{
			get { return subject; }
			set { subject = value; }
		}
		
		public System.String Notes
		{
			get { return notes; }
			set { notes = value; }
		}
		
		public PointLatLng? LocationCoord
		{
			get { return locationCoord; }
			set { locationCoord = value; }
		}

		public System.String LocationAddress
		{
			get { return locationAddress; }
			set { locationAddress = value; }
		}

		public String MonitorStartTimeStr
		{
			get { return monitorStartTimeStr; }
			set 
			{ 
				monitorStartTimeStr = value;
				DateTime dt = DateTime.Parse(monitorStartTimeStr);
				monitorStartTime = new StedySoft.SenseSDK.Time(dt);
			}
		}

		public String MonitorEndTimeStr
		{
			get { return monitorEndTimeStr; }
			set
			{
				monitorEndTimeStr = value;
				DateTime dt = DateTime.Parse(monitorEndTimeStr);
				monitorEndTime = new StedySoft.SenseSDK.Time(dt);
			}
		}


		[XmlIgnore]
		public StedySoft.SenseSDK.Time MonitorStartTime
		{
			get { return monitorStartTime; }
			set { monitorStartTime = value; monitorStartTimeStr = monitorStartTime.To24HrFormatString(); }
		}

		[XmlIgnore]
		public StedySoft.SenseSDK.Time MonitorEndTime
		{
			get { return monitorEndTime; }
			set { monitorEndTime = value; monitorEndTimeStr = monitorEndTime.To24HrFormatString(); }
		}
		
		public HelperLib.Task.ActionTypes ActionType
		{
			get { return actionType; }
			set { actionType = value; }
		}
		
		public System.String Application
		{
			get { return application; }
			set { application = value; }
		}
		
		public System.String SmsRecipient
		{
			get { return smsRecipient; }
			set { smsRecipient = value; }
		}
		
		public System.String SmsBody
		{
			get { return smsBody; }
			set { smsBody = value; }
		}
		
		
	}
}
