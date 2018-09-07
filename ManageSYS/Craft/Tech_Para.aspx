<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tech_Para.aspx.cs" Inherits="Craft_Tech_Para" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    
</head>
<body>

    <form id="form1" runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="gridtools  auth">               
                    <asp:Button ID="btnAdd" CssClass="btnadd auth" runat="server" OnClick="btnAdd_Click"
                        Text="新增" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnModify" CssClass="btnview  auth" runat="server" OnClick="btnModify_Click"
                        Text="保存" />
                    &nbsp; &nbsp;
                    <asp:Button ID="btnDel" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel_Click" />       
        <asp:HiddenField ID="hdcode" runat="server" />         
          <asp:Button ID="btnUpdate" runat="server"  CssClass = "btnhide"  OnClick = "btnUpdate_Click"/>   
            </div>
  <div   class="framelist"> 
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode = "Conditional">
            <ContentTemplate>
    <table class="tablelist">    	
        <tbody>
        <tr>
        <td width="100">所属工艺段</td>
        <td>
            <asp:DropDownList ID="listSection" runat="server" CssClass = "drpdwnlist" 
                onselectedindexchanged="listSection_SelectedIndexChanged" 
                AutoPostBack="True"  >
            </asp:DropDownList>
        </td>
        <td>所属设备</td>
       <td>
           <asp:DropDownList ID="listEquip" runat="server" CssClass = "drpdwnlist">
           </asp:DropDownList>
       </td>
        </tr>
        <tr>
        <td width="100">参数点编码</td>
        <td><asp:TextBox ID="txtCode" runat="server" class="dfinput1" 
               Enabled="False"></asp:TextBox></td>
        <td  width="100">参数点名称</td>
        <td><asp:TextBox ID="txtName" runat="server" class="dfinput1"    ></asp:TextBox></td>
        </tr> 
        <tr>
        <td width="100">单位</td>
        <td><asp:TextBox ID="txtUnit" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">是否有效</td>
        <td>
            <asp:CheckBox ID="rdValid" runat="server" Text=" "  Checked="true"/>
        </td>
      
        </tr>
        <tr>
      
        <td  width="100">类型</td>
        <td colspan="3">
            <p>
                <asp:CheckBox ID="ckCenterCtrl" runat="server" Text="集控显示" CssClass="ckfloat" />
                <asp:CheckBox ID="ckRecipePara" runat="server" Text="设定参数" CssClass="ckfloat"/>
                <asp:CheckBox ID="ckSetPara" runat="server" Text="工艺参数" CssClass="ckfloat"/>
                 <asp:CheckBox ID="ckQuality" runat="server" Text="质量统计" OnCheckedChanged="ckQuality_CheckedChanged" AutoPostBack="True" CssClass="ckfloat"/>
                  <asp:CheckBox ID="ckManul" runat="server" Text="人工录入" CssClass="ckfloat" />
                 <asp:CheckBox ID="ckEqpara" runat="server" Text="设备记录" CssClass="ckfloat" Visible="False"/>
                <asp:CheckBox ID="ckQuaAnalyze" runat="server" Text="质量考核" OnCheckedChanged="ckQuaAnalyze_CheckedChanged" AutoPostBack="True" CssClass="ckfloat"/>
                    <asp:CheckBox ID="ckCalibrate" runat="server" Text="计量检查" CssClass="ckfloat"/>
               
            </p>
    
        </td>       
        </tr>
          <tr>
         <td width="100">设定标签地址</td>
        <td ><asp:TextBox ID="txtSetTag" runat="server" class="dfinput1" 
                Width="200px"    ></asp:TextBox></td>
                 <td width="100">反馈标签地址</td>
        <td ><asp:TextBox ID="txtValueTag" runat="server" class="dfinput1" 
                Width="200px"    ></asp:TextBox></td>
                </tr>    
        <tr>
         <td width="100">备注</td>
        <td colspan="3"><asp:TextBox ID="txtDscrp" runat="server" class="dfinput1" 
                Width="500px"    ></asp:TextBox></td></tr>    
        </tbody>
        </table>
           </ContentTemplate> 
             <Triggers>
        <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
        <asp:AsyncPostBackTrigger ControlID = "btnUpdate" />
        <asp:AsyncPostBackTrigger ControlID = "btnDel" />
        <asp:AsyncPostBackTrigger ControlID = "btnModify" />
         <asp:AsyncPostBackTrigger ControlID = "txtCode" />
         <asp:AsyncPostBackTrigger ControlID = "listSection" />
                 <asp:AsyncPostBackTrigger ControlID ="ckQuality" />
                 <asp:AsyncPostBackTrigger ControlID ="ckQuaAnalyze" />
        </Triggers>           
        </asp:UpdatePanel>
 </div>
   
    </form>
</body>
</html>
