<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentInfo.aspx.cs" Inherits="Device_EquipmentInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1. 0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设备管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script> 
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                },
                persist: "cookie",
                collapsed: true

            });
            $(".folder").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });
           

        });

     
        function treeClick(code) {
            $('#hdcode').val(code);
            $('#btnUpdate').click();
        }
      
     
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
                <div class="leftinfo" >
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="listtitle">
                            台帐管理</div>
                        <% = tvHtml %>
                    </ContentTemplate>
                    <Triggers>
                     
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            </div>
            <div class="mainright">               
                   
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                            <div class="listtitle">
                                查询条件<span style="position: relative; float: right">
                                    <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btnhide" OnClick="btnUpdate_Click" />
                                    <asp:HiddenField ID="hdcode" runat="server" />
                                </span>
                            </div>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td class="caption">工艺段
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listSectionM" runat="server" CssClass ="drpdwnlist"></asp:DropDownList>
                                        </td>
                                        <td class="caption">设备类型
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtType" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="listtitle" style="margin-top: 10px">
                                设备列表<span style="position: relative; float: right"><asp:Button ID="btnAdd" runat="server"
                                    CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnCkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnCkAll_Click" />
                                    <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click"  OnClientClick="javascript:return confirm('确认删除？');"/>
                                </span>
                            </div>
                            <div >                              
                                        <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="设备编号" AllowPaging="True" HeaderStyle-Wrap="False" RowStyle-Wrap="False" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize ="12">
                                            <Columns>
                                                  <asp:TemplateField     >
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField     >
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGridview" runat="server" Text="设备详情" CssClass="btn1 auth" OnClick="btnGridview_Click"
                                                            Width="80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField      HeaderText="润滑记录">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGridrh" runat="server" Text="查看" CssClass="btnred" OnClick="btnGridrh_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField      HeaderText="维保记录">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGridwb" runat="server" Text="查看" CssClass="btnred" OnClick="btnGridwb_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField      HeaderText="维修记录">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGridwx" runat="server" Text="查看" CssClass="btnred" OnClick="btnGridwx_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridheader" />
                                            <RowStyle CssClass="gridrow" />
                                            <AlternatingRowStyle CssClass="gridalterrow" />
                                                  <PagerStyle CssClass="gridpager" />
                            <PagerTemplate>
                                <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> ' Width="120px"></asp:Label>
                                <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
                                <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                                <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                                <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                                到第
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />
                                页  
             <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                 CommandName="Page" Text="跳转" />

                            </PagerTemplate>
                                        </asp:GridView>
                                  
                            </div>
                        </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                                        <asp:AsyncPostBackTrigger ControlID="btnDelSel" />
                                        <asp:AsyncPostBackTrigger ControlID="btnCkAll" />
                                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                                        <asp:AsyncPostBackTrigger ControlID ="btnAdd" />                                        
                                    </Triggers>
                                </asp:UpdatePanel>
          
        </div>
         <div class="shade">
                <div  style="width:1100px; height:380px; position:absolute;top:6%; left:5%;background:#fcfdfd;box-shadow:1px 8px 10px 1px #9b9b9b;border-radius:1px;behavior:url(js/pie.htc); ">
                    <div class="tiphead">
                        <span>设备详情</span><a onclick="$('.shade').fadeOut(100);"></a>
                    </div>
                    <div class="gridinfo">                  
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                               
                                    <table class="tablelist">
                                        <tbody>
                                            <tr>
                                                <td class="caption">设备编码
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIDKey" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                                </td>
                                                <td class="caption">分类路径
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCLS" runat="server" class="easyui-combobox"  Width ="150" Height ="25" ></asp:TextBox>
                                                 
                                                  
                                                </td>
                                                <td class="caption">设备名称
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEqname" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                          
                                                <td class="caption">固定资产编码
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSGSCode" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                  </tr>
                                            <tr>
                                                <td class="caption">NC编码
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNCCode" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">财务固定资产名
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFncName" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                           
                                                <td class="caption">企业设备分类
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEQType" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">设备状态
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listEQStatus" runat="server" CssClass="drpdwnlist">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                 </tr>
                                            <tr>
                                                <td class="caption">转固日期
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtZGDate" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                          
                                                <td class="caption">设备型号
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEQModel" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">设备资产原值
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOriWorth" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">设备资产净值
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNetWorth" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="caption">投入使用日期
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUsedDate" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">额定生产能力
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRatedPower" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">实际生产能力
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRealPower" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                          
                                                <td class="caption">能力单位
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPowerUnit" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                  </tr>
                                            <tr>
                                                <td class="caption">所属企业名称
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOwner" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">设备来源
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEQSource" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                     
                                                <td class="caption">原所属企业名称
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOriOwner" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">制造商
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtManufct" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                       </tr>
                                            <tr>
                                                <td class="caption">出厂编号
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSerialNo" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                         
                                                <td class="caption">供应商
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSupplier" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">是否特种设备
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rdSpecEQ" runat="server" Text=" " />
                                                </td>
                                                <td class="caption">是否国产
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="rdMadeChina" runat="server" Text=" " />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="caption">管理部门
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listMGdept" runat="server" CssClass="drpdwnlist">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="caption">使用部门
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listUseDept" runat="server" CssClass="drpdwnlist">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="caption">责任人
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDutier" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                          
                                                <td class="caption">IP地址
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIp" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                  </tr>
                                            <tr>
                                                <td class="caption">MAC地址
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMAC" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">设备SN
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSN" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                          
                                                <td class="caption">操作系统
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOpSYS" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td>所属工艺段
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listSection" runat="server" CssClass="drpdwnlist">
                                                    </asp:DropDownList>
                                                </td>
                                                  </tr>
                                            <tr>
                                                <td class="caption">备注
                                                </td>
                                                <td colspan ="7">
                                                    <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1" Width ="700px"></asp:TextBox>
                                                </td>
                                            </tr>                                           
                                        </tbody>
                                    </table>   
                         <div class="shadebtn" align="center">                                    
                                    <asp:Button ID="btnModify" class="sure" runat="server" Text="保存" OnClick="btnModify_Click" />
                                    <input name="" type="button" class="cancel" value="关闭" onclick="$('.shade').fadeOut(100);" />
                                </div>
                              </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />                                                                                   <asp:AsyncPostBackTrigger ControlID="btnAdd" />       
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </form>
</body>
</html>
