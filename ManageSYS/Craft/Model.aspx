<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Model.aspx.cs" Inherits="Craft_Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>设备层次模型</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            initTree();


        });

        function initTree() {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                },
                persist: "cookie",
                collapsed: true
            });
            $("#gridPanel").scrollTop($("#hideY").val());
            $(".folder").bind("click", function () {
                var code = $(this).attr('value');
                $('#hdcode').val(code);
                if (code.length == 5) {
                    $('#tabtop1').click();
                    $('#btnUpdate1').click();
                }
                else {
                    $('#tabtop2').click();
                    $('#btnUpdate2').click();
                }
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });
            $(".file").bind("click", function () {
                var code = $(this).attr('value');
                $('#hdcode').val(code);
                $('#tabtop3').click();
                $('#btnUpdate3').click();
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });


        }
        function saveScroll() {
            var y = $("#gridPanel").scrollTop();
            $("#hideY").val(y);
        }

    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">工艺管理</a></li>
                <li><a href="#">工艺模型管理</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <div class="mainleft">
                <asp:HiddenField ID="hdcode" runat="server" />
                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hideY" runat="server" />
                        <div class="leftinfo" id="gridPanel"  onscroll="saveScroll()">
                            <div class="listtitle">
                                工艺模型
                            </div>
                            <% = tvHtml %>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnModify1" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel1" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify2" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel2" />
                        <asp:AsyncPostBackTrigger ControlID="btnDel" />
                        <asp:AsyncPostBackTrigger ControlID="btnModify" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!--mainleft end-->
            <div class="mainright">
                <div id="usual1" class="usual">
                    <div class="itab">
                        <ul>
                            <li><a href="#tab1" class="selected" id="tabtop1">工艺段</a></li>
                            <li><a href="#tab2" id="tabtop2">设备</a></li>
                            <li><a href="#tab3" id="tabtop3">参数点</a></li>
                        </ul>
                    </div>
                    <div id="tab1" class="tabson">
                        <div class="gridtools  auth">
                            <asp:Button ID="btnAdd1" CssClass="btnadd auth" runat="server" OnClick="btnAdd1_Click"
                                Text="新增" />
                            &nbsp; &nbsp;
                  
                    <asp:Button ID="btnDel1" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel1_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                              &nbsp; &nbsp;
                              <asp:Button ID="btnModify1" CssClass="btnmodify  auth" runat="server" OnClick="btnModify1_Click"
                        Text="保存" />
                          
                            <asp:Button ID="btnUpdate1" runat="server" CssClass="btnhide" OnClick="btnUpdate1_Click" />
                        </div>
                        <div class="framelist">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table class="tablelist">
                                        <tbody>
                                            <tr>
                                                <td width="100">工艺段编码</td>
                                                <td>
                                                    <asp:TextBox ID="txtCode_1" runat="server" class="dfinput1" Enabled="False" Width ="300px"></asp:TextBox></td>
                                                <td width="100">名称</td>
                                                <td>
                                                    <asp:TextBox ID="txtName_1" runat="server" class="dfinput1" Width ="300px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td width="100">备注</td>
                                                <td>
                                                    <asp:TextBox ID="txtDscrp_1" runat="server" class="dfinput1" Width ="300px"></asp:TextBox></td>
                                                <td width="100">路径配置</td>
                                                <td title=" ">
                                                    <asp:CheckBox ID="rdValid_1" runat="server" Text=" "/>
                                                </td>

                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd1" />
                                    <asp:AsyncPostBackTrigger ControlID="btnUpdate1" />


                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div id="tab2" class="tabson">
                        <div class="gridtools  auth">
                            <asp:Button ID="btnAdd2" CssClass="btnadd auth" runat="server" OnClick="btnAdd2_Click"
                                Text="新增" />
                            &nbsp; &nbsp;
                               <asp:Button ID="btnDel2" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel2_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                             &nbsp; &nbsp;
                    <asp:Button ID="btnModify2" CssClass="btnmodify  auth" runat="server" OnClick="btnModify2_Click"
                        Text="保存" />
                           
                 
                            <asp:Button ID="btnUpdate2" runat="server" CssClass="btnhide" OnClick="btnUpdate2_Click" />
                        </div>
                        <div class="framelist">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="tablelist">
                                        <tbody>
                                            <tr>
                                                <td>工艺段
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listSection_2" runat="server" CssClass="drpdwnlist"  Width="300px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="100">设备分类
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="listSort_2" runat="server" CssClass="drpdwnlist"  Width="300px">
                                                        <asp:ListItem Value="01">生产设备</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>


                                            </tr>
                                            <tr>
                                                <td width="100">设备编码
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCode_2" runat="server" class="dfinput1" Enabled="False"  Width ="300px"></asp:TextBox>
                                                </td>

                                                <td width="100">设备名称
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtName_2" runat="server" class="dfinput1"  Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100">控制设备
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ckCtrl_2" runat="server" Text=""  />
                                                </td>

                                                <td width="100">备注
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDscpt_2" runat="server" class="dfinput1"  Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnUpdate2" />
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd2" />
                                    <asp:AsyncPostBackTrigger ControlID="btnModify1" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <div id="tab3" class="tabson">
                        <div class="gridtools  auth">
                            <asp:Button ID="btnAdd" CssClass="btnadd auth" runat="server" OnClick="btnAdd_Click"
                                Text="新增" />
                            &nbsp; &nbsp;
                              <asp:Button ID="btnDel" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                  &nbsp; &nbsp;
                    <asp:Button ID="btnModify" CssClass="btnmodify  auth" runat="server" OnClick="btnModify_Click"
                        Text="保存" />
                      
                  

                            <asp:Button ID="btnUpdate3" runat="server" CssClass="btnhide" OnClick="btnUpdate3_Click" />
                        </div>
                        <div class="framelist">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="tablelist">
                                        <tbody>
                                            <tr>
                                                <td width="100">所属工艺段</td>
                                                <td>
                                                    <asp:DropDownList ID="listSection" runat="server" CssClass="drpdwnlist" Width="300px" OnSelectedIndexChanged="listSection_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>所属设备</td>
                                                <td>
                                                    <asp:DropDownList ID="listEquip" runat="server" CssClass="drpdwnlist" Width="300px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100">参数点编码</td>
                                                <td>
                                                    <asp:TextBox ID="txtCode" runat="server" class="dfinput1"
                                                        Enabled="False" Width="300px"></asp:TextBox></td>
                                                <td width="100">参数点名称</td>
                                                <td>
                                                    <asp:TextBox ID="txtName" runat="server" Width="300px" class="dfinput1"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td width="100">单位</td>
                                                <td>
                                                    <asp:TextBox ID="txtUnit" runat="server" Width="300px" class="dfinput1"></asp:TextBox></td>
                                                <td width="100">是否有效</td>
                                                <td>
                                                    <asp:CheckBox ID="rdValid" runat="server" Text=" " Checked="true" />
                                                </td>

                                            </tr>
                                            <tr>

                                                <td width="100">类型</td>
                                                <td colspan="3">
                                                    <p>
                                                        <asp:CheckBox ID="ckCenterCtrl" runat="server" Text="报表导出" CssClass="ckfloat" />
                                                        <asp:CheckBox ID="ckRecipePara" runat="server" Text="设定参数" CssClass="ckfloat" />
                                                        <asp:CheckBox ID="ckSetPara" runat="server" Text="工艺参数" CssClass="ckfloat" />
                                                        <asp:CheckBox ID="ckQuality" runat="server" Text="质量统计" OnCheckedChanged="ckQuality_CheckedChanged" AutoPostBack="True" CssClass="ckfloat" />
                                                        <asp:CheckBox ID="ckManul" runat="server" Text="人工录入" OnCheckedChanged="ckManul_CheckedChanged" AutoPostBack="True" CssClass="ckfloat" />
                                                        <asp:CheckBox ID="ckEqpara" runat="server" Text="设备记录" CssClass="ckfloat" Visible="False" />
                                                        <asp:CheckBox ID="ckQuaAnalyze" runat="server" Text="质量考核" OnCheckedChanged="ckQuaAnalyze_CheckedChanged" AutoPostBack="True" CssClass="ckfloat" />
                                                        <asp:CheckBox ID="ckCalibrate" runat="server" Text="计量检查" CssClass="ckfloat" />
                                                          <asp:CheckBox ID="ckProdOut" runat="server" Text="产量统计" CssClass="ckfloat" OnCheckedChanged="ckProdOut_CheckedChanged"  AutoPostBack="True"/>

                                                    </p>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100">设定标签地址</td>
                                                <td>
                                                    <asp:TextBox ID="txtSetTag" runat="server" class="dfinput1"
                                                        Width="300px"></asp:TextBox></td>
                                                <td width="100">反馈标签地址</td>
                                                <td>
                                                    <asp:TextBox ID="txtValueTag" runat="server" class="dfinput1"
                                                        Width="300px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>维护部门</td>
                                                <td>
                                                    <asp:DropDownList ID="listApt" runat="server" CssClass="drpdwnlist" Width="300px"></asp:DropDownList>
                                                </td>
                                                <td width="100">备注</td>
                                                <td>
                                                    <asp:TextBox ID="txtDscrp" runat="server" class="dfinput1"
                                                        Width="300px"></asp:TextBox></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                                    <asp:AsyncPostBackTrigger ControlID="btnUpdate3" />
                                    <asp:AsyncPostBackTrigger ControlID="txtCode" />
                                    <asp:AsyncPostBackTrigger ControlID="listSection" />
                                    <asp:AsyncPostBackTrigger ControlID="ckQuality" />
                                    <asp:AsyncPostBackTrigger ControlID="ckQuaAnalyze" />
                                    <asp:AsyncPostBackTrigger ControlID="btnModify1" />
                                    <asp:AsyncPostBackTrigger ControlID="ckManul" />
                                    <asp:AsyncPostBackTrigger ControlID ="ckProdOut" />
                                    <asp:AsyncPostBackTrigger ControlID ="ckProdOut" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                </div>
            </div>
            <!--mainright end-->
            <script type="text/javascript">
                $("#usual1 ul").idTabs();

                $('.tablelist tbody tr:odd').addClass('odd');
            </script>
        </div>
    </form>
</body>
</html>
