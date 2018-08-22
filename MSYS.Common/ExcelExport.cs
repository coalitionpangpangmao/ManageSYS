
    using System;
    using System.Reflection;
    using Microsoft.Office.Interop.Excel;
    using System.Data;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
namespace MSYS.Common
{
    class ExcelExport
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        protected Microsoft.Office.Interop.Excel.Application xlApp;
        protected Microsoft.Office.Interop.Excel.Workbook xlBook;
        protected Microsoft.Office.Interop.Excel.Workbooks xlBooks;
        //Excel.Range xlRange;
        protected Microsoft.Office.Interop.Excel.Sheets xlsheets;
        protected Microsoft.Office.Interop.Excel.Worksheet xlSheet;
        IntPtr intptr;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="bCreate">创建模式</param>
        public ExcelExport(string filename, bool bCreate)
        {
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlBooks = xlApp.Workbooks;
                xlBook = xlBooks.Open(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlsheets = xlBook.Worksheets;
                intptr = new IntPtr(xlApp.Hwnd);
            }
            catch
            {
                xlBook = null;
                xlApp = null;
            }
            finally
            {
                //xlRange = null;            
            }
        }
        public ExcelExport()
        {
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlBooks = xlApp.Workbooks;
                xlBook = xlBooks.Add(Type.Missing);
                xlsheets = xlBook.Worksheets;
                intptr = new IntPtr(xlApp.Hwnd);
            }
            catch
            {
                xlBook = null;
                xlApp = null;
            }
            finally
            {
                //xlRange = null;            
            }
        }

        /// <summary>
        /// 设置当前工作表
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string SetCurrentSheet(int index)
        {
            try
            {
                xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlsheets.get_Item(index + 1);
                return "OK";
            }
            catch (Exception ee)
            {
                xlSheet = null;
                return ee.Message;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 向指定位置写入数据
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="dt"></param>
        public void WriteDataIntoWorksheet(int x, int y, System.Data.DataTable dt)
        {
            try
            {
                int count = dt.Columns.Count;
                int Rcount = dt.Rows.Count;
                for (int j = 0; j < Rcount; j++)
                {
                    for (int i = 0; i < count; i++)
                    {
                        //  Microsoft.Office.Interop.Excel.Range r1 = xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, i + 1]);
                        //  r1.NumberFormatLocal = "@";
                        string v = dt.Rows[j][i].ToString();
                        if (IsOnlyNumber(v) && Convert.ToDouble(v) > 100000000000)
                        {
                            ((Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[j + x, i + y]).NumberFormatLocal = "@";
                            //   Microsoft.Office.Interop.Excel.Range r2 = xlSheet.get_Range(xlSheet.Cells[2, 1], xlSheet.Cells[2, i + 1]);
                            //   r2.NumberFormatLocal = "@";
                        }
                        ((Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[j + x, i + y]).Value2 = v;

                    }
                }
            }
            catch
            {
            }
        }
        public void WriteDataIntoWorksheetWithCaption(int x, int y, System.Data.DataTable dt)
        {
            try
            {
                int count = dt.Columns.Count;
                int Rcount = dt.Rows.Count;
                for (int j = 0; j < Rcount; j++)
                {
                    for (int i = 0; i < count; i++)
                    {
                        //  Microsoft.Office.Interop.Excel.Range r1 = xlSheet.get_Range(xlSheet.Cells[1, 1], xlSheet.Cells[1, i + 1]);
                        //  r1.NumberFormatLocal = "@";
                        if (j == 0)
                        {
                            string h = dt.Columns[i].ColumnName;
                            ((Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[j + x, i + y]).Value2 = h;
                        }
                        string v = dt.Rows[j][i].ToString();
                        if (IsOnlyNumber(v) && Convert.ToDouble(v) > 100000000000)
                        {
                            ((Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[j + x + 1, i + y]).NumberFormatLocal = "@";
                            //   Microsoft.Office.Interop.Excel.Range r2 = xlSheet.get_Range(xlSheet.Cells[2, 1], xlSheet.Cells[2, i + 1]);
                            //   r2.NumberFormatLocal = "@";
                        }
                        ((Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[j + x + 1, i + y]).Value2 = v;

                    }
                }
            }
            catch
            {
            }
        }



        public string WriteData(int x, int y, string data)
        {
            try
            {
                ((Microsoft.Office.Interop.Excel.Range)xlSheet.Cells[x, y]).Value2 = data;
                //   Microsoft.Office.Interop.Excel.Range r2 = xlSheet.get_Range(xlSheet.Cells[2, 1], xlSheet.Cells[2, i + 1]);
                //   r2.NumberFormatLocal = "@";
                return "OK";
            }
            catch (Exception ee)
            {
                return ee.Message;
            }
        }

        public void SaveAs(string filename)
        {
            try
            {
                xlBook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlBook.Close(false, Type.Missing, Type.Missing);
                xlBooks.Close();
                xlApp.Quit();
            }
            catch (Exception)
            {

            }
            finally
            {
                //xlRange = null;
                xlSheet = null;
                xlBook = null;
                xlApp = null;
            }
        }

        public void SaveAsHtm(string filename)
        {
            try
            {
                xlBook.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                xlBook.Close(false, Type.Missing, Type.Missing);
                xlBooks.Close();
                xlApp.Quit();
            }
            catch (Exception)
            {

            }
            finally
            {
                //xlRange = null;
                xlSheet = null;
                xlBook = null;
                xlApp = null;
            }
        }
        public void Dispose()
        {
            if (xlSheet != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet);
                xlSheet = null;
            }
            if (xlBook != null)
            {
                xlBook.Close(false, Type.Missing, Type.Missing);
                xlBooks.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook);
                xlBook = null;
            }
            if (xlApp != null)
            {
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }
        }

        private static bool IsOnlyNumber(string value)
        {
            Regex r = new Regex(@"^[0-9]+$");

            return r.Match(value).Success;
        }

        private static bool IsNumberAndString(string value)
        {
            Regex r = new Regex(@"(\d+[a-zA-Z])|([a-zA-Z]\d+)");

            return r.Match(value).Success;
        }

        private static bool IsOnlyWord(string value)
        {
            Regex r = new Regex(@"^[a-zA-Z]+$");

            return r.Match(value).Success;
        }

    }
}

