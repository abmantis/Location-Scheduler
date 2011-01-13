using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using HelperLib;


namespace Position_Lib
{
	public class PositionTools
	{
		static int _earthRadius = 6371; // km
		bool _useCellLocation = false;
		GpsLocationClass _gpsLoc = new GpsLocationClass();

		public PositionTools()
		{
			
		}
		
		public bool UseCellLocation
		{
			get { return _useCellLocation; }
			set { _useCellLocation = value; }
		}

		public void Init()
		{
			Globals.WriteToDebugFile("PositionTools: Init");
			_gpsLoc.Init();
		}

		public void Shutdown()
		{
			Globals.WriteToDebugFile("PositionTools: Shutdown");
			_gpsLoc.Shutdown();
		}

		public Coordinates GetCurrentPosition(int maxGpsAge)
		{			
			try
			{
				Coordinates coord = _gpsLoc.GetPosition(maxGpsAge);
				if (!coord.IsValid() && _useCellLocation)
				{
					coord = CellLocationClass.GetCurrentPosition();
				}
				return coord;
			}
#pragma warning disable 0168
			catch (System.Exception ex) { }
#pragma warning restore 0168

			return new Coordinates();
		}

		public static double GetDistance(Coordinates coord1, Coordinates coord2)
		{
			if (coord1.IsValid() == false || coord2.IsValid() == false) return -1;

			return GetDistance(coord1.Latitude, coord1.Longitude, coord2.Latitude, coord2.Longitude);
		}

		public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
		{
			//////////////////////
			// harversine formula
			// 

			lat1 = DegreeToRadian(lat1);
			lat2 = DegreeToRadian(lat2);
			lon1 = DegreeToRadian(lon1);
			lon2 = DegreeToRadian(lon2);

			double deltaLat = lat2 - lat1;
			double deltaLon = lon2 - lon1;
			double sqrdHalfChordLength = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
										 Math.Cos(lat1) * Math.Cos(lat2) *
										 Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);
			double angularDist = 2 * Math.Atan2(Math.Sqrt(sqrdHalfChordLength), Math.Sqrt(1 - sqrdHalfChordLength));
			double dist = _earthRadius * angularDist;
			return dist;
		}

		private static double DegreeToRadian(double angle)
		{
			return Math.PI * angle / 180.0;
		}
		
		
	}

    
}
