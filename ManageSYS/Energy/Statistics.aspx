<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Statistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>能源统计数据</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>  
    <link rel="stylesheet" href="../js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="../js/jquery-treeview/screen.css" />

    <script type="text/javascript" src="../js/jquery-treeview/jquery.cookie.js"></script>
    <script src="../js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                }
            });
            $(".folder").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });
            $(".file").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });
        });
        function tab1Click(code) {
            $('#tabtop1').click();
            $("#Frame1").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#Frame1").contents().find("'*[id$=btnUpdate]'").click();
        }
        function tab2Click(code) {
            debugger;
            $('#tabtop2').click();
            $("#Frame2").contents().find("'*[id$=hdcode]'").attr('value', code.substr(4));
            $("#Frame2").contents().find("'*[id$=btnUpdate]'").click();
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置：</span>
        <ul class="placeul">
            <li><a href="#">能源管理</a></li>
            <li><a href="#">能源统计数据</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <!--mainleft end-->
        <div class=" ">        
                <div class="framelist">
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tablelist">
                                    <tbody>
                                        <tr>
                                             <td width="100">
                                                 能耗项目
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ENG_NAME" runat="server" CssClass="drpdwnlist"></asp:DropDownList>  
                                            </td>
                                             <td width="100">
                                                能耗工艺段
                                            </td>
                                            <td>
                                              <asp:DropDownList ID="PROCESS_NAME" runat="server" CssClass="drpdwnlist"></asp:DropDownList>   
        
                                            </td>
                                             <td width="100">
                                                能耗类型
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="UNIT_NAME" runat="server" CssClass="drpdwnlist"></asp:DropDownList>   
        
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">
                                                开始时间
                                            </td>
                                            <td>
                                             <asp:TextBox ID="StartTime" runat="server" class="dfinput1"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>   
        
                                            </td>
                                            <td width="100">
                                                结束时间
                                            </td>
                                            <td>
                                                 <asp:TextBox ID="EndTime" class="dfinput1" runat="server"  onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">
                                                 消耗量
                                            </td>
                                            <td>
                                               <asp:TextBox ID="COUNT" class="dfinput1" runat="server" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td width="100">
                                              <asp:Button ID="Button1" runat="server" Text="查询" OnClick="QueryData" CssClass="btnview"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
 <asp:GridView ID="GridView1" runat="server" class="grid" 
                        AllowPaging="True" AutoGenerateColumns="False"  >
                     <Columns>
                     <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  >
                        <ItemTemplate>                                                  
                            <asp:CheckBox ID="ck" runat="server" />            
                        </ItemTemplate>
                            
                        </asp:TemplateField>  
                        <asp:BoundField DataField="记录ID" HeaderText="记录ID" />
                         <asp:BoundField DataField="能耗点" HeaderText="能耗点" />
                          <asp:BoundField DataField="工序编码" HeaderText="工序编码" />
                           <asp:BoundField DataField="日期" HeaderText="日期" />
                             <asp:BoundField DataField="能耗总量" HeaderText="能耗总量" />
                              <asp:BoundField DataField="单位" HeaderText="单位" />
                  </Columns>
                     <HeaderStyle CssClass="gridheader" />
                <RowStyle CssClass="gridrow" />
            </asp:GridView>
        </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div>
                                    <div id="container" style="margin-left:10%; width:400px;height:400px; display:inline-block"></div>
                                    <div id ="container3" style=" margin-right:10%; margin-left:10%;width:400px; height:400px; display:inline-block"></div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
           
        </div>
    <!--mainright end-->
    <script type="text/javascript">
        //$("#usual1 ul").idTabs();
    </script>
    </div>
    </form>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script> 
    <script type="text/javascript" src="../js/EnergyStatistics.js"></script>
    <script type="text/javascript" src="../js/highcharts.js"></script>
    <script type="text/javascript" src="../js/exporting.js"></script>
    <script type="text/javascript" src="../js/highcharts-zh_CN.js"></script>
</body>
</html>