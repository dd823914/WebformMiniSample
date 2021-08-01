using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
	public partial class Login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{ 
			if(this.Session["UserLoginInfo"] != null)   //使用者是否登入
			{
				this.plcLogin.Visible = true;
				Response.Redirect("/SystemAdmin/UserInfo.aspx");
			}
			else
			{
				this.plcLogin.Visible = true;
			}
		}

		protected void btnLogin_Click(object sender, EventArgs e)
		{
			
			string inp_Account = this.txtAccount.Text;
			string inp_Password = this.txtPassword.Text;

			//check empty
			if (string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_Password))
			{
				this.ltlMsg.Text = "Account / Password is required";
				return;
			}
			//check null
			var dr = UserInfoManger.GetUserInfoByAccount(inp_Account);
			if (dr == null)
			{
				this.ltlMsg.Text = "Account doesn't exsits";
				return;
			}

			//check account / pwd
			if (string.Compare(dr["Account"].ToString(), inp_Account, true) == 0 && string.Compare(dr["PWD"].ToString(), inp_Password, false) == 0)
			{
				this.Session["UserLoginInfo"] = dr["Account"].ToString();
				Response.Redirect("/SystemAdmin/UserInfo.aspx");   //inp = input
			}
			else
			{
				this.ltlMsg.Text = "Login fail.Please check Account / Password";
				return;
			}
		}

		private static string NewMethod()
		{
			return "Admin";
		}
	}
}