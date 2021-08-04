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

		/// <summary>
		/// 查詢流水帳清單
		/// </summary>
		/// <param name="id"></param>
		/// <param name="userID"></param>
		/// <returns></returns>
		public static DataRow GetAccounting(int id, string userID)  //查詢資料庫資料
		{
			string connStr = DBHelper.GetConnectingString();
			string dbCommand = $@"SELECT ID, Caption, Amount, ActType, CreateDate,Body
                                FROM  Accounting
								WHERE id = @id AND UserID = @userID;
                               ";

			List<SqlParameter> list = new List<SqlParameter>();
			list.Add(new SqlParameter("@id", id));
			list.Add(new SqlParameter("@userID", userID));
			try
			{
				return DBHelper.ReadDataRow(connStr, dbCommand, list);

			}
			catch (Exception ex)
			{
				Logger.writeLog(ex);
				return null;
			}

		}

		public static object GetAccountingList(string iD)
		{
			throw new NotImplementedException();
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
			string connStr = DBHelper.GetConnectingString();
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
					comm.Parameters.AddWithValue("@actType", actType);
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
		/// 變更流水帳
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
			string connStr = DBHelper.GetConnectingString();
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
			List<SqlParameter> paramList = new List<SqlParameter>();
			paramList.Add(new SqlParameter("@id", ID));
			paramList.Add(new SqlParameter("@userID", userID));
			paramList.Add(new SqlParameter("@caption", Caption));
			paramList.Add(new SqlParameter("@amount", amount));
			paramList.Add(new SqlParameter("@actType", actType));
			paramList.Add(new SqlParameter("@createDate", DateTime.Now));
			paramList.Add(new SqlParameter("@body", body));



			//connect db & exexute
			try
			{
				
				int effectRows = DBHelper.ModifyData(connStr, dbCommand,paramList);
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
		}   //編輯用


		/// <summary>
		/// 刪除流水帳
		/// </summary>
		/// <param name="ID"></param>
		public static void DeleteAccounting(int ID)
		{

			string connStr = DBHelper.GetConnectingString();
			string dbCommand =
				$@"DELETE [Accounting]
				 WHERE ID = @id
				";

			List<SqlParameter> paramList = new List<SqlParameter>();
			paramList.Add(new SqlParameter("@id", ID));
			//connect db & exexute

			try
			{
				DBHelper.ModifyData(connStr, dbCommand, paramList);
			}
			catch (Exception ex)
			{
				Logger.writeLog(ex);

			}
		}


	}
}
