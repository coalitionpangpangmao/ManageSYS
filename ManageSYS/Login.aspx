﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>鑫源再源梗丝生产管理系统</title>
<link rel="stylesheet" type="text/css" href="css/main.css"/>
<script type="text/javascript" src="../js/js.js"></script>
</head>
<body>
<div id="top"> </div>
<form id="login" name="login" runat ="server">
  <div id="center">
    <div id="center_left"></div>
    <div id="center_middle">
      <div class="user">
        <label>用户名：
        <input type="text" name="user" id="user" />
        </label>
      </div>
      <div class="user">
        <label>密　码：
        <input type="password" name="pwd" id="pwd" />
        </label>
      </div>
      
    </div>
    <div id="center_middle_right"></div>
    <div id="center_submit">
      <div class="button">
          <asp:ImageButton ID="btnLogin" src="images/dl.gif" width="57" height="20" 
              runat="server" BorderStyle="None" onclick="btnLogin_Click" />
       </div>
      <div class="button">
        <asp:ImageButton ID="btnReset" src="images/cz.gif" width="57" height="20" 
              runat="server" BorderStyle="None" onclick="btnReset_Click" />
      </div>
    </div>
    <div id="center_right"></div>
  </div>
</form>
<div id="footer"></div>
</body>
</html>
