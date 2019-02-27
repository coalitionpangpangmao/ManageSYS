<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Evaluat_Leave.aspx.cs" Inherits="Quality_Evaluat_Leave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>理化检测报告</title>
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
            $('#btnExport').hide();
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
                <li><a href="#">出厂报告</a></li>
            </ul>
        </div>

        <div class="mainbox">
            <table class="tablelist" style="margin-bottom: 5px">
                <tbody>
                    <tr>
                         <td>产品: <asp:DropDownList ID="prod_code" runat="server" CssClass="drpdwnlist">
                            </asp:DropDownList>
                            
                        </td>
                          <td>发货地址: <asp:DropDownList ID="address" runat="server" CssClass="drpdwnlist">
                            </asp:DropDownList>
                            
                        </td>
                          <td>发货日期: <asp:DropDownList ID="time" runat="server" CssClass="drpdwnlist">
                            </asp:DropDownList>
                            
                        </td>
                        <td colspan="7" align="center"><!--请选择月度：-->     
                   <!-- <asp:TextBox ID="txtBtime" runat="server" CssClass="dfinput1"
                        onclick="WdatePicker({dateFmt:'yyyy-MM'})"></asp:TextBox>-->
                            <asp:Button ID="btnSearch" runat="server" Text="查询" CssClass="btnview" OnClick="btnSearch_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="btnPrint" type="button" value="打印" class="btnpatch" onclick="$('#report').printArea();" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <input id="btnExport" type="button" value="导出" class="btnset" onclick="MSYS_Export('Msysexport');" />
                              <input href="#tab3" id="newTable" type="button" value="新增" class="btnset" onclick=" Vue.set(app, 'isEdit', false); $('#tabtop3').parent().show(); $('#tabtop3').click();" />
                            
                        </td>

                    </tr>
                </tbody>
            </table>
            <div id="usual1" class="usual">
                <div class="itab">
                    <ul>
                        <li><a href="#tab1" class="selected" id="tabtop1">出厂报告</a></li>
                        <li><a href="#tab2" id="tabtop2">出厂检测报告</a><span onclick="$('#tabtop1').click();$('#tabtop2').parent().hide(); $('#btnPrint').hide(); $('#btnExport').hide();"></span></li>
                        <li><a href="#tab3" id="tabtop3">出厂检测详情</a><span id="detail" onclick="$('#tabtop1').click();$('#tabtop3').parent().hide(); Vue.set(app, 'isEdit', false);"></span></li>
                    </ul>
                </div>
            </div>

           
            <div id="tab1" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div style="width: 100%; height: 100%; overflow-x: scroll">
                            <asp:GridView ID="GridView1" runat="server" class="grid" AutoGenerateColumns="True" OnRowCreated="GridView1_RowCreated" HeaderStyle-Wrap="False" RowStyle-Wrap="False">
                                <Columns>
                                    <asp:TemplateField HeaderText="操作" HeaderStyle-Width="220px">
                                        <ItemTemplate>
                                            <asp:Button ID="btngridview" runat="server" Text="出厂报告" CssClass="btn1" OnClick="btngridview_Click"  Width="100px"  OnClientClick=" $('#tabtop2').parent().show(); $('#tabtop2').click();$('#btnPrint').show();$('#btnExport').show();"/>
                                            <asp:Button ID="btngridDetail" runat="server" Text="修改详情" CssClass="btn1" OnClick="btngridDetail_Click" Width="100px" OnClientClick =" $('#tabtop3').parent().show(); $('#tabtop3').click(); Vue.set(app,'currentTableId', this.parentNode.nextSibling.innerHTML); Vue.set(app, 'isEdit', true);"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="gridheader" />
                                <RowStyle CssClass="gridrow" />
                                <AlternatingRowStyle CssClass="gridalterrow" />
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>

            <!--出产检测报告  -->
            <div id="tab2" class="tabson" style="margin-top: 0px; padding-top: 0px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <!--startprint-->
                        <div id="report" class="PrintArea" style="width: 90%; margin-left: 50px">
                            <% = htmltable %>
                        </div>
                        <!--endprint-->
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView1" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <!--出厂检测详情-->
            <div id="tab3" class="tabson" style="margin-top: 0px; padding-top: 0px;">

                    
                        <table id="input" class="tablelist" style="margin-bottom: 5px">
                            <tbody>
                               <thead>
                                   <th colspan="3">基本信息</th>
                                   <thead>
                            <tr>
                                <td>出厂地址:
                                    <input class="dfinput1" id="factoryaddress" type="text" v-model="editData.table[1]"/>
                                 </td>

                                  <td>出厂产品:
                                      <select class="drpdwnlist" id="prod" v-model="product" v-if="products.length>0">
                                          <option v-for="(prod, index) in products" :value="prod">{{prod.prodName}}</option>
                                       </select>
                                 </td>

                                </tr>
                                <tr>
                                                       <td>出厂日期:
                                    <input class="dfinput1" id="factorytime" type="text"  :value="editData.table[2]||getInputValue('factorytime')"  onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"/>
                                 </td>
                                  <td>
                                      检测日期:
                                      <input class="dfinput1" id="inspecttime" :value="editData.table[3]||getInputValue('inspecttime')"  onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"/>
                                   </td>
                                     <td style="display:none">生产日期:
                                         <input class="dfinput1" id="producetime" :value="editData.table[4]||getInputValue('producetime')"   onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"/>
                                         </td>
                            </tr>
                                       <tr><td>生产周期1：<input class="dfinput1" id="producetime1_s" :value="editData.table[7]||getInputValue('producetime1_s')" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"/>
                                            <input class="dfinput1" id="producetime1_e" :value="editData.table[8]||getInputValue('producetime1_e')" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"/></td>
                                       </tr>
                                                                              <tr><td>生产周期2：<input class="dfinput1" id="producetime2_s" :value="editData.table[9]||getInputValue('producetime2_s')" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"/>
                                            <input class="dfinput1" id="producetime2_e" :value="editData.table[10]||getInputValue('producetime2_e')" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"/></td>
                                       </tr>
                                                                              <tr><td>生产周期3:<input class="dfinput1" id="producetime3_s" :value="editData.table[11]||getInputValue('producetime3_s')" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"/>
                                            <input class="dfinput1" id="producetime3_e" :value="editData.table[12]||getInputValue('producetime3_e')" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"/></td>
                                       </tr>
                                       </table>
                            <table class="tablelist" style="margin-bottom: 5px">
                               <thead>
                                   <th colspan="2">参数信息</th>
                                   </thead>
                              <tr v-for = "(para, index) in parameters" v-if="parameters.length > 0">
                                <td style="width:5%">
                                    {{para.name}}:</td><td> <input class="dfinput1" :id="para.code" v-model="editData.tableDetail[index]"/></td>
                                
                            </tr>
                                <tr>
                                    <td></td><td></td>
                                </tr>
                                <tr>
                                    <td><div class="btn1"v-if="isEdit"  @click="deleteTable">删除</div></td>
                                    <td><div class="btn1" v-if="!isEdit" @click="addTable(1)">新增</div>
                                    <div class="btn1" v-if="isEdit" @click="addTable(0)">保存</div></td>
                                    
                                    </tr>
                                </table>
                                </tbody>

                        </table>
                        
                        

            </div>

        </div>

        <script type="text/javascript">
            $("#usual1 ul").idTabs();
        </script>

    </form>
             <script type="text/javascript" src="../js/msys/Evaluat_Leave.js"></script>
</body>

</html>

