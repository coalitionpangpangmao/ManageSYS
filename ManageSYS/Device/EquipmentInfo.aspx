<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentInfo.aspx.cs" Inherits="Device_EquipmentInfo"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1. 0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设备管理</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
	<link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
	<script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
	<script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
   <script type="text/javascript">
       $(document).ready(function () {
           $("#browser").treeview({
               toggle: function () {
                   console.log("%s was toggled.", $(this).find(">span").text());
               }
           });
         
       });
       function togtreeview() {
           $("#browser").treeview({
               toggle: function () {
                   console.log("%s was toggled.", $(this).find(">span").text());
               }
           });
       }
       function tab1Click(code) {
           $('#tabtop1').click();
           $('#hdcode').val(code);
           $('#btnUpdate').click();
          
       }
       function tab2Click(code) {
           $('#tabtop2').click();
           $('#txtIDKey').val(code);
           $('#btnUpdate').click();
          
       }

        function clsClick() {          
           $("#flowinfo").fadeIn(200);
       }
       function SelClick(code) {
           debugger;
           $("#txtCLS").val(code);
       }
        function Aprvlisthide() {
            $("#flowinfo").fadeOut(100);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">设备管理</a></li>
            <li><a href="#">设备台帐</a></li>
        </ul>
    </div>
     <div class="mainbox">
        <div class="mainleft">
             <div class="leftinfo">
     <div class="listtitle">台帐管理</div>
     <div>
         <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode = "Conditional">
         <ContentTemplate>
    <% = tvHtml %>
    </ContentTemplate>
    <Triggers>
       <asp:AsyncPostBackTrigger ControlID = "btn3Save" />
       <asp:AsyncPostBackTrigger ControlID = "btn3Del" />
    </Triggers>
    </asp:UpdatePanel>
    </div>
    </div>
        </div>
    <div class="mainright">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id = "tabtop1">设备列表</a></li>
                    <li><a href="#tab2"  id = "tabtop2">设备详情</a></li>
                    <li><a href="#tab3"  id = "tabtop3">台帐分类</a></li>
                </ul>
            </div>
            
        </div>
        <div id="tab1" class="tabson">
                <div class = "framelist">
                    <div class="listtitle">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" 
                            onclick="btnSearch_Click" />   
                            <asp:Button ID="btnUpdate" runat="server"  CssClass="btnhide" 
                            onclick="btnUpdate_Click" /> 
                             <asp:HiddenField ID="hdcode" runat="server" /> </span>

                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    设备名称
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNameS" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    设备类型
                                </td>
                                <td>
                                    <asp:TextBox ID="txtType" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                              
                            </tr>                         
                               
                        </tbody>
                    </table>
               
                <div class="listtitle" style="margin-top: 10px">
                    设备列表<span style="position: relative; float: right"><asp:Button 
                    ID="Button1" runat="server"
                    CssClass="btnadd  auth" Text="新增" onclick="btnAdd_Click"  />                   
                        <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" onclick="btnCkAll_Click" />
                         <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" onclick="btnDelSel_Click" />
                    </span></div>
                   <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                 <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="设备编码" 
                        AllowPaging="True"  >
                     <Columns>
                        <asp:TemplateField >
                        <ItemTemplate>                                                  
                            <asp:Button ID="btnGridview" runat="server" Text="设备详情"  CssClass = "btn1 auth" OnClick = "btnGridview_Click" Width = "80px"/>                    
                        </ItemTemplate>
                        </asp:TemplateField >       
                         <asp:TemplateField  HeaderText = "润滑记录">
                        <ItemTemplate>                                                  
                            <asp:Button ID="btnGridrh" runat="server" Text="查看"  CssClass = "btnred" OnClick = "btnGridrh_Click" />                    
                        </ItemTemplate>
                        </asp:TemplateField >    
                         <asp:TemplateField  HeaderText = "维保记录">
                        <ItemTemplate>                                                  
                            <asp:Button ID="btnGridwb" runat="server" Text="查看"  CssClass = "btnred" OnClick = "btnGridwb_Click" />                    
                        </ItemTemplate>
                        </asp:TemplateField >                    
                  </Columns>
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView>
                  </ContentTemplate>
                <Triggers>
               <asp:AsyncPostBackTrigger ControlID = "btnSearch" />
               <asp:AsyncPostBackTrigger ControlID = "btnUpdate" />
               <asp:AsyncPostBackTrigger ControlID = "btnDelSel" />
               <asp:AsyncPostBackTrigger ControlID = "btnCkAll" />
               <asp:AsyncPostBackTrigger ControlID = "btnModify" />
                </Triggers>
            </asp:UpdatePanel>
            </div>
            </div>
             </div> 
        <div id="tab2" class="tabson">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                 <div class = "framelist">                   
                    <table class="tablelist">  
        <tbody>
        <tr>
        <td width="100">设备编码</td>
        <td><asp:TextBox ID="txtIDKey" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">分类路径</td><!-- code name-->
        <td>
          <asp:TextBox ID="txtCLS" runat="server" class="dfinput1"  onclick = "clsClick()"  ></asp:TextBox>
        </td>        
        <td width="100">设备名称</td>
        <td><asp:TextBox ID="txtEqname" runat="server" class="dfinput1"    ></asp:TextBox></td>
        </tr> 
        <tr>
        <td  width="100">固定资产编码</td>
        <td><asp:TextBox ID="txtSGSCode" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td width="100">NC编码</td>
        <td><asp:TextBox ID="txtNCCode" runat="server" class="dfinput1"    ></asp:TextBox></td>
         <td  width="100">财务固定资产名</td>
        <td><asp:TextBox ID="txtFncName" runat="server" class="dfinput1"    ></asp:TextBox></td>
       
        </tr> 
           <tr>
        <td  width="100">企业设备分类</td>
        <td><asp:TextBox ID="txtEQType" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td width="100">设备状态</td>
        <td>
            <asp:DropDownList ID="listEQStatus" runat="server" CssClass = "drpdwnlist">
                <asp:ListItem></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td  width="100">转固日期</td>
        <td><asp:TextBox ID="txtZGDate" runat="server" class="dfinput1"    ></asp:TextBox></td>
          </tr>
         <tr>
        <td  width="100">设备型号</td>
        <td><asp:TextBox ID="txtEQModel" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td width="100">设备资产原值</td>
        <td><asp:TextBox ID="txtOriWorth" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">设备资产净值</td>
        <td><asp:TextBox ID="txtNetWorth" runat="server" class="dfinput1"    ></asp:TextBox></td>
         </tr>
        <tr>
        <td  width="100">投入使用日期</td>
        <td><asp:TextBox ID="txtUsedDate" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td width="100">额定生产能力</td>
        <td><asp:TextBox ID="txtRatedPower" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">实际生产能力</td>
        <td><asp:TextBox ID="txtRealPower" runat="server" class="dfinput1"    ></asp:TextBox></td>
         </tr>
         <tr>
        <td  width="100">能力单位</td>
        <td><asp:TextBox ID="txtPowerUnit" runat="server" class="dfinput1"    ></asp:TextBox></td>
       <td width="100">所属企业名称</td>
        <td><asp:TextBox ID="txtOwner" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">设备来源</td>
        <td><asp:TextBox ID="txtEQSource" runat="server" class="dfinput1"    ></asp:TextBox></td>
        </tr>
         <tr>
        <td  width="100">原所属企业名称</td>
        <td><asp:TextBox ID="txtOriOwner" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">制造商</td>
        <td><asp:TextBox ID="txtManufct" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td width="100">出厂编号</td>
        <td><asp:TextBox ID="txtSerialNo" runat="server" class="dfinput1"    ></asp:TextBox></td>
        </tr>
        <tr>
         <td  width="100">供应商</td>
        <td><asp:TextBox ID="txtSupplier" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">是否特种设备</td>
        <td>
            <asp:RadioButton ID="rdSpecEQ" runat="server" Text=" " />
        </td>
        <td width="100">是否国产</td>
        <td><asp:RadioButton ID="rdMadeChina" runat="server" Text=" " /></td>
         </tr>
        <tr>
        <td  width="100">管理部门</td>
        <td>
            <asp:DropDownList ID="listMGdept" runat="server" CssClass = "drpdwnlist">
            </asp:DropDownList>
        </td>
        <td  width="100">使用部门</td>
        <td><asp:DropDownList ID="listUseDept" runat="server" CssClass = "drpdwnlist">
            </asp:DropDownList></td>
   <td  width="100">责任人</td>
        <td><asp:TextBox ID="txtDutier" runat="server" class="dfinput1"    ></asp:TextBox></td>
        </tr>
        <tr>
        <td  width="100">IP地址</td>
        <td><asp:TextBox ID="txtIp" runat="server" class="dfinput1"    ></asp:TextBox></td>
          <td  width="100">MAC地址</td>
        <td><asp:TextBox ID="txtMAC" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td width="100">设备SN</td>
        <td><asp:TextBox ID="txtSN" runat="server" class="dfinput1"    ></asp:TextBox></td>
        </tr>
        <tr>
        <td  width="100">操作系统</td>
        <td><asp:TextBox ID="txtOpSYS" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td>所属工艺段</td>
        <td>
            <asp:DropDownList ID="listSection" runat="server" CssClass = "drpdwnlist">
            </asp:DropDownList>
        </td>
    <td  width="100">备注</td>
        <td ><asp:TextBox ID="txtDscpt" runat="server" class="dfinput1" 
                ></asp:TextBox></td>
        </tr>
        <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btnAdd" runat="server" Text="重置"  CssClass = "btnset"/>
            <asp:Button ID="btnModify" runat="server" Text="保存"  CssClass = "btnview  auth"/>           

        </td>
        </tr>
        
        </tbody>
        </table>
                    </div>
                </ContentTemplate>
                  <Triggers>                 
               <asp:AsyncPostBackTrigger  ControlID = "GridView1" />
               <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
               <asp:AsyncPostBackTrigger ControlID = "btnUpdate" />
               <asp:AsyncPostBackTrigger ControlID = "txtCLS" />
                  </Triggers>
            </asp:UpdatePanel> 
                  
            </div>   
         <div id="tab3" class="tabson">
                <div class = "framelist">    
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode = "Conditional">
                         <ContentTemplate>   
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    节点名称
                                </td>
                                <td>
                                    <asp:TextBox ID="txt3Name" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    节点编码
                                </td>
                                <td>
                                    <asp:TextBox ID="txt3Code" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                              
                            </tr>                         
                               <tr>
                                <td width="100">
                                    类型
                                </td>
                                <td>
                                   <asp:DropDownList ID = "list3Type" runat = "server" CssClass = "drpdwnlist" />
                                </td>
                                <td width="100">
                                    路径
                                </td>
                                <td>
                                      <asp:DropDownList ID = "list3Path" runat = "server" CssClass = "drpdwnlist" />
                                </td>
                            </tr>
                            <tr>
                            <td>父节点</td>
                            <td colspan="3" valign="top">
                            <div style="height: 300px; overflow: scroll">
                                <asp:TreeView ID="tvClass" runat="server" Width = "450px" 
                                    ImageSet="BulletedList3" ShowExpandCollapse="False" ExpandDepth="3" >
                                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black"  
                                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                                    <ParentNodeStyle Font-Bold="False" />
                                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                                        HorizontalPadding="0px" VerticalPadding="0px" />
                                </asp:TreeView></div>
                               </td>
                            </tr>
                            <tr>
                            <td colspan="4" align="center">
                                <asp:Button ID="btn3Save" runat="server" Text="保存"  CssClass = "btnview  auth" 
                                    onclick="btn3Save_Click"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn3Del" runat="server" Text="删除"  CssClass = "btndel auth" 
                                    onclick="btn3Del_Click"/>
                            </td>
                            </tr>
                        </tbody>
                    </table>
                    </ContentTemplate>  
                    <Triggers>
                    <asp:AsyncPostBackTrigger ControlID = "btn3Save" />
                    <asp:AsyncPostBackTrigger ControlID = "btn3Del" />
                    <asp:AsyncPostBackTrigger ControlID = "tvClass" />
                    </Triggers>
                </asp:UpdatePanel>  
             
            </div>
             </div>
    </div>
    </div>

    <div class="aprvinfo" id="flowinfo">
            <div class="tiptop">
                <span>设备分类</span><a onclick="Aprvlisthide()"></a></div>
            <div class="flowinfo">             
                    <div style="height: 220px; overflow: scroll">
                       <% = tvHtml2 %>
                       </div>
                       <div align="center">
                        <input id="Button2" type="button" value="确定"  onclick = "Aprvlisthide()" class = "sure"/></div>
               
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
