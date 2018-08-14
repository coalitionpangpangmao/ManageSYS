<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchFluct.aspx.cs" Inherits="Product_InOutRatio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>成品批间波动</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    	


</head>
<body>
    <script type="text/javascript" src="../js/code/highcharts.js"></script>
    <script type="text/javascript" src="../../modules/series-label.js"></script>
    <script type="text/javascript" src="../../modules/exporting.js"></script>
      <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置:</span>
        <ul class="placeul">
            <li><a href="#">质量管理</a></li>
            <li><a href="#">成品批间波动</a></li>
        </ul>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="framelist">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers = "true">
                <ContentTemplate>
                    <table class="tablelist">
                        <tr>
                            <th>
                                牌号：<asp:DropDownList ID="listRecipe" runat="server" CssClass="drpdwnlist" 
                                    onselectedindexchanged="listRecipe_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="rdSort1" runat="server" GroupName="Sort" Text="按月" 
                                    oncheckedchanged="rdSort1_CheckedChanged" />
                                <asp:RadioButton ID="rdSort2" runat="server" GroupName="Sort" Text="按季" 
                                    oncheckedchanged="rdSort2_CheckedChanged" />
                                <asp:RadioButton ID="rdSort3" runat="server" GroupName="Sort" Text="按年" 
                                    oncheckedchanged="rdSort3_CheckedChanged" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1" 
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" 
                                    ontextchanged="txtBtime_TextChanged"></asp:TextBox>
                                至：<asp:TextBox ID="txtEtime" runat="server" CssClass="dfinput1" 
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" 
                                    ontextchanged="txtEtime_TextChanged"></asp:TextBox>

                                &nbsp;
                                
                                  <input id="btnCompare" type="button" value="查询" class="btnview" onclick = "DrawPic()"/>
                            </th>
                        </tr>
                      
                    </table>
              
        <div >
            <asp:HiddenField ID="hdcode1" runat="server" />
             <asp:HiddenField ID="hdcode2" runat="server" />
              <asp:HiddenField ID="hdcode3" runat="server" />
                <asp:HiddenField ID="hdXaxis" runat="server" />
        </div>
        <div id="container" style="height: 400px; max-width: 800px; margin: 0 auto">
        </div>
          </ContentTemplate>
                <Triggers>
                  
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
