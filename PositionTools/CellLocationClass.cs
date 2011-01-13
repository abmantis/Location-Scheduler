using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using GetCellDetails.Business;
using GetCellDetails.Entities;
using HelperLib;

namespace Position_Lib
{
    public class CellLocationClass
    {
		public static Coordinates GetCurrentPosition()
        {

            /*
             zona de lisboa
             * Cell:12664 mcc:268 mnc:1 lac:8  OK
             zona do porto
             * Cell:4661727 mcc:268 mnc:3 lac:51000 OK
             cidade de madrid
             * Cell:10487683 mcc:214 mnc:3 lac:21601    OK
             mexico
             * Cell:50243 mcc:310 mnc:100 lac:5 OK
             * 
             * CellTowerInfo.dwCellID = 123365432;      ERROR
             * CellTowerInfo.dwLocationAreaCode = 124654321;
             * CellTowerInfo.dwMobileCountryCode = 310;
             * CellTowerInfo.dwMobileNetworkCode = 100; 
             * 
             *CellTowerInfo.dwCellID = 123365432;       ERROR
             *CellTowerInfo.dwLocationAreaCode = 3;
             *CellTowerInfo.dwMobileCountryCode = 543214;
             *CellTowerInfo.dwMobileNetworkCode = 100;
             *
             * CellTowerInfo.dwCellID = 123365432;
             * CellTowerInfo.dwLocationAreaCode = 3;
             * CellTowerInfo.dwMobileCountryCode = 108;
             * CellTowerInfo.dwMobileNetworkCode = 12765433;
             */

            RILCELLTOWERINFO CellTowerInfo = CellData.GetCellTowerInfo();

            Coordinates location = OpenCellIDCellService.GetLocation(CellTowerInfo);
			return location;
        }
    }
}
