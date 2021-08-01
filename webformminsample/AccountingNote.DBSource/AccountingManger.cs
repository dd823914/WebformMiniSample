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
	public class AccountingManger 
	{
		private static string GetConnectingString()
		{

			string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			return val;
		}

		
		public static DataTable GetAccountingList(string userID)
		{
			string connStr = GetConnectingString();
			string dbCommand = $@"SELECT ID, Caption, Amount, ActType, CreateDate
                                FROM  Accounting
								WHERE UserID = @userID
                               ";
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				using (SqlCommand comm = new SqlCommand(dbCommand, conn))
				{
					comm.Parameters.AddWithValue("@userID", userID);
					try
					{
						conn.Open();
						var reader = comm.ExecuteReader();

						DataTable dt = new DataTable();
						dt.Load(reader);

						return dt;
					}
					catch (Exception ex)
					{
						Logger.writeLog(ex);
						return null;
					}
				}
			}
		}
		/// <summary>
		/// 查詢流水帳清單
		/// </summary>
		/// <param name="id"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public static DataRow GetAccounting(int id, string userID)  //查詢資料庫資料
		{
			string connStr = GetConnectingString();
			string dbCommand = $@"SELECT ID, Caption, Amount, ActType, CreateDate,Body
                                FROM  Accounting
								WHERE id = @id AND UserID = @userID;
                               ";
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				using (SqlCommand comm = new SqlCommand(dbCommand, conn))
				{
					comm.Parameters.AddWithValue("@id", id);
					comm.Parameters.AddWithValue("@userID", userID);
					try
					{
						conn.Open();
						var reader = comm.ExecuteReader();

						DataTable dt = new DataTable();
						dt.Load(reader);

						if (dt.Rows.Count == 0)
							return null;
						return dt.Rows[0];
					}
					catch (Exception ex)
					{
						Logger.writeLog(ex);
						return null;
					}
				}
			}
		}

		/// <summary>
		/// 建立流水帳
		/// </summary>
		/// <param name="userID"></param>
		/// <param name="Caption"></param>
		/// <param name="amount"></param>
		/// <param name="actType"></param>
		/// <param name="body"></param>
		public static void CreateAccounting(string userID, string Caption, int amount, int actType, string body)
		{
			//check input
			if (amount < 0 || amount > 1000000)
				throw new ArgumentException("amount must between 0 and 1,000,000");
			if (actType < 0 || actType > 1)
				throw new ArgumentException("actype must be 0 or 1.");
			string connStr = GetConnectingString();
			string dbCommand =
				$@"INSERT INTO [dbo].[Accounting]
                  (UserID
                  ,Caption
                  ,Amount
                 ,ActType
                 ,CreateDate
                 ,Body) 
                  VALUES
                  @UserID
                  @Caption
                  @Amount
                  @ActType
                  @CreateDate
                  @Body

				";

			//connect db & exexute
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				using (SqlCommand comm = new SqlCommand(dbCommand, conn))
				{

					comm.Parameters.AddWithValue("@userID", userID);
					comm.Parameters.AddWithValue("@caption", Caption);
					comm.Parameters.AddWithValue("@amount", amount);
					comm.Parameters.AddWithValue("@actType",actType);
					comm.Parameters.AddWithValue("@createDate", DateTime.Now);
					comm.Parameters.AddWithValue("@body", body);
					try
					{
						conn.Open();
					    comm.ExecuteNonQuery();

					}
					catch (Exception ex)
					{
						Logger.writeLog(ex);
	
					}
				}
			}
		}

		/// <summary>
		/// 建立流水帳
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="userID"></param>
		/// <param name="Caption"></param>
		/// <param name="amount"></param>
		/// <param name="actType"></param>
		/// <param name="body"></param>
		public static bool UpdateAccounting(int ID, string userID, string Caption, int amount, int actType, string body)
		{
			//check input
			if (amount < 0 || amount > 1000000)
				throw new ArgumentException("amount must between 0 and 1,000,000");
			if (actType < 0 || actType > 1)
				throw new ArgumentException("actype must be 0 or 1.");
			string connStr = GetConnectingString();
			string dbCommand =
				$@"UPDATE [Accounting]
				   SET
                  UserID       =  @UserID
                  ,Caption     =  @Caption
                  ,Amount      =  @Amount
                  ,ActType     =  @ActType
                  ,CreateDate  =  @CreateDate
                  ,Body        =  @Body
				 WHERE ID = @id
				";

			//connect db & exexute
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				using (SqlCommand comm = new SqlCommand(dbCommand, conn))
				{
					comm.Parameters.AddWithValue("@id", ID);
					comm.Parameters.AddWithValue("@userID", userID);
					comm.Parameters.AddWithValue("@caption", Caption);
					comm.Parameters.AddWithValue("@amount", amount);
					comm.Parameters.AddWithValue("@actType", actType);
					comm.Parameters.AddWithValue("@createDate", DateTime.Now);
					comm.Parameters.AddWithValue("@body", body);
					
					try
					{
						conn.Open();
						int effectRows = comm.ExecuteNonQuery();
						if (effectRows == 1)
							return true;
						else
							return false;

					}
					catch (Exception ex)
					{
						Logger.writeLog(ex);
						return false;

					}
				}
			}
		}   //編輯用

		public static void DeleteAccounting(int ID)
		{
			
			string connStr = GetConnectingString();
			string dbCommand =
				$@"DELETE [Accounting]
				 WHERE ID = @id
				";

			//connect db & exexute
			using (SqlConnection conn = new SqlConnection(connStr))
			{
				using (SqlCommand comm = new SqlCommand(dbCommand, conn))
				{
					comm.Parameters.AddWithValue("@id", ID);
					
					try
					{
						conn.Open();
					    comm.ExecuteNonQuery();
						

					}
					catch (Exception ex)
					{
						Logger.writeLog(ex);
					
					}
				}
			}
		}

	}
}
