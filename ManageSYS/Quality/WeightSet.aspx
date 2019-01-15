<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WeightSet.aspx.cs" Inherits="Quality_WeightSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>质量管理</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
  
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">质量评估与分析</a></li>
            <li><a href="#">管理与配置</a></li>
            <li><a href="#">考核权重设置</a></li>
        </ul>
    </div>
    <div class="formbody">
        <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">月度考核权重</a></li>
                    <li><a href="#tab2" id="tabtop2">工艺段权重</a></li>
                    <li><a href="#tab3" id="tabtop3">工艺点权重</a></li>
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
           <div class="framelist"> 
                <div class="listtitle">
                                <span style="position: relative; float: right">
                                    <asp:Button ID="btnGrid2Save" runat="server" CssClass="btnmodify auth" Text="保存" OnClick="btnGrid2Save_Click" />
                               </span>
                            </div>
               <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" class="grid" AllowPaging="False" AutoGenerateColumns="False"
                            DataKeyNames="ID">
                            <Columns>
                                
                                 <asp:BoundField    DataField="NAME" HeaderText="考核项" />
                                
                                <asp:TemplateField      HeaderText = "权重">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWeight" runat="server" CssClass = 'tbinput1'></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField      HeaderText = "备注">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass = 'tbinput1'></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                              
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>                      
                        <asp:AsyncPostBackTrigger ControlID="btnGrid2Save" />                     
                    </Triggers>
                </asp:UpdatePanel>
             </div>
        </div>
        
    </div>
        <div id="tab2" class="tabson">
            <div class="framelist"> 
                 <div class="listtitle">
                               <span style="position: relative; float: right">
                                    <asp:Button ID="btnGrid1Save" runat="server" CssClass="btnmodify auth" Text="保存" OnClick="btnGrid1Save_Click" />
                                  
                               </span>
                            </div>
                <div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="False"
                            DataKeyNames="section_code">
                            <Columns>
                                <asp:TemplateField      HeaderText = "工艺段">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="listSection" runat="server" CssClass = 'drpdwnlist'  DataSource = "<%#bindSection() %>"  DataTextField = "SECTION_NAME"  DataValueField = "SECTION_CODE" Width ="220px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText = "权重">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWeight" runat="server" CssClass = 'tbinput1'></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField      HeaderText = "备注">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass = 'tbinput1'></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>                      
                        <asp:AsyncPostBackTrigger ControlID="btnGrid1Save" />                     
                    </Triggers>
                </asp:UpdatePanel>
              </div>
        </div>
        </div>
        <div id="tab3" class="tabson">
            <div class="framelist"> 
                 <div class="listtitle">
                     工艺段：<asp:DropDownList ID ="listSection" runat="server" CssClass="drpdwnlist" AutoPostBack ="true" OnSelectedIndexChanged="listSection_SelectedIndexChanged" ></asp:DropDownList>
                               <span style="position: relative; float: right">
                                    <asp:Button ID="btnGrid3Save" runat="server" CssClass="btnmodify auth" Text="保存" OnClick="btnGrid3Save_Click" />
                                  
                               </span>
                            </div>
                <div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridView3" runat="server" class="grid" AutoGenerateColumns="False"
                            DataKeyNames="para_code">
                            <Columns>
                                <asp:TemplateField      HeaderText = "工艺点">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPara" runat="server" CssClass = 'tbinput1' Width ="250px"></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField      HeaderText = "权重">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWeight" runat="server" CssClass = 'tbinput1'></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField      HeaderText = "备注">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemark" runat="server" CssClass = 'tbinput1'></asp:TextBox>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                             
                            </Columns>
                            <HeaderStyle CssClass="gridheader" />
                            <RowStyle CssClass="gridrow" />
                            <AlternatingRowStyle CssClass="gridalterrow" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>                      
                        <asp:AsyncPostBackTrigger ControlID="listSection" />       
                        <asp:AsyncPostBackTrigger ControlID ="btnGrid3Save" />              
                    </Triggers>
                </asp:UpdatePanel>
              </div>
        </div>
        </div>
    <script type="text/javascript">
        $("#usual1 ul").idTabs(); 
    </script>
   
    </form>
</body>
</html>
