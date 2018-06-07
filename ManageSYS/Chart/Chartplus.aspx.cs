using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChartDirector;
public partial class Quality_Chartplus : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        //
        // This script handles both the full page request, as well as the subsequent partial updates
        // (AJAX chart updates). We need to determine the type of request first before we processing it.
        //
        if (WebChartViewer.IsPartialUpdateRequest(Page))
        {
            // Is a partial update request.

            // The .NET platform will not restore the states of the controls before or during Page_Load,
            // so we need to restore the state ourselves
            WebChartViewer1.LoadViewerState();

            // Draw the chart in partial update mode
            drawChart(WebChartViewer1);

            // Output the chart immediately and then terminate the page life cycle (PartialUpdateChart
            // will cause Page_Load to terminate immediately without running the following code).
            WebChartViewer1.PartialUpdateChart();
        }

        //
        // If the code reaches here, it is a full page request.
        //

        // In this exapmle, we just need to initialize the WebChartViewer and draw the chart.
        initViewer(WebChartViewer1);
        drawChart(WebChartViewer1);
    }

    private void initViewer(WebChartViewer viewer)
    {
        // The full x-axis range is from Jan 1, 2007 to Jan 1, 2012
        DateTime startDate = new DateTime(2010, 1, 1);
        DateTime endDate = new DateTime(2015, 1, 1);
        viewer.setFullRange("x", startDate, endDate);

        // Initialize the view port to show the last 366 days (out of 1826 days)
        viewer.ViewPortWidth = 366.0 / 1826;
        viewer.ViewPortLeft = 1 - viewer.ViewPortWidth;

        // Set the maximum zoom to 10 days (out of 1826 days)
        viewer.ZoomInWidthLimit = 10.0 / 1826;
    }

    //
    // Create a random table for demo purpose.
    //
    private RanTable getRandomTable()
    {
        RanTable r = new RanTable(127, 4, 1828);
        r.setDateCol(0, new DateTime(2010, 1, 1), 86400);
        r.setCol(1, 150, -10, 10);
        r.setCol(2, 200, -10, 10);
        r.setCol(3, 250, -8, 8);
        return r;
    }

    //
    // Draw the chart
    //
    private void drawChart(WebChartViewer viewer)
    {
        // Determine the visible x-axis range
        DateTime viewPortStartDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft));
        DateTime viewPortEndDate = Chart.NTime(viewer.getValueAtViewPort("x", viewer.ViewPortLeft +
            viewer.ViewPortWidth));

        // We need to get the data within the visible x-axis range. In real code, this can be by using a
        // database query or some other means as specific to the application. In this demo, we just
        // generate a random data table, and then select the data within the table.
        RanTable r = getRandomTable();

        // Select the data for the visible date range viewPortStartDate to viewPortEndDate. It is
        // possible there is no data point at exactly viewPortStartDate or viewPortEndDate. In this
        // case, we also need the data points that are just outside the visible date range to "overdraw"
        // the line a little bit (the "overdrawn" part will be clipped to the plot area) In this demo,
        // we do this by adding a one day margin to the date range when selecting the data.
        r.selectDate(0, viewPortStartDate.AddDays(-1), viewPortEndDate.AddDays(1));

        // The selected data from the random data table
        DateTime[] timeStamps = Chart.NTime(r.getCol(0));
        double[] dataSeriesA = r.getCol(1);
        double[] dataSeriesB = r.getCol(2);
        double[] dataSeriesC = r.getCol(3);

        //
        // Now we have obtained the data, we can plot the chart.
        //

        //================================================================================
        // Configure overall chart appearance.
        //================================================================================

        // Create an XYChart object of size 640 x 350 pixels
        XYChart c = new XYChart(640, 350);

        // Set the plotarea at (55, 55) with width 80 pixels less than chart width, and height 90 pixels
        // less than chart height. Use a vertical gradient from light blue (f0f6ff) to sky blue (a0c0ff)
        // as background. Set border to transparent and grid lines to white (ffffff).
        c.setPlotArea(55, 55, c.getWidth() - 80, c.getHeight() - 90, c.linearGradientColor(0, 55, 0,
            c.getHeight() - 35, 0xf0f6ff, 0xa0c0ff), -1, Chart.Transparent, 0xffffff, 0xffffff);

        // As the data can lie outside the plotarea in a zoomed chart, we need to enable clipping.
        c.setClipping();

        // Add a title to the chart using 18pt Times New Roman Bold Italic font
        c.addTitle("    Zooming and Scrolling with Track Line", "Times New Roman Bold Italic", 18);

        // Set the axis stem to transparent
        c.xAxis().setColors(Chart.Transparent);
        c.yAxis().setColors(Chart.Transparent);

        // Add axis title using 10pt Arial Bold Italic font
        c.yAxis().setTitle("Ionic Temperature (C)", "Arial Bold Italic", 10);

        //================================================================================
        // Add data to chart
        //================================================================================

        //
        // In this example, we represent the data by lines. You may modify the code below to use other
        // layer types (areas, scatter plot, etc).
        //

        // Add a line layer for the lines, using a line width of 2 pixels
        LineLayer layer = c.addLineLayer2();
        layer.setLineWidth(2);

        // In this demo, we do not have too many data points. In real code, the chart may contain a lot
        // of data points when fully zoomed out - much more than the number of horizontal pixels in this
        // plot area. So it is a good idea to use fast line mode.
        layer.setFastLineMode();

        // Add up to 3 data series to a line layer, depending on whether the user has selected the data
        // series.
        layer.setXData(timeStamps);
        if (viewer.GetCustomAttr("data0CheckBox") != "F")
        {
            layer.addDataSet(dataSeriesA, 0xff3333, "Alpha Series");
        }
        if (viewer.GetCustomAttr("data1CheckBox") != "F")
        {
            layer.addDataSet(dataSeriesB, 0x008800, "Beta Series");
        }
        if (viewer.GetCustomAttr("data2CheckBox") != "F")
        {
            layer.addDataSet(dataSeriesC, 0x3333cc, "Gamma Series");
        }

        //================================================================================
        // Configure axis scale and labelling
        //================================================================================

        // Set the x-axis as a date/time axis with the scale according to the view port x range.
        viewer.syncDateAxisWithViewPort("x", c.xAxis());

        //
        // In this demo, the time range can be from a few years to a few days. We demonstrate how to set
        // up different date/time format based on the time range.
        //

        // If all ticks are yearly aligned, then we use "yyyy" as the label format.
        c.xAxis().setFormatCondition("align", 360 * 86400);
        c.xAxis().setLabelFormat("{value|yyyy}");

        // If all ticks are monthly aligned, then we use "mmm yyyy" in bold font as the first label of a
        // year, and "mmm" for other labels.
        c.xAxis().setFormatCondition("align", 30 * 86400);
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(), "<*font=bold*>{value|mmm yyyy}",
            Chart.AllPassFilter(), "{value|mmm}");

        // If all ticks are daily algined, then we use "mmm dd<*br*>yyyy" in bold font as the first
        // label of a year, and "mmm dd" in bold font as the first label of a month, and "dd" for other
        // labels.
        c.xAxis().setFormatCondition("align", 86400);
        c.xAxis().setMultiFormat(Chart.StartOfYearFilter(),
            "<*block,halign=left*><*font=bold*>{value|mmm dd<*br*>yyyy}", Chart.StartOfMonthFilter(),
            "<*font=bold*>{value|mmm dd}");
        c.xAxis().setMultiFormat2(Chart.AllPassFilter(), "{value|dd}");

        // For all other cases (sub-daily ticks), use "hh:nn<*br*>mmm dd" for the first label of a day,
        // and "hh:nn" for other labels.
        c.xAxis().setFormatCondition("else");
        c.xAxis().setMultiFormat(Chart.StartOfDayFilter(), "<*font=bold*>{value|hh:nn<*br*>mmm dd}",
            Chart.AllPassFilter(), "{value|hh:nn}");

        //================================================================================
        // Step 5 - Output the chart
        //================================================================================

        // Output the chart
        viewer.Image = c.makeWebImage(Chart.PNG);

        // Output Javascript chart model to the browser to suppport tracking cursor
        viewer.ChartModel = c.getJsChartModel();
    }

    //
    // Page Load event handler
    //
}