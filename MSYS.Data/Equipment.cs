using System;
using System.Collections.Specialized;
using System.Data;
using MSYS.DAL;
namespace MSYS.Data
{
    // 摘要:
    //     系统用户对象
    [Serializable]
    public class Equipment : SysBaseObject
    {
        private string idkey;
        private string cls_code;
        private string eq_name;
        private string sgs_code;
        private string nc_code;
        private string finance_eq_name;
        private string eq_type;
        private string eq_status;
        private string zg_date;
        private string eq_model;
        private string ori_worth;
        private string net_worth;
        private string used_date;
        private string rated_power;
        private string real_power;
        private string power_unit;
        private string owner_name;
        private string eqp_from;
        private string ori_owner_name;
        private string manufacturer;
        private string serial_number;
        private string supplier;
        private string is_spec_eqp;
        private string is_madeinchina;
        private string mgt_dept_code;
        private string mgt_dept_name;
        private string use_dept_code;
        private string use_dept_name;
        private string duty_name;
        private string eqp_ip;
        private string eqp_mac;
        private string eqp_sn;
        private string eqp_sys;
        private string remark;
        private string creator;
        private string create_time;
        private string process_code;
        // 摘要:
        //     初始化用户对象
        public Equipment()
        {
            this.id = "";
            this.name = "";
            this.idkey = "";
            this.cls_code = "";
            this.eq_name = "";
            this.sgs_code = "";
            this.nc_code = "";
            this.finance_eq_name = "";
            this.eq_type = "";
            this.eq_status = "";
            this.zg_date = "";
            this.eq_model = "";
            this.ori_worth = "";
            this.net_worth = "";
            this.used_date = "";
            this.rated_power = "";
            this.real_power = "";
            this.power_unit = "";
            this.owner_name = "";
            this.eqp_from = "";
            this.ori_owner_name = "";
            this.manufacturer = "";
            this.serial_number = "";
            this.supplier = "";
            this.is_spec_eqp = "";
            this.is_madeinchina = "";
            this.mgt_dept_code = "";
            this.mgt_dept_name = "";
            this.use_dept_code = "";
            this.use_dept_name = "";
            this.duty_name = "";
            this.eqp_ip = "";
            this.eqp_mac = "";
            this.eqp_sn = "";
            this.eqp_sys = "";
            this.remark = "";
            this.creator = "";
            this.create_time = "";
            this.process_code = "";
        }

        public Equipment(string id)
        {
            CreateObjfromDB(id);
        }
        protected override void CreateObjfromDB(string id)
        {
            DbOperator opt = new DbOperator();
            DataSet data = opt.CreateDataSetOra("select * from ht_eq_eqp_tbl where  idkey = '" + id + "' and is_del= '0'");
            if (data != null && data.Tables[0].Rows.Count > 0)
            {
                DataRow row = data.Tables[0].Rows[0];
                this.idkey = row["idkey"].ToString();
                this.id = this.idkey;
                this.cls_code = row["cls_code"].ToString();
                this.eq_name = row["eq_name"].ToString();
                this.name = this.eq_name;
                this.sgs_code = row["sgs_code"].ToString();
                this.nc_code = row["nc_code"].ToString();
                this.finance_eq_name = row["finance_eq_name"].ToString();
                this.eq_type = row["eq_type"].ToString();
                this.eq_status = row["eq_status"].ToString();
                this.zg_date = row["zg_date"].ToString();
                this.eq_model = row["eq_model"].ToString();
                this.ori_worth = row["ori_worth"].ToString();
                this.net_worth = row["net_worth"].ToString();
                this.used_date = row["used_date"].ToString();
                this.rated_power = row["rated_power"].ToString();
                this.real_power = row["real_power"].ToString();
                this.power_unit = row["power_unit"].ToString();
                this.owner_name = row["owner_name"].ToString();
                this.eqp_from = row["eqp_from"].ToString();
                this.ori_owner_name = row["ori_owner_name"].ToString();
                this.manufacturer = row["manufacturer"].ToString();
                this.serial_number = row["serial_number"].ToString();
                this.supplier = row["supplier"].ToString();
                this.is_spec_eqp = row["is_spec_eqp"].ToString();
                this.is_madeinchina = row["is_madeinchina"].ToString();
                this.mgt_dept_code = row["mgt_dept_code"].ToString();
                this.mgt_dept_name = row["mgt_dept_name"].ToString();
                this.use_dept_code = row["use_dept_code"].ToString();
                this.use_dept_name = row["use_dept_name"].ToString();
                this.duty_name = row["duty_name"].ToString();
                this.eqp_ip = row["eqp_ip"].ToString();
                this.eqp_mac = row["eqp_mac"].ToString();
                this.eqp_sn = row["eqp_sn"].ToString();
                this.eqp_sys = row["eqp_sys"].ToString();
                this.remark = row["remark"].ToString();
                this.creator = row["creator"].ToString();
                this.create_time = row["create_time"].ToString();
                this.process_code = row["process_code"].ToString();
            }
        }

        /// <summary>
        /// 属性
        /// </summary>
        public string IDKEY { get; set; }
        public string CLS_CODE { get; set; }
        public string EQ_NAME { get; set; }
        public string SGS_CODE { get; set; }
        public string NC_CODE { get; set; }
        public string FINANCE_EQ_NAME { get; set; }
        public string EQ_TYPE { get; set; }
        public string EQ_STATUS { get; set; }
        public string ZG_DATE { get; set; }
        public string EQ_MODEL { get; set; }
        public string ORI_WORTH { get; set; }
        public string NET_WORTH { get; set; }
        public string USED_DATE { get; set; }
        public string RATED_POWER { get; set; }
        public string REAL_POWER { get; set; }
        public string POWER_UNIT { get; set; }
        public string OWNER_NAME { get; set; }
        public string EQP_FROM { get; set; }
        public string ORI_OWNER_NAME { get; set; }
        public string MANUFACTURER { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string SUPPLIER { get; set; }
        public string IS_SPEC_EQP { get; set; }
        public string IS_MADEINCHINA { get; set; }
        public string MGT_DEPT_CODE { get; set; }
        public string MGT_DEPT_NAME { get; set; }
        public string USE_DEPT_CODE { get; set; }
        public string USE_DEPT_NAME { get; set; }
        public string DUTY_NAME { get; set; }
        public string EQP_IP { get; set; }
        public string EQP_MAC { get; set; }
        public string EQP_SN { get; set; }
        public string EQP_SYS { get; set; }
        public string REMARK { get; set; }
        public string CREATOR { get; set; }
        public string CREATE_TIME { get; set; }
        public string PROCESS_CODE { get; set; }
    }
}

