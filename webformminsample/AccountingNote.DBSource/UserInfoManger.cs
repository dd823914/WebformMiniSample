﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class UserInfoManger
    {
		
		public static DataRow GetUserInfoByAccount(string account)
		{
			string connectionString =DBHelper.GetConnectingString();
			string dbCommandString = @"SELECT  [ID] ,[Account]  ,[PWD] ,[Name] ,[Email]
									  FROM UserInfo
									  WHERE  [Account]  = @account;
                                      ";



			List<SqlParameter> list = new List<SqlParameter>();
			list.Add(new SqlParameter("@account", account));

			try
			{
				return DBHelper.ReadDataRow(connectionString, dbCommandString, list);

			}
			catch (Exception ex)
			{
				Logger.writeLog(ex);
				return null;
			}
			
	    }

	}
}
