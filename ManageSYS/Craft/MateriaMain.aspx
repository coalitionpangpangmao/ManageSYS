<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MateriaMain.aspx.cs" Inherits="Craft_MateriaMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>物料管理</title>
    <link href="/ManageSYS/css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/ManageSYS/js/jquery.js"></script>
    <link rel="stylesheet" href="/ManageSYS/js/jquery-treeview/jquery.treeview.css" />
    <link rel="stylesheet" href="/ManageSYS/js/jquery-treeview/screen.css" />
    <script type="text/javascript" src="/ManageSYS/js/jquery-treeview/jquery.cookie.js"></script>
    <script src="/ManageSYS/js/jquery-treeview/jquery.treeview.js" type="text/javascript"></script>


    <script type="text/javascript">
       
        $(document).ready(function () {
            initTree();
        });    
       
        function initTree() {            
            $.ajax({
                type: "POST",
                url: "../Response/TreeviewDataHandler.ashx",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    "table": 'ht_pub_mattree',
                    "idSeg": 'mattree_code',
                    "textSeg": 'mattree_name',
                    "rootSeg": 'PARENT_CODE',
                    "roottext": "",
                    "childtable": 'ht_pub_mattree',
                    "childrootseg": 'PARENT_CODE',
                    "childidseg": 'mattree_code',
                    "childtextseg": 'mattree_name',
                    "type": 'source',
                }),
                dataType: "text",
                success: function (result) {
                    $('#treeContainer').empty();
                    $('#treeContainer').html(result);//这儿可以替换异步取数据函数
                    toggleTree();
                },
                error: function (message) {
                    $("#request-process-patent").html("从服务器获取数据失败！");
                }
            });
        }
        function toggleTree()
        {
            $("#browser").treeview({
                toggle: function () {
                    console.log("%s was toggled.", $(this).find(">span").text());
                },
                persist: "cookie",
                collapsed: true
            });
            $("li").bind("click", function (e) {
                var target = event.target;

                if ($(target).hasClass("collapsable-hitarea"))
                {
                    $(target).removeClass("expandable-hitarea");
                    var haschild = $(target).next().attr('hasChild');
                    var hasDone = $(target).next().attr('hasDone');
              //  if (hasDone == "True") return;
                if (haschild == 'True') {
                    var id =  $(target).next().attr('value');
                    var ele =  $(target).next().next();
                    $.ajax({
                        type: "POST",
                        url: "../Response/TreeviewDataHandler.ashx",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({
                            "table": 'ht_pub_mattree',
                            "idSeg": 'mattree_code',
                            "textSeg": 'mattree_name',
                            "rootSeg": 'PARENT_CODE',
                            "roottext": id,
                            "childtable": 'ht_pub_mattree',
                            "childrootseg": 'PARENT_CODE',
                            "childidseg": 'mattree_code',
                            "childtextseg": 'mattree_name',
                            "type": 'root'
                        }),
                        dataType: "text",
                        success: function (result) {
                            ele.replaceWith(result);//这儿可以替换异步取数据函数
                            $("#browser").treeview();
                            $(".folder").bind("click", function () {

                                $('#hdcode').val($(this).attr('value'));
                                $('#btnUpdate1').click();
                                $('.folder').removeClass("selectedbold");
                                $('.file').removeClass("selectedbold");
                                $(this).addClass("selectedbold");
                            });

                            $(".file").click(function () {
                                $('.folder').removeClass("selectedbold");
                                $('.file').removeClass("selectedbold");
                                $(this).addClass("selectedbold");
                            });
                           
                        },
                        error: function (message) {
                            $("#request-process-patent").html("从服务器获取数据失败！");
                        }
                    });
                }
                }
               
            });
            $(".folder").bind("click", function () {
               
                $('#hdcode').val($(this).attr('value'));
                $('#btnUpdate1').click();
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });

            $(".file").click(function () {
                $('.folder').removeClass("selectedbold");
                $('.file').removeClass("selectedbold");
                $(this).addClass("selectedbold");
            });
            $("#gridPanel").scrollTop($("#hideY").val());
        }
        function saveScroll() {
            var y = $("#gridPanel").scrollTop();
            $("#hideY").val(y);
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">工艺管理</a></li>
                <li><a href="#">物料管理</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <div class="mainleft">
                <asp:HiddenField ID="hdcode" runat="server" />
                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hideY" runat="server" />
                        <div class="leftinfo" id="gridPanel"  onscroll="saveScroll()">
                            <div class="listtitle">物料分类</div>
                          <div id ="treeContainer"></div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID ="btnModify1" />
                        <asp:AsyncPostBackTrigger ControlID ="btnDel1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!--mainleft end-->
            <div class="mainright">
                <div class="listtitle">
                    类型查询与维护<span style="position: relative; float: right">
                        <asp:Button ID="btnAdd1" CssClass="btnadd auth" runat="server" OnClick="btnAdd1_Click"
                            Text="新增"  />
                        &nbsp; &nbsp;
                    <asp:Button ID="btnModify1" CssClass="btnmodify auth" runat="server" OnClick="btnModify1_Click"
                        Text="保存"  />
                        &nbsp; &nbsp;
                    <asp:Button ID="btnDel1" CssClass="btndel  auth" runat="server" Text="删除" OnClick="btnDel1_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                        &nbsp; &nbsp;    
                          <asp:Button ID ="btnUpdate" CssClass ="btnpatch auth" runat ="server" Text ="同步数据" OnClick  ="btnUpdate_Click"  Width ="100px"/>     
                <asp:Button ID="btnSearch1" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch1_Click" />
                        <asp:Button ID="btnUpdate1" runat="server" OnClick="btnUpdate1_Click" CssClass="btnhide" />
                       
                    </span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tablelist">
                                <tbody>
                                    <tr>
                                        <td width="100">分类名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtName1" runat="server" class="dfinput1"></asp:TextBox>
                                        </td>
                                        <td width="100">分类编码
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCode1" runat="server" class="dfinput1" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100">父级分类
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="listPrt1" runat="server" CssClass="drpdwnlist">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="100">是否有效
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="ckValid1" runat="server" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnUpdate1" />
                            <asp:AsyncPostBackTrigger ControlID="btnAdd1" />
                            <asp:AsyncPostBackTrigger ControlID ="btnUpdate" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="listtitle" style="margin-top: 10px">
                    物料列表<span style="position: relative; float: right">
                        <asp:Button ID="btnAdd2" CssClass="btnadd auth" runat="server" OnClick="btnAdd2_Click"
                            Text="新增"  /></span>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" class="grid" DataKeyNames="物料编码" AllowPaging="True"  OnPageIndexChanging="GridView1_PageIndexChanging"  PageSize ="12">
                                <Columns>
                                    <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:Button ID="btnGridDel1" runat="server" Text="删除" CssClass="btn1" Width="90px" OnClick="btnGridDel1_Click" OnClientClick="javascript:return confirm('确认删除？');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField     >
                                        <ItemTemplate>
                                            <asp:Button ID="btnDetail1" runat="server" Text="物料详情" CssClass="btn1" Width="90px" OnClick="btnDetail1_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                                    <PagerStyle CssClass="gridpager" />
                            <PagerTemplate>
                                <asp:Label ID="lblPage" runat="server" Text='<%# "第" + (((GridView)Container.NamingContainer).PageIndex + 1)  + "页/共" + (((GridView)Container.NamingContainer).PageCount) + "页" %> ' Width="120px"></asp:Label>
                                <asp:LinkButton ID="lbnFirst" runat="Server" Text="首页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="First"></asp:LinkButton>
                                <asp:LinkButton ID="lbnPrev" runat="server" Text="上一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != 0 %>' CommandName="Page" CommandArgument="Prev"></asp:LinkButton>
                                <asp:LinkButton ID="lbnNext" runat="Server" Text="下一页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Next"></asp:LinkButton>
                                <asp:LinkButton ID="lbnLast" runat="Server" Text="尾页" Enabled='<%# ((GridView)Container.NamingContainer).PageIndex != (((GridView)Container.NamingContainer).PageCount - 1) %>' CommandName="Page" CommandArgument="Last"></asp:LinkButton>
                                到第
                                <asp:TextBox ID="txtNewPageIndex" runat="server" Width="20px" Text='<%# ((GridView)Container.Parent.Parent).PageIndex + 1 %>' />
                                页  
             <asp:LinkButton ID="btnGo" runat="server" CausesValidation="False" CommandArgument="-2"
                 CommandName="Page" Text="跳转" />

                            </PagerTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd1" />
                            <asp:AsyncPostBackTrigger ControlID="btnModify1" />
                            <asp:AsyncPostBackTrigger ControlID="btnDel1" />
                            <asp:AsyncPostBackTrigger ControlID="btnUpdate1" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearch1" />
                             <asp:AsyncPostBackTrigger ControlID="btnModify2" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>

                <div class="shade">
                    <div style="width: 800px; height: 300px; position: absolute; top: 6%; left: 12%; background: #fcfdfd; box-shadow: 1px 8px 10px 1px #9b9b9b; border-radius: 1px; behavior: url(js/pie.htc);">
                        <div class="tiphead">
                            <span>物料信息</span><a onclick="$('.shade').fadeOut(100);"></a>
                        </div>
                        <div class="gridinfo">
                            
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table class="tablelist">
                                        <tbody>
                                            <tr>
                                                <td width="100">物料编码</td>
                                                <td>
                                                    <asp:TextBox ID="txtCode2" runat="server" class="dfinput1" Enabled ="false" Width ="220px"></asp:TextBox></td>
                                                <td width="100">物料名称</td>
                                                <!-- code name-->
                                                <td>
                                                    <asp:TextBox ID="txtName2" runat="server" class="dfinput1"  Width ="220px"></asp:TextBox></td>
                                                 </tr>
                                            <tr>
                                                <td width="100">父级类别</td>
                                                <td>
                                                    <asp:DropDownList ID="listType2" runat="server" CssClass="drpdwnlist" Enabled ="false" Width ="220px"></asp:DropDownList></td>
                                           
                                                <td width="100">物料类型</td>
                                                <td>
                                                    <asp:TextBox ID="txtCtgr2" runat="server" class="dfinput1" Width ="220px"></asp:TextBox></td>   </tr>
                                            <tr>
                                                <td width="100">单位</td>
                                              
                                                <td>
                                                    <asp:TextBox ID="txtUint2" runat="server" class="dfinput1" Width ="220px"></asp:TextBox></td>
                                                <td width="100">用友编码</td>
                                                <td>
                                                    <asp:TextBox ID="txtPkmtr2" runat="server" class="dfinput1" Width ="220px"></asp:TextBox></td>                                          
                                              
                                                  </tr>
                                            <tr>
                                                <td width="100">种类</td>
                                                <td>
                                                    <asp:TextBox ID="txtVrt2" runat="server" class="dfinput1" Width ="220px"></asp:TextBox></td>
                                                <td width="100">单重</td>
                                                <td>
                                                    <asp:TextBox ID="txtWeight2" runat="server"  onkeyup="value=value.replace(/[^\d\.]/g,'')"  class="dfinput1" Width ="220px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td width="100">是否有效</td>
                                                <td>
                                                    <asp:CheckBox ID="ckValid2" runat="server" /></td>
                                                <td width="100">备注</td>
                                                <td >
                                                    <asp:TextBox ID="txtDscrp2" runat="server" class="dfinput1" Width ="220px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                  <td width="100">等级</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtLevel2" runat="server" class="dfinput1" Width ="220px"></asp:TextBox></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                         <div class="shadebtn" align="center">
                                <asp:HiddenField ID="hdScrollY" runat="server" />
                                <asp:Button ID="btnModify2" class="sure" runat="server" Text="保存" OnClick="btnModify2_Click" />
                                <input name="" type="button" class="cancel" value="关闭" onclick="$('.shade').fadeOut(100);" />
                            </div>
                                </ContentTemplate>
                                <Triggers>                                  
                                    <asp:AsyncPostBackTrigger ControlID="btnModify2" />
                                    <asp:AsyncPostBackTrigger ControlID ="btnAdd2" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--mainright end-->
    
    </form>
</body>
</html>
