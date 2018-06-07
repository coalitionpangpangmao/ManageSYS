using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChartDirector;
public partial class Quality_Chart : System.Web.UI.Page
{
    //
    // Page Load event handler
    //
    protected void Page_Load(object sender, EventArgs e)
    {
        ////////////多线图//////////////////////////////////
 /*       // The data for the line chart
        double[] data0 = {42, 49, 33, 38, 51, 46, 29, 41, 44, 57, 59, 52, 37, 34, 51, 56, 56, 60, 70,
        76, 63, 67, 75, 64, 51};
        double[] data1 = {50, 55, 47, 34, 42, 49, 63, 62, 73, 59, 56, 50, 64, 60, 67, 67, 58, 59, 73,
        77, 84, 82, 80, 84, 98};
        double[] data2 = {36, 28, 25, 33, 38, 20, 22, 30, 25, 33, 30, 24, 28, 15, 21, 26, 46, 42, 48,
        45, 43, 52, 64, 60, 70};

        // The labels for the line chart
        string[] labels = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13",
        "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24"};

        // Create an XYChart object of size 600 x 300 pixels, with a light blue (EEEEFF) background,
        // black border, 1 pxiel 3D border effect and rounded corners
        XYChart c = new XYChart(800, 300, 0xeeeeff, 0x000000, 1);
        c.setRoundedFrame();

        // Set the plotarea at (55, 58) and of size 520 x 195 pixels, with white background. Turn on
        // both horizontal and vertical grid lines with light grey color (0xcccccc)
        c.setPlotArea(55, 58, 520, 195, 0xffffff, -1, -1, 0xcccccc, 0xcccccc);

        // Add a legend box at (50, 30) (top of the chart) with horizontal layout. Use 9pt Arial Bold
        // font. Set the background and border color to Transparent.
        c.addLegend(50, 30, false, "Arial Bold", 9).setBackground(Chart.Transparent);

        // Add a title box to the chart using 15pt Times Bold Italic font, on a light blue (CCCCFF)
        // background with glass effect. white (0xffffff) on a dark red (0x800000) background, with a 1
        // pixel 3D border.
        c.addTitle("Application Server Throughput", "Times New Roman Bold Italic", 15).setBackground(
            0xccccff, 0x000000, Chart.glassEffect());

        // Add a title to the y axis
        c.yAxis().setTitle("MBytes per hour");

        // Set the labels on the x axis.
        c.xAxis().setLabels(labels);

        // Display 1 out of 3 labels on the x-axis.
        c.xAxis().setLabelStep(3);

        // Add a title to the x axis
        c.xAxis().setTitle("Jun 12, 2006");

        // Add a line layer to the chart
        LineLayer layer = c.addLineLayer2();

        // Set the default line width to 2 pixels
        layer.setLineWidth(2);

        // Add the three data sets to the line layer. For demo purpose, we use a dash line color for the
        // last line
        layer.addDataSet(data0, 0xff0000, "Server #1");
        layer.addDataSet(data1, 0x008800, "Server #2");
        layer.addDataSet(data2, c.dashLineColor(0x3333ff, Chart.DashLine), "Server #3");

        // Output the chart
        WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

        // Include tool tip for the chart
        WebChartViewer1.ImageMap = c.getHTMLImageMap("", "",
            "title='[{dataSetName}] Hour {xLabel}: {value} MBytes'");*/

        RanSeries r = new RanSeries(129);
        double[] data0 = r.getSeries(100, 100, -15, 15);
        double[] data1 = r.getSeries(100, 160, -15, 15);
        double[] data2 = r.getSeries(100, 220, -15, 15);
        DateTime[] timeStamps = r.getDateSeries(100, new DateTime(2014, 1, 1), 86400);

        // Create a XYChart object of size 600 x 400 pixels
        XYChart c = new XYChart(600, 200);

        // Set default text color to dark grey (0x333333)
        c.setColor(Chart.TextColor, 0x333333);

        // Add a title box using grey (0x555555) 20pt Arial font
        c.addTitle("    Multi-Line Chart Demonstration", "Arial", 20, 0x555555);

        // Set the plotarea at (70, 70) and of size 500 x 300 pixels, with transparent background and
        // border and light grey (0xcccccc) horizontal grid lines
        c.setPlotArea(70, 70, 500, 150, Chart.Transparent, -1, Chart.Transparent, 0xcccccc);

        // Add a legend box with horizontal layout above the plot area at (70, 35). Use 12pt Arial font,
        // transparent background and border, and line style legend icon.
        LegendBox b = c.addLegend(70, 35, false, "Arial", 12);
        b.setBackground(Chart.Transparent, Chart.Transparent);
        b.setLineStyleKey();

        // Set axis label font to 12pt Arial
        c.xAxis().setLabelStyle("Arial", 12);
        c.yAxis().setLabelStyle("Arial", 12);

        // Set the x and y axis stems to transparent, and the x-axis tick color to grey (0xaaaaaa)
        c.xAxis().setColors(Chart.Transparent, Chart.TextColor, Chart.TextColor, 0xaaaaaa);
        c.yAxis().setColors(Chart.Transparent);

        // Set the major/minor tick lengths for the x-axis to 10 and 0.
        c.xAxis().setTickLength(10, 0);

        // For the automatic axis labels, set the minimum spacing to 80/40 pixels for the x/y axis.
        c.xAxis().setTickDensity(80);
        c.yAxis().setTickDensity(40);

        // Add a title to the y axis using dark grey (0x555555) 14pt Arial font
        c.yAxis().setTitle("Y-Axis Title Placeholder", "Arial", 14, 0x555555);

        // Add a line layer to the chart with 3-pixel line width
        LineLayer layer = c.addLineLayer2();
        layer.setLineWidth(3);

        // Add 3 data series to the line layer
        layer.addDataSet(data0, 0x5588cc, "Alpha");
        layer.addDataSet(data1, 0xee9944, "Beta");
        layer.addDataSet(data2, 0x99bb55, "Gamma");

        // The x-coordinates for the line layer
        layer.setXData(timeStamps);

        // Output the chart
        WebChartViewer1.Image = c.makeWebImage(Chart.PNG);

        // Include tool tip for the chart
        WebChartViewer1.ImageMap = c.getHTMLImageMap("", "",
            "title='[{x|mm/dd/yyyy}] {dataSetName}: {value}'");
    }
}