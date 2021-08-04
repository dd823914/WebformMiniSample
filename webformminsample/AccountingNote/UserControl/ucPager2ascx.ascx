<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager2ascx.ascx.cs" Inherits="AccountingNote.UserControl.ucPager2ascx" %>
 <div> 
     <a runat="server" id="aLinkFirst" herf="#">first</a>
     <a runat="server" id="aLink1" herf="#">1</a>
     <a runat="server" id="aLink2" herf="#">2</a>
     <asp:Literal ID="ltlCurrentPage" runat="server"></asp:Literal>
     <a runat="server" id="aLink4" herf="#">4</a>
     <a runat="server" id="aLink5" herf="#">5</a>
     <a runat="server" id="aLinkLast" herf="#">Last</a>
     <asp:Literal ID="ltPager" runat="server"></asp:Literal>
</div> 