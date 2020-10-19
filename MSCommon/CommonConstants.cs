using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace MSCommon
{
	public static class CommonConstants
	{
		
		
		public const int MasterServerPort = 14343;
		private static string masterServerAddress;
		
		public static string MasterServerAddress 
		{ 
			get 
			{
				if(masterServerAddress==null || masterServerAddress.Length<2)
                {
					//Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None); ;
					//masterServerAddress = config.AppSettings.Settings["MasterServerAddress"].Value;
				}
				return "localhost";//masterServerAddress;
			}
		} 
		//public const int GameServerPort = 14242;
	}
}
