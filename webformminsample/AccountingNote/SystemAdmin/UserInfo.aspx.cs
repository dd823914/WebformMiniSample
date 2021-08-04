﻿using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
	public partial class UserInfo : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)                      //可能是按鈕跳回本頁所以要判斷postback
			{

				if (!AuthManger.IsLogined())    //尚未登入導至登入頁
				{
				Response.Redirect("/Login.aspx"); 
				return;
			}

			var currentUser = AuthManger.GetCurrentUser();

	  
			if(currentUser == null)                                               //如帳號不存在導至登入頁
			{
				
				Response.Redirect("/Login.aspx");
				return;
			}
			this.ltAccount.Text = currentUser.Account;      
			this.ltName.Text = currentUser.Name;
			this.ltEmail.Text = currentUser.Email;
			}
		}

		protected void btnLogout_Click(object sender, EventArgs e)
		{
			AuthManger.Logout();             //登出並導至登入頁
			Response.Redirect("/Login.aspx");
		}
	}
}