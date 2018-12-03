<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnergyType.aspx.cs" Inherits="EnergyType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>录入能耗点</title>   
<link href="../css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".click").click(function () {
            var energyid = document.getElementById("ENERGYID");
            var eng_name = document.getElementById("ENG_NAME");
            var eng_code = document.getElementById("ENG_CODE");1
            var process_name = document.getElementById("PROCESS_NAME");
            var process_code = document.getElementById("PROCESS_CODE");
            var unit_name = document.getElementById("UNIT_NAME");
            var unit_code = document.getElementById("UNIT_CODE");
            if (eng_name.value == '' || eng_code.value == '' || process_name.value == '' || process_code.value == '' || unit_name.value == '' || unit_code.value == "") {
                alert("数据内容有缺失,请补充完必要数据！");
                return;
            }
            if (energyid.value=="请勿输入") {
                $("#addtip").fadeIn(200);
            } else {
                alert("不能对正在编辑的数据执行添加操作");
                return;
            }
0        });
        $(".tiptop a").click(function () {
            $(".tip").fadeOut(200);
        });
        $(".sure").click(function () {
            $(".tip").fadeOut(100);
        });
        $(".cancel").click(function () {
            $(".tip").fadeOut(100);
        });
        $(".click2").click(function () {
            var id = document.getElementById("ENERGYID")
            if (id.value) {
                $("#mdftip").fadeIn(200)
            }
            else {
                alert("请选择要修改的对象");
            }
        });
        $(".click3").click(function () {
            $("#deltip").fadeIn(200);
            $("#consumeID").attr("value", "");
            $(".click4").click();
        });
        $(".click4").click(function () {
            $("#consumeID").attr("value", null);
            $("#energyConsumptionPoint").attr("value", "-1");
            $("#energyConsumption").attr("value", "");
            $("#processName").attr("value", "-1");
            $("#department").attr("value", "-1");
            $("#StartTime").attr("value", "");
            $("#EndTime").attr("value", "");
            $("#rdValid").attr("checked", false);
        });
    });
</script>
</head>

<body>
<form id="Form1" runat = "server">
<asp:ScriptManager ID="ScriptManager1" runat="server">  
</asp:ScriptManager> 
	<div class="place">
    <span>位置：</span>
    <ul class="placeul">
    <li><a href="#">能源管理</a></li>
    <li><a href="#">能耗点维护</a></li>
    </ul>
    </div>
    <div class="mainbox"> 
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
  <ContentTemplate>
 <table class="tablelist">    	
        <tbody>
        <tr>
        <td width="100">能耗项目</td>
        <td>
             <asp:TextBox ID="ENG_NAME" runat="server" class="dfinput1"></asp:TextBox>   
        </td>
        <td width="100">能耗项目编号</td>
        <td>
             <asp:TextBox ID="ENG_CODE" runat="server" class="dfinput1"></asp:TextBox>   
        </td>
        <td  width="100">能耗类型</td> 
        <td>
             <asp:TextBox ID="UNIT_NAME" runat="server" class="dfinput1"></asp:TextBox>  
        </td>
        <td  width="100">能耗类型编号</td> 
        <td>
             <asp:TextBox ID="UNIT_CODE" runat="server" class="dfinput1"></asp:TextBox>  
        </td>
      
        </tr> 
        <tr>
        <td  width="100">能耗工艺段</td>
        <td>
            <asp:TextBox ID="PROCESS_NAME" class="dfinput1" runat="server"></asp:TextBox> 
        </td>
        <td  width="100">能耗工艺段编号</td>
        <td>
            <asp:TextBox ID="PROCESS_CODE" class="dfinput1" runat="server"></asp:TextBox> 
        </td>
        <td  width="100">能耗ID</td>
        <td>
            <asp:TextBox ID="ENERGYID" class="dfinput1" ReadOnly="true" Text="请勿输入" runat="server"></asp:TextBox> 
        </td>
            <td width="100">是否有效</td>
            <td><asp:CheckBox ID="rdValid" runat="server" Text=" " /></td>
        </tr>
        <tr>
            <td colspan="4">
                备注:<asp:TextBox TextMode="MultiLine" ID="remark" runat="server" class="dfinput1"  style="width:100%;height:200px"></asp:TextBox>
            </td>
        </tr>
        </tbody>
        </table>
        </ContentTemplate>
        </asp:UpdatePanel>
        <div class="gridtools  auth">
    
    	<ul class="toolbar">
        <li class="click"><span><img src="../images/t01.png" /></span>添加</li>
        <li class="click2"><span><img src="../images/t02.png" /></span>修改</li>
        <li class="click3"><span><img src="../images/t03.png" /></span>删除</li>
        <li><asp:Button ID="Button1" class="sure" runat="server" Text="清空" OnClick="btnClear_Click"/></li>
        <!--<li><span><img src="../images/t04.png" /></span>统计</li>-->
        </ul>
    
    </div>
         <table class="tablelist">    
                <thead>
    	<tr>
        <th colspan="6">
        查看详情
        </th>
        </tr>
        </thead> 
        <tbody>
        <tr>
        <td colspan="6">
 <asp:GridView ID="GridView1" runat="server" class="grid" 
                        AllowPaging="True" AutoGenerateColumns="False"  >
                     <Columns>
                     <asp:TemplateField      >
                        <ItemTemplate>                                                  
                            <asp:CheckBox ID="ck" runat="server" />            
                        </ItemTemplate>
                            
                        </asp:TemplateField>  
                         <asp:BoundField    DataField="记录ID" HeaderText="记录ID" />
                          <asp:BoundField    DataField="能耗项目" HeaderText="能耗项目" />
                           <asp:BoundField    DataField="能耗项目编码" HeaderText="能耗项目编码" />
                           <asp:BoundField    DataField="能耗类型" HeaderText="能耗类型" />
                          <asp:BoundField    DataField="能耗类型编码" HeaderText="能耗类型编码" />
                            <asp:BoundField    DataField="能耗工艺段" HeaderText="能耗工艺段" />
                             <asp:BoundField    DataField="能耗工艺段编码" HeaderText="能耗工艺段编码" />  
                             <asp:BoundField    DataField="是否有效" HeaderText="是否有效" />
                         <asp:TemplateField       HeaderText = "操作">
                        <ItemTemplate>                                                  
                                
                            <asp:Button ID="btnEdit" runat="server" Text="编辑" CssClass = "btn1" OnClick = "btnEdit_Click"/>               
                        </ItemTemplate>
                            
                        </asp:TemplateField >
                  </Columns>
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView>
        </td>
        </tr></tbody>
    </table>
    
   
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
    
     
    <div class="tip" id="addtip">
    	<div class="tiptop"><span>提示信息</span><a></a></div>
        
      <div class="tipinfo">
        <span><img src="../images/ticon.png" /></span>
        <div class="tipright">
        <p>是否确认添加信息 ？</p>
        <cite>如果是请点击确定按钮 ，否则请点取消。</cite>
        </div>
        </div>
        
        <div class="tipbtn">
        <!--<input name="" runat="server" type="button" OnClick="btnAdd_Click"  class="sure" value="确定" />&nbsp;-->
        <asp:Button ID="btnAdd" class="sure" runat="server" Text="确定" OnClick="btnAdd_Click" />&nbsp;
        <input name="" type="button"  class="cancel" value="取消" />
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

    </div>   
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
	</script>  
</form>
<script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>  
</body>
</html>