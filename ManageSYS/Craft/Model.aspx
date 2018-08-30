<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Model.aspx.cs" Inherits="Craft_Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>设备层次模型</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
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
                },
                persist: "cookie",
                collapsed: true

            });
           
           
        });
        
        function saveScroll() {
            var y = $("#gridPanel").scrollTop();            
            $("#hideY").val(y);
        }
        function tab1Click(code) {
            $('#tabtop1').click();
            $("#sessionFrame").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#sessionFrame").contents().find("'*[id$=btnUpdate]'").click();
        }
        function tab2Click(code) {
            $('#tabtop2').click();
            $("#ProcessFrame").contents().find("'*[id$=hdcode]'").attr('value', code);

            $("#ProcessFrame").contents().find("'*[id$=btnUpdate]'").click();
        }

        function tab3Click(code) {
            $('#tabtop3').click();
            $("#ParaFrame").contents().find("'*[id$=hdcode]'").attr('value', code);

            $("#ParaFrame").contents().find("'*[id$=btnUpdate]'").click();
        }
        function update() {
            $('#btnUpdate').click();           

        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">工艺管理</a></li>
                <li><a href="#">工艺模型</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="mainbox">
            <div class="mainleft">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hideY" runat="server" />
                        <asp:Button ID="btnUpdate" CssClass="btnhide" runat="server" OnClick="btnUpdate_Click" />
                        <div class="leftinfo" id ="gridPanel" style="overflow: scroll" onscroll="saveScroll()">
                            <div class="listtitle">
                                工艺模型
                            </div>
                            <% = tvHtml %>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnUpdate" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <!--mainleft end-->
            <div class="mainright">
                <div id="usual1" class="usual">
                    <div class="itab">
                        <ul>
                            <li><a href="#tab1" class="selected" id="tabtop1">工艺段</a></li>
                            <li><a href="#tab2" id="tabtop2">设备</a></li>
                            <li><a href="#tab3" id="tabtop3">参数点</a></li>
                        </ul>
                    </div>
                    <div id="tab1" class="tabson">
                        <iframe id="sessionFrame" name="sessionFrame" src="Tech_Session.aspx" height="400"
                            scrolling="no" style="width: 80%; position: absolute"></iframe>
                    </div>
                    <div id="tab2" class="tabson">
                        <iframe id="ProcessFrame" name="ProcessFrame" src="Tech_Equip.aspx" height="400"
                            scrolling="no" style="width: 80%; position: absolute"></iframe>
                    </div>
                    <div id="tab3" class="tabson">
                        <iframe id="ParaFrame" name="ParaFrame" src="Tech_Para.aspx" height="400" scrolling="no"
                            style="width: 80%; position: absolute"></iframe>
                    </div>
                </div>
            </div>
            <!--mainright end-->
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
