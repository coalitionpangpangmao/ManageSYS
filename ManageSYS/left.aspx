<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/jquery.accordion.js"></script>
   <script language="javascript" type="text/javascript">
       jQuery().ready(function () {
           $('.subhead').next('ul').hide();
           $('.subhead').click(function () {
               var $ul = $(this).next('ul');
               //   $('dd').find('ul').slideUp();
               if ($ul.is(':visible')) {
                   $(this).next('ul').slideUp();
               } else {
                   $(this).next('ul').slideDown();
               }
           });
           jQuery('#navigation').accordion({
               header: '.head',
               navigation: true,
               event: 'click',
               fillSpace: false,
               animated: 'bounceslide'
           });
           
       });
    
     
    </script>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px;
            font-size: 12px;
        }
        #navigation
        {
            margin: 0px;
            padding: 0px;
            width: 147px;
        }
        #navigation a.head
        {
            cursor: pointer;
            background: url(images/main_34.gif) no-repeat scroll;
            display: block;
            font-weight: bold;
            margin: 0px;
            padding: 5px 0 5px;
            text-align: center;
            font-size: 12px;
            text-decoration: none;
        }
        #navigation ul
        {
            border-width: 0px;
            margin: 0px;
            padding: 0px;
            text-indent: 0px;
        }
        #navigation li
        {
            list-style: none;
            display: inline;
        }
        #navigation li li a
        {
            display: block;
            font-size: 12px;
            text-decoration: none;
            text-align: center;
            padding: 3px;
        }
        
        #navigation li li a:hover
        {
            background: url(images/tab_bg.gif) repeat-x;
            border: solid 1px #adb9c2;
        }
        
        #navigation li li a.subhead
        {
            display: block;
            font-size: 12px;
             font-weight: bold;
            text-decoration: none;
            text-align: center;
            padding: 3px;
            background: url(images/list.gif) no-repeat;
        }
    </style>
</head>
<body> 
<div  style="height:100%;">
<%=SysHtml %>
</div>
</body>
</html>

