using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SysConfig_ImportExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Import_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.HasFile == false)
            {
                Msg.Text = "要上传的文件不允许为空！";
                return;
            }
            else
            {
                string filepath = FileUpload1.PostedFile.FileName;
                string filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
                string serverpath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"templates\" + filename;
                FileUpload1.PostedFile.SaveAs(serverpath);
                Msg.Text = "上传成功！";
            }
        }
        catch (Exception error)
        {
            Msg.Text = "处理发生错误！原因：" + error.ToString();
        }
    }
}