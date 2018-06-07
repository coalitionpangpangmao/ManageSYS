﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chartplus.aspx.cs" Inherits="Quality_Chartplus" %>
<%@ Import Namespace="ChartDirector" %>
<%@ Register TagPrefix="chart" Namespace="ChartDirector" Assembly="netchartdir" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Zooming and Scrolling with Track Line</title>
    <script type="text/javascript" src="../js/cdjcv.js"></script>
    <style type="text/css">
        .chartButton { font:12px Verdana; border-bottom:#000000 1px solid; padding:5px; cursor:pointer;}
        .chartButtonSpacer { font:12px Verdana; border-bottom:#000000 1px solid; padding:5px;}
        .chartButton:hover { box-shadow:inset 0px 0px 0px 2px #444488; }
        .chartButtonPressed { background-color: #CCFFCC; }
    </style>
</head>
<body style="margin:0px;">
<script type="text/javascript">

    //
    // Execute the following initialization code after the web page is loaded
    //
    JsChartViewer.addEventListener(window, 'load', function () {
        // Update the chart when the view port has changed (eg. when the user zooms in using the mouse)
        var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');
        viewer.attachHandler("ViewPortChanged", viewer.partialUpdate);

        // The Update Chart can also trigger a view port changed event to update the chart.
        document.getElementById("SubmitButton").onclick = function () { viewer.raiseViewPortChangedEvent(); return false; };

        // Before sending the update request to the server, we include the state of the check boxes as custom
        // attributes. The server side charting code will use these attributes to decide the data sets to draw.
        viewer.attachHandler("PreUpdate", function () {
            var checkBoxes = ["data0CheckBox", "data1CheckBox", "data2CheckBox"];
            for (var i = 0; i < checkBoxes.length; ++i)
                viewer.setCustomAttr(checkBoxes[i], document.getElementById(checkBoxes[i]).checked ? "T" : "F");
        });

        // Draw track cursor when mouse is moving over plotarea or if the chart updates
        viewer.attachHandler(["MouseMovePlotArea", "TouchStartPlotArea", "TouchMovePlotArea", "PostUpdate",
        "Now", "ChartMove"], function (e) {
            this.preventDefault(e);   // Prevent the browser from using touch events for other actions
            trackLineLegend(viewer, viewer.getPlotAreaMouseX());
        });
    });

    //
    // Draw track line with legend
    //
    function trackLineLegend(viewer, mouseX) {
        // Remove all previously drawn tracking object
        viewer.hideObj("all");

        // The chart and its plot area
        var c = viewer.getChart();
        var plotArea = c.getPlotArea();

        // Get the data x-value that is nearest to the mouse, and find its pixel coordinate.
        var xValue = c.getNearestXValue(mouseX);
        var xCoor = c.getXCoor(xValue);
        if (xCoor == null)
            return;

        // Draw a vertical track line at the x-position
        viewer.drawVLine("trackLine", xCoor, plotArea.getTopY(), plotArea.getBottomY(), "black 1px dotted");

        // Array to hold the legend entries
        var legendEntries = [];

        // Iterate through all layers to build the legend array
        for (var i = 0; i < c.getLayerCount(); ++i) {
            var layer = c.getLayerByZ(i);

            // The data array index of the x-value
            var xIndex = layer.getXIndexOf(xValue);

            // Iterate through all the data sets in the layer
            for (var j = 0; j < layer.getDataSetCount(); ++j) {
                var dataSet = layer.getDataSetByZ(j);

                // We are only interested in visible data sets with names, as they are required for legend entries.
                var dataName = dataSet.getDataName();
                var color = dataSet.getDataColor();
                if ((!dataName) || (color == null))
                    continue;

                // Build the legend entry, consist of a colored square box, the name and the data value.
                var dataValue = dataSet.getValue(xIndex);
                legendEntries.push("<nobr>" + viewer.htmlRect(7, 7, color) + " " + dataName + ": " +
                ((dataValue == null) ? "N/A" : dataValue.toPrecision(4)) + viewer.htmlRect(20, 0) + "</nobr> ");

                // Draw a track dot for data points within the plot area
                var yCoor = c.getYCoor(dataSet.getPosition(xIndex), dataSet.getUseYAxis());
                if ((yCoor != null) && (yCoor >= plotArea.getTopY()) && (yCoor <= plotArea.getBottomY())) {
                    viewer.showTextBox("dataPoint" + i + "_" + j, xCoor, yCoor, JsChartViewer.Center,
                    viewer.htmlRect(7, 7, color));
                }
            }
        }

        // Create the legend by joining the legend entries.
        var legend = "<nobr>[" + c.xAxis().getFormattedLabel(xValue, "mm/dd/yyyy") + "]" + viewer.htmlRect(20, 0) +
        "</nobr> " + legendEntries.reverse().join("");

        // Display the legend on the top of the plot area
        viewer.showTextBox("legend", plotArea.getLeftX(), plotArea.getTopY(), JsChartViewer.BottomLeft, legend,
        "width:" + plotArea.getWidth() + "px;font:bold 11px Arial;padding:3px;-webkit-text-size-adjust:100%;");
    }

    //
    // This method is called when the user clicks on the Pointer, Zoom In or Zoom Out buttons
    //
    function setMouseMode(mode) {
        var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');
        if (mode == viewer.getMouseUsage())
            mode = JsChartViewer.Default;

        // Set the button color based on the selected mouse mode
        document.getElementById("scrollButton").className = "chartButton" +
        ((mode == JsChartViewer.Scroll) ? " chartButtonPressed" : "");
        document.getElementById("zoomInButton").className = "chartButton" +
        ((mode == JsChartViewer.ZoomIn) ? " chartButtonPressed" : "");
        document.getElementById("zoomOutButton").className = "chartButton" +
        ((mode == JsChartViewer.ZoomOut) ? " chartButtonPressed" : "");

        // Set the mouse mode
        viewer.setMouseUsage(mode);
    }

    //
    // This method is called when the user clicks on the buttons that selects the last NN days
    //
    function setTimeRange(duration) {
        var viewer = JsChartViewer.get('<%=WebChartViewer1.ClientID%>');

        // Set the view port width to represent the required duration (as a ratio to the total x-range)
        viewer.setViewPortWidth(Math.min(1,
        duration / (viewer.getValueAtViewPort("x", 1) - viewer.getValueAtViewPort("x", 0))));

        // Set the view port left so that the view port is moved to show the latest data
        viewer.setViewPortLeft(1 - viewer.getViewPortWidth());

        // Trigger a view port change event
        viewer.raiseViewPortChangedEvent();
    }

</script>
<form method="post" id="ZoomScrollTrack" runat="server">
<table cellspacing="0" cellpadding="0" style="border:black 1px solid;">
    <tr>
        <td align="right" colspan="2" style="background:#000088; color:#ffff00; padding:0px 4px 2px 0px;">
            <a style="color:#FFFF00; font:italic bold 10pt Arial; text-decoration:none" href="http://www.advsofteng.com/">
                Advanced Software Engineering
            </a>
        </td>
    </tr>
    <tr valign="top">
        <td style="width:130px; background:#c0c0ff;">
        <div style="width:130px">
            <!-- The following table is to create 3 cells for 3 buttons to control the mouse usage mode. -->
            <table style="width:100%; padding:0px; border:0px; border-spacing:0px;">
                <tr>
                    <td class="chartButton" id="scrollButton" onclick="setMouseMode(JsChartViewer.Scroll)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="../images/scrollew.gif" style="vertical-align:middle" alt="Drag" />&nbsp;&nbsp;Drag to Scroll
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" id="zoomInButton" onclick="setMouseMode(JsChartViewer.ZoomIn)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="../images/zoomInIcon.gif" style="vertical-align:middle" alt="Zoom In" />&nbsp;&nbsp;Zoom In
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" id="zoomOutButton" onclick="setMouseMode(JsChartViewer.ZoomOut)"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="../images/zoomOutIcon.gif" style="vertical-align:middle" alt="Zoom Out" />&nbsp;&nbsp;Zoom Out
                    </td>
                </tr>
                <tr>
                    <td class="chartButtonSpacer">
                        <div style="padding:2px">&nbsp;</div>
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" onclick="setTimeRange(30 * 86400);"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="../images/goto.gif" style="vertical-align:middle" alt="Last 30 days" />&nbsp;&nbsp;Last 30 days
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" onclick="setTimeRange(90 * 86400);"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="../images/goto.gif" style="vertical-align:middle" alt="Last 90 days" />&nbsp;&nbsp;Last 90 days
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" onclick="setTimeRange(366 * 86400);"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="../images/goto.gif" style="vertical-align:middle" alt="Last Year" />&nbsp;&nbsp;Last Year
                    </td>
                </tr>
                <tr>
                    <td class="chartButton" onclick="setTimeRange(1E15);"
                        ontouchstart="this.onclick(event); event.preventDefault();">
                        <img src="../images/goto.gif" style="vertical-align:middle" alt="All Time" />&nbsp;&nbsp;All Time
                    </td>
                </tr>
            </table>
            <div style="font:9pt Verdana; line-height:1.5; padding-top:25px">
            <input id="data0CheckBox" type="checkbox" checked="checked" /> Alpha Series<br />
            <input id="data1CheckBox" type="checkbox" checked="checked" /> Beta Series<br />
            <input id="data2CheckBox" type="checkbox" checked="checked" /> Gamma Series<br />
            </div>
            <div style="font:9pt Verdana; margin-top:15px; text-align:center">
                <asp:button id="SubmitButton" runat="server" Text="Update Chart"></asp:button>
            </div>
        </div>
        </td>
        <td style="border-left:black 1px solid; padding:10px 5px 0px 5px;">
           <chart:WebChartViewer id="WebChartViewer1" runat="server" />
        </td>
    </tr>
</table>
</form>
</body>
</html>
