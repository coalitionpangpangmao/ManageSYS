/*$.ajax({
    type:"get",
    url:"../Response/Inspect.ashx",
    success:function(data){
        console.log("ajax over");
        console.log(data.d);
    }
});*/

var app = new Vue({
    el: "#view",
    data: {
        title: [],
        rows: [],
        id:[]
    },
    mounted: function () {
        console.log("mounted finished");
        this.getTitles();

    },
    methods: {
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

saveAll(){
    console.log(this.rows.length);
    for(let i=0;i<this.rows.length; i++)
    {
        this.save(i, true);
    }
setTimeout(this.getRows.bind(this),500);
//this.getRows();
},

save(rindex, all=false){
    console.log(rindex);
    console.log(this.id);
    axios({
        method:"post",
        url:"../Response/InspectSave.ashx",
        dataType:"json",
        data:{
            data:this.rows[rindex],
            inspectID:this.id,
            count:this.id.length
        }
    }).then((res)=>{
        if(all === false){
            this.getRows();
        }
}).catch((err)=>{console.log(err.message);});
},
change(rindex,index,data){
    let tem = this.rows[rindex].splice(0);
    tem[index] = parseInt(data);
    this.$set(this.rows, rindex, tem);
    console.log(this.rows[rindex]);
},
getRows() {
    let start_time = document.getElementById("start_time");
    let end_time = document.getElementById("end_time");
    let prod_code = document.getElementById("prod_code");
    let team_code = document.getElementById("team_code");
    let schedule_time = document.getElementById("schedule_time");
    console.log(start_time.value);
    console.log(end_time.value);
    console.log(prod_code.value);
    console.log(team_code.value);
    console.log(schedule_time.value)
    axios({
        url: "../Response/InspectRows.ashx",
        method: "post",
        data: {
            start_time: start_time.value,
            end_time: end_time.value,
            prod_code: prod_code.value,
            team_code: team_code.value,
            schedule_time: schedule_time.value
        }
    }).then((res) => {
        this.rows = res.data.rows;
    console.log(this.rows[0]);
    console.log(this.rows);
}).catch((err) => {
    console.log(err.message);
});
}
}
});
