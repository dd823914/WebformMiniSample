using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
	public partial class AccountList : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//check is logined
			
			if(!AuthManger.IsLogined())
			{
				Response.Redirect("/Login.aspx");
				return;
			}
			var currentUser = AuthManger.GetCurrentUser();


			if (currentUser == null)                                               //如帳號不存在導至登入頁
			{
				this.Session["UserLoginInfo"] = null;
				Response.Redirect("/Login.aspx");
				return;
			}

			//read accounting date
			var dt = AccountingManger.GetAccountingList(currentUser, ID);
			if (dt.Rows.Count <= 0)  //check is empty data
			{
				int totalPages = this.GetTotalPages(dt);
				var dtPaged = this.GetPagedDataTable(dt);

				this.gvAccountList.DataSource = dtPaged;
				this.gvAccountList.DataBind();

				this.ucPager.TotalSize = dt.Rows.Count;
				this.ucPager.Bind();

				var pages = (dt.Rows.Count / 10);
				if (dt.Row.Count % 10 > 0)
					pages += 1;
				
			
			}
			else
			{

				this.gvAccountList.Visible = false;
				this.plcNoData.Visible = true;
			}

		}
	
		private int GetCurrentPage()
		{
			string pageText = Request.QueryString["Pages"];
			if (string.IsNullOrWhiteSpace(pageText))
				return 1;
			int intPage;
			if (!int.TryParse(pageText, out intPage))
				return 1;
			if (intPage <= 0)
				return 1;
			return intPage;
		}
		
        private DataTable GetPagedDataTable(DataTable dt)
		{
			DataTable dtPaged = dt.Clone();  //copy整個結構

			//foreach(DataRow dr in dt.Rows)    //複製現有資料做回傳
			//for(var i = 0; i <dt.Rows.Count; i++)

			int startIndex = (this.GetCurrentPage() - 1) * 10;   
			int endtIndex = (this.GetCurrentPage() ) * 10;

			if (endtIndex > dt.Rows.Count)      //演算修正
				endtIndex = dt.Rows.Count;

			for (var i = 0; i < endtIndex; i++)
			{
				DataRow dr = dt.Rows[i];
				var drNew = dtPaged.NewRow();
				foreach(DataColumn dc in dt.Columns)
				{
					drNew[dc.ColumnName] = dr[dc];

				}
				dtPaged.Rows.Add(drNew);
			}
			return dtPaged;
			
		}
		protected void btnCreate_Click(object sender, EventArgs e)
		{
			Response.Redirect("/SystemAdmin/AccountingDetail.aspx");
		}

		protected void gvAccountList_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			var row = e.Row;
			if(row.RowType == DataControlRowType.DataRow)
			{
				Literal literal = row.FindControl("ltActType") as Literal;
				Label label = row.FindControl("lblActType") as Label;
				//literal.Text = "ok";

				var dr = row.DataItem as DataRowView;
				int actType = dr.Row.Field<int>("ActType");

				if (actType == 0)
				{
					//literal.Text = "支出";
					label.Text = "支出";

				}
				else
				{
					//literal.Text = "收入";
					label.Text = "收入";
				}
				if(dr.Row.Field<int>("Amount") > 1500)
				{
					label.ForeColor = Color.Red;
				}
					

				
			}
			

		}
	}
}