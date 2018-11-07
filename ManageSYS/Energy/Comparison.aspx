<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comparison.aspx.cs" Inherits="Comparison" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>能耗比对</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript" src="../js/msys/Comparison2.js"></script>
    <script type="text/javascript">
        function changeStatisticType()
        {
            var type = document.getElementById("statisticType");
            var time = document.getElementById("time");
            console.log(time.getAttribute("onclick"));
            if (type.value == 0) {
                //time.onclick = function () { WdatePicker({dateFmt:'yyyy-MM-dd'});};
            } else {
                //time.onclick = function () { WdatePicker({ dateFmt: 'yyyy-MM' }); };
            }
        }

        function inputtime() {
            var type = document.getElementById("statisticType");
            if (type.value == "0") {
                WdatePicker({ dateFmt: 'yyyy-MM-dd' });
            }
            else {
                WdatePicker({ dateFmt: 'yyyy-MM' });
            }
        }
        function changeType()
        {
            var comparison = document.getElementById("comparisontype");
            var index = comparison.selectedIndex;
            var value = comparison.options[index].value;
            var energyConsumptionPoint = document.getElementById("energyConsumptionPoint");
            var processName = document.getElementById("processName");
            var department = document.getElementById("department");
            if (value == 1)
            {
                energyConsumptionPoint.style.backgroundColor="grey";
                energyConsumptionPoint.disabled = true;
                processName.style.backgroundColor = "grey";
                processName.disabled = true;
                department.style.backgroundColor = "grey";
                department.disabled = true;
            }else{
                energyConsumptionPoint.style.backgroundColor=null;
                energyConsumptionPoint.disabled = false;
                processName.style.backgroundColor = null;
                processName.disabled = false;
                department.style.backgroundColor = null;
                department.disbaled = false;
            }
        }
        
            $(document).ready(function () {
                //DrawPic();
            });
            function DrawPic() {
                var comparison = document.getElementById("comparisontype");
                var index = comparison.selectedIndex;
                var value = comparison.options[index].value;
                var btime = document.getElementById("Btime");
                var etime = document.getElementById("Etime");
                if (btime.value == "" || etime.value == "")
                {
                    alert("请把时间补充完整");
                    return;
                }
                if (value == 1) {
                    console.log("发起ajax请求");
                    comparisonByTime();
                } else {
                    var eng_code = document.getElementById("energyConsumptionPoint");
                    var process_code = document.getElementById("processName");
                    var unit_code = document.getElementById("department").value;
                    if (eng_code.value =="" || process_code.value ==""|| unit_code.value =="")
                    {
                        alert("请把数据补充完整");
                        return;
                    }
                    comparisonByProcessName();
                }
            }


            function DrawStatisticPic() {
                var type = document.getElementById("statisticType");
                var time = document.getElementById("time");
                if (time.value == "") {
                    alert("请选择时间");
                    return;
                }
                if (type.value == "0") {
                    getStatisticData();
                } else {
                    getStatisticData();
                }
            }
         
		</script>	


</head>
<body>
    <form id="Form1" runat="server">
    <div class="place">
        <span>位置:</span>
        <ul class="placeul">
            <li><a href="#">能源管理</a></li>
            <li><a href="#">能耗比对</a></li>
        </ul>
    </div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="formbody">
                    <div id="usual1" class="usual">
            <div class="itab">
                <ul>
                    <li><a href="#tab1" class="selected" id="tabtop1">能耗比对</a></li>
                    <li><a href="#tab2" id="tabtop2">能耗统计</a></li> 
                </ul>
            </div>
        </div>
        <div id="tab1" class="tabson">
            <div class="framelist">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers = "true">
                <ContentTemplate>
                    <table class="tablelist">
                        <tr>
                            <th>    
                                比较类型：<asp:DropDownList ID="comparisontype" runat="server" CssClass="drpdwnlist"  onchange ="changeType()">
                                    <asp:ListItem value= "0">按时间比较</asp:ListItem>
                                    <asp:ListItem value= "1">按设施比较</asp:ListItem>
                                </asp:DropDownList>
                                查询时间范围：   
                                <asp:TextBox ID="Btime" runat="server" CssClass="dfinput1" 
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" 
                                    ></asp:TextBox>
                                至：<asp:TextBox ID="Etime" runat="server" CssClass="dfinput1" 
                                    onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})" 
                                    ></asp:TextBox>

                                &nbsp;
                                
                                  <input id="btnCompare" type="button" value="比对" class="btnview" onclick = "DrawPic()"/>
                            </th> 
                        </tr>
                        <tr>
                            <th>
                            能耗点:
                            
                                     <asp:DropDownList ID="energyConsumptionPoint" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="bindProcessList" AutoPostBack="true"></asp:DropDownList>
                            
                            工序名:
                            
                                     <asp:DropDownList ID="processName" runat="server" CssClass="drpdwnlist" OnSelectedIndexChanged="bindUnitList" AutoPostBack="true"></asp:DropDownList>
                            
                            单位: 
                            
                                <asp:DropDownList ID="department" runat="server" CssClass="drpdwnlist">
                                </asp:DropDownList>
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
        </div>
        <div id="tab2" class="tabson">
                       <div class="framelist">
        <div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers = "true">
                <ContentTemplate>
                    <table class="tablelist">
                        <tr>
                            <th>
                                统计类型：<asp:DropDownList ID="statisticType" runat="server" CssClass="drpdwnlist"  onchange ="changeStatisticType()">
                                    <asp:ListItem value="0">按日统计</asp:ListItem>
                                    <asp:ListItem value="1">按月统计</asp:ListItem>
                                </asp:DropDownList>
                                查询时间：
                                <asp:TextBox ID="time" runat="server" CssClass="dfinput1" 
                                    onclick="inputtime()" 
                                    ></asp:TextBox>

                                &nbsp;
                                
                                  <input id="Button1" type="button" value="统计" class="btnview" onclick="DrawStatisticPic()"/>
                            </th>
                        </tr>
                    </table>
              
        <div >
            <asp:HiddenField ID="HiddenField1" runat="server" />
             <asp:HiddenField ID="HiddenField2" runat="server" />
              <asp:HiddenField ID="HiddenField3" runat="server" />
                <asp:HiddenField ID="HiddenField4" runat="server" />
        </div>
        <div id="container2" style="height: 400px; max-width: 800px; margin: 0 auto">
        </div>
          </ContentTemplate>
                <Triggers>
                  
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
        </div>
    </div>
    </form>
    <script type="text/javascript" src="../js/code/highcharts.js"></script>
    <script type="text/javascript" src="../../modules/series-label.js"></script>
    <script type="text/javascript" src="../../modules/exporting.js"></script>
    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
                $("#usual1 ul").idTabs();
    </script>
</body>
</html>
