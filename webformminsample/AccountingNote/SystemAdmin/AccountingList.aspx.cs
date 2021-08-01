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
			if(this.Session["UserLoginInfo"] == null)
			{
				Response.Redirect("/Login.aspx");
				return;
			}
			string account = this.Session["UserLoginInfo"] as string;
			var dr = UserInfoManger.GetUserInfoByAccount(account);
			if(dr == null)
			{
				Response.Redirect("/Login.aspx");
				return;
			}

			//read accounting date
			var dt = AccountingManger.GetAccountingList(dr["ID"].ToString());
			if(dt.Rows.Count > 0)  //check is empty data
			{
				this.gvAccountList.DataSource = dt;
				this.gvAccountList.DataBind();
			}
			else
			{
				this.gvAccountList.Visible = false;
				this.plcNoData.Visible = true;

			}
			
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