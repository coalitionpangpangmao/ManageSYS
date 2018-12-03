<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmgrpExe.aspx.cs" Inherits="Device_EmgrpExe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>应急维修</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript" src="../js/jquery.js"></script>
        <link href="../css/falutsearchbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/msys/falutSearch.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#case").hide();
        });
     
        function ckFaultClick() {
            var ck = $("#ckFault").attr("checked");
            if (ck) {
                $("#case").show();
            }
            else {
                $("#case").hide();
            }
        }
      
    </script>
</head>
<body>
      <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>   
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">设备管理</a></li>
            <li><a href="#">应急维修</a></li>
        </ul>
    </div>
    <div class="formbody">
            <div class="framelist">
                <div class="listtitle" >
                    维修计划执行与处理<span style="position: relative; float: right">
                       <asp:CheckBox ID="ckFault" runat="server" Text="应急维修 "  onclick = "ckFaultClick()" />  <asp:Button ID="btnSumit" runat="server" Text="上报" CssClass="btnview  auth" OnClick = "btnSumit_Click" />
                    </span><span>  </span>
                </div>
                 <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate> 
                            <table class="tablelist" style="margin-bottom: 10px">
                                <tbody>
                                    <tr>
                                          <td width="100">
                                            区域
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listArea" runat="server" CssClass = "drpdwnlist" OnSelectedIndexChanged="listArea_SelectedIndexChanged"  AutoPostBack="true">    
                                        </asp:DropDownList>                                          
                                        </td>       
                                        <td width="100">
                                            维修设备
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listEq" runat="server" CssClass = "drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                  
                                        <td width="100">
                                            操作时间
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOpttime" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" ></asp:TextBox>
                                        </td>
                                        <td width="100">
                                            操作员
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listOptor" runat="server" CssClass = "drpdwnlist" >
                                            </asp:DropDownList>
                                        </td>
                                   
                                                                      
                                    </tr>                                  
                                    <tr><td>维修原因
                                    </td>
                                    <td colspan="3">
                                          <asp:TextBox ID="txtReasons" runat="server" class="dfinput1"  Width = "250px"></asp:TextBox>
                                    </td>
                                    <td>维修内容</td>
                                     <td colspan="3">
                                          <asp:TextBox ID="txtContent" runat="server" class="dfinput1"  Width = "250px"></asp:TextBox>
                                    </td></tr>
                                    </tbody>
                                    </table>
                                     </ContentTemplate>
                        <Triggers>                    
                        <asp:AsyncPostBackTrigger ControlID = "btnSave" />   
                        <asp:AsyncPostBackTrigger ControlID = "btnSumit" />   
                            <asp:AsyncPostBackTrigger ControlID ="listArea" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                    <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>                  
                 <table class="tablelist" id = "case">
                        <tbody>
                      <tr>
                                <td width="100">
                                    故障名
                                </td>
                               <td>
                                           
                                            <asp:TextBox ID="txtFtID" runat="server" CssClass="btnhide"></asp:TextBox>
                                             <asp:Button ID="btnShow" runat="server" CssClass="btnhide" OnClick="btnShow_Click" />
                                            <div class="search"   >                                              
                                                 <asp:TextBox ID="txtName" runat="server" CssClass="dfinput1" onkeyup="keySelectHistory2()" onchange="keySelectHistory2()"   onclick="keySelectHistory()" onblur ="keyhide()"></asp:TextBox>
                                                <div class="text" id="keytext" style="display: none">
                                                </div>
                                            </div>
                                        </td>
                                <td width="100">
                                    故障类型
                                </td>
                              <td>
                                  <asp:DropDownList ID="listEqType" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value = '0'>电气故障</asp:ListItem>
                                            <asp:ListItem Value = '1'>机械故障</asp:ListItem>
                                  </asp:DropDownList>
                                </td>
                                <td width="100">
                                    具体位置
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLocation" runat="server" CssClass = "dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    所属工段
                                </td>
                              <td>
                                  <asp:DropDownList ID="listSection" runat="server" CssClass = "drpdwnlist">
                                  </asp:DropDownList>
                                </td>
                               
                            </tr>
                            <tr>
                                <td width="100">
                                    发生状态
                                </td>
                                <td>
                                    <asp:DropDownList ID="listStyle1" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">渐发性故障</asp:ListItem>
                                    <asp:ListItem Value = "2">突发性故障</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    故障性质
                                </td>
                              <td>
                                    <asp:DropDownList ID="listStyle2" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">间断性</asp:ListItem>
                                    <asp:ListItem Value = "2">永久性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    影响程度
                                </td>
                                <td width="100">
                                    <asp:DropDownList ID="listStyle3" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">完全性</asp:ListItem>
                                    <asp:ListItem Value = "2">局部性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                          
                                <td width="100">
                                    危险性
                                </td>
                                <td>
                                    <asp:DropDownList ID="listStyle4" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">危险性</asp:ListItem>
                                    <asp:ListItem Value = "2">安全性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                  </tr>                         
                             <tr>
                                <td width="100px">
                                    发生原因
                                </td>
                              <td>
                                    <asp:DropDownList ID="listStyle5" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">磨损性</asp:ListItem>
                                    <asp:ListItem Value = "2">错用性</asp:ListItem>
                                    <asp:ListItem Value = "3">固有薄弱性</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    发展规律
                                </td>
                                <td >
                                    <asp:DropDownList ID="listStyle6" runat="server" CssClass = "drpdwnlist">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value = "1">随机故障</asp:ListItem>
                                    <asp:ListItem Value = "2">周期性故障</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="4"></td>
                            </tr>  
                            <tr>
                               <td>故障现场</td>
                            <td colspan="3" height="65px">  <asp:TextBox ID="txtScean" runat="server" CssClass = "dfinput1" 
                                    TextMode="MultiLine" Height="60px" Width="400px"></asp:TextBox>
                            </td>
                           
                            <td>故障描述</td>
                            <td colspan="3" height="65px">  <asp:TextBox ID="txtDescpt" runat="server" CssClass = "dfinput1"   TextMode="MultiLine" Height="60px" Width="400px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                               <td>故障原因</td>
                            <td colspan="3">  <asp:TextBox ID="txtReason" runat="server" CssClass = "dfinput1" 
                                    TextMode="MultiLine" Height="60px" Width="400px"></asp:TextBox>
                            </td>
                           
                               <td>解决方案</td>
                            <td colspan="3" height="65px">  <asp:TextBox ID="txtSolution" runat="server" CssClass = "dfinput1" 
                                    TextMode="MultiLine" Height="60" Width="400px"></asp:TextBox>
                            </td>
                            </tr>                        
                        </tbody>
                    </table>  
                       </ContentTemplate>
                        <Triggers>                    
                        <asp:AsyncPostBackTrigger ControlID = "ckFault" />   
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div  align="center">  
                           
                                <asp:Button ID="btnSave" runat="server" Text="保存" CssClass = "btnmodify  auth" OnClick = "btnSave_Click" />
                          </div>
            </div>
      
        <script type="text/javascript">
            $("#usual1 ul").idTabs(); 
        </script>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </div>
    </form>
</body>
</html>
