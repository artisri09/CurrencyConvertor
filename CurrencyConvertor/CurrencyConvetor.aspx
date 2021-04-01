<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrencyConvetor.aspx.cs" Inherits="CurrencyConvertor.CurrencyConvetor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="color:blue;font:200;">
         Currency Rate on <b> <asp:Label ID="lblRate" runat="server"> </asp:Label> </b>
        </div>
        <br />
    <div style="width:50%">
   <%-- <table style="width:100%" border="1">
  <tr>
    <th>Currency</th>
    <th>Buy</th>
    <th>Sell</th>
  </tr>
  <tr>
    <td>USD</td>
    <td>1.3392</td>
    <td>1.3574</td>
  </tr>
  <tr>
    <td>HKD</td>
    <td>0.1738</td>
    <td>0.1698</td>
  </tr>
</table>--%>
        <asp:GridView ID="grdCurrency" runat="server"></asp:GridView>
    </div>
        <br /><br />
        <div>
           Amount: <asp:TextBox ID="currencyAmount" runat="server" ViewStateMode="Inherit" CausesValidation="True" ValidationGroup="DigitsOnly" Width="70px">1</asp:TextBox>
            <asp:DropDownList ID="currencyA" runat="server">
                <asp:ListItem Text="SGD" Value="SGD"></asp:ListItem>
               </asp:DropDownList>
            <asp:DropDownList ID="currencyB" runat="server">
                <asp:ListItem Text="HKD" Value="HKD"></asp:ListItem>
                <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:Button ID="buttonConvert" runat="server" Text="Buy" OnClick="buttonConvert_Click" />
            &nbsp;<asp:Button ID="btnSell" runat="server" Text="Sell" OnClick="btnSell_Click" />
            <asp:RangeValidator ID="amountValidator" runat="server" ControlToValidate="currencyAmount" ErrorMessage="Integers greater than zero please." MaximumValue="1000000000" MinimumValue="1" Type="Integer" ForeColor="Red">*</asp:RangeValidator>
            <asp:RequiredFieldValidator ID="requiredValidator" runat="server" ControlToValidate="currencyAmount" ErrorMessage="You must enter a value for the amount." ForeColor="Red">*</asp:RequiredFieldValidator>
            <asp:Label ID="resultAmount" runat="server"></asp:Label>
  <br />
<br />
<asp:ValidationSummary ID="validationSummary"  runat="server" />
        </div>

    </form>
</body>
</html>
