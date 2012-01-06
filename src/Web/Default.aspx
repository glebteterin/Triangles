<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Triangles.Web.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <telerik:RadGrid ID="rgExpenditures" AutoGenerateColumns="False" AllowPaging="True"
                      PageSize="15" runat="server" AllowSorting="true" AllowFilteringByColumn="True"
                      GridLines="None"
                      OnNeedDataSource="rgExpenditures_NeedDataSource"
                      AllowAutomaticDeletes="False"
                      AllowAutomaticInserts="False"
                      AllowAutomaticUpdates="False"
                      EnableLinqExpressions="False"
                      OnUpdateCommand="rgExpenditures_UpdateCommand">
            <MasterTableView />
            <PagerStyle Mode="NextPrevAndNumeric" />
            <ClientSettings AllowDragToGroup="True" />
            <GroupingSettings CaseSensitive="false" />
            <FilterMenu EnableTheming="True">
                <CollapseAnimation Duration="200" Type="OutQuint" />
            </FilterMenu>
            <MasterTableView TableLayout="Fixed" DataKeyNames="Id" EditMode="EditForms" CommandItemDisplay="TopAndBottom">
                <Columns>

                    <telerik:GridBoundColumn HeaderText="Кто" DataField="Who" UniqueName="Who"
                        SortExpression="Who" HeaderStyle-Width="200px" FilterControlWidth="140px"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />

                    <telerik:GridBoundColumn HeaderText="Сколько" DataField="Amount" UniqueName="Amount"
                        SortExpression="Amount" HeaderStyle-Width="200px" FilterControlWidth="140px"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />

                    <telerik:GridBoundColumn HeaderText="Описание" DataField="Description" UniqueName="Description"
                        SortExpression="Description" FilterControlWidth="280px"
                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false" />
                        
                    <telerik:GridEditCommandColumn ButtonType="PushButton" HeaderStyle-Width="70px" UniqueName="EditCommandColumn" />

                    <telerik:GridButtonColumn Text="Delete" HeaderStyle-Width="70px" ConfirmText="Delete this advertisment?" ConfirmDialogType="RadWindow"
                                              ConfirmTitle="Delete" ButtonType="PushButton" CommandName="Delete" />
                </Columns>
                <EditFormSettings UserControlName="ExpenditureEditForm.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1"/>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    </form>
</body>
</html>
