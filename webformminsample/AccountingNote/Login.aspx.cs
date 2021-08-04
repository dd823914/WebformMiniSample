using AccountingNote.Auth;
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
			if (this.Session["UserLoginInfo"] != null)   //使用者是否登入
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

			string msg;
			if (!AuthManger.TryLogin(inp_Account, inp_Password, out msg))
			{
				this.ltlMsg.Text = msg;
				return;
			}

			Response.Redirect("/SystemAdmin/UserInfo.aspx");   //inp = input

		}
	}
}