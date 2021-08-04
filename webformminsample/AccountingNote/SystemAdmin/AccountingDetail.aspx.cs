using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.SystemAdmin
{
	public partial class AccountingDetail : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//check is logined
			if (!AuthManger.IsLogined())
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

			if (!this.IsPostBack)
			{

				//check is  create mode or edit mode
				if (this.Request.QueryString["ID"] == null)   //編輯
				{
					this.btnDelete.Visible = false;
				}
				else                                           //新增
				{
					this.btnDelete.Visible = true;
					string idText = this.Request.QueryString["ID"];
					int id;
					if (int.TryParse(idText, out id))
					{
						var drAccounting = AccountingManger.GetAccounting(id, currentUser.ID);    //作保護
						if (drAccounting == null)
						{
							this.ltMsg.Text = "data doesn't exsit";
							this.btnSave.Visible = false;
							this.btnDelete.Visible = false;
						}
						else
						{
							
							this.ddlActType.SelectedValue = drAccounting["AcyType"].ToString();
							this.txtAmount.Text = drAccounting["Amount"].ToString();
							this.txtCaption.Text = drAccounting["Caption"].ToString();
							this.txtDesc.Text = drAccounting["Body"].ToString();
						}
					}
					else
					{
						this.ltMsg.Text = "id is required";
						this.btnSave.Visible = false;
						this.btnDelete.Visible = false;
					}
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			List<string> msgList = new List<string>();
			if(!this.CheckInput(out msgList))
			{
				this.ltMsg.Text = string.Join("<br/>", msgList);
			}

			UserInfoModel currentUser = AuthManger.GetCurrentUser();
	
			if (currentUser == null)
			{
				Response.Redirect("/Login.aspx");
				return;
			}

			string userID = currentUser.ID;                  //使用者欄位
			string actTypeText = this.ddlActType.SelectedValue;
			string amountText = this.txtAmount.Text;
			string caption = this.txtCaption.Text;
			string body = this.txtDesc.Text;

			int amount = Convert.ToInt32(amountText);
			int actType = Convert.ToInt32(actTypeText);

			//execute 'insert into db'
			
			string idText = this.Request.QueryString["ID"];
			if (string.IsNullOrWhiteSpace(idText))           //新增模式
			{
				AccountingManger.CreateAccounting(userID, caption, amount, actType, body);
			}
			else                                            //編輯模式
			{
				int id;
				if (int.TryParse(idText, out id))
				{
					AccountingManger.UpdateAccounting(id,userID, caption, amount, actType, body);
				}
			}
			
				Response.Redirect("/SystemAdmin/AccountingList.aspx");

		}
		private bool CheckInput(out List<string> errorMsgList)
		{
			List<string> msgList = new List<string>();

			//Type
			if (this.ddlActType.SelectedValue != "0" && this.ddlActType.SelectedValue != "1")
			{
				msgList.Add("Type must be 0 or 1" );
			}

			//Amount
			if (string.IsNullOrWhiteSpace(this.txtAmount.Text))
			{
				msgList.Add("amount is required");
			}
			else
			{
				int tempInt;
				if (!int.TryParse(this.txtAmount.Text,out tempInt))
				{
					msgList.Add("amount must be a number");
				}
				if(tempInt < 0 || tempInt > 1000000)
				{
					msgList.Add("amount must between 0 and 1,000,000");
				}
			}
			errorMsgList = msgList;
			if (msgList.Count == 0)
				return true;
			else
				return false;

			
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			string idText = this.Request.QueryString["ID"];
			if (string.IsNullOrWhiteSpace(idText))
				return;
				int id;
				if (int.TryParse(idText, out id))
				{
					//excute delete db
					AccountingManger.DeleteAccounting(id);
				}
				Response.Redirect("/SystemAdmin/AccountingList.aspx");
					

			
		}
	}
}