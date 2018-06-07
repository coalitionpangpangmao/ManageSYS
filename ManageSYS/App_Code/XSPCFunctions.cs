namespace SPCLib
{
    using System;
    //针对数据组的操作
    /// <summary>
    /// /计算分组后的每组均值方差
    /// </summary>
    public class XSPCFunctions
    {
        private double[] c4 = new double[] { 1.0, 0.7979, 0.8862, 0.9213, 0.94, 0.9515, 0.9594, 0.965, 0.9693, 0.9727,0.9754,0.9776,0.9794,0.9810,0.9823,0.9835,0.9845,0.9854,0.9862,0.9869,0.9876,0.9882,0.9887,0.9892,0.9896};
        private double[] d2 = new double[] { 1.0, 1.128, 1.693, 2.059, 2.326, 2.534, 2.704, 2.847, 2.97, 3.078,3.1730,3.2580,3.3360,3.4070,3.4720,3.5320,3.5880,3.6400,3.6890,3.7350,3.7880,3.8190,3.8580,3.8950,3.9310};
       // private double[] d3 = new double[] { 1.0, 0.853, 0.888, 0.88, 0.864, 0.848, 0.833, 0.82, 0.808, 0.797 };
        private double[] d3 = new double[]{1.0,0.0000,0.0000,0.0000,0.0000,0.0000,0.0760,0.1360,0.1840,0.2230,0.2560,0.2830,0.3070,0.3280,0.3470, 0.3630,0.3780,0.3910,0.4030,0.4150,0.4250,0.4340,0.4430,0.4510,0.4590};
        private double LSL;
        private double Mean;
        private double[] MeaninSampleGroup;
        private double Range;
        private double RangeCL;
        private double[] RangeinSampleGroup;
        private double RangeLCL;
        private double RangeUCL;
        private double[][] SamplesByGroup;
        private double Stddev;
        private double StddevCL;
        private double StddevLCL;
        private double[] StddevSampleGroup;
        private double StddevUCL;
        private double USL;
        private double XrCL;
        private double XrLCL;
        private double XrUCL;
        private double XsCL;
        private double XsLCL;
        private double XsUCL;

        public XSPCFunctions(double[][] sampleByGroup)
        {
            this.SamplesByGroup = sampleByGroup;
            this.initXSPCFunctions();
        }

        public double getLSL()
        {
            return this.LSL;
        }

        public double getMean()
        {
            return this.Mean;
        }

        public double[] getMeaninSampleGroup()
        {
            return this.MeaninSampleGroup;
        }

        public double getRange()
        {
            return this.Range;
        }

        public double getRangeCL()
        {
            return this.RangeCL;
        }

        public double[] getRangeinSampleGroup()
        {
            return this.RangeinSampleGroup;
        }

        public double getRangeLCL()
        {
            return this.RangeLCL;
        }

        public double getRangeUCL()
        {
            return this.RangeUCL;
        }

        public double getStddev()
        {
            return this.Stddev;
        }

        public double getStddevCL()
        {
            return this.StddevCL;
        }

        public double getStddevLCL()
        {
            return this.StddevLCL;
        }

        public double[] getStddevSampleGroup()
        {
            return this.StddevSampleGroup;
        }

        public double getStddevUCL()
        {
            return this.StddevUCL;
        }

        public double getUSL()
        {
            return this.USL;
        }

        public double getXrCL()
        {
            return this.XrCL;
        }

        public double getXrLCL()
        {
            return this.XrLCL;
        }

        public double getXrUCL()
        {
            return this.XrUCL;
        }

        public double getXsCL()
        {
            return this.XsCL;
        }

        public double getXsLCL()
        {
            return this.XsLCL;
        }

        public double getXsUCL()
        {
            return this.XsUCL;
        }

        public void initXSPCFunctions()
        {
            SPCFunctions sPCFunctions = new SPCFunctions();
            //分别计算每一组的均值，极差，方差
            sPCFunctions.setHistogramGroupNumber(this.SamplesByGroup.Length);
            this.MeaninSampleGroup = new double[this.SamplesByGroup.Length];
            this.RangeinSampleGroup = new double[this.SamplesByGroup.Length];
            this.StddevSampleGroup = new double[this.SamplesByGroup.Length];
            for (int i = 0; i < this.SamplesByGroup.Length; i++)
            {
                sPCFunctions.setSamples(this.SamplesByGroup[i]);
                this.MeaninSampleGroup[i] = sPCFunctions.avg;
                this.RangeinSampleGroup[i] = sPCFunctions.Range;
                this.StddevSampleGroup[i] = sPCFunctions.stddev;
            }
            //分别计算均值平均，极差平均，方差平均
            sPCFunctions.setSamples(this.MeaninSampleGroup);
            this.Mean = sPCFunctions.avg;
            sPCFunctions.setSamples(this.RangeinSampleGroup);
            this.Range = sPCFunctions.Range;
            sPCFunctions.setSamples(this.StddevSampleGroup);
            this.Stddev = sPCFunctions.stddev;
            //根据公式分别计算均值，极差，方差的上下限
            this.XrCL = this.Mean;
            double CLdiffer = (3.0 / (Math.Sqrt((double) this.SamplesByGroup[0].Length) * this.d2[this.SamplesByGroup[0].Length - 1])) * this.Range;
            this.XrUCL = this.Mean + CLdiffer;
            this.XrLCL = this.Mean - CLdiffer;
            this.RangeCL = this.Range;
            double d3byd2 = (3.0 * this.d3[this.SamplesByGroup[0].Length - 1]) / this.d2[this.SamplesByGroup[0].Length - 1];
            this.RangeUCL = (1.0 + d3byd2) * this.Range;
            this.RangeLCL = Math.Max((double) ((1.0 - d3byd2) * this.Range), (double) 0.0);
            this.XsCL = this.Mean;
            double a3s = (3.0 / (Math.Sqrt((double) this.SamplesByGroup[0].Length) * this.c4[this.SamplesByGroup[0].Length - 1])) * this.Stddev;
            this.XsUCL = this.Mean + a3s;
            this.XsLCL = this.Mean - a3s;
            this.StddevCL = this.Stddev;
            double b3b4 = (3.0 * Math.Sqrt(1.0 - Math.Pow(this.c4[this.SamplesByGroup[0].Length - 1], 2.0))) / this.c4[this.SamplesByGroup[0].Length - 1];
            this.StddevUCL = this.Stddev * (1.0 + b3b4);
            this.StddevLCL = Math.Max((double) (this.Stddev * (1.0 - b3b4)), (double) 0.0);
        }

        public void setLSL(double LSL)
        {
            this.LSL = LSL;
        }

        public void setUSL(double USL)
        {
            this.USL = USL;
        }
    }
}

