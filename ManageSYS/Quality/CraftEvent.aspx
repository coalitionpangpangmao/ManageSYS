<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CraftEvent.aspx.cs" Inherits="Quality_CraftEvent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>工艺事件确认</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
</head>
<body>
<form id="Form1" runat = "server">
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="#">质量评估</a></li>
    <li><a href="#">工艺事件</a></li>
    </ul>
    </div>
    
      <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id = "tabtop1">系统事件</a></li>
                    <li><a href="#tab2"  id = "tabtop2">人工检查</a></li>
                     <li><a href="#tab3"  id = "tabtop3">工艺管理</a></li>
                </ul>
            </div>
        </div>
        <table class = "tablelist">
        <tr><td colspan="4" align="center">
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script> 
   时间
    <asp:TextBox ID="txtStartTime" runat="server"  CssClass = "dfinput1"
        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" ></asp:TextBox>  至
            <asp:TextBox ID="txtEndTime" runat="server"  CssClass = "dfinput1"
        onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" ></asp:TextBox>
    &nbsp;&nbsp;     
    <asp:Button ID="btnSearch" runat="server" Height="25px" Width="72px" Text="查找" 
                CssClass="btn1 auth" onclick="btnSearch_Click"/>
        
        </td></tr></table>
        <div id="tab1" class="tabson">             
    <table class="tablelist" >     
           <tr >
            <th colspan="2" 
                   style=" border-left-style: solid; border-left-width: thin; border-left-color: #3333CC; margin-left: 10px;">
                质量采集事件   </th>  
                 <th colspan="2" style=" margin: 3px;">
                过程断流事件   </th>  
        </tr>
        <tr>
            <td   colspan="2" valign="top" >           

            <div style="width: 100%; height: 200px; overflow: auto;">              

                 <asp:GridView ID="GridView1" runat="server" class="grid" >
                     <HeaderStyle CssClass="gridheader" />
                 <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
            </asp:GridView> </div>
            </td>
             <td   colspan="2" valign="top" >
            <div style="width: 100%; height: 200px; overflow: auto;">
                 <asp:GridView ID="GridView2" runat="server" class="grid" >
                     <HeaderStyle CssClass="gridheader" />
                 <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
            </asp:GridView> </div>
            </td>
</tr>
       
        <tr>
        <th colspan="4">
      结果确认</th>   </tr>
        
<tr><td colspan="4" height="35">
   任务号：
    <asp:TextBox ID="txtBatch" runat="server" CssClass = "dfinput1"></asp:TextBox>
    异常类别
           <asp:DropDownList ID="DropDownList1" runat="server" CssClass = "drpdwnlist" Width = "100px">
        <asp:ListItem Value="0">质量采集事件</asp:ListItem>
        <asp:ListItem Value="1">断流事件</asp:ListItem>      
    </asp:DropDownList>

   数据点
    <asp:TextBox ID="txtPoint" runat="server" CssClass = "dfinput1"></asp:TextBox>
     操作
    <asp:DropDownList ID="listAction" runat="server" CssClass = "drpdwnlist" Width = "70px">
        <asp:ListItem Value="0">未处理</asp:ListItem>
        <asp:ListItem Value="1">己忽略</asp:ListItem>
        <asp:ListItem Value="2">己处理</asp:ListItem>
    </asp:DropDownList>
  扣分
    <asp:TextBox ID="txtScore" runat="server" CssClass = "dfinput1" Width = "70px"></asp:TextBox>
     <asp:Button ID="btnSave" runat="server" Text="保存记录" CssClass="btn1 auth"  
        Width = "80px" onclick="btnSave_Click"/>
</td></tr>
</table>
</div>
  <div id="tab2" class="tabson">
<table class = "tablelist"> 
     <tr><th colspan="4" >人工抽检记录</th></tr>
  
            <tr>
                <td >
                   产品：
                </td>
                <td  >
                    <asp:TextBox ID="txtProd" runat="server"  CssClass = "dfinput1"></asp:TextBox>
                </td>
                <td >
                    数据点
                </td>
                <td  >
                    <asp:DropDownList ID="listpara" runat="server"  CssClass = "drpdwnlist">
                    </asp:DropDownList>
                     
                </td>
            </tr>
            <tr>      
                 <td >
                   数据点值
                </td>
                <td>
                    <asp:TextBox ID="DataValue" runat="server"  CssClass = "dfinput1" ></asp:TextBox>
                </td>
             <td>状态</td>
             <td>
               <asp:DropDownList ID="listStatus" runat="server"  CssClass = "drpdwnlist">
                   <asp:ListItem>正常</asp:ListItem>
                   <asp:ListItem>异常</asp:ListItem>
                    </asp:DropDownList>
             </td>
            </tr>
            <tr>  
                 <td >
                    扣分
                </td>
                <td>
                      <asp:TextBox ID="PointScore" runat="server" CssClass = "dfinput1" ></asp:TextBox>
                </td>     
             <td >
                  班组
                </td>
                <td>
                  <asp:DropDownList ID="listShift" runat="server"  CssClass = "drpdwnlist">  </asp:DropDownList>  
                </td>
                 </tr>
                 <tr>  
               <td colspan="4" align="center">
                    <asp:Button ID="btnAddData" runat="server" Text="添加记录" CssClass="btn1 auth" 
                        Width = "80px" onclick="btnAddData_Click"></asp:Button>
                    
                </td>
            </tr>
            <tr>
            <td colspan="4"  height="200px" align="left" valign="top" 
                    style="border-style: groove; border-width: thin">
                    <div style="width: 100%; height: 200px; overflow: auto;"> 
             <asp:GridView ID="GridView3" runat="server" class="grid" >
                     <HeaderStyle CssClass="gridheader" />
                 <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
            </asp:GridView> 
                </div>
            </td></tr>   
     </table>
     </div>
       <div id="tab3" class="tabson">
     <table class = "tablelist">
     <tr><th colspan="4">工艺检查记录</th></tr>
    <tr>
   
                <td >
                  产品：
                </td>
                <td  >
                    <asp:TextBox ID="listProd2" runat="server" CssClass = "dfinput1"></asp:TextBox>
                    </td>
                <td >
                   工艺检查项目
                </td>
                <td  >
                    <asp:DropDownList ID="listExcept" runat="server"  CssClass = "drpdwnlist">                                    
                    </asp:DropDownList>
                </td>
            </tr>
            <tr> 
                  <td >
                   扣分
                </td>
                <td >
                      <asp:TextBox ID="txtScores2" runat="server"  CssClass= "dfinput1" ></asp:TextBox>
                </td>  
                 <td >
                   班组
                </td>
                <td>
                  <asp:DropDownList ID="listShift2" runat="server"  CssClass = "drpdwnlist">  </asp:DropDownList>  
                </td>                  
            </tr>
            <tr>              
                 <td >
                  详细说明
                </td>
                <td colspan="3" align="left" height="60">
                    <asp:TextBox ID="Descript" runat="server" Width="500px" Height="50px"  CssClass = "dfinput1"
                        TextMode="MultiLine" ></asp:TextBox>
                        
                </td>
                </tr><tr>
                   <td height="30" colspan="4" align="center">
                    <asp:Button ID="AddRecord" runat="server" Text="添加记录" CssClass="btn1 auth"  
                           Width = "80px" onclick="AddRecord_Click"
                        ></asp:Button>                       
                </td>       
               
            </tr>         
            <tr>
            <td colspan="4" height="150px" align="left" valign="top"  >
                    <div style="width: 100%; height: 150px; overflow: auto;"> 
             <asp:GridView ID="GridView4" runat="server" class="grid" >
                     <HeaderStyle CssClass="gridheader" />
                 <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
            </asp:GridView> 
                </div>
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
