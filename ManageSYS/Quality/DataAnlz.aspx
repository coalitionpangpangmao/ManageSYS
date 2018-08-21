<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataAnlz.aspx.cs" Inherits="Quality_DataAnlz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>工艺点趋势图</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
</head>
<body>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <form id="Form1" runat="server">
    <div class="mainbox">
        <div class="listtitle">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="labEquip" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="listpoint" runat="server" CssClass="drpdwnlist">
                        </asp:DropDownList>
                    </td>
                    <td>
                        时间：
                        <asp:TextBox runat="server" ID="txtstartTime" CssClass="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                        至
                        <asp:TextBox runat="server" ID="txtendTime" CssClass="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <td>
                        <input id="clear-button" type="button" value="清除" class="btndel" />
                    </td>
                    <td>
                        <input id="refresh-button" type="button" value="刷新" class="btnpatch" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdEquip" runat="server" />
        </div>
        <div>
            <div id="container" style="min-width: 310px; height: 400px; margin: 0 auto">
            </div>
            <div id="statics">
            </div>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="../js/code/highcharts.js"></script>
    <script type="text/javascript" src="../js/code/modules/series-label.js"></script>
    <script type="text/javascript" src="../js/code/modules/exporting.js"></script>
    <script type="text/javascript" src="../js/msys/dataAnlz.js"></script>
</body>
</html>
