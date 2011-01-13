using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.Samples.Location;

namespace Position_Lib
{
	internal class GpsLocationClass
	{
		Gps _gps = new Gps();
		GpsDeviceState _gpsDeviceState = null;
		
		internal GpsLocationClass()
		{
			_gps.DeviceStateChanged += new DeviceStateChangedEventHandler(gps_DeviceStateChanged);
			if (!_gps.Opened)
			{
				_gps.Open();
			}
		}

		public void Shutdown()
		{
			if (_gps.Opened)
			{
				_gps.Close();
			}
		}

		public Coordinates GetPosition()
		{
			Coordinates retCoords = new Coordinates();

			if (_gpsDeviceState != null &&
				_gpsDeviceState.DeviceState == GpsServiceState.On &&
				_gpsDeviceState.ServiceState == GpsServiceState.On)
			{
				//TimeSpan maxage = new TimeSpan(0, 0, 1);
				GpsPosition position = _gps.GetPosition(TimeSpan.Zero);
				if (position.LatitudeValid && position.LongitudeValid)
				{
					retCoords.Latitude = position.Latitude;
					retCoords.Longitude = position.Longitude;
				}
			}

			return retCoords;
		}

		void gps_DeviceStateChanged(object sender, DeviceStateChangedEventArgs args)
		{
			_gpsDeviceState = args.DeviceState;
		}
	}
}
