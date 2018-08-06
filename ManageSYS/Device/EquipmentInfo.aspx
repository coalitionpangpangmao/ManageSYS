<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipmentInfo.aspx.cs" Inherits="Device_EquipmentInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1. 0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设备管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-EasyUI/themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-EasyUI/themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-EasyUI/demo.css" />
    <script type="text/javascript" src="../js/jquery-EasyUI/jquery.min.js"></script>
    <script type="text/javascript" src="../js/jquery-EasyUI/jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "../Response/EQ_InfoHandler.ashx",
                success: function (data) {
                    debugger;
                    $("#treenav").tree({
                        data: data,
                        lines: true,
                        onClick: function (node) {                          
                            $('#tabs').tabs('select', '设备列表');
                            $('#hdcode').val(node.id);
                            $('#btnUpdate').click();
                        }
                    });
                    $('#txtCLS').combotree({
                        data: data,
                        valueField:'id',   
                        textField:'text'  
                    });
                   
                    $('#txtPrtnode').combotree({
                        data: data,
                        valueField: 'id',
                        textField: 'text'
                    });
                },
                error: function () {
                    alert("初始化导航树失败");
                }

            });

        });     

        function bindcombo(value) {
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "../Response/EQ_InfoHandler.ashx",
                success: function (data) { 
                    $('#txtCLS').combotree({
                        data: data,
                        valueField: 'id',
                        textField: 'text'
                    });
                    $('#txtCLS').combotree('setValue', value);
                  
                },
                error: function () {
                    alert("初始化导航树失败");
                }

            });
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
                <div class="leftinfo">
                    <div class="listtitle">
                        台帐管理
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class='easyui-panel' style='padding: 5px; border: none'>
                                    <ul class='easyui-tree' id='treenav'>
                                    </ul>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn3Save" />
                                <asp:AsyncPostBackTrigger ControlID="btn3Del" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="mainright">
                <div class="easyui-tabs" style ="margin:0px 0px;" id="tabs">
                    <div title="设备列表" style="padding: 10px">
                      
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
                                        <td class="caption">设备名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNameS" runat="server" class="dfinput1"></asp:TextBox>
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
                                    <asp:Button ID="btnDelSel" runat="server" CssClass="btndel auth" Text="删除" OnClick="btnDelSel_Click" />
                                </span>
                            </div>
                            <div style="width: 100%; height: 350px; overflow: scroll">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="设备编号" AllowPaging="True" HeaderStyle-Wrap="False" RowStyle-Wrap="False">
                                            <Columns>
                                                  <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGridview" runat="server" Text="设备详情" CssClass="btn1 auth" OnClick="btnGridview_Click"
                                                            Width="80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="润滑记录">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGridrh" runat="server" Text="查看" CssClass="btnred" OnClick="btnGridrh_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="维保记录">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGridwb" runat="server" Text="查看" CssClass="btnred" OnClick="btnGridwb_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="gridheader" />
                                            <RowStyle CssClass="gridrow" />
                                            <AlternatingRowStyle CssClass="gridalterrow" />
                                        </asp:GridView>
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
                      
                    </div>
                    <div title="设备详情" style="padding: 10px">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="framelist">
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
                                            </tr>
                                            <tr>
                                                <td class="caption">固定资产编码
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSGSCode" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
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
                                            </tr>
                                            <tr>
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
                                                <td class="caption">转固日期
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtZGDate" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
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
                                            </tr>
                                            <tr>
                                                <td class="caption">能力单位
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPowerUnit" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
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
                                            </tr>
                                            <tr>
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
                                                <td class="caption">出厂编号
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSerialNo" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
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
                                            </tr>
                                            <tr>
                                                <td class="caption">IP地址
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtIp" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
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
                                            </tr>
                                            <tr>
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
                                                <td class="caption">备注
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" align="center">
                                                    <asp:Button ID="btnAdd1" runat="server"
                                    CssClass="btnadd  auth" Text="新增" OnClick="btnAdd_Click" />
                                                       <asp:Button ID="btnModify" runat="server" Text="保存" CssClass="btnview  auth" OnClick ="btnModify_Click" />
                                                    <asp:Button ID="btnReset" runat="server" Text="重置" CssClass="btnset"  OnClick ="btnReset_Click"/>
                                                 

                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                                <asp:AsyncPostBackTrigger ControlID="btnReset" />  
                         
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div title="台帐分类" data-options="iconCls:'icon-help',closable:true" style="padding: 10px">
                        <div class="framelist">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="tablelist">
                                        <tbody>
                                            <tr>
                                                <td class="caption">节点名称
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt3Name" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                                <td class="caption">节点编码
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt3Code" runat="server" class="dfinput1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="caption">类型
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="list3Type" runat="server" CssClass="drpdwnlist" />
                                                </td>
                                                <td class="caption">路径
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="list3Path" runat="server" CssClass="drpdwnlist" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>父节点
                                                </td>
                                                <td colspan="3" valign="top">

   <asp:TextBox ID="txtPrtnode" runat="server" class="easyui-combobox"  Width ="150" Height ="25"  ></asp:TextBox>
                                                  
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" align="center">
                                                    <asp:Button ID="btn3Save" runat="server" Text="保存" CssClass="btnview  auth" OnClick="btn3Save_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btn3Del" runat="server" Text="删除" CssClass="btndel auth" OnClick="btn3Del_Click" />
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btn3Save" />
                                    <asp:AsyncPostBackTrigger ControlID="btn3Del" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
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
