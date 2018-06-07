<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportExcel.aspx.cs" Inherits="SysConfig_ImportExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="../css/style.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:FileUpload ID="FileUpload1" runat="server" ForeColor="Black" ViewStateMode="Enabled"
                                        Width="250px" CssClass="dfinput" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Import" runat="server" Text="导入" OnClick="Import_Click" CssClass="btnset" />
                                   <asp:Label ID="Msg" runat="server" Text=" "></asp:Label> 
    </div>
    </form>
</body>
</html>
