<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalculationResult.aspx.cs" Inherits="Triangles.Web.CalculationResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ListView runat="server" ID="lvRecords" ItemPlaceholderID="phItem">
            <LayoutTemplate>
                <table>
                    <tr>
                        <td>Кто</td>
                        <td>Сумма</td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="phItem"/>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Who") %></td>
                    <td><%# Eval("Amount") %></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <br/>

        <asp:ListView runat="server" ID="lvTransfers" ItemPlaceholderID="phItem">
            <LayoutTemplate>
                <table>
                    <tr>
                        <td>Кто</td>
                        <td>Кому</td>
                        <td>Сумма</td>
                    </tr>
                    <asp:PlaceHolder runat="server" ID="phItem"/>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("From")%></td>
                    <td><%# Eval("To") %></td>
                    <td><%# Eval("Amount") %></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>


    </form>
</body>
</html>
