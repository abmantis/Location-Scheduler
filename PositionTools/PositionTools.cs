using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.Samples.Location;

namespace Position_Lib
{
	public class Coordinates
	{
		double? latitude = null;
		
		double? longitude = null;

		double precision = 0;

		public Coordinates() { }

		public Coordinates(double lat, double lon)
		{
			latitude = lat;
			longitude = lon;
		}
		public Coordinates(double lat, double lon, double precision) : this(lat, lon)
		{
			this.precision = precision;
		}

		public double Latitude
		{
			get { return (latitude.HasValue)? latitude.Value : 0; }
			set { latitude = value; }
		}
		public double Longitude
		{
			get { return (longitude.HasValue) ? longitude.Value : 0; }
			set { longitude = value; }
		}
		public double Precision
		{
			get { return precision; }
			set { precision = value; }
		}

		public bool IsValid()
		{
			return (latitude.HasValue && longitude.HasValue);
		}

	}
	
	public class PositionTools
	{
		static int _earthRadius = 6371; // km
		bool _useCellLocation = false;
		Gps _gps = new Gps();

		public PositionTools()
		{
			if (!_gps.Opened)
			{
				_gps.Open();
			}
		}
		
		public bool UseCellLocation
		{
			get { return _useCellLocation; }
			set { _useCellLocation = value; }
		}

		public void Shutdown()
		{
			if (_gps.Opened)
            {
                _gps.Close();
			}
		}

		public Coordinates GetCurrentPosition()
		{
			Coordinates coord = GetGPSPosition();
			
			if (!coord.IsValid() && _useCellLocation)
			{
				coord = CellLocationClass.GetCurrentPosition();
			}
			
			return coord;
		}

		private Coordinates GetGPSPosition()
		{
			Coordinates retCoords = new Coordinates();
			GpsPosition position = _gps.GetPosition();
			if (position.LatitudeValid && position.LongitudeValid)
			{
				retCoords.Latitude = position.Latitude;
				retCoords.Longitude = position.Longitude;
			}

			return retCoords;			
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
