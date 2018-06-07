
namespace SPCLib
{
    using System;
    //针对一组数据的操作
    /// <summary>
    /// /进行样本点的质量分析   算出统计值
    /// </summary>
    public class SPCFunctions
    {
        /// <summary>
        /// 传入参数，计算数据点，上下限等
        /// </summary>
        private double[] Samples;
        private double LSL;//标准下限
        private double CL;//标准值
        private double USL;//标准上限    
        /// <summary>
        /// 直方图相关参数
        /// </summary>
        private double UCL;//控制上限
        private double LCL;//控制下限   
        private const string HistogramGrouInfo = "50-100个样本分成6-10组，100-250个样本分成7-12组，250个以上样本分成10-25组";
        private int HistogramGroupNumber;
        private const string HistogramGroupTips = "对于绝大多数用户来讲,50个样本具备足够的可靠性，测量成本低时，为了达到准确，尽量将样本数量满足100个以上";
        /// <summary>
        /// 计算参数
        /// </summary>
        public int count { get; set; }//总点数
        public double avg { get; set; }//均值
        public int upcount { get; set; }//超上限数
        public double uprate { get; set; }//超上限率
        public int downcount { get; set; }//超下限数
        public double downrate { get; set; }//超下限率
        public int passcount { get; set; }//合格点数
        public double passrate { get; set; }//合格率
        public double min { get; set; }//最小值
        public double max { get; set; }//最大值
        public double var { get; set; }//方差
        public double stddev { get; set; }//标准差
        public double absdev { get; set; }//绝对差
        public double Range { get; set; }//最大跨距
        public double Cp { get; set; }//过程能力指数
        public double Cr { get; set; }//过程能力比值
        public double K { get; set; }//偏移系数，用来计算检验结果偏移中心程度的指标
        public double Cpu { get; set; }//上限过程能力指数
        public double Cpl { get; set; }//下限过程能力指数        
        public double Cpk { get; set; }//修正的过程能力指数
        public double Cpm { get; set; }//目标能力指数


        
/// <summary>
/// 初始化
/// </summary>
        public SPCFunctions()
        {
        }
        //带入双精度数组和规格上下限
        public SPCFunctions(object[] array, double usl, double lsl)
        {            
            this.USL = usl;
            this.LSL = lsl;
            this.count = array.Length;
            this.upcount = 0;
            this.downcount = 0;
            this.max = Convert.ToDouble(array[0]);
            this.min = Convert.ToDouble(array[0]);
            double sum = 0;
            for (int i = 0; i < this.count; i++)
            {
                this.Samples[i] = Convert.ToDouble(array[i]);
                sum += this.Samples[i];
                if (this.max < this.Samples[i])
                    this.max = this.Samples[i];
                if (this.min > this.Samples[i])
                    this.min = this.Samples[i];
                    

                if (this.Samples[i] > this.USL)
                    this.upcount++;
                else if (this.Samples[i] < this.LSL)
                    this.downcount++;
                else
                    continue;
            }
            this.passcount = this.count - this.upcount - this.downcount;
            this.uprate = this.upcount / this.count;
            this.downrate = this.downcount / this.count;
            this.passrate = 1 - this.uprate - this.downrate;
            this.avg = sum / count;
            double sum2 = 0;
            sum = 0;
            for (int i = 0; i < this.count; i++)
            {
                sum += (this.Samples[i] - this.avg) * (this.Samples[i] - this.avg);
                sum2 += Math.Abs(this.Samples[i] - this.avg);
            }
            this.var = sum / (this.count - 1);
            this.absdev = Math.Sqrt(sum2/(this.count -1));
            this.stddev = Math.Sqrt(this.var);
            this.Range = this.max - this.min;
            this.Cp = (this.USL - this.LSL) / (6 * this.stddev);
            this.Cr = 6 * this.stddev / (this.USL - this.LSL);
            this.K = SPC_K();
            this.Cpu = SPC_Cpu();
            this.Cpl = SPC_Cpl();
            this.Cpk = SPC_Cpk();
            this.Cpm = SPC_Cpm();

        }

        public void setSamples(double[] samples)
        {
            this.count = samples.Length;

            this.max = samples[0];
            this.min = samples[0];
            double sum = 0;
            for (int i = 0; i < this.count; i++)
            {
                this.Samples[i] = Convert.ToDouble(samples[i]);
                sum += this.Samples[i];
                if (this.max < this.Samples[i])
                    this.max = this.Samples[i];
                if (this.min > this.Samples[i])
                    this.min = this.Samples[i];

            }         
            this.avg = sum / count;
            double sum2 = 0;
            sum = 0;
            for (int i = 0; i < this.count; i++)
            {
                sum += (this.Samples[i] - this.avg) * (this.Samples[i] - this.avg);
                sum2 += Math.Abs(this.Samples[i] - this.avg);
            }
            this.var = sum / (this.count - 1);
            this.absdev = Math.Sqrt(sum2 / (this.count - 1));
            this.stddev = Math.Sqrt(this.var);
            this.Range = this.max - this.min;
        }
        /// <summary>
        /// //Set 分组数，上下限，采集数
        /// </summary>
        /// <param name="samples"></param>




        /// <summary>
        /// /Get  属性值
        /// </summary>
        /// <returns></returns>
        public double getCL()
        {
            return this.CL;
        }
        public int getHistogramGroupNumber()
        {
            return this.HistogramGroupNumber;
        }
        public double getLCL()
        {
            return this.LCL;
        }
        public double getLSL()
        {
            return this.LSL;
        }
        public double[] getSamples()
        {
            return this.Samples;
        }
        public double getSL()
        {
            return ((this.USL + this.LSL) / 2.0);
        }
        public double getUCL()
        {
            return this.UCL;
        }
        public double getUSL()
        {
            return this.USL;
        }
        /// <summary>
        /// /直方图计算
        /// </summary>
        /// <param name="HistogramGroupNumber"></param>
        public void setHistogramGroupNumber(int HistogramGroupNumber)
        {
            this.HistogramGroupNumber = HistogramGroupNumber;
        }//分组数
       public double[][] HistogramStepUpperLowerMidCountNormCumu()
        {
            double[][] HistogramUpperLowerMidCountNormCumu = new double[][] { new double[this.HistogramGroupNumber + 2], new double[this.HistogramGroupNumber + 2], new double[this.HistogramGroupNumber + 2], new double[this.HistogramGroupNumber + 2], new double[this.HistogramGroupNumber + 2], new double[this.HistogramGroupNumber + 2] };
            double step = this.Range / ((double) this.HistogramGroupNumber);//步长
            double min = this.min;//最小值
            double mean = this.avg;//平均值
            double std = this.stddev;//标准差
            for (int i = 0; i <= (this.HistogramGroupNumber + 1); i++)
            {
                HistogramUpperLowerMidCountNormCumu[0][i] = (min - (step / 2.0)) + (i * step);//[0]直方图每组下限
                HistogramUpperLowerMidCountNormCumu[2][i] = (min + (step / 2.0)) + (i * step);//【2】直方图每组上限
                HistogramUpperLowerMidCountNormCumu[1][i] = min + (i * step);//【1】直方图每组中值
                HistogramUpperLowerMidCountNormCumu[3][i] = 0.0;//落在各组中点的个数
               // HistogramUpperLowerMidCountNormCumu[4][i] = NORMDIST(min + (i * step), mean, std, false);
                HistogramUpperLowerMidCountNormCumu[5][i] = NORMDIST(min + (i * step), mean, std, true);//
                for (int j = 0; j < this.Samples.Length; j++)
                {
                    if ((this.Samples[j] >= HistogramUpperLowerMidCountNormCumu[0][i]) && (this.Samples[j] < HistogramUpperLowerMidCountNormCumu[2][i]))
                    {
                        HistogramUpperLowerMidCountNormCumu[3][i]++;
                    }
                }
                HistogramUpperLowerMidCountNormCumu[4][i] = HistogramUpperLowerMidCountNormCumu[3][i]/this.Samples.Length;//落在各组中点的概率
            }
            this.XbarChart();//计算控制上下限
            return HistogramUpperLowerMidCountNormCumu;
        }//与直方图相关的计算
       public void XbarChart()
       {
           this.CL = this.avg;
           double range = this.Range;
           this.UCL = this.CL + (0.42992261392949266 * range);
           this.LCL = this.CL - (0.42992261392949266 * range);
       }//控制上下限
/// <summary>
/// //数据计算公式
/// </summary>
/// <param name="z"></param>
/// <returns></returns>
       private static double erf(double z)
        {
            double t = 1.0 / (1.0 + (0.5 * Math.Abs(z)));
            double ans = 1.0 - (t * Math.Exp(((-z * z) - 1.26551223) + (t * (1.00002368 + (t * (0.37409196 + (t * (0.09678418 + (t * (-0.18628806 + (t * (0.27886807 + (t * (-1.13520398 + (t * (1.48851587 + (t * (-0.82215223 + (t * 0.17087277)))))))))))))))))));
            if (z >= 0.0)
            {
                return ans;
            }
            return -ans;
        }
        public static double NORMDIST(double x, double mean, double std, bool cumulative)
        {
            if (cumulative)
            {
                return Phi(x, mean, std);
            }
            double tmp = 1.0 / (Math.Sqrt(6.2831853071795862) * std);
            return (tmp * Math.Exp(-0.5 * Math.Pow((x - mean) / std, 2.0)));
        }
        private static double Phi(double z)
        {
            return (0.5 * (1.0 + erf(z / Math.Sqrt(2.0))));
        }
        private static double Phi(double z, double mu, double sigma)
        {
            return Phi((z - mu) / sigma);
        }



/// <summary>
/// /SPC统计信息计算
/// </summary>
/// <returns></returns>
/// 
        public double SPC_K()
        {
            return Math.Abs((this.avg - (this.USL + this.LSL) / 2)) * 2 / (this.USL - this.LSL);
        }
        public double SPC_Cpu()
        {
            return (this.USL - this.avg) / (3 * this.stddev);
        }
        public double SPC_Cpl()
        {
            return (this.avg - this.LSL) / (3 * this.stddev);
        }
        public double SPC_Cpk()
        {
            if (this.Cpu > this.Cpl)
                return this.Cpl;
            else
                return this.Cpu;
        }
        public double SPC_Cpm()
        {
            return ((this.USL - this.LSL) / (6.0 * Math.Sqrt(Math.Pow(this.stddev, 2.0) + Math.Pow(this.avg - (this.USL - this.LSL), 2.0))));
        }              
 
        public double SPC_ZuCap()//规格上限水平
        {
            return (this.Cpu * 3.0);
        }
            public double SPC_ZlCap()//规格下限水平
        {
            return (this.Cpl * 3.0);
        }
         public double SPC_FpuCap()//超出规格上限概率
            {
                return (1.0 - Phi(this.Cpu * 3.0));
            }
         public double SPC_FplCap()//超出规格下限概率
         {
             return (1.0 - Phi(this.Cpl * 3.0));
         }
         public double SPC_FpCap()//超出规格上下限概率
         {
             return (this.SPC_FpuCap() + this.SPC_FplCap());
         }
         public double SPC_Pp()//过程性能指数
         {
             return (((this.USL - this.LSL) / 6.0) / this.stddev);
         }
         public double SPC_Pr()//过程性能比值
         {
             return 6 * this.stddev / (this.USL - this.LSL);
         }
         public double SPC_Ppu()//上限过程性能指数
         {
             return ((this.USL - this.avg) / (3.0 * this.stddev));
         }
         public double SPC_Ppl()//下限过程性能指数
         {
             return ((this.avg - this.LSL) / (3.0 * this.stddev));
         }
         public double SPC_Ppk()//修正的过程性能指数
         {
             return Math.Min(this.SPC_Ppu(), this.SPC_Ppl());
         }
         public double SPC_Ppm()//目标过程性能指数
         {
             return ((this.USL - this.LSL) / (6.0 * Math.Sqrt(Math.Pow(this.stddev, 2.0) + Math.Pow(this.avg - (this.USL - this.LSL), 2.0))));
         }
        public double SPC_Rate()
        {
            return NORMDIST((this.USL - this.avg) / this.stddev,0,1,true) - NORMDIST((this.LSL - this.avg) / this.stddev,0,1,true);
        }
        public double SPC_Kurtosis()//峰度
        {
            double sumup4 = 0.0;
            for (int i = 0; i < this.Samples.Length; i++)
            {
                sumup4 += Math.Pow(this.Samples[i] - this.avg, 4.0);
            }
            return ((((this.Samples.Length * (this.Samples.Length + 1)) * sumup4) / ((((this.Samples.Length - 1) * (this.Samples.Length - 2)) * (this.Samples.Length - 3)) * Math.Pow(this.stddev, 4.0))) - ((3.0 * Math.Pow((double) (this.Samples.Length - 1), 2.0)) / ((double) ((this.Samples.Length - 2) * (this.Samples.Length - 3)))));
        }
   

        public double SPC_Skewness()//偏度
        {
            double sumup3 = 0.0;
            for (int i = 0; i < this.Samples.Length; i++)
            {
                sumup3 += Math.Pow(this.Samples[i] - this.avg, 3.0);
            }
            return ((this.Samples.Length * sumup3) / (((this.Samples.Length - 1) * (this.Samples.Length - 2)) * Math.Pow(this.stddev, 3.0)));
        }
   
        public double SPC_ZlPerf()//规格下限&水平
        {
            return (3.0 * this.SPC_Ppl());
        }
  
        public double SPC_ZuPerf()//规格上限&水平
        {
            return (3.0 * this.SPC_Ppu());
        }
        public double SPC_FplPerf()//超出规格下限根率
        {
            return (1.0 - Phi(3.0 * this.SPC_Ppl()));
        }
        public double SPC_FpPerf()//超出规格下下限概率
        {
            return (this.SPC_FpuPerf() + this.SPC_FplPerf());
        }

        public double SPC_FpuPerf()//超出规格上限概率
        {
            return (1.0 - Phi(3.0 * this.SPC_Ppu()));
        }
    
    }
}

