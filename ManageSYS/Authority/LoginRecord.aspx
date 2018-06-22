<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginRecord.aspx.cs" Inherits="Authority_LoginRecord" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>日志管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
     <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>   
      <script type="text/javascript" src="../js/jquery.js"></script>
     <script type ="text/javascript">
         $.ready(function () {
             debugger;
             $(".btn").hide();
         });
     </script> 
</head>
<body >
 <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1"     runat="server">    
              </asp:ScriptManager>           
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">系统管理</a></li>
            <li><a href="#">日志管理</a></li>
        </ul>
    </div>
    <div class="rightinfo">
        <div >
         
         时间  <asp:TextBox ID="StartTime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>   至<asp:TextBox ID="EndTime" class="dfinput1" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox> 
                 &nbsp; 
                 <asp:Button ID="btnSearch" runat="server" Height="25px" Width="72px" 
                Text="查询" CssClass="btn" onclick="btnSearch_Click" />
                  &nbsp;
                  <asp:Button ID="btnDelete" runat="server" Height="25px" Width="72px" 
                Text="删除" CssClass="btn auth" onclick="btnDelete_Click" />  
        </div>
         <asp:UpdatePanel ID="updtpanel"  runat="server">
            <ContentTemplate>
        <div style="overflow: auto; height: 350px;">
        <asp:GridView ID="GridView1" runat="server" Width="900px" CssClass="datable" border="0"
            CellPadding="2" CellSpacing="1">
            <RowStyle CssClass="lupbai" />           
            <HeaderStyle CssClass="lup" />
            <AlternatingRowStyle CssClass="trnei" />
        </asp:GridView>
        </div>
         </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger    ControlID="btnSearch" />
                <asp:AsyncPostBackTrigger    ControlID="btnDelete" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
  
    </form>
</body>
</html>
