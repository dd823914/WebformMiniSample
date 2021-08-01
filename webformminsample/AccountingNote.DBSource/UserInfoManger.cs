using System;
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


			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				using (SqlCommand command = new SqlCommand(dbCommandString, connection))
				{
					command.Parameters.AddWithValue("@account", account);

					try
					{
						connection.Open();
						SqlDataReader reader = command.ExecuteReader();

						DataTable dt = new DataTable();        //datatable表示記憶體中資料的一個資料表。
						dt.Load(reader);
						reader.Close();

						if (dt.Rows.Count == 0)
						{
							return null;

						}
						DataRow dr = dt.Rows[0];         
						return dr;


					}
					catch (Exception ex)
					{
						Logger.writeLog(ex);
						Console.WriteLine(ex.ToString());
						return null;                     
					}
				}
			}
		}

	}
}
