<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Storage.aspx.cs" Inherits="Product_StorageOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>库存查询</title>
<link href="../css/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../js/jquery.js"></script>
<script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
   <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>   
  
</head>
<body>
    <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">库存管理</a></li>
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
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />  </span>

                    </div>
                    <table class="tablelist">
                        <tbody>
                            <tr>
                                <td width="100">
                                    年份
                                </td>
                                <td>
                                    <asp:TextBox ID="txtYear" runat="server" class="dfinput1" onclick="WdatePicker({dateFmt:'yyyy'})"></asp:TextBox>
                                </td>
                                 <td width="100">
                                    物料名称
                                </td>
                                <td>
                                    <asp:TextBox ID="txtname" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                  <td width="100">
                                    类别
                                </td>
                                <td>
                                    <asp:DropDownList ID="txtcatgory" runat="server" class="dfinput1">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="片烟">片烟</asp:ListItem>
                                        <asp:ListItem Value="长梗">长梗</asp:ListItem>
                                        <asp:ListItem Value="短梗">短梗</asp:ListItem>
                                        <asp:ListItem Value="碎片">碎片</asp:ListItem>
                                        <asp:ListItem Value="烟末">烟末</asp:ListItem>
                                        <asp:ListItem Value="烟棒">烟棒</asp:ListItem>
                                        <asp:ListItem Value="大末">大末</asp:ListItem>
                                        <asp:ListItem Value="小末">小末</asp:ListItem>
                                        <asp:ListItem Value="木浆板">木浆板</asp:ListItem>
                                        <asp:ListItem Value="碳酸钙">碳酸钙</asp:ListItem>
                                        <asp:ListItem Value="瓜尔胶">瓜尔胶</asp:ListItem>
                                        <asp:ListItem Value="纸箱">纸箱</asp:ListItem>
                                        <asp:ListItem Value="塑料袋">塑料袋</asp:ListItem>
                                        <asp:ListItem Value="打包带">打包带</asp:ListItem>
                                        <asp:ListItem Value="酒精">酒精</asp:ListItem>
                                        <asp:ListItem Value="丙二醇">丙二醇</asp:ListItem>
                                        <asp:ListItem Value="蜂蜜">蜂蜜</asp:ListItem>
                                        <asp:ListItem Value="香精香料">香精香料</asp:ListItem>
                                        <asp:ListItem Value="回填液">回填液</asp:ListItem>
                                        <asp:ListItem Value="胶带">胶带</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    类型
                                </td>
                                <td>
                                    <asp:DropdownList ID="txttype" runat="server" class="dfinput1">
                                                <asp:ListItem Value=""></asp:ListItem>
                                               <asp:ListItem Value="原材料">原材料</asp:ListItem>
                                                <asp:ListItem Value="辅助材料">辅助材料</asp:ListItem>
                                                <asp:ListItem Value="库存商品">库存商品</asp:ListItem>                                                
                                    </asp:DropdownList>
                                </td>
                                 <td width="100">
                                    产地
                                </td>
                                <td>
                                    <asp:TextBox ID="txtprovince" runat="server" class="dfinput1"></asp:TextBox>
                                </td>
                                  <td width="100">
                                    仓库
                                </td>
                                <td>
                                    <asp:DropDownList ID="txtwarehouse" runat="server" class="dfinput1">
                                        <asp:ListItem Value=""></asp:ListItem>
                                        <asp:ListItem Value="烟厂原料库">烟厂原料库</asp:ListItem>
                                        <asp:ListItem Value="鑫源原材料">鑫源原材料</asp:ListItem>
                                        <asp:ListItem Value="烟厂免费原料库">烟厂免费原料库</asp:ListItem>
                                        <asp:ListItem Value="鑫源免费原料库">鑫源免费原料库</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
               
                <div class="listtitle" style="margin-top: 10px">
                    原料库存</div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                
                 <asp:GridView ID="GridView1" runat="server" class="grid3" DataKeyNames="ID" 
                        AllowPaging="false" AutoGenerateColumns="True"  >
                     <Columns>
                       <asp:TemplateField      >
                        <ItemTemplate>
                            <asp:CheckBox ID="chk" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                          <asp:BoundField    DataField="物料编码" HeaderText="物料编码" />
                          <asp:BoundField    DataField="物料名称" HeaderText="物料名称" />
                          <asp:BoundField    DataField="仓库" HeaderText="仓库" />
                          <asp:BoundField    DataField="货位号" HeaderText="货位号" />
                          <asp:BoundField    DataField="类别" HeaderText="类别" />
                           <asp:BoundField    DataField="类型" HeaderText="类型" />
                          <asp:BoundField    DataField="产地" HeaderText="产地" />
                          <asp:BoundField    DataField="库存(kg)" HeaderText="库存(kg)" />
                             <asp:BoundField    DataField="使用叶组" HeaderText="使用叶组" />
                          <asp:BoundField    DataField="件数" HeaderText="件数" />
                          <asp:BoundField    DataField="单重(kg)" HeaderText="单重(kg)" />                                             
                  </Columns>
                     <HeaderStyle CssClass="gridheader" />
                 <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
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
                   SAP库存</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                <div style="overflow: scroll">
                 <asp:GridView ID="GridView2" runat="server" class="grid3" DataKeyNames="ID" 
                        AllowPaging="True" AutoGenerateColumns="False" >
                     <Columns>									
                                    <asp:BoundField    DataField="物料组" HeaderText="物料组" />
                          <asp:BoundField    DataField="物料名称" HeaderText="物料名称" />
                          <asp:BoundField    DataField="工厂" HeaderText="工厂" />
                          <asp:BoundField    DataField="总库存(kg)" HeaderText="总库存(kg)" />
                          <asp:BoundField    DataField="可用库存(kg)" HeaderText="可用库存(kg)" />
                           <asp:BoundField    DataField="使用叶组" HeaderText="使用叶组" />
                          <asp:BoundField    DataField="年份" HeaderText="年份" />
                          <asp:BoundField    DataField="产地" HeaderText="产地" />
                             <asp:BoundField    DataField="地区" HeaderText="地区" />
                          <asp:BoundField    DataField="等级" HeaderText="等级" />
                          <asp:BoundField    DataField="品种" HeaderText="品种" />   
                          <asp:BoundField    DataField="物料编号" HeaderText="物料编号" />
                          <asp:BoundField    DataField="单重" HeaderText="单重" />                                            
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
