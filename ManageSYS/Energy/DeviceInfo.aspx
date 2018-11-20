<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeviceInfo.aspx.cs" Inherits="Energy_DeviceInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>能源设施详情</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/select.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
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
            <li><a href="#">能源设施详情</a></li>
        </ul>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="mainbox">
        <div class="mainleft">
            <div class="leftinfo">
                <div class="listtitle">
                    设施分类</div>
                <% = tvHtml %>
            </div>
        </div>
        <!--mainleft end-->
        <div class="mainright">
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">能源设施列表</a></li>
                        <li><a href="#tab2" id="tabtop2">设施详情</a></li>
                    </ul>
                </div>
            </div>
            <div id="tab1" class="tabson">
                <div class="framelist">
                    <div class="listtitle">
                        类型查询与维护<span style="position: relative; float: right">
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview"  />
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btnhide" />
                            <asp:HiddenField ID="hdcode" runat="server" />
                        </span>
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="tablelist">
                                    <tbody>
                                        <tr>
                                            <td width="100">
                                                分类名称
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                            <td width="100">
                                                分类编码
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCode" runat="server" class="dfinput1"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="100">
                                                父级分类
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="listPrt" runat="server" CssClass="drpdwnlist">
                                                </asp:DropDownList>
                                            </td>
                                            <td width="100">
                                                是否有效
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="ckValid" runat="server" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div>
                                    <asp:GridView ID="GridView1" runat="server" class="grid" AllowPaging="True">
                                        <Columns>
                                            <asp:TemplateField   HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderText="选择">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="gridheader" />
                                         <RowStyle CssClass="gridrow" /> <AlternatingRowStyle CssClass="gridalterrow" />
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div id="tab2" class="tabson">
            <div class = "framelist">
            <table class="tablelist">  
        <tbody>
        <tr>
        <td width="100">能耗项目名</td>
        <td><asp:TextBox ID="TextBox1" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">能耗类型</td><!-- code name-->
         <td><asp:TextBox ID="TextBox2" runat="server" class="dfinput1"    ></asp:TextBox></td>  
          </tr>
        <tr>   
        <td width="100">项目区域</td>
        <td><asp:DropDownList ID="listType" runat="server" CssClass = "drpdwnlist"></asp:DropDownList></td>
        
        <td  width="100">备注</td>
        <td><asp:TextBox ID="txtCtgr" runat="server" class="dfinput1"    ></asp:TextBox></td>
        </tr> 
        <tr>
        <td  width="100">单位</td>
        <td><asp:TextBox ID="txtUint" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td  width="100">排序</td>
        <td><asp:TextBox ID="txtPkmtr" runat="server" class="dfinput1"    ></asp:TextBox></td>
          </tr>
        <tr>
        <td  width="100">编制人</td>
        <td><asp:TextBox ID="txtLevel" runat="server" class="dfinput1"    ></asp:TextBox></td>
        <td width="100">编制时间</td>
        <td><asp:TextBox ID="txtVrt" runat="server" class="dfinput1"    ></asp:TextBox></td>
      
         </tr>
          
        </tbody>
        </table>
            </div>
            </div>
        </div>
    <!--mainright end-->
    <script type="text/javascript">
        $("#usual1 ul").idTabs(); 
    </script>
    </div>
    </form>
</body>
</html>
