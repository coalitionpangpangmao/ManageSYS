<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Materia.aspx.cs" Inherits="Craft_Materia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".click1").click(function () {
                $("#addtip").fadeIn(200);
            });

            $(".click2").click(function () {
                $("#mdftip").fadeIn(200);
            });

            $(".click3").click(function () {
                $("#deltip").fadeIn(200);
            });

            $(".tiptop a").click(function () {
                $(".tip").fadeOut(200);
            });

            $(".sure").click(function () {
                $(".tip").fadeOut(100);
            });

            $(".cancel").click(function () {
                $(".tip").fadeOut(100);
            });

        });
        function GridClick(code) {
            window.parent.tab2Click(code);
        /*    $('#tabtop2', parent.document).click();
            debugger;
            $('#Frame2', parent.document).contents().find("'*[id$=hdcode]'").attr('value', code.substr(4));
            $('#Frame2', parent.document).contents().find("'*[id$=btnUpdate]'").click();*/
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="framelist">
        <div class="listtitle">
            类型查询与维护<span style="position: relative; float: right">
                <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btnhide" />
                <asp:HiddenField ID="hdcode" runat="server" />
            </span>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    分类名称
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    分类编码
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    父级分类
                                </td>
                                <td>
                                    <asp:DropDownList ID="listPrt" runat="server" CssClass="drpdwnlist">
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    是否有效
                                </td>
                                <td>
                                    <asp:CheckBox ID="ckValid" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="tools  auth">
            <ul class="toolbar">
                <li class="click1 auth"><span>
                    <img src="../images/t01.png" /></span>添加</li>
                <li class="click2  auth"><span>
                    <img src="../images/t02.png" /></span>修改</li>
                <li class="click3  auth"><span>
                    <img src="../images/t03.png" /></span>删除</li>
                <asp:Button ID="Button1" runat="server" OnClick="btnUpdate_Click" CssClass="btnhide" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
            </ul>
        </div>
        <div class="listtitle" style="margin-top: 10px">
            物料列表</div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="物料编码" AllowPaging="True">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="gridcode" onclick="GridClick(this.innerText)" Text="物料详情"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="gridheader" />
                        <RowStyle CssClass="gridrow" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID="btnModify" />
                    <asp:AsyncPostBackTrigger ControlID="btnDel" />
                    <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="tip" id="addtip">
        <div class="tiptop">
            <span>提示信息</span><a></a></div>
        <div class="tipinfo">
            <span>
                <img src="../images/ticon.png" /></span>
            <div class="tipright">
                <p>
                    是否确认添加此条记录 ？</p>
                <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
            </div>
        </div>
        <div class="tipbtn">
            <asp:Button ID="btnAdd" class="sure" runat="server" Text="确定" OnClick="btnAdd_Click" />&nbsp;
            <input name="" type="button" class="cancel" value="取消" />
        </div>
    </div>
    <div class="tip" id="mdftip">
        <div class="tiptop">
            <span>提示信息</span><a></a></div>
        <div class="tipinfo">
            <span>
                <img src="../images/ticon.png" /></span>
            <div class="tipright">
                <p>
                    确认修改此条记录 ？</p>
                <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
            </div>
        </div>
        <div class="tipbtn">
            <asp:Button ID="btnModify" class="sure" runat="server" Text="确定" OnClick="btnModify_Click" />&nbsp;
            <input name="" type="button" class="cancel" value="取消" />
        </div>
    </div>
    <div class="tip" id="deltip">
        <div class="tiptop">
            <span>提示信息</span><a></a></div>
        <div class="tipinfo">
            <span>
                <img src="../images/ticon.png" /></span>
            <div class="tipright">
                <p>
                    确认删除此条记录 ？</p>
                <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
            </div>
        </div>
        <div class="tipbtn">
            <asp:Button ID="btnDel" class="sure" runat="server" Text="确定" OnClick="btnDel_Click" />&nbsp;
            <input name="" type="button" class="cancel" value="取消" />
        </div>
    </div>
    <script type="text/javascript">
        $('.tablelist tbody tr:odd').addClass('odd');
    </script>
    </form>
</body>
</html>
