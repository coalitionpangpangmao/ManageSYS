/*$.ajax({
    type:"get",
    url:"../Response/Inspect.ashx",
    success:function(data){
        console.log("ajax over");
        console.log(data.d);
    }
});*/


Date.prototype.format = function() {
    var s = '';
    var mouth = (this.getMonth() + 1)>=10?(this.getMonth() + 1):('0'+(this.getMonth() + 1));
    var day = this.getDate()>=10?this.getDate():('0'+this.getDate());
    s += this.getFullYear() + '-'; // 获取年份。
    s += mouth + "-"; // 获取月份。
    s += day; // 获取日。
    return (s); // 返回日期。
};
 
function getAll(begin, end, dates) {
    var ab = begin.split("-");
    var ae = end.split("-");
    var db = new Date();
    db.setUTCFullYear(ab[0], ab[1] - 1, ab[2]);
    var de = new Date();
    de.setUTCFullYear(ae[0], ae[1] - 1, ae[2]);
    var unixDb = db.getTime();
    var unixDe = de.getTime();
    for (var k = unixDb; k <= unixDe;) {
        //console.log((new Date(parseInt(k))).format());
        dates.push([(new Date(parseInt(k))).format(),"甲班"]);
        dates.push([(new Date(parseInt(k))).format(),"乙班"]);
        dates.push([(new Date(parseInt(k))).format(),"丙班"]);
        k = k + 24 * 60 * 60 * 1000;
    }
}

var app = new Vue({
    el: "#view",
    data: {
        isAdd:false,
        isBatchInsert: false,
        title: [],
        rows: [],
        id:[],
        dates:[],
        rowsDates:[],
        prod:"产品",
        team:"",
        t:"中班",
        changed:[],
        deleteList:[],
        addData:[],
        addTime:"",
        addTeam:"",
        addSchedule:"",
        addProduct:"",
        productInfo:[]

    },
    mounted: function () {
        console.log("mounted finished");
        this.getProductInfo();
        this.getTitles();

    },
    methods: {
        
        keyDown(event) {
var inputs = document.getElementById("grid").getElementsByTagName("input");
//alert("keydown");
var focus = document.activeElement;
if (!document.getElementById("grid").contains(focus)) return;
console.log("keydownrun");
var event = window.event || event;
var key = event.keyCode;
for (var i = 0; i < inputs.length; i++) {
    if (inputs[i] === focus) break;
}
switch (key) {
    case 37:
        if (i > 0) inputs[i - 1].focus();
        break;
    case 38:
        if (i - 4 >= 0) inputs[i - 5- this.id.length].focus();
        break;
    case 39:
        if (i < inputs.length - 1) inputs[i + 1].focus();
        break;
    case 40:
        if (i + 4 < inputs.length) inputs[i + 5+this.id.length].focus();
        break;
}
},
batchAddRecord(){
    this.getRows();
        this.isBatchInsert = true;
},

        //获得排班
    getSchedule(start_time, end_time, dates,  team_code, schedule_time){
        axios({
    method:"post",
    url:"../Response/getSchedule.ashx",
    dataType:"json",
    data:{
        start_time:start_time,
        end_time:end_time
    }
}).then((res)=>{
    this.dates = res.data;
if(team_code.value!="00"){
    this.dates = this.dates.filter((ele)=>{
        if(ele[3]==team_code.value){
        return true;    
}
return false;
});
}
if(schedule_time.value!="00"){
    this.dates = this.dates.filter((ele)=>{
        if(ele[1]==schedule_time.value){
        return true;    
}
return false;
});
}

console.log(dates);
}).catch((err)=>{console.log(err.message);});
},
getTitles() {
    console.log("getTitles is running");
    axios({
        url: "../Response/Inspect.ashx",
        method: "post",
    }).then((res) => {
        this.title = res.data.titles;
    console.log(this.title);
}).catch((err) => {
    console.log(err.message);
});

axios({
    url:"../Response/InspectId.ashx",
    method:"post",
}).then((res)=>{this.id = res.data.id;});
},
show(dindex){
    console.log("show is running");
    let time = document.getElementById("txtProdTime");
    let team = document.getElementById("listTeam");
    let schedule = document.getElementById("listShift");
    let tab = document.getElementById("tabtop1")
    let prod = document.getElementById("listProd");

    team.value = this.dates[dindex][3];
    schedule.value = this.dates[dindex][1];
    prod.value="7031004";
    prod.onchange();
    prod.value = document.getElementById("prod_code").value
    time.value="1990-01-01";
    time.value = this.dates[dindex][0];
    setTimeout(()=>{time.value=this.dates[dindex][0];}, 100);
//document.getElementById("query").click();
tab.click();
},

deleteA(){
    /*this.deleteList.forEach((ele)=>{
        var temp = this.rows[ele];
    temp.forEach((ele,index,list)=>{
        list[index] = 0;
    });
        this.$set(this.rows, ele, temp);
});*/
        //this.changed.push(this.deleteList);
        this.saveAll(true);
        
},

getProductInfo(){
    axios({
        method:"POST",
        url:"../Response/getProductInfo.ashx",
        dataType:"json",

    }).then((res)=>{
        console.log("获取产品信息成功");
    this.productInfo = res.data;
    });
},

    changeDeleteList(index){

        //this.getProductInfo();
    var deleteL = [];
    deleteL = this.deleteList;
    var add = deleteL.indexOf(index);
    if(add!=-1){
       deleteL.splice(add, 1);
    }else{
        deleteL.push(index);
    }
    this.deleteList = deleteL;
    console.log(deleteL);
    console.log(this.deleteList);
},

initAdd(){
    let tr = document.getElementById("addtr")
    let tempData = [];
    for(let i=0; i<this.id.length; i++){
        tempData.push(0);
    }
    this.addData = tempData;
    this.addTeam = null;
    this.addTime = null;
    this.addSchedule = null;
    this.addProduct = null;
},

getTrData(tr){
    let result=[];
    for(let i=1; i<=4; i++){
        console.log(tr.children[i].children[0].value);
        result.push(tr.children[i].children[0].value.toString());
    }
    return result;
},

    softDeleteAll(){
        console.log(this.deleteList);
        for(let i=0; i<this.deleteList.length; i++){
        let tr = document.getElementById(this.deleteList[i]);
        let tempData = this.getTrData(tr);
        this.softDelete(tempData);
    }
},

softDelete(Data){
    let tempData = Data.splice(3,);
    if(Data[2] == "甲班"){
        Data[2] = "01";
    }
    if(Data[2] == "乙班"){
        Data[2] = "02";
    }
    if(Data[2] == "丙班"){
        Data[2] = "03";
    }
    let date = Data;
    axios({
        method:"post",
        url:"../Response/Inspect_Process_Delete.ashx",
        dataType:"json",
        data:{
            data:tempData,
            inspectID:this.id,
            count:this.id.length,
            dates:date,
            prod:document.getElementById("prod_code").value,
            createId:document.getElementById("listEditor").value,
        }
    }).then((res)=>{
        console.log("删除成功");
    this.deleteList = [];
    this.getRows();
    }).catch((err)=>{
        console.log("删除失败");
        console.log(err.message);
    });
},

saveAll(isDelete=false){
    console.log(this.dates.length);
    console.log(this.changed);
    var changed = Array.from(new Set(this.changed))
    console.log(changed);
    if(isDelete == true){
        changed = this.deleteList;
    }
    let ss = this.save.bind(this);
    console.log("delete list");
    console.log(changed);
    for(let i=0;i<changed.length; i++)
    {
        this.save(changed[i], true, isDelete);

        //setTimeout(()=>{ss(i, true);},1000)
    }
this.changed=[];
setTimeout(this.getRows.bind(this),500);
//this.getRows();

},

save(rindex, all=false, isDelete=false){
    let tempData = [];
    if(isDelete == true){
        for(let i=0;i<this.title.length; i++){
           tempData.push(0);
           console.log(tempData[i]);
        }
    }
for(let i=0;i<this.title.length; i++){
   tempData.push(document.getElementById(rindex.toString()+i.toString()).value.toString());
   console.log(tempData[i]);
}
console.log(tempData);
    /*let flag = tempData.some((value)=>{
        return value!=0;
    });
    if(!flag){
        return;
    }*/
let isUpdated = this.rows.some((val)=>{
console.log("judgeupdate");
console.log(this.dates[rindex][0]);
console.log(this.dates[rindex][4]);
return val[0]==this.dates[rindex][0] && this.dates[rindex][4] == val[2];
});
let recordId="";
if(isUpdated){
this.rows.forEach((val)=>{
    if(val[0]==this.dates[rindex][0] && val[2] == this.dates[rindex][4]){
    recordId = val[val.length-2];        
}
});
}
console.log("isupdate"+isUpdated.toString());
axios({
    method:"post",
    url:"../Response/InspectSave.ashx",
    dataType:"json",
    data:{
    data:tempData,
    inspectID:this.id,
    count:this.id.length,
    isUpdate:isUpdated?1:0,
    dates:this.dates[rindex],
    prod:document.getElementById("prod_code").value,
    createId:document.getElementById("listEditor").value,
    recordId:recordId
}
}).then((res)=>{
if(all === false){
    this.getRows();
}
}).catch((err)=>{console.log(err.message);});
},
add(dindex){
console.log("add+"+dindex);
this.changed.push(dindex);
},

    //加入更细队列
change(dindex,rindex,index,data){
this.changed.push(dindex);
    //let tem = this.rows[rindex].splice(0);
    //tem[index] = parseInt(data);
    //this.$set(this.rows, rindex, tem);
    //console.log(this.rows[rindex]);
console.log("the dindex is"+ dindex);
},

    //显示新增数据框
showAddRecord(){
    //this.getProductInfo();
    this.initAdd();
    this.isAdd = true;
    this.isBatchInsert = false;

},

hiddenAddRecord(){
    this.isAdd = false;
    this.initAdd();
},

    //新增数据
    addRecord(){

    this.addTime = document.getElementById("addtime").value;
    let index_in_dates = this.dates.indexOf(this.addTime);
    let dates = this.dates.find((val)=>{
        return val[0] == this.addTime && val[3] == document.getElementById("addteam").value.toString();

});
    console.log(this.dates);
    //let shift = this.dates[index_in_dates][1];
    // dates = [this.addTime, document.getElementById("addschedule").value.toString(), 01, document.getElementById("addteam").value.toString(), 01];
    //dates = [this.addTime, shift, 01, document.getElementById("addteam").value.toString(), 01];
    axios({
    method:"post",
    url:"../Response/InspectSave.ashx",
    dataType:"json",
    data:{
    data:this.addData,
    inspectID:this.id,
    count:this.id.length,
    isUpdate:0,
    dates:dates,
    prod:document.getElementById("addproduct").value.toString(),
    createId:document.getElementById("listEditor").value,
    recordId:null
}
}).then((res)=>{
    this.hiddenAddRecord();
    this.getRows();


}).catch((err)=>{console.log(err.message);});
},



    //获取数据
getRows() {
    this.isBatchInsert = false;
    let start_time = document.getElementById("start_time");
    let end_time = document.getElementById("end_time");
    let prod_code = document.getElementById("prod_code"); 
    let team_code = document.getElementById("team_code");
    let schedule_time = document.getElementById("schedule_time");
    /* console.log(start_time.value);
     console.log(end_time.value);
     console.log(prod_code.value);
     console.log(team_code.value);
     console.log(schedule_time.value) */
    this.team="";
    if(team_code.value=="01"){
        this.team = "甲班";
}
    if(team_code.value=="02"){
        this.team = "乙班";
}
    if(team_code.value=="03"){
        this.team="丙班"
}

    this.dates=[];
    // getAll(start_time.value, end_time.value, this.dates);
    this.getSchedule(start_time.value, end_time.value, this.dates, team_code, schedule_time);

    console.log(this.dates);
    axios({
    url: "../Response/InspectRows.ashx",
    method: "post",
    data: {
    start_time: start_time.value,
    end_time: end_time.value,
    prod_code: prod_code.value,
    team_code: team_code.value,
    schedule_time: "00"
}
}).then((res) => {
        this.rowsDates = res.data.rows.map(function(ele){return ele[0].toString();});
    this.prod = prod_code.options[prod_code.selectedIndex].text;
    console.log(this.rowsDates);
    this.rows = res.data.rows;
    console.log(this.rowsDates.indexOf("2018-10-02"));
    console.log(this.rows[0]);
    console.log(this.rows);
}).catch((err) => {
    console.log(err.message);
});
}
}
});
