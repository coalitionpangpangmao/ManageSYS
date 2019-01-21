//import Axios from "axios";

globalParameters = [];

var app = new Vue({
    el: "#tab3",
    data: {
        currentTableId:'',
        isEdit:false,
        parameters: [],
        products:[],
        inputvalues:[],
        factoryAddress:'',
        product:'',
        editData:{
            table:[],
            tableDetail:[[],[]]
        }

    },
    mounted: function () {
        this.getCheckParameters();
        this.getProduct();
    },

    watch: {
        isEdit:function(val){
            if(val==true){
                this.bindEditData();
            }
            else{
                this.clearEditData();
            }
        },
    },

    methods: {

        deleteTable(){
            axios({
    method:"post",
    url:"../Response/updateEvaluatLeave.ashx",
    typs:"json",
    data:{
        "isInsert":"2",
        "id":this.currentTableId
    }
}).then((res)=>{
    console.log("删除成功");
document.getElementById("detail").click();
document.getElementById("btnSearch").click();
}).catch((err)=>{
    console.log("删除失败");
console.log(err.message);
});
        },

        clearEditData(){
    console.log("清除数据");
this.editData = {
    table:[],
    tableDetail:[[],[]]
};
//Vue.set(app, 'editData', {});
this.product={};
this.currentTableId='';
    },
        
bindEditData(){
    
    axios({
        method:"post",
        url:"../Response/getEvaluatLeaveData.ashx",
        data:{
            factoryId:this.currentTableId
        }
    }).then((res)=>{
        console.log("获取编辑信息成功");
    console.log(res.data);
    this.editData = res.data;
    this.product = {
        prodCode:this.editData.table[5],
        prodName:this.editData.table[6]
    }
}).catch((err)=>{
    console.log("获取编辑信息失败");
    console.log(err.message)
});
},

        buildTableId(){
            return new Date().getTime().toString();
},

buildCreateTime(){
    return new Date().toLocaleString();
},

        getInputValue(id){
            let ele = document.getElementById(id);
            return ele.value.toString();
},

        addTable(isInsert){
            let jsonData = {};
            jsonData.id  = this.currentTableId==''? this.buildTableId():this.currentTableId;
            jsonData.factoryTime = this.getInputValue("factorytime");
            //jsonData.factoryTime = this.editData.table[2];
            //jsonData.factoryAddress = this.factoryAddress;
            jsonData.factoryAddress = this.editData.table[1];
            jsonData.inspectTime = this.getInputValue("inspecttime");
            //jsonData.inspectTime = this.editData.table[3];
            jsonData.produceTime = this.getInputValue("producetime");
            //jsonData.productTime = this.editData.table[4];
            jsonData.productCode = this.product.prodCode;
            jsonData.productName = this.product.prodName;
            jsonData.createTime = this.buildCreateTime();
            jsonData.parameters = this.parameters;
            //jsonData.detailData = this.inputvalues;
            jsonData.detailData = this.editData.tableDetail;
            jsonData.detailDataCount = this.parameters.length;
            jsonData.isInsert = isInsert;
            //console.log(this.product.prodCode+this.product.prodName);
            //console.log(this.product);
            console.log(this.currentTableId);
            console.log(this.isEdit);
            axios({
                method:"post",
                url:"../Response/updateEvaluatLeave.ashx",
                type:"json",
                data:jsonData
            }).then((res)=>{
                console.log("更新成功");
                
        }).catch((err)=>{
            console.log("更新失败");
        console.log(err.message);
        });
        },

        getProduct(){
            axios({
                method:"post",
                url:"../Response/getProductInfo.ashx",
                data:{}
            }).then((res)=>{
                console.log("获取产品信息成功");
            this.products = res.data;
            }).catch((err)=>{
                console.log("获取产品信息失败");
            console.log(err.message);
            });
        },

        getCheckParameters() {
            axios({
    method: "post",
    url: "../Response/getCheckParameterHandler.ashx",
    data: {},
}).then((res) => {
    console.log("获取检测项成功");
console.log(res.data);
globalParameters = res.data;
this.parameters = res.data;
}).catch((err) => {
    console.log("获取检测项失败");
console.log(err.message);
});
},


}
});