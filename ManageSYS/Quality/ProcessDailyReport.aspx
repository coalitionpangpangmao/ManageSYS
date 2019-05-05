﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessDailyReport.aspx.cs" Inherits="Quality_ProcessDailyReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>过程检测日报</title>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
    <link href="../css/report.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/jquery.js"></script>
    <script type="text/javascript" src="../js/jquery.idTabs.min.js"></script>
    <script type="text/javascript" src="../js/jquery.PrintArea.js"></script>
   <script type="text/javascript" src="../js/msys/export.js"></script>
        <script type="text/javascript" src="../js/vue.js"></script>
     <script type="text/javascript" src="../js/axios.js"></script>

    <script language="javascript" type="text/javascript" src="../My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tabtop2').parent().hide();
            $('#tabtop3').parent().hide();
            $('#btnPrint').hide();
        });


    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="place">
            <span>位置：</span>
            <ul class="placeul">
                <li><a href="#">质量评估与分析</a></li>
                 <li><a href="#">质量考核</a></li>          
                <li><a href="#">过程检测日报</a></li>
            </ul>
        </div>

        <div class="mainbox">
            <table class="tablelist" style="margin-bottom: 5px">
                <tbody>
                    <tr>
                        <td colspan="7" align="center"> 
                            请选择日期：
                           <input id="time" class="dfinput1" onclick="WdatePicker()"/>

                            <div style="display:inline-block" class="btnview" onclick="triggerButton()">查询</div>
                            <div id="btnPrint" style="display:inline-block" class="btnpatch" onclick="$('#Msysexport').printArea();">打印</div>
                            <input id="Button1" type="button" value="打印" class="btnpatch" onclick="$('#app').printArea();" />
                              <input id="btnExport" type="button" value="导出" class ="btnset"  onclick ="MSYS_Export('Msysexport');"/>

                        </td>

                    </tr>
                </tbody>
            </table>

            <div id="app">
                <div id="tablebutton" style="display:none" @click="search()"></div>
                <table id = "Msysexport" class = "reporttable" style="text-align:right">
        <thead>
        <th :colspan="2+AllCheckItems.length">{{time}}梗丝日报</th>
        </thead>
        <tbody>
            <tr>
                <td rowspan="2" style="text-align:right">配方</td>
                <td>检测项目</td>
                <td v-for="(item, index) in  UniqueCheckItems" :colspan="CheckTimes[index]">{{item}}</td>
            </tr>
            <tr>
                <td>技术标准</td>
                <td v-for="(item, index) in CheckTimes" :colspan="item">{{Standar[index].standar}}</td>
            </tr>
            <tr>
                <td rowspan="5" style="text-align:right">当日</td>
                <td rowspan="3">检测结果</td>
                
                
                    <td v-for="(i,index) in AllCheckItems">{{DailyValue[0][index]}}</td>
               
                 
            </tr>
            <tr>
               <td v-for="(i,index) in AllCheckItems">{{DailyValue[1][index]}}</td>
                
            </tr>
            <tr>
                 <td v-for="(i,index) in AllCheckItems">{{DailyValue[2][index]}}</td>
                 
            </tr>
            <tr>
                <td>均值</td>
                <td v-for="(item, index) in CheckTimes" :colspan="item">{{DailyAvg[index]}}</td>
             
            </tr>
            <tr>
                <td>合格率</td>
                 <td v-for="(item, index) in CheckTimes" :colspan="item">{{DailyQuare[index]}}</td>
              
            </tr>
            <tr>
                <td rowspan="3" style="text-align:right">月度</td>
                <td>均值</td>
                 <td v-for="(item, index) in CheckTimes" :colspan="item">{{MonthAvg[index]}}</td>
               
            </tr>
            <tr>
                <td>合格率</td>
                 <td v-for="(item, index) in CheckTimes" :colspan="item">{{MonthQua[index]}}</td>
               
            </tr>
            <tr>
                <td>标准差</td>
                 <td v-for="(item, index) in CheckTimes" :colspan="item">{{MonthStd[index]}}</td>
               
            </tr>
        </tbody>
    </table>

            </div>
        

        </div>

        <script type="text/javascript">
            $("#usual1 ul").idTabs();
        </script>
       
    </form>
    <script type="text/javascript" src="../js/msys/ProcessDailyReport.js"></script>
    <script type="text/javascript">
        $(function () {
            changeWH();
        });

        function triggerButton() {
            var time = document.getElementById('time').value.toString();
            app.search(time);
        }

        function changeWH() {

            $("#Frame1").height($(document).height() - 100);
            
            //$("#Frame1").width($(document).width());
        }

        window.onresize = function () {
            changeWH();
        }
    </script>
</body>

</html>
