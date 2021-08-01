using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class Logger
	{
		public static void writeLog(Exception ex)
		{
			string msg =
			$@" {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
				{ex.ToString()}

			";
			System.IO.File.AppendAllText("D:\\Logs\\Log.log",msg);
			throw ex;
		}
	}
}
