<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Energy_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>能耗报表</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">能源管理</a></li>
            <li><a href="#">能耗报表</a></li>
        </ul>
    </div>
    <div class="rightinfo">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">日报表</a></li>
                    <li><a href="#tab2" id="tabtop2">月报表</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div>
                <div>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                        CssClass="grid">
                        <Columns>
                            <asp:BoundField DataField="序号" HeaderText="序号" />
                            <asp:BoundField DataField="分类" HeaderText="分类" />
                            <asp:BoundField DataField="区域" HeaderText="区域" />
                            <asp:BoundField DataField="区域用量" HeaderText="区域用量" />
                            <asp:BoundField DataField="总用量" HeaderText="总用量" />
                            <asp:BoundField DataField="单位" HeaderText="单位" />
                            <asp:BoundField DataField="单价" HeaderText="单价" />
                            <asp:BoundField DataField="费用" HeaderText="费用" />
                        </Columns>
                        <HeaderStyle CssClass="gridheader" />
                        <RowStyle CssClass="gridrow" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div id="tab2" class="tabson">
            <div>
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    CssClass="grid">
                    <Columns>
                        <asp:BoundField DataField="序号" HeaderText="序号" />
                        <asp:BoundField DataField="分类" HeaderText="分类" />
                        <asp:BoundField DataField="区域" HeaderText="区域" />
                        <asp:BoundField DataField="区域用量" HeaderText="区域用量" />
                        <asp:BoundField DataField="总用量" HeaderText="总用量" />
                        <asp:BoundField DataField="单位" HeaderText="单位" />
                        <asp:BoundField DataField="单价" HeaderText="单价" />
                        <asp:BoundField DataField="费用" HeaderText="费用" />
                    </Columns>
                    <HeaderStyle CssClass="gridheader" />
                    <RowStyle CssClass="gridrow" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $("#usual1 ul").idTabs(); 
    </script>
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
