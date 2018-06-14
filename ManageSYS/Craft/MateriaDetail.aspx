<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MateriaDetail.aspx.cs" Inherits="Craft_MateriaDetail" %>

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
 <div   class="framelist">
 
     <asp:UpdatePanel ID="updtpanel1" runat="server">
            <ContentTemplate>
    <table class="tablelist">  
        <tbody>
        <tr>
        <td width="100">物料编码</td>
        <td><asp:TextBox ID="txtCode" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">物料名称</td><!-- code name-->
         <td><asp:TextBox ID="txtName" runat="server" class="dfinput1"    ></asp:TextBox></td>     
        <td width="100">物料类别</td>
        <td><asp:DropDownList ID="listType" runat="server" CssClass = "drpdwnlist"></asp:DropDownList></td>
        </tr> 
        <tr>
        <td  width="100">物料类型</td>
        <td><asp:TextBox ID="txtCtgr" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">单位</td>
        <td><asp:TextBox ID="txtUint" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">用友编码</td>
        <td><asp:TextBox ID="txtPkmtr" runat="server" class="dfinput1"    ></asp:TextBox></td>
          </tr>
        <tr>
        <td  width="100">等级</td>
        <td><asp:TextBox ID="txtLevel" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td width="100">种类</td>
        <td><asp:TextBox ID="txtVrt" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">单重</td>
        <td><asp:TextBox ID="txtWeight" runat="server" class="dfinput1"    ></asp:TextBox></td>
         </tr>
         <tr>
        <td  width="100">是否有效</td>
        <td>
            <asp:CheckBox ID="ckValid" runat="server" /></td>
       <td width="100" >备注</td>
        <td colspan="3"><asp:TextBox ID="txtDscrp" runat="server" class="dfinput1"    ></asp:TextBox></td>
      
        </tr> 
        </tbody>
        </table>
           </ContentTemplate>
              <Triggers>
        <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
        <asp:AsyncPostBackTrigger ControlID = "btnUpdate" />
        <asp:AsyncPostBackTrigger ControlID = "btnDel" />
        <asp:AsyncPostBackTrigger ControlID = "btnModify" />
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
            <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" CssClass="btnhide" 
                        />
            <asp:HiddenField ID="hdcode" runat="server" /> 
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
