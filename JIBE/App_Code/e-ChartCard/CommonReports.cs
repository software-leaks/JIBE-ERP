using System;
using System.ComponentModel;
using System.Collections.Generic;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using SMS.Data.PortageBill;

namespace SMS.Business.Reports
{
    public class CommonReports : ReportClass
    {
        string _resourcename = "";

        public CommonReports()
        {
        }


        public override string ResourceName
        {
            get
            {
                return _resourcename;
            }
            set
            {
                _resourcename = value;
            }
        }

        public static void ExportReport(string reportfile, string diskfilename, DataTable dt)
        {
                ReportDocument repDoc = new ReportDocument();
                repDoc.Load(reportfile);
                repDoc.SetDataSource(dt);
               
                ExportOptions exp = new ExportOptions();
                DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions pd = new PdfRtfWordFormatOptions();

                dk.DiskFileName = SanitizeFileName(ref diskfilename, ".pdf");

                exp.ExportDestinationType = ExportDestinationType.DiskFile;
                exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                exp.DestinationOptions = dk;
                exp.FormatOptions = pd;
                repDoc.Export(exp);
                repDoc.Close();
                repDoc.Dispose();
        }

        public static void ExportReportDoc(string reportfile,ref string diskfilename, DataTable dt)
        {
            ReportDocument repDoc = new ReportDocument();
            repDoc.Load(reportfile);
            repDoc.SetDataSource(dt);

            ExportOptions exp = new ExportOptions();
            DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions pd = new PdfRtfWordFormatOptions();

            dk.DiskFileName = SanitizeFileName(ref diskfilename, ".doc");

            exp.ExportDestinationType = ExportDestinationType.DiskFile;
            exp.ExportFormatType = ExportFormatType.WordForWindows;
            exp.DestinationOptions = dk;
            exp.FormatOptions = pd;
            repDoc.Export(exp);
            repDoc.Close();
            repDoc.Dispose();
        }

        public static void ExportReportDoc(string[] reportfile,ref string diskfilename, DataSet ds)
        {
            ReportDocument repDoc = new ReportDocument();
            repDoc.Load(reportfile[0]);
            repDoc.SetDataSource(ds.Tables[0]);
            //repDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;             

            for (int j = 1; j < reportfile.Length; j++)
            {
                if (j < ds.Tables.Count)
                {
                    repDoc.Subreports[reportfile[j]].SetDataSource(ds.Tables[j]);
                    //repDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    //repDoc.Subreports[reportfile[j]].PrintOptions.PaperOrientation = PaperOrientation.Landscape;                     
                }
                else
                    break;
            }

            ExportOptions exp = new ExportOptions();

            DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions pd = new PdfRtfWordFormatOptions();

            dk.DiskFileName = SanitizeFileName(ref diskfilename, ".doc");

            exp.ExportDestinationType = ExportDestinationType.DiskFile;
            exp.ExportFormatType = ExportFormatType.WordForWindows;
            exp.DestinationOptions = dk;
            exp.FormatOptions = pd;
            repDoc.Export(exp);
            repDoc.Close();
            repDoc.Dispose();
        }
        public static void ExportReportExcel(string reportfile,ref string diskfilename, DataTable dt)
        {
            ReportDocument repDoc = new ReportDocument();
            repDoc.Load(reportfile);
            repDoc.SetDataSource(dt);

            ExportOptions exp = new ExportOptions();
            DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
            ExcelFormatOptions pd = new ExcelFormatOptions();

            dk.DiskFileName = SanitizeFileName(ref diskfilename, ".xls");

            exp.ExportDestinationType = ExportDestinationType.DiskFile;
            exp.ExportFormatType = ExportFormatType.Excel;
            exp.DestinationOptions = dk;
            exp.FormatOptions = pd;
            repDoc.Export(exp);
            repDoc.Close();
            repDoc.Dispose();
        }
        public static void ExportReportExcel(string[] reportfile,ref string diskfilename, DataSet ds)
        {
            ReportDocument repDoc = new ReportDocument();
            repDoc.Load(reportfile[0]);
            repDoc.SetDataSource(ds.Tables[0]);
            //repDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;             

            for (int j = 1; j < reportfile.Length; j++)
            {
                if (j < ds.Tables.Count)
                {
                    repDoc.Subreports[reportfile[j]].SetDataSource(ds.Tables[j]);
                    //repDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    //repDoc.Subreports[reportfile[j]].PrintOptions.PaperOrientation = PaperOrientation.Landscape;                     
                }
                else
                    break;
            }

            ExportOptions exp = new ExportOptions();

            DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
            ExcelFormatOptions pd = new ExcelFormatOptions();

            dk.DiskFileName = SanitizeFileName(ref diskfilename, ".xls");

            exp.ExportDestinationType = ExportDestinationType.DiskFile;
            exp.ExportFormatType = ExportFormatType.Excel;
            exp.DestinationOptions = dk;
            exp.FormatOptions = pd;
            repDoc.Export(exp);
            repDoc.Close();
            repDoc.Dispose();
        }
        public static void ExportReport(string[] reportfile, string diskfilename, DataSet ds)
        {
            ReportDocument repDoc = new ReportDocument();
            repDoc.Load(reportfile[0]);
            repDoc.SetDataSource(ds.Tables[0]);
            //repDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;             

            for (int j = 1; j < reportfile.Length; j++)
            {
                if (j < ds.Tables.Count)
                {
                    repDoc.Subreports[reportfile[j]].SetDataSource(ds.Tables[j]);
                    //repDoc.PrintOptions.PaperOrientation = PaperOrientation.Landscape;
                    //repDoc.Subreports[reportfile[j]].PrintOptions.PaperOrientation = PaperOrientation.Landscape;                     
                }
                else
                    break;
            }

            ExportOptions exp = new ExportOptions();
        
            DiskFileDestinationOptions dk = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions pd = new PdfRtfWordFormatOptions();

            dk.DiskFileName = SanitizeFileName(ref diskfilename, ".pdf");

            exp.ExportDestinationType = ExportDestinationType.DiskFile;
            exp.ExportFormatType = ExportFormatType.PortableDocFormat;
            exp.DestinationOptions = dk;
            exp.FormatOptions = pd;
            repDoc.Export(exp);
            repDoc.Close();
            repDoc.Dispose();
        }
        private static string SanitizeFileName(ref string filename,string extension)
        {
            filename = filename.Replace(".pdf", "");
            filename = filename + extension;
            return filename;
        }
    }




    ///



    [System.Drawing.ToolboxBitmapAttribute(typeof(CrystalDecisions.Shared.ExportOptions), "report.bmp")]
    public class CachedPhoenixReportClass : Component, ICachedReport
    {

        public CachedPhoenixReportClass()
        {
        }

        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool IsCacheable
        {
            get
            {
                return true;
            }
            set
            {
                // 
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual bool ShareDBLogonInfo
        {
            get
            {
                return false;
            }
            set
            {
                // 
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public virtual System.TimeSpan CacheTimeOut
        {
            get
            {
                return CachedReportConstants.DEFAULT_TIMEOUT;
            }
            set
            {
                // 
            }
        }

        public virtual CrystalDecisions.CrystalReports.Engine.ReportDocument CreateReport()
        {
            CommonReports rpt = new CommonReports();
            rpt.Site = this.Site;
            return rpt;
        }

        public virtual string GetCustomizedCacheKey(RequestContext request)
        {
            String key = null;
            // // The following is the code used to generate the default
            // // cache key for caching report jobs in the ASP.NET Cache.
            // // Feel free to modify this code to suit your needs.
            // // Returning key == null causes the default cache key to
            // // be generated.
            // 
            // key = RequestContext.BuildCompleteCacheKey(
            //     request,
            //     null,       // sReportFilename
            //     this.GetType(),
            //     this.ShareDBLogonInfo );
            return key;
        }
    }

}