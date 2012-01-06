<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExpenditureEditForm.ascx.cs" Inherits="Triangles.Web.ExpenditureEditForm" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Label runat="server" AssociatedControlID="txtId">Запись №:</asp:Label>
<asp:TextBox runat="server" ID="txtId" Text='<%# DataBinder.Eval( Container, "DataItem.Id" ) %>' Enabled="False"/>
<br/>

<asp:RequiredFieldValidator runat="server" ControlToValidate="txtWho" ErrorMessage="Пожалуйста, укажите кто платил"/>
<asp:Label ID="Label2" runat="server" AssociatedControlID="txtWho">Кто:</asp:Label>
<asp:TextBox ID="txtWho" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Who" ) %>' />
<br/>

<asp:RequiredFieldValidator runat="server" ControlToValidate="txtAmount" ErrorMessage="Пожалуйста, укажите сумму"/>
<asp:Label ID="Label1" runat="server" AssociatedControlID="txtAmount">Сколько:</asp:Label>
<asp:TextBox ID="txtAmount" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Amount" ) %>' />
<br/>

<asp:Label ID="Label3" runat="server" AssociatedControlID="txtDescription">Описание:</asp:Label>
<asp:TextBox ID="txtDescription" runat="server" Text='<%# DataBinder.Eval( Container, "DataItem.Description" ) %>' TextMode="MultiLine" />
<br/>

<asp:Button ID="btnUpdate" Text="Update" runat="server" CommandName="Update" Visible='<%# !(DataItem is Telerik.Web.UI.GridInsertionObject) %>' />
<asp:Button ID="btnInsert" Text="Insert" runat="server" CommandName="PerformInsert" Visible='<%# DataItem is Telerik.Web.UI.GridInsertionObject %>' />
<asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False" CommandName="Cancel" />
<br/>