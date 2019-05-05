var app = new Vue({
    el: '#app',
    data: {
        AllCheckItems: [],
        Time: '',
        DailyValue: [],
        RawData: [],
        RawStandar: [],
        MonthData: []
    },

    mounted: function () {
        axios({
            url: "../Response/Inspect_Process_getTitle.ashx",
            method: "post",
        }).then((res) => {
            //this.getDailyValue();
            //this.getQuaAndMonthData();
            this.AllCheckItems = res.data.titles;
        //console.log(this.AllCheckItems);
        //console.log(this.UniqueCheckItems);
        //console.log(this.CheckTimes);
    });
this.getRawStandar();
},

methods: {
        search: function (time) {
            this.time = time;
            console.log(this.time);

            this.getDailyValue();
            this.getQuaAndMonthData();
            this.getCurrentMonthData();
        },

    //获取当月数据
        getCurrentMonthData: function () {
            axios({
                method: 'post',
                url: '../Response/ProcessDailyReport.ashx',
                dataType: 'json',
                data: {
                    methodName: 'getMonthData',
                    time: this.time.substr(0, 7) + '%'

                }
            }).then((val) => {
                this.MonthData = val.data;
            console.log(this.MonthData);
            console.log(this.MonthAvg);
            console.log(this.MonthStd);
            console.log(this.MonthQua);
        });
},

//获取检测标准
getRawStandar: function () {
    axios({
        method: 'post',
        url: '../Response/ProcessDailyReport.ashx',
        dataType: 'json',
        data: {
            methodName: 'getStandar',

        }
    }).then((val) => {
        this.RawStandar = val.data;

});
},

getQuaAndMonthData: function () {
    let time = this.time;

    axios({
        method: 'post',
        url: '../Response/ProcessDailyReport.ashx',
        dataType: 'json',
        data: {
            methodName: 'getRawData',
            time: time
        }
    }).then((val) => {
        this.RawData = val.data;

});
},


getDailyValue: function () {
    let time = this.time;
    axios({
        method: 'post',
        url: '../Response/ProcessDailyReport.ashx',
        dataType: 'json',
        data: {
            methodName: 'getDailyValue',
            time: time
        }
    }).then((val) => {
        this.DailyValue = val.data;
    if (this.DailyValue.length == 0) {
        alert("无数据,日期：" + this.time);
    }
});
},

//计算数组标准差
standardDeviation: function (arr, usePopulation = false) {
    arr = arr.map(val => {
        return parseFloat(val);
});
    const mean = arr.reduce((acc, val) => acc + val, 0) / arr.length;
    return Math.sqrt(
        arr.reduce((acc, val) => acc.concat((val - mean) ** 2), []).reduce((acc, val) => acc + val, 0) /
        (arr.length - (usePopulation ? 0 : 1))
    ).toFixed(2);
},
//计算数组平均值
computeAvg: function (arr) {
    let sum = 0;
    for (let i = 0; i < arr.length; i++) {
        sum += parseFloat(arr[i]);
    }
return (sum / arr.length).toFixed(2);
},
//计算数组合格率
computeQua: function (arr, index) {
    let result = 0;
    for (let i = 0; i < arr.length; i++) {
        if (parseFloat(arr[i]) >= parseFloat(this.Standar[index].lower) && parseFloat(arr[i]) <= parseFloat(this.Standar[index].upper)) {
            result++;
    }
}
return (result / arr.length).toFixed(2);
}
},

computed: {
    //月度数据平均值
        MonthAvg: function () {
            return this.ProcessedMonthData.map(val => {
                return this.computeAvg(val);
        });
},

//月度数据合格率
MonthQua: function () {
    return this.ProcessedMonthData.map((val, index, arr) => {
        return this.computeQua(val, index);
});
},

//月度数据标准差
MonthStd: function () {
    return this.ProcessedMonthData.map(val => {
        return this.standardDeviation(val);
});
},
//月度数据处理
ProcessedMonthData: function () {
    let result = [];
    let processedData = []
    if (this.MonthData.length == 0) {
        return result;
    }
    let pos = 0;
    for (let i = 0; i < this.CheckTimes.length; i++) {
        let temp = [];
        for (let j = 0; j < this.CheckTimes[i]; j++) {
            for (let z = 0; z < this.MonthData.length; z++) {
                temp.push(this.MonthData[z][pos])
    }
pos++;
}
processedData.push(temp);
}
processedData = processedData.map(val => {
    let temp = [];
for (let n = 0; n < val.length; n++) {
    if (val[n] != "") {
        temp.push(val[n]);
}
}
return temp;
});
return processedData;
},

    Standar: function () {
let result = [];
if (this.RawStandar.length == 0) {
return result;
}
for (let i = 0; i < this.RawStandar.length; i++) {
if (i == 0) {
    result.push(this.RawStandar[i]);
    continue;
}
if (this.RawStandar[i].name.substr(0, 3) == this.RawStandar[i - 1].name.substr(0, 3)) {
    continue
}
result.push(this.RawStandar[i]);
}
return result;
},
    //计算日合格率
    DailyQua: function () {
let result = [];
if (this.RawData.length == 0) {
return result;
}
let pos = 0;
let sumQUA = parseFloat(this.RawData[0].qua);
let sumQUAS = parseFloat(this.RawData[0].quas);
let sumAVGS = parseFloat(this.RawData[0].avgs);
let sumSTD = parseFloat(this.RawData[0].std);
let count = 1;
for (let i = 1; i < this.RawData.length; i++) {
if (this.RawData[i].inspect_name == this.RawData[i - 1].inspect_name) {
    sumQUA += parseFloat(this.RawData[i]["qua"]);
    sumQUAS += parseFloat(this.RawData[i]["quas"]);
    sumAVGS += parseFloat(this.RawData[i]["avgs"]);
    sumSTD += parseFloat(this.RawData[i]["std"]);
    count++;
} else {
    let temp = {};
    temp.name = this.RawData[i - 1]["inspect_name"];
    temp.qua = (sumQUA / count).toFixed(2);
    temp.quas = (sumQUAS / count).toFixed(2);
    temp.avgs = (sumAVGS / count).toFixed(2);
    temp.std = (sumSTD / count).toFixed(2);
    count = 1;
    sumQUA = parseFloat(this.RawData[i].qua);
    sumQUAS = parseFloat(this.RawData[i]["quas"]);
    sumAVGS = parseFloat(this.RawData[i]["avgs"]);
    sumSTD = parseFloat(this.RawData[i]["std"]);
    result.push(temp);
}
}
let temp = {};
temp.name = this.RawData[this.RawData.length - 1]["inspect_name"];
temp.qua = (sumQUA / count).toFixed(2);
temp.quas = (sumQUAS / count).toFixed(2);
temp.avgs = (sumAVGS / count).toFixed(2);
temp.std = (sumSTD / count).toFixed(2);
result.push(temp);
for (let i = 0; i < this.UniqueCheckItems.length; i++) {
if (result[i].name == this.UniqueCheckItems[i].substr(0, 3)) {
    continue;
} else {
    result.splice(i, 0, {});
}
}
return result;
},


    DailyQuare: function () {
        console.log(this.Standar);
        let result = [];
        if (this.DailyValue.length == 0 || this.CheckTimes.length == 0) {
            return [];
        }
        let pos = 0;
        let itemNum = 0;
        for (let i = 0; i < this.CheckTimes.length; i++) {
            let sumA = 0;
            let right = 0;
            for (let j = 0; j < this.CheckTimes[i]; j++) {
                if (this.DailyValue[0] && this.DailyValue[0][pos] != '') {
                    itemNum++;

                    sumA = parseFloat(this.DailyValue[0][pos]);
                    if (sumA >= parseFloat(this.Standar[i].lower) && sumA <= parseFloat(this.Standar[i].upper)) {
                        console.log('正确');
                        console.log(this.Standar);
                        right++;
        }
}
if (this.DailyValue[1] && this.DailyValue[1][pos] != '') {
    itemNum++;
    sumA = parseFloat(this.DailyValue[1][pos]);
    if (sumA >= parseFloat(this.Standar[i].lower) && sumA <= parseFloat(this.Standar[i].upper)) {
        right++;
    }
}
if (this.DailyValue[2] && this.DailyValue[2][pos] != '') {
    itemNum++;
    sumA = parseFloat(this.DailyValue[2][pos]);
    if (sumA >= parseFloat(this.Standar[i].lower) && sumA <= parseFloat(this.Standar[i].upper)) {
        right++;
    }
}
//sumA += this.DailyValue[0][pos] + this.DailyValue[1][pos] + this.DailyValue[2][pos];
pos++;
}

result.push((right / itemNum).toFixed(2));

itemNum = 0;
}

return result.map((val) => {
    if (isNaN(val)) {
        return "";
}
return val;
});

},

//计算日均值
DailyAvg: function () {
    let result = [];
    if (this.DailyValue.length == 0 || this.CheckTimes.length == 0) {
        return [];
    }
    let pos = 0;
    let itemNum = 0;
    for (let i = 0; i < this.CheckTimes.length; i++) {
        let sumA = 0;
        for (let j = 0; j < this.CheckTimes[i]; j++) {
            if (this.DailyValue[0] && this.DailyValue[0][pos] != '') {
                itemNum++;

                sumA += parseFloat(this.DailyValue[0][pos]);
    }
if (this.DailyValue[1] && this.DailyValue[1][pos] != '') {
    itemNum++;
    sumA += parseFloat(this.DailyValue[1][pos]);
}
if (this.DailyValue[2] && this.DailyValue[2][pos] != '') {
    itemNum++;
    sumA += parseFloat(this.DailyValue[2][pos]);
}
//sumA += this.DailyValue[0][pos] + this.DailyValue[1][pos] + this.DailyValue[2][pos];
pos++;
}

result.push((sumA / itemNum).toFixed(2));

itemNum = 0;
}

return result.map((val) => {
    if (isNaN(val)) {
        return "";
}
return val;
});
},
//计算检查项
UniqueCheckItems: function () {
    let result = [];
    for (let i = 0; i < this.AllCheckItems.length; i++) {
        if (i == 0) {
            result.push(this.AllCheckItems[i].slice(0, -1));
            continue;
    }
if (this.AllCheckItems[i].substr(0, 3) != this.AllCheckItems[i - 1].substr(0, 3)) {
    result.push(this.AllCheckItems[i].slice(0, -1));
}
continue;
}
return result;
},

//计算检查频次
CheckTimes: function () {
    let result = [];
    let count = 1;
    for (let i = 0; i < this.AllCheckItems.length; i++) {
        if (i == 0) {
            continue;
    }
if (this.AllCheckItems[i].substr(0, 3) == this.AllCheckItems[i - 1].substr(0, 3)) {
    count++;
    continue;
} else {
    let item = count;
    result.push(item);
    count = 1;
}
}
result.push(count);
return result;
}




}

});