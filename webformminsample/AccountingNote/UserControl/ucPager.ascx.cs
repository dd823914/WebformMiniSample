using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote.UserControl
{
	public partial class ucPageer : System.Web.UI.UserControl
	{
		/// <summary>
		///頁面URL
		/// </summary>
		public string url { get; set; }
		/// <summary>
		/// 總筆數
		/// </summary>
		public int TotalSize { get; set; }
		/// <summary>
		/// 頁面筆數
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		///頁面筆數
		/// </summary>
		public int CurrentPage { get; set; }


		protected void Page_Load(object sender, EventArgs e)
		{
			
			//this.Bind();
		}
		public void Bind()
		{
			int totalPages = this.GetTotalPages();
			for (var i = 1; i <= totalPages; i++)
			{
				this.ltPager.Text = $"共{dt.Rows.Count}筆，共{Page}頁,目前在第{this.GetCurrentPage()} 頁";
				this.ltPager.Text += "<a herf='AccountingList.aspx?'>page={i}'</a>&nbsp;";
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
		private int GetTotalPages()
		{
			int pagers = this.TotalSize / this.PageSize;   //取得總筆數
			if ((this.TotalSize % this.PageSize) > 0)
				pagers += 1;
			return pagers;
		}
	}
}