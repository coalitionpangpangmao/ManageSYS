﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tech_Path.aspx.cs" Inherits="Craft_Tech_Path" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺路径管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript">
       
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
            <li><a href="#">工艺路径管理</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">工艺路径配置</a></li>
                     <li><a href="#tab2" id="tabtop2">工艺段路径管理</a></li>
                    <li><a href="#tab3" id="tabtop3">路径节点配置</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    工艺路径配置&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    计划号：<asp:DropDownList ID="listProdplan" runat="server" CssClass ="drpdwnlist" 
                        AutoPostBack="True" onselectedindexchanged="listProdplan_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span style="position: relative; float: right" class="click2">                         
                    <asp:Button ID="btnSavePath" runat="server"  CssClass="btnset" Text="保存设置" 
                        OnClick="btnSavePath_Click" Width="120px" />                     
                    </span>
                </div>               
                
                <div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView4" runat="server" class="grid"   DataKeyNames="section_code" 
                            AutoGenerateColumns="False">
                        <Columns>
                              <asp:TemplateField HeaderText="工艺段" >
                                    <ItemTemplate>
                                    <asp:TextBox ID="txtSection" runat="server" DataValueField="工艺段" DataTextField="工艺段" CssClass="tbinput" Enabled="False" Width = "150px"></asp:TextBox>    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="路径选择" >
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listpath" runat="server" CssClass = "drpdwnlist" Width = "200px" OnSelectedIndexChanged = "listpath_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:TemplateField HeaderText="路径详情" >
                                    <ItemTemplate>
                                          
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="操作" >
                                    <ItemTemplate>
                                        <asp:Button ID="btnGrid4Save" runat="server" Text="保存"  CssClass = "btn1" OnClick = "btnGrid4Save_Click"/>   
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID = "listProdplan" />
                      <asp:AsyncPostBackTrigger ControlID = "GridView4" />
                      <asp:AsyncPostBackTrigger ControlID = "btnSavePath" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>            
        </div>
        <div id="tab2" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    工艺段路径管理<asp:DropDownList ID="listSection1" runat="server" 
                        CssClass ="drpdwnlist" 
                        onselectedindexchanged="listSection1_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <span style="position: relative; float: right" class="click2">                         <asp:Button ID="btnGrid1Add" runat="server"  CssClass="btnadd" Text="新增" OnClick="btnGrid1Add_Click" />
                        <asp:Button ID="btnGrid1CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid1CkAll_Click" />
                        <asp:Button ID="btnGrid1DelSel" runat="server" CssClass="btndel" Text="删除" OnClick="btnGrid1DelSel_Click" />
                     
                    </span>
                </div>               
                
                <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hideQuery" runat="server" />
                        <asp:GridView ID="GridView1" runat="server" class="grid"  AutoGenerateColumns="False"  DataKeyNames = "SECTION_CODE,PATHCODE" >                            
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID = "listSection1" />
                      <asp:AsyncPostBackTrigger ControlID = "btnGrid1Add" />
                      <asp:AsyncPostBackTrigger ControlID = "btnGrid1CkAll" />
                      <asp:AsyncPostBackTrigger ControlID = "btnGrid1DelSel" />
                      <asp:AsyncPostBackTrigger ControlID = "GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>            
        </div>
        <div id="tab3" class="tabson">
            <div class="framelist">
                <div class="listtitle">
                    路径节点配置<asp:DropDownList ID="listSection2" runat="server" 
                        CssClass ="drpdwnlist" 
                        onselectedindexchanged="listSection2_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <span style="position: relative; float: right" >                      
                                           <asp:Button ID="btnGrid2Add" runat="server"  CssClass="btnadd" Text="新增" OnClick="btnGrid2Add_Click" />
                        <asp:Button ID="btnGrid2CkAll" runat="server" CssClass="btnset" Text="全选" OnClick="btnGrid2CkAll_Click" />
                        <asp:Button ID="btnGrid2DelSel" runat="server" CssClass="btndel" Text="删除" OnClick="btnGrid2DelSel_Click" />
                     
                    </span>
                </div>               
                
                <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="True" AutoGenerateColumns="False"
                            DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="工艺段" >
                                    <ItemTemplate>
                                    <asp:TextBox ID="txtSection" runat="server" DataValueField="工艺段" DataTextField="工艺段" CssClass="tbinput" Enabled="False" Width = "120px"></asp:TextBox>    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="节点名" >
                                    <ItemTemplate>
                                       <asp:TextBox ID="txtNodeName" runat="server" DataValueField="节点名" DataTextField="节点名" CssClass="tbinput" ></asp:TextBox>       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="顺序号" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtOrder" runat="server" DataValueField="顺序号" DataTextField="顺序号"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                 <asp:TemplateField HeaderText="控制标签" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTag" runat="server" DataValueField="控制标签" DataTextField="控制标签"  CssClass="tbinput"  Width = "150px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="创建时间" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCreatetime" runat="server" DataValueField="创建时间" DataTextField="创建时间"  CssClass="tbinput" Enabled="False" Width = "100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="描述" >
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDscrpt" runat="server" DataValueField="描述" DataTextField="描述"
                                            CssClass="tbinput"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作" ItemStyle-Width="80">
                                    <ItemTemplate>
                                        <asp:Button ID="btnGrid2Save" runat="server" Text="保存" CssClass="btn1" Width="75"
                                            OnClick="btnGrid2Save_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                       <asp:AsyncPostBackTrigger ControlID = "listSection2" />
                       <asp:AsyncPostBackTrigger ControlID = "btnGrid2Add" />
                       <asp:AsyncPostBackTrigger ControlID = "btnGrid2CkAll" />
                       <asp:AsyncPostBackTrigger ControlID = "btnGrid2DelSel" />
                       <asp:AsyncPostBackTrigger ControlID = "GridView2" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </div>            
        </div>

        
        <div class="aprvinfo" id="flowinfo">
            <div class="tiptop">
                <span>审批流程详情</span><a onclick="Aprvlisthide()"></a></div>
            <div class="flowinfo">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView3" runat="server" class="grid">
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
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
