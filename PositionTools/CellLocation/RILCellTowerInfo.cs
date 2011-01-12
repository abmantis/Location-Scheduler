using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace GetCellDetails.Entities
{
    /*
 * Class definition converted from the struct definition 
 *  RILCELLTOWERINFO from MSDN:
 * 
 * http://msdn2.microsoft.com/en-us/library/aa921533.aspx
 */
    public class RILCELLTOWERINFO
    {
        public uint cbSize;
        public uint dwParams;
        public uint dwMobileCountryCode;
        public uint dwMobileNetworkCode;
        public uint dwLocationAreaCode;
        public uint dwCellID;
        public uint dwBaseStationID;
        public uint dwBroadcastControlChannel;
        public uint dwRxLevel;
        public uint dwRxLevelFull;
        public uint dwRxLevelSub;
        public uint dwRxQuality;
        public uint dwRxQualityFull;
        public uint dwRxQualitySub;
        public uint dwIdleTimeSlot;
        public uint dwTimingAdvance;
        public uint dwGPRSCellID;
        public uint dwGPRSBaseStationID;
        public uint dwNumBCCH;
    }
}
