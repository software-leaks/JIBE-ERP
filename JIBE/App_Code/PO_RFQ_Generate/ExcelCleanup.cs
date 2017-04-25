using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;


namespace PO_RFQ_Generate
{
   public static class ExcelCleanup
    {

      public static void ExcelProcessKill()
       {
           Process[] procs = Process.GetProcessesByName("EXCEL");
           foreach (Process p in procs)
           {
               //int baseAdd = p.MainModule.BaseAddress.ToInt32(); 
               //oXL is Excel.ApplicationClass object 
               p.Kill();
           }
       }


       public static string GetDrive()
       {
           string[] strDirName =new string[10];

           DriveInfo[] allDrives = DriveInfo.GetDrives();
           int i = 0;

           foreach (DriveInfo dirInfo in allDrives)
           {
               if (dirInfo.IsReady == true)
               {
                  strDirName[i] = dirInfo.Name.ToString();
                  i++;
               }
           }
           return strDirName[0];
       }



    }
}
