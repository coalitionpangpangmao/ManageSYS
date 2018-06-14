<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prdct.aspx.cs" Inherits="Craft_Prdct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>产品管理</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
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
       function GridClick() {
           $('#tabtop2').click();     
         
       }
  
       function Aprvlist() {
           $("#flowinfo").fadeIn(200);
       };

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
            <li><a href="#">工艺管理</a></li>
            <li><a href="#">产品管理</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id = "tabtop1">产品管理</a></li>
                    <li><a href="#tab2"  id = "tabtop2">产品信息编辑</a></li>
                </ul>
            </div>
            
        </div>
        <div id="tab1" class="tabson">
                <div class = "framelist">
                    <div class="listtitle">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" 
                            onclick="btnSearch_Click" /> </span>

                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    产品名称
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCodeS" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    产品编码
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNameS" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    是否有效
                                </td>
                                <td width="100">
                                    <asp:CheckBox ID="rdValidS" runat="server" Text=" " />
                                </td>
                            </tr>                         
                               
                        </tbody>
                    </table>
               
                <div class="listtitle" style="margin-top: 10px">
                    产品列表</div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                
                 <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="产品编码" 
                        AllowPaging="True"  >
                     <Columns>
                     <asp:TemplateField >
                        <ItemTemplate>                                                  
                                  <asp:Button ID="btnSubmit" runat="server" Text="提交审批" CssClass = "btn1 auth" Width = "100px"  OnClick = "btnSubmit_Click"/>      
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="110">
                        <ItemTemplate> 
                            <asp:Button ID="btnGridDetail" runat="server" Text="产品详情" CssClass = "btn1" Width = "100px"  OnClick = "btnGridDetail_Click"/>                
                        </ItemTemplate>
                        </asp:TemplateField >
                          
                         <asp:TemplateField  ItemStyle-Width="110">
                          <ItemTemplate>                                                  
                                 <asp:Button ID="btnFLow" runat="server" Text="审批进度" CssClass = "btn1" Width = "100px"  OnClick = "btnFLow_Click"/>  
                        </ItemTemplate>
                        </asp:TemplateField>
                  </Columns>
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView>
                  </ContentTemplate>
                <Triggers>
                 <asp:AsyncPostBackTrigger ControlID = "btnAdd" />
                  <asp:AsyncPostBackTrigger ControlID = "btnModify" />
                  <asp:AsyncPostBackTrigger ControlID = "btnDel" />
                  <asp:AsyncPostBackTrigger ControlID = "btnSearch" />
                    <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                </Triggers>
            </asp:UpdatePanel>
            </div>
             </div> 
        <div id="tab2" class="tabson">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                 <div class = "framelist">                   
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    产品名称
                                </td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    产品编码
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    包装规格
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPack" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                <td width="100">
                                    加工方式
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHand" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    技术标准编码
                                </td>
                                <td>
                                    <asp:DropDownList ID="listTechStd" runat="server"  CssClass = "drpdwnlist">
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    标准值
                                </td>
                                <td>
                                    <asp:TextBox ID="txtValue" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    原料配方编码
                                </td>
                                <td>
                                    <asp:DropDownList ID="listMtrl" runat="server" class="drpdwnlist">
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    辅料配方编码
                                </td>
                                <td>
                                    <asp:DropDownList ID="listAux" runat="server" class="drpdwnlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    回填液配方编码
                                </td>
                                <td>
                                    <asp:DropDownList ID="listcoat" runat="server" class="drpdwnlist">
                                    </asp:DropDownList>
                                </td>
                                <td width="100">
                                    质量考核标准
                                </td>
                                <td>
                                    <asp:DropDownList ID="listqlt" runat="server" class="drpdwnlist">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                              
                                <td width="100">
                                    描述
                                </td>
                                <td colspan = "3">
                                    <asp:TextBox ID="txtDscpt" runat="server" class="dfinput1" Width = "450px"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    </div>
                </ContentTemplate>
                  <Triggers>                 
                  <asp:AsyncPostBackTrigger ControlID = "GridView1"/>
                  </Triggers>
            </asp:UpdatePanel> 
              <div class="tools">  
    	<ul class="toolbar">
       <li class="click1 auth"><span>
                    <img src="../images/t01.png" /></span>添加</li>
                <li class="click2  auth"><span>
                    <img src="../images/t02.png" /></span>修改</li>
                <li class="click3  auth"><span>
                    <img src="../images/t03.png" /></span>删除</li>
        </ul>
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
         <div class="aprvinfo" id="flowinfo">
            <div class="tiptop">
                <span>审批流程详情</span><a onclick = "Aprvlisthide()"></a></div>
            <div class="flowinfo">  
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>        
               <asp:GridView ID="GridView3" runat="server" class="grid" >
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView> 
            </ContentTemplate>
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID = "GridView1" />
            </Triggers>
            </asp:UpdatePanel>
            </div>           
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
