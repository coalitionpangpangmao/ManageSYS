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
        title: [],
        rows: [],
        id:[],
        dates:[],
        rowsDates:[],
        prod:"产品",
        team:"",
        t:"中班",
        changed:[]
    },
    mounted: function () {
        console.log("mounted finished");
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
        if (i - 4 >= 0) inputs[i - 4- this.id.length].focus();
        break;
    case 39:
        if (i < inputs.length - 1) inputs[i + 1].focus();
        break;
    case 40:
        if (i + 4 < inputs.length) inputs[i + 4+this.id.length].focus();
        break;
}
},
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
        url: "../Response/Inspect_Process_getTitle.ashx",
        method: "post",
    }).then((res) => {
        this.title = res.data.titles;
    console.log(this.title);
}).catch((err) => {
    console.log(err.message);
});

axios({
    url:"../Response/Inspect_Process_getID.ashx",
    method:"post",
}).then((res)=>{this.id = res.data.id;});
},
show(dindex){
    console.log("show is running");
    let time = document.getElementById("txtProdTime");
    let team = document.getElementById("listTeam");
    let schedule = document.getElementById("listShift");
    let tab = document.getElementById("tabtop2")
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
saveAll(){
    console.log(this.dates.length);
    let ss = this.save.bind(this);
    this.changed = Array.from(new Set(this.changed))
    for(let i=0;i<this.changed.length; i++)
    {
        this.save(this.changed[i], true);

        //setTimeout(()=>{ss(i, true);},1000)
    }
this.changed=[];
setTimeout(this.getRows.bind(this),500);
//this.getRows();

},

save(rindex, all=false){
    let tempData = [];
    for(let i=0;i<this.title.length; i++){
       tempData.push(document.getElementById(rindex.toString()+i.toString()).value.toString());
       console.log(tempData[i]);
    }
console.log(tempData);
let flag = tempData.some((value)=>{
    return value!=0;
});
if(!flag){
    return;
}
let isUpdated = this.rows.some((val)=>{
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
    this.changed.push(dindex);
},
change(dindex,rindex,index,data){
    this.changed.push(dindex);
    let tem = this.rows[rindex].splice(0);
    if(data == ''){
        tem[index] = '';
    }else{
        tem[index] = parseFloat(data);
    }
    this.$set(this.rows, rindex, tem);
    console.log(this.rows[rindex]);
},
getRows() {
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
        url: "../Response/Inspect_Process_getRows.ashx",
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
    //3-27修改
    for(let i=0; i<this.rows.length; i++){
        for(let j=4; j<this.rows[0].length; j++){
            if(this.rows[i][j]==-1){
                this.rows[i][j]="";
            }
        }
}
//
    console.log(this.rowsDates.indexOf("2018-10-02"));
    console.log(this.rows[0]);
    console.log(this.rows);
}).catch((err) => {
    console.log(err.message);
});
}
}
});
