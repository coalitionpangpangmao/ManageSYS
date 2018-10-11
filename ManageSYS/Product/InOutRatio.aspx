<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InOutRatio.aspx.cs" Inherits="Product_InOutRatio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>投入产出比</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    	


</head>
<body>
    <script type="text/javascript" src="../js/code/highcharts.js"></script>
    <script type="text/javascript" src="../js/code/modules/series-label.js"></script>
    <script type="text/javascript" src="../js/code/modules/exporting.js"></script>
      <script src="../js/msys/InoutRatio.js" type="text/javascript"></script>
      <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置:</span>
        <ul class="placeul">
            <li><a href="#">生产管理</a></li>
            <li><a href="#">投入产出比</a></li>
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
                                产品：<asp:DropDownList ID="listProd" runat="server" CssClass="drpdwnlist" >
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:RadioButton ID="rdSort1" runat="server" GroupName="Sort" Text="按天"  oncheckedchanged="rdSort1_CheckedChanged" AutoPostBack="True" />
                                <asp:RadioButton ID="rdSort2" runat="server" GroupName="Sort" Text="按月"  AutoPostBack="True"  oncheckedchanged="rdSort2_CheckedChanged" />
                                <asp:RadioButton ID="rdSort3" runat="server" GroupName="Sort" Text="按季"  AutoPostBack="True"  oncheckedchanged="rdSort3_CheckedChanged" />
                                <asp:RadioButton ID="rdSort4" runat="server" GroupName="Sort" Text="按年"  AutoPostBack="True" oncheckedchanged="rdSort4_CheckedChanged" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"   ></asp:TextBox>
                                至：<asp:TextBox ID="txtEtime" runat="server" CssClass="dfinput1" ></asp:TextBox>

                                &nbsp;
                                
                                  <input id="btnCompare" type="button" value="查询" class="btnview" onclick = "showPointPlot();"/>
                            </th>
                        </tr>
                      
                    </table>
               </ContentTemplate>
                <Triggers>
                  
                </Triggers>
            </asp:UpdatePanel>
        <div >
          
        </div>
        <div id="container" style="height: 400px; max-width: 800px; margin: 0 auto">
        </div>
                      <div id="statics" >
                        </div>   
         
        </div>
    </div>
    </form>
</body>
</html>
