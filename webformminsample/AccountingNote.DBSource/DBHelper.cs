using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
	public class DBHelper
	{
		public static string GetConnectingString()
		{

			string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			return val;
		}

	}
}
