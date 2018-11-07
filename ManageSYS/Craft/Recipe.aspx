<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recipe.aspx.cs" Inherits="Craft_Recipe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>配方管理</title>
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
                }
            });
        });
        function tab1Click(code) {
            $('#tabtop1').click();
            debugger;
            $("#Frame1").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#Frame1").contents().find("'*[id$=btnUpdate]'").click();
            $("#Frame2").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#Frame2").contents().find("'*[id$=btnUpdate]'").click();
            $("#Frame3").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#Frame3").contents().find("'*[id$=btnUpdate]'").click();
            $("#Frame4").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#Frame4").contents().find("'*[id$=btnUpdate]'").click();
        }
        function tab2Click(code) {
            $('#tabtop2').click();
            $("#Frame2").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#Frame2").contents().find("'*[id$=btnUpdate]'").click();
        }
        function tab3Click(code) {
            $('#tabtop3').click();
            $("#Frame3").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#Frame3").contents().find("'*[id$=btnUpdate]'").click();
        }
        function tab4Click(code) {
            $('#tabtop4').click();
            $("#Frame4").contents().find("'*[id$=hdcode]'").attr('value', code);
            $("#Frame4").contents().find("'*[id$=btnUpdate]'").click();
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
                <li><a href="#">配方管理</a></li>
            </ul>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="mainbox">
            <div class="mainleft">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnUpdate" CssClass="btnhide" runat="server" OnClick="btnUpdate_Click" />
                        <div class="leftinfo">
                            <div class="listtitle">配方管理</div>
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
                            <li><a href="#tab1" id="tabtop1">产品配方表</a></li>
                            <li><a href="#tab2" id="tabtop2">原料配方</a></li>
                            <li><a href="#tab3" id="tabtop3">辅料配方</a></li>
                            <li><a href="#tab4" id="tabtop4">回填液配方</a></li>

                        </ul>
                    </div>
                </div>
                <div id="tab1" class="tabson">
                    <iframe name="Frame1" id="Frame1" src="RecipeList.aspx" height="400" scrolling="no"
                        style="width: 100%; position: relative;"></iframe>
                </div>
                <div id="tab2" class="tabson">
                    <iframe name="Frame2" id="Frame2" src="MtrRecipe.aspx" height="400" scrolling="no" style="width: 100%; position: relative"></iframe>
                </div>
                <div id="tab3" class="tabson">
                    <iframe name="Frame3" id="Frame3" src="RecipeAux.aspx" height="400" scrolling="no" style="width: 100%; position: relative;"></iframe>
                </div>
                <div id="tab4" class="tabson">
                    <iframe name="Frame4" id="Frame4" src="RecipeCoat.aspx" height="400" scrolling="no" style="width: 100%; position: relative;"></iframe>
                </div>
            </div>
        </div>
        <!--mainright end-->
        <script type="text/javascript">
            $("#usual1 ul").idTabs();
        </script>

    </form>
</body>
</html>
