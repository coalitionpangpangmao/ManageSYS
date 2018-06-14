<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyAnlz.aspx.cs" Inherits="Quality_MonthlyAnlz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>月度评估</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
 <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script> 
<script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
</head>


<body>
<form id="Form1" runat = "server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="#">质量评估</a></li>
    <li><a href="#">月度评估</a></li>
    </ul>
    </div>
    
    <div class="rightinfo">
    <table class="tablelist">    
    <tr><td align="center">  
    时间
    <asp:TextBox ID="txtStartTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
       CssClass = "dfinput1"></asp:TextBox>  至
 <asp:TextBox ID="txtEndTime" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"
       CssClass = "dfinput1"></asp:TextBox>  
    <asp:Button ID="btnSearch" runat="server"  CssClass = "btn1 auth" Text="查找" />
        &nbsp;&nbsp;
        <asp:Button ID="btnExport" runat="server"  Text="导出" 
        CssClass="btn1 auth"  />
    </td></tr>	
        
        </table>
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id = "tabtop1">甲班</a></li>
                    <li><a href="#tab2"  id = "tabtop2">乙班</a></li>
                     <li><a href="#tab3"  id = "tabtop3">丙班</a></li>                   
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">             
    <table class = "tablelist">
    <tr><th>质量特性合格率评估
    </th></tr>               
        <tr><td >
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                ShowHeaderWhenEmpty="True"  CssClass = "grid" >
                <Columns>
                    <asp:BoundField DataField="工序" HeaderText="工序"  />
                    <asp:BoundField DataField="权重" HeaderText="权重"  />
                    <asp:BoundField DataField="工序得分" HeaderText="工序得分"  />
                    <asp:BoundField DataField="批次号" HeaderText="批次号"  />
                    <asp:BoundField DataField="牌号" HeaderText="牌号"  />
                    <asp:BoundField DataField="得分" HeaderText="得分"  />
                    <asp:BoundField DataField="是否断料" HeaderText="是否断料"  />
                    <asp:BoundField DataField="断料确认" HeaderText="断料确认" />
                    <asp:BoundField DataField="批开始时间" HeaderText="批开始时间"  />   
                </Columns> 
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />              
            </asp:GridView>
        </td></tr>
            <tr><th>异常情况汇总
    </th></tr>               
        <tr><td >
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                ShowHeaderWhenEmpty="True"  CssClass = "grid" >
                <Columns>
                       <asp:BoundField DataField="计划号" HeaderText="计划号"  />
                    <asp:BoundField DataField="产品" HeaderText="产品"  />
                    <asp:BoundField DataField="项目" HeaderText="项目"  />
                    <asp:BoundField DataField="原因分析" HeaderText="原因分析" />
                    <asp:BoundField DataField="解决措施" HeaderText="解决措施" />
                    <asp:BoundField DataField="物料处理" HeaderText="物料处理"  />
                    <asp:BoundField DataField="扣分" HeaderText="扣分"  />      
                </Columns> 
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />              
            </asp:GridView>
        </td></tr>
        </table>
</div>

<div id="tab2" class="tabson">             
    <table class = "tablelist">
    <tr><th>质量特性合格率评估
    </th></tr>               
        <tr><td >
            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                ShowHeaderWhenEmpty="True"  CssClass = "grid" >
                <Columns>
                    <asp:BoundField DataField="工序" HeaderText="工序"  />
                    <asp:BoundField DataField="权重" HeaderText="权重"  />
                    <asp:BoundField DataField="工序得分" HeaderText="工序得分"  />
                    <asp:BoundField DataField="批次号" HeaderText="批次号"  />
                    <asp:BoundField DataField="牌号" HeaderText="牌号"  />
                    <asp:BoundField DataField="得分" HeaderText="得分"  />
                    <asp:BoundField DataField="是否断料" HeaderText="是否断料"  />
                    <asp:BoundField DataField="断料确认" HeaderText="断料确认" />
                    <asp:BoundField DataField="批开始时间" HeaderText="批开始时间"  />   
                </Columns> 
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />              
            </asp:GridView>
        </td></tr>
            <tr><th>异常情况汇总
    </th></tr>               
        <tr><td >
            <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" 
                ShowHeaderWhenEmpty="True"  CssClass = "grid" >
                <Columns>
                       <asp:BoundField DataField="计划号" HeaderText="计划号"  />
                    <asp:BoundField DataField="产品" HeaderText="产品"  />
                    <asp:BoundField DataField="项目" HeaderText="项目"  />
                    <asp:BoundField DataField="原因分析" HeaderText="原因分析" />
                    <asp:BoundField DataField="解决措施" HeaderText="解决措施" />
                    <asp:BoundField DataField="物料处理" HeaderText="物料处理"  />
                    <asp:BoundField DataField="扣分" HeaderText="扣分"  />      
                </Columns> 
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />              
            </asp:GridView>
        </td></tr>
        </table>
</div>
<div id="tab3" class="tabson">             
    <table class = "tablelist">
    <tr><th>质量特性合格率评估
    </th></tr>               
        <tr><td >
            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                ShowHeaderWhenEmpty="True"  CssClass = "grid" >
                <Columns>
                    <asp:BoundField DataField="工序" HeaderText="工序"  />
                    <asp:BoundField DataField="权重" HeaderText="权重"  />
                    <asp:BoundField DataField="工序得分" HeaderText="工序得分"  />
                    <asp:BoundField DataField="批次号" HeaderText="批次号"  />
                    <asp:BoundField DataField="牌号" HeaderText="牌号"  />
                    <asp:BoundField DataField="得分" HeaderText="得分"  />
                    <asp:BoundField DataField="是否断料" HeaderText="是否断料"  />
                    <asp:BoundField DataField="断料确认" HeaderText="断料确认" />
                    <asp:BoundField DataField="批开始时间" HeaderText="批开始时间"  />   
                </Columns> 
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />              
            </asp:GridView>
        </td></tr>
            <tr><th>异常情况汇总
    </th></tr>               
        <tr><td >
            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" 
                ShowHeaderWhenEmpty="True"  CssClass = "grid" >
                <Columns>
                       <asp:BoundField DataField="计划号" HeaderText="计划号"  />
                    <asp:BoundField DataField="产品" HeaderText="产品"  />
                    <asp:BoundField DataField="项目" HeaderText="项目"  />
                    <asp:BoundField DataField="原因分析" HeaderText="原因分析" />
                    <asp:BoundField DataField="解决措施" HeaderText="解决措施" />
                    <asp:BoundField DataField="物料处理" HeaderText="物料处理"  />
                    <asp:BoundField DataField="扣分" HeaderText="扣分"  />      
                </Columns> 
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />              
            </asp:GridView>
        </td></tr>
        </table>
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
