<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Storage.aspx.cs" Inherits="Product_StorageOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存查询</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<link href="../css/select.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
<script type="text/javascript" src="../js/select-ui.min.js"></script>
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
           $('#tabtop2').click();
          
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
            <li><a href="#">生产管理</a></li>
            <li><a href="#">库存查询</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id = "tabtop1">原料库存</a></li>
                    <li><a href="#tab2"  id = "tabtop2">SAP原料库存</a></li>
                </ul>
            </div>
            
        </div>
        <div id="tab1" class="tabson">
                <div class = "framelist">
                    <div class="listtitle">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" />  </span>

                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    物料编码
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStart" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                 <td width="100">
                                    物料名称
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                  <td width="100">
                                    类别
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox4" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    类型
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox5" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                 <td width="100">
                                    产地
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox6" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                  <td width="100">
                                    仓库
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox7" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
               
                <div class="listtitle" style="margin-top: 10px">
                    原料库存<span style="position: relative; float: right" >
                        <asp:Button ID="btnIssued" runat="server" Text="下发" class="btn1" 
                      />  <asp:Button ID="btnGridDel" runat="server" Text="删除" class="btn1" />               
                </span></div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                
                 <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="ID" 
                        AllowPaging="True" AutoGenerateColumns="False"  >
                     <Columns>
                       <asp:TemplateField >
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                         <asp:BoundField DataField="物料编码" HeaderText="生产日期" />
                         <asp:BoundField DataField="物料名称" HeaderText="物料名称" />
                         <asp:BoundField DataField="仓库" HeaderText="仓库" />
                         <asp:BoundField DataField="货位号" HeaderText="货位号" />
                         <asp:BoundField DataField="类别" HeaderText="类别" />
                          <asp:BoundField DataField="类型" HeaderText="类型" />
                         <asp:BoundField DataField="产地" HeaderText="产地" />
                         <asp:BoundField DataField="库存(kg)" HeaderText="库存(kg)" />
                            <asp:BoundField DataField="使用叶组" HeaderText="使用叶组" />
                         <asp:BoundField DataField="件数" HeaderText="件数" />
                         <asp:BoundField DataField="单重(kg)" HeaderText="单重(kg)" />                                             
                  </Columns>
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView>
                  </ContentTemplate>
                <Triggers>
               
                </Triggers>
            </asp:UpdatePanel>
            </div>
             </div> 
        <div id="tab2" class="tabson">
                <div class = "framelist">
                    <div class="listtitle">
                        查询条件<span style="position: relative; float: right">
                            <asp:Button ID="Button1" runat="server" Text="查询" CssClass="btnview" 
                           />  </span>

                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    物料编码
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                 <td width="100">
                                    物料名称
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                  <td width="100">
                                    类别
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox8" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    类型
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox9" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                 <td width="100">
                                    产地
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox10" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                  <td width="100">
                                    仓库
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBox11" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
               
                <div class="listtitle" style="margin-top: 10px">
                   SAP库存<span style="position: relative; float: right" >
                        <asp:Button ID="Button2" runat="server" Text="下发" class="btn1"/>  <asp:Button ID="Button3" runat="server" Text="删除" class="btn1" />               
                </span></div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                <div style="overflow: scroll">
                 <asp:GridView ID="GridView2" runat="server" class="grid" DataKeyNames="ID" 
                        AllowPaging="True" AutoGenerateColumns="False"  >
                     <Columns>									
                                   <asp:BoundField DataField="物料组" HeaderText="物料组" />
                         <asp:BoundField DataField="物料名称" HeaderText="物料名称" />
                         <asp:BoundField DataField="工厂" HeaderText="工厂" />
                         <asp:BoundField DataField="总库存(kg)" HeaderText="总库存(kg)" />
                         <asp:BoundField DataField="可用库存(kg)" HeaderText="可用库存(kg)" />
                          <asp:BoundField DataField="使用叶组" HeaderText="使用叶组" />
                         <asp:BoundField DataField="年份" HeaderText="年份" />
                         <asp:BoundField DataField="产地" HeaderText="产地" />
                            <asp:BoundField DataField="地区" HeaderText="地区" />
                         <asp:BoundField DataField="等级" HeaderText="等级" />
                         <asp:BoundField DataField="品种" HeaderText="品种" />   
                         <asp:BoundField DataField="物料编号" HeaderText="物料编号" />
                         <asp:BoundField DataField="单重" HeaderText="单重" />                                            
                  </Columns>
                     <HeaderStyle CssClass="gridheader" Wrap="False" />
                <RowStyle CssClass="gridrow" Wrap="False" />
            </asp:GridView>
            </div>
                  </ContentTemplate>
                <Triggers>
               
                </Triggers>
            </asp:UpdatePanel>
            </div>
             </div>
        <script type="text/javascript">
            $("#usual1 ul").idTabs(); 
        </script>
        <script type="text/javascript">
            $('.tablelist tbody tr:odd').addClass('odd');
        </script>
    </div>
    </form>
</body>
</html>
