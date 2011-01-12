using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GetCellDetails.Entities;
using System.Net;
using System.IO;
using System.Xml;
using Position_Lib;


namespace GetCellDetails.Business
{
    class OpenCellIDCellService
    {
		private const string OpenCellID_Service_Uri = "http://www.opencellid.org/cell/get?key=d728e624adad237a39294be2a434ff12";
		
        public static Coordinates GetLocation(RILCELLTOWERINFO t)
        {
			Coordinates coord = new Coordinates();		
            try
            {
                string apiCall = OpenCellID_Service_Uri + 
					"&mnc=" + t.dwMobileNetworkCode + 
					"&mcc=" + t.dwMobileCountryCode + 
					"&lac=" + t.dwLocationAreaCode + 
					"&cellid=" + t.dwCellID;               

                XmlDocument doc = new XmlDocument();
				doc.Load(apiCall);

				//EXAMPLE:
				// <cell mnc="99" lac="0" lat="50.5715642160311" nbSamples="57" range="6000" lon="25.2897075399231" cellId="29513" mcc="250"/> 
				XmlNode rspNode = doc.SelectSingleNode("rsp");
				string status = rspNode.Attributes["stat"].Value;
				status.ToLower();                

				if (status == "ok")
                {                        
                        XmlNode cellNode = rspNode.SelectSingleNode("cell");
                        int samples = int.Parse(cellNode.Attributes["nbSamples"].Value);

                        if (samples>0)
                        {
                            coord.Latitude = double.Parse(cellNode.Attributes["lat"].Value);
                            coord.Longitude = double.Parse(cellNode.Attributes["lon"].Value);
                            coord.Precision = double.Parse(cellNode.Attributes["range"].Value);  
                        }
                }
            }
            catch
            { }
			return coord;
        }        
    }
}
