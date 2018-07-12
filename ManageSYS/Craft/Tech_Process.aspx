<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tech_Process.aspx.cs" Inherits="Craft_Tech_Process" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="framelist">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="tablelist">
                    <tbody>
                        <tr>
                            <td width="100">
                                所属工艺段
                            </td>
                            <td>
                                <asp:DropDownList ID="list2Section" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="list2Section_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td width="100">
                                编码
                            </td>
                            <td>
                                <asp:TextBox ID="txt2pcode" runat="server" class="dfinput1" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                工序编码
                            </td>
                            <td>
                                <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td width="100">
                                名称
                            </td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td width="100">
                                备注
                            </td>
                            <td>
                                <asp:TextBox ID="txtDscrp" runat="server" class="dfinput1"></asp:TextBox>
                            </td>
                            <td width="100">
                                是否有效
                            </td>
                            <td>
                                <asp:CheckBox ID="rdValid" runat="server" Text=" " />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                <asp:AsyncPostBackTrigger ControlID="btnDel" />
                <asp:AsyncPostBackTrigger ControlID="btnModify" />
                <asp:AsyncPostBackTrigger ControlID="list2Section" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="tools">
        <ul class="toolbar">
            <li class="click1  auth"><span>
                <img src="../images/t01.png" /></span>添加</li>
            <li class="click2  auth"><span>
                <img src="../images/t02.png" /></span>修改</li>
            <li class="click3  auth"><span>
                <img src="../images/t03.png" /></span>删除</li>
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btnhide" />
            <asp:HiddenField ID="hdcode" runat="server" onvaluechanged="hdcode_ValueChanged" />
        </ul>
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
    </form>
</body>
</html>
