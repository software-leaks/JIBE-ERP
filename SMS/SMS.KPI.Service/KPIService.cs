using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;
using System.Threading;
using System.ServiceModel.Activation;
using SMS.KPI.Service.DataContract;
using System.Text;
using System.IO;
using System.Configuration;
using SMS.Business.TMSA;
using SMS.KPI.Service;
using System.Web.Script.Serialization;
using System.ServiceModel.Web;
using SMS.Business.VET;
using SMS.Business.Infrastructure;




namespace SMS.KPI.Service
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TMSA_Service" in both code and config file together.
    public partial class KPIService : IKPIService
    {
        BLL_TMSA_KPI objKPI = new BLL_TMSA_KPI();
        BLL_TMSA_REPORTS objREPORT = new BLL_TMSA_REPORTS();
        BLL_TMSA_EEDI objEEDIBLL = new BLL_TMSA_EEDI();
        BLL_TMSA_Worklist objWorklist = new BLL_TMSA_Worklist();
        BLL_TMSA_Vetting_Reports objVTReports = new BLL_TMSA_Vetting_Reports();
        BLL_VET_VettingLib objVet = new BLL_VET_VettingLib();
        BLL_VET_Index objVetIndex = new BLL_VET_Index();

        public List<KPICO2> GetData(string VID, string KPI_ID, string Startdate, string EndDate)
        {
            List<KPICO2> NT = new List<KPICO2>();
            try
            {

                DataTable dtnodehierarchy = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Startdate, EndDate).Tables[0];
                KPICO2 nt;

                if (dtnodehierarchy.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Rows)
                    {
                        nt = new KPICO2();
                        nt.RDATE = Convert.ToString(dr["Record_Date"]);
                        nt.VALUE = Convert.ToString(dr["Value"]);

                        DataTable dtt = objKPI.GetGoal(int.Parse(VID), "KPI005", 0);
                        if (dtt.Rows.Count > 0)
                        {
                            nt.GOAL = dtt.Rows[0]["Goal"].ToString();
                        }
                        else
                        {
                            nt.GOAL = "0";
                        }
                        nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                        DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                        if (dt.Rows.Count != 0)
                        {
                            nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                        }
                        else
                            nt.EEDI = "0";
                        NT.Add(nt);
                    }
                }
                else
                {
                    nt = new KPICO2();
                    nt.RDATE = Startdate;
                    nt.VALUE = "0.00";

                    DataTable dtt = objKPI.GetGoal(int.Parse(VID), "KPI005", 0);
                    if (dtt.Rows.Count > 0)
                    {
                        nt.GOAL = dtt.Rows[0]["Goal"].ToString();
                    }
                    else
                    {
                        nt.GOAL = "0";
                    }
                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                    nt = new KPICO2();
                    nt.RDATE = EndDate;
                    nt.VALUE = "0.00";

                    DataTable dtte = objKPI.GetGoal(int.Parse(VID), "KPI005", 0);
                    if (dtte.Rows.Count > 0)
                    {
                        nt.GOAL = dtt.Rows[0]["Goal"].ToString();
                    }
                    else
                    {
                        nt.GOAL = "0";
                    }
                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    DataTable dte = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dte.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }



            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }





        public List<KPISOx> GetDataSOx(string VID, string Startdate, string EndDate)
        {
            List<KPISOx> NT = new List<KPISOx>();
            try
            {

                DataTable dtnodehierarchy = BLL_TMSA_PI.Search_PI_ValuesSOX(int.Parse(VID), Startdate, EndDate).Tables[0];
                KPISOx nt;
                if (dtnodehierarchy.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Rows)
                    {
                        nt = new KPISOx();
                        nt.RDATE = Convert.ToString(dr["Record_Date"]);
                        nt.VALUE = Convert.ToString(dr["Value"]);
                        // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();
                        nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                        DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                        if (dt.Rows.Count != 0)
                        {
                            nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                        }
                        else
                            nt.EEDI = "0";
                        NT.Add(nt);
                    }
                }
                else
                {
                    nt = new KPISOx();
                    nt.RDATE = Startdate;
                    nt.VALUE = "0.00";
                    // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();
                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                    nt = new KPISOx();
                    nt.RDATE = EndDate;
                    nt.VALUE = "0.00";
                    // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();
                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    DataTable dt1 = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt1.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt1.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }


        public List<KPINOx> GetDataNOx(string VID, string Startdate, string EndDate)
        {
            List<KPINOx> NT = new List<KPINOx>();
            try
            {

                DataTable dtnodehierarchy = BLL_TMSA_PI.Search_PI_ValuesNOX(int.Parse(VID), Startdate, EndDate).Tables[0];
                KPINOx nt;
                if (dtnodehierarchy.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Rows)
                    {
                        nt = new KPINOx();
                        nt.RDATE = Convert.ToString(dr["Record_Date"]);
                        nt.VALUE = Convert.ToString(dr["Value"]);
                        // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();

                        nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                        DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                        if (dt.Rows.Count != 0)
                        {
                            nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                        }
                        else
                            nt.EEDI = "0";
                        NT.Add(nt);
                    }
                }
                else
                {
                    nt = new KPINOx();
                    nt.RDATE = Startdate;
                    nt.VALUE = "0.00";
                    // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();

                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);

                    nt = new KPINOx();
                    nt.RDATE = EndDate;
                    nt.VALUE = "0.00";
                    // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();

                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    DataTable dt1 = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt1.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt1.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }





        public List<KPICO2> GetVoyageData(string VID, string KPI_ID, string Startdate, string EndDate, string telId1, string telId2)
        {
            List<KPICO2> NT = new List<KPICO2>();
            try
            {

                DataSet dtnodehierarchy = BLL_TMSA_PI.Search_Voyage_PI_Values(int.Parse(VID), int.Parse(KPI_ID), telId1, telId2);
                KPICO2 nt;

                if (dtnodehierarchy.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Tables[0].Rows)
                    {
                        nt = new KPICO2();
                        nt.RDATE = Convert.ToString(dr["Record_Date"]);
                        nt.VALUE = Convert.ToString(dr["Value"]);

                        DataTable dtt = objKPI.GetGoal(int.Parse(VID), "KPI005", 0);
                        if (dtt.Rows.Count > 0)
                        {
                            nt.GOAL = dtt.Rows[0]["Goal"].ToString();
                        }
                        else
                        {
                            nt.GOAL = "0";
                        }
                        nt.AVERAGE = dtnodehierarchy.Tables[0].Compute("AVG(Value)", "").ToString();
                        DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                        if (dt.Rows.Count != 0)
                        {
                            nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                        }
                        else
                            nt.EEDI = "0";
                        NT.Add(nt);
                    }
                }
                else
                {
                    nt = new KPICO2();

                    nt.VALUE = "0.00";


                    if (dtnodehierarchy.Tables[1].Rows.Count > 0)
                    {
                        nt.GOAL = dtnodehierarchy.Tables[1].Rows[0]["Goal"].ToString();
                        nt.RDATE = dtnodehierarchy.Tables[1].Rows[2]["From_Date"].ToString();
                    }
                    else
                    {
                        nt.GOAL = "0";
                    }
                    nt.AVERAGE = dtnodehierarchy.Tables[0].Compute("AVG(Value)", "").ToString();
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                    nt = new KPICO2();

                    nt.VALUE = "0.00";


                    if (dtnodehierarchy.Tables[1].Rows.Count > 0)
                    {
                        nt.GOAL = dtnodehierarchy.Tables[1].Rows[0]["Goal"].ToString();
                        nt.RDATE = dtnodehierarchy.Tables[2].Rows[0]["To_Date"].ToString(); ;
                    }
                    else
                    {
                        nt.GOAL = "0";
                    }
                    nt.AVERAGE = dtnodehierarchy.Tables[0].Compute("AVG(Value)", "").ToString();
                    DataTable dte = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dte.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }



            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }





        public List<KPISOx> GetVoyageDataSOx(string VID, string Startdate, string EndDate, string telId1, string telId2)
        {
            List<KPISOx> NT = new List<KPISOx>();
            try
            {

                DataSet dtnodehierarchy = BLL_TMSA_PI.Search_Voyage_PI_ValuesSOX(int.Parse(VID), telId1, telId2);
                KPISOx nt;
                if (dtnodehierarchy.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Tables[0].Rows)
                    {
                        nt = new KPISOx();
                        nt.RDATE = Convert.ToString(dr["Record_Date"]);
                        nt.VALUE = Convert.ToString(dr["Value"]);
                        // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();
                        nt.AVERAGE = dtnodehierarchy.Tables[0].Compute("AVG(Value)", "").ToString();
                        DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                        if (dt.Rows.Count != 0)
                        {
                            nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                        }
                        else
                            nt.EEDI = "0";
                        NT.Add(nt);
                    }
                }
                else
                {
                    nt = new KPISOx();
                    // nt.RDATE = Startdate;
                    nt.VALUE = "0.00";
                    // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();
                    nt.AVERAGE = dtnodehierarchy.Tables[0].Compute("AVG(Value)", "").ToString();
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                    nt = new KPISOx();
                    //nt.RDATE = EndDate;
                    nt.VALUE = "0.00";
                    // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();
                    nt.AVERAGE = dtnodehierarchy.Tables[0].Compute("AVG(Value)", "").ToString();
                    DataTable dt1 = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt1.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt1.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }


        public List<KPINOx> GetVoyageDataNOx(string VID, string Startdate, string EndDate, string telId1, string telId2)
        {
            List<KPINOx> NT = new List<KPINOx>();
            try
            {

                DataTable dtnodehierarchy = BLL_TMSA_PI.Search_Voyage_PI_ValuesNOX(int.Parse(VID), telId1, telId2).Tables[0];
                KPINOx nt;
                if (dtnodehierarchy.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Rows)
                    {
                        nt = new KPINOx();
                        nt.RDATE = Convert.ToString(dr["Record_Date"]);
                        nt.VALUE = Convert.ToString(dr["Value"]);
                        // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();

                        nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                        DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                        if (dt.Rows.Count != 0)
                        {
                            nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                        }
                        else
                            nt.EEDI = "0";
                        NT.Add(nt);
                    }
                }
                else
                {
                    nt = new KPINOx();
                    //nt.RDATE = Startdate;
                    nt.VALUE = "0.00";
                    // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();

                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);

                    nt = new KPINOx();
                    //nt.RDATE = EndDate;
                    nt.VALUE = "0.00";
                    // nt.GOAL = BLL_TMSA_PI.Search_PI_Values(int.Parse(VID), int.Parse(KPI_ID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1].Rows[0]["Goal"].ToString();

                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    DataTable dt1 = objEEDIBLL.Get_EEDI(Convert.ToInt32(VID));
                    if (dt1.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt1.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        public List<KPICO2VOYAGE> GetTelDate(TDATE objTDATE)
        {

            List<KPICO2VOYAGE> NT = new List<KPICO2VOYAGE>();
            string fDate = "";
            string lDate = "";
            string[] id = Convert.ToString(objTDATE.TID).Trim(',').Split('-');
            var tempId = new List<string>();
            foreach (var s in id)
            {
                if (!string.IsNullOrEmpty(s))
                    tempId.Add(s);
            }
            id = tempId.ToArray(); int c = 0;
            double avg = 0;
            for (int x = 0; x < id.Length; x++)
            {

                c++;

                try
                {
                    DataTable dt1 = BLL_TMSA_PI.GetTelDate(id[x], Convert.ToInt32(objTDATE.VID)).Tables[0];

                    if (dt1.Rows.Count > 0)
                    {
                        fDate = dt1.Rows[0][0].ToString();
                        lDate = dt1.Rows[dt1.Rows.Count - 1][0].ToString();
                    }
                    KPICO2VOYAGE nt;

                    //foreach (DataRow dr in dtnodehierarchy.Rows)
                    //{
                    //    nt = new TDATE();
                    //    nt.TID = dr["Telegram_Date"].ToString();
                    //    NT.Add(nt);
                    //}
                    DataTable dtH = BLL_TMSA_PI.GetVoyageData(id[x], Convert.ToInt32(objTDATE.VID), Convert.ToInt32(objTDATE.KPID)).Tables[0];
                    foreach (DataRow dr in dtH.Rows)
                    {

                        nt = new KPICO2VOYAGE();
                        nt.EFROM = dr["EffectiveFrom"].ToString();
                        nt.ETO = dr["EffectiveTo"].ToString();
                        nt.FPORT = dr["FromPort"].ToString();
                        nt.TPORT = dr["ToPort"].ToString();
                        nt.VALUE = dr["Value"].ToString();
                        avg += Convert.ToDouble(dr["Value"].ToString());
                        nt.PORT = dr["FromPort"].ToString() + "-" + dr["ToPort"].ToString();
                        nt.AVERAGE = "0"; //(avg/c).ToString();
                        NT.Add(nt);
                    }
                    foreach (KPICO2VOYAGE n in NT)
                    {
                        n.AVERAGE = (avg / c).ToString();
                    }

                }

                catch (Exception ex)
                {
                    string s = ex.Message;
                }

            }
            return NT;
        }

        public List<KPISOxVOYAGE> GetTelDateSOx(TDATE objTDATE)
        {

            List<KPISOxVOYAGE> NT = new List<KPISOxVOYAGE>();
            string fDate = "";
            string lDate = "";
            string[] id = Convert.ToString(objTDATE.TID).Trim(',').Split('-');
            var tempId = new List<string>();
            foreach (var s in id)
            {
                if (!string.IsNullOrEmpty(s))
                    tempId.Add(s);
            }
            id = tempId.ToArray(); int c = 0;
            double avg = 0;
            for (int x = 0; x < id.Length; x++)
            {

                c++;

                try
                {
                    DataTable dt1 = BLL_TMSA_PI.GetTelDate(id[x], Convert.ToInt32(objTDATE.VID)).Tables[0];

                    if (dt1.Rows.Count > 0)
                    {
                        fDate = dt1.Rows[0][0].ToString();
                        lDate = dt1.Rows[dt1.Rows.Count - 1][0].ToString();
                    }
                    KPISOxVOYAGE nt;

                    //foreach (DataRow dr in dtnodehierarchy.Rows)
                    //{
                    //    nt = new TDATE();
                    //    nt.TID = dr["Telegram_Date"].ToString();
                    //    NT.Add(nt);
                    //}
                    DataTable dtH = BLL_TMSA_PI.GetVoyageDataSOx(id[x], Convert.ToInt32(objTDATE.VID)).Tables[0];
                    foreach (DataRow dr in dtH.Rows)
                    {

                        nt = new KPISOxVOYAGE();
                        nt.EFROM = dr["EffectiveFrom"].ToString();
                        nt.ETO = dr["EffectiveTo"].ToString();
                        nt.FPORT = dr["FromPort"].ToString();
                        nt.TPORT = dr["ToPort"].ToString();
                        nt.VALUE = dr["Value"].ToString();
                        avg += Convert.ToDouble(dr["Value"].ToString());
                        nt.PORT = dr["FromPort"].ToString() + "-" + dr["ToPort"].ToString();
                        nt.AVERAGE = "0"; //(avg/c).ToString();
                        NT.Add(nt);
                    }
                    foreach (KPISOxVOYAGE n in NT)
                    {
                        n.AVERAGE = (avg / c).ToString();
                    }

                }

                catch (Exception ex)
                {
                    string s = ex.Message;
                }

            }
            return NT;
        }

        public List<KPINOxVOYAGE> GetTelDateNOx(TDATE objTDATE)
        {

            List<KPINOxVOYAGE> NT = new List<KPINOxVOYAGE>();
            string fDate = "";
            string lDate = "";
            string[] id = Convert.ToString(objTDATE.TID).Trim(',').Split('-');
            var tempId = new List<string>();
            foreach (var s in id)
            {
                if (!string.IsNullOrEmpty(s))
                    tempId.Add(s);
            }
            id = tempId.ToArray(); int c = 0;
            double avg = 0;
            for (int x = 0; x < id.Length; x++)
            {

                c++;

                try
                {
                    DataTable dt1 = BLL_TMSA_PI.GetTelDate(id[x], Convert.ToInt32(objTDATE.VID)).Tables[0];

                    if (dt1.Rows.Count > 0)
                    {
                        fDate = dt1.Rows[0][0].ToString();
                        lDate = dt1.Rows[dt1.Rows.Count - 1][0].ToString();
                    }
                    KPINOxVOYAGE nt;

                    //foreach (DataRow dr in dtnodehierarchy.Rows)
                    //{
                    //    nt = new TDATE();
                    //    nt.TID = dr["Telegram_Date"].ToString();
                    //    NT.Add(nt);
                    //}
                    DataTable dtH = BLL_TMSA_PI.GetVoyageDataNOx(id[x], Convert.ToInt32(objTDATE.VID)).Tables[0];
                    foreach (DataRow dr in dtH.Rows)
                    {

                        nt = new KPINOxVOYAGE();
                        nt.EFROM = dr["EffectiveFrom"].ToString();
                        nt.ETO = dr["EffectiveTo"].ToString();
                        nt.FPORT = dr["FromPort"].ToString();
                        nt.TPORT = dr["ToPort"].ToString();
                        nt.VALUE = dr["Value"].ToString();
                        avg += Convert.ToDouble(dr["Value"].ToString());
                        nt.PORT = dr["FromPort"].ToString() + "-" + dr["ToPort"].ToString();
                        nt.AVERAGE = "0"; //(avg/c).ToString();
                        NT.Add(nt);
                    }
                    foreach (KPINOxVOYAGE n in NT)
                    {
                        n.AVERAGE = (avg / c).ToString();
                    }

                }

                catch (Exception ex)
                {
                    string s = ex.Message;
                }

            }
            return NT;
        }

        public List<KPICO2> GetMultipleVesselData(KPICO2 objCO2)
        {

            string VIDs;
            string Startdate;
            string EndDate;
            VIDs = objCO2.Vessel_Id;
            Startdate = objCO2.Start_date;
            EndDate = objCO2.End_date;
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("Vessel_Id");

            //VIDs = VIDs.TrimEnd(']');
            //VIDs = VIDs.TrimStart('[');

            string[] values = VIDs.Split(',');
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            {
                DataRow dr = dtVessel.NewRow();
                dr["Vessel_Id"] = values[i];
                dtVessel.Rows.Add(dr);
            }


            List<KPICO2> NT = new List<KPICO2>();
            try
            {

                DataTable dtnodehierarchy = objKPI.Get_CO2_Average(dtVessel, Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate));
                KPICO2 nt;
                foreach (DataRow dr in dtnodehierarchy.Rows)
                {
                    nt = new KPICO2();
                    nt.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                    nt.Vessel_Id = Convert.ToString(dr["Vessel_Id"]);
                    DataTable dtt = objKPI.GetGoal(Convert.ToInt32(Convert.ToString(dr["Vessel_Id"])), "KPI005", 0);
                    if (dtt.Rows.Count > 0)
                    {
                        nt.GOAL = dtt.Rows[0]["Goal"].ToString();
                    }
                    else
                    {
                        nt.GOAL = "0";
                    }
                    nt.AVERAGE = Convert.ToString(dr["Average"]);
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(Convert.ToString(dr["Vessel_Id"])));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        public List<KPISOx> GetMultipleVesselDataSOx(KPISOx objSOx)
        {

            string VIDs;
            string Startdate;
            string EndDate;
            VIDs = objSOx.Vessel_Id;
            Startdate = objSOx.Start_date;
            EndDate = objSOx.End_date;
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("Vessel_Id");

            //VIDs = VIDs.TrimEnd(']');
            //VIDs = VIDs.TrimStart('[');

            string[] values = VIDs.Split(',');
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            {
                DataRow dr = dtVessel.NewRow();
                dr["Vessel_Id"] = values[i];
                dtVessel.Rows.Add(dr);
            }


            List<KPISOx> NT = new List<KPISOx>();
            try
            {

                DataTable dtnodehierarchy = objKPI.Get_SOx_Average(dtVessel, Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[0];
                KPISOx nt;
                foreach (DataRow dr in dtnodehierarchy.Rows)
                {
                    nt = new KPISOx();
                    nt.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                    nt.Vessel_Id = Convert.ToString(dr["Vessel_Id"]);
                    nt.AVERAGE = Convert.ToString(dr["Average"]);
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(Convert.ToString(dr["Vessel_Id"])));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        public List<KPINOx> GetMultipleVesselDataNOx(KPINOx objNOx)
        {

            string VIDs;
            string Startdate;
            string EndDate;
            VIDs = objNOx.Vessel_Id;
            Startdate = objNOx.Start_date;
            EndDate = objNOx.End_date;
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("Vessel_Id");

            //VIDs = VIDs.TrimEnd(']');
            //VIDs = VIDs.TrimStart('[');

            string[] values = VIDs.Split(',');
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            {
                DataRow dr = dtVessel.NewRow();
                dr["Vessel_Id"] = values[i];
                dtVessel.Rows.Add(dr);
            }


            List<KPINOx> NT = new List<KPINOx>();
            try
            {

                DataTable dtnodehierarchy = objKPI.Get_NOx_Average(dtVessel, Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate));
                KPINOx nt;
                foreach (DataRow dr in dtnodehierarchy.Rows)
                {
                    nt = new KPINOx();
                    nt.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                    nt.Vessel_Id = Convert.ToString(dr["Vessel_Id"]);
                    nt.AVERAGE = Convert.ToString(dr["Average"]);
                    NT.Add(nt);
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        public List<KPISOx> GetMultipleVesselDataSOxInd(KPISOx objSOx)
        {

            string VIDs;
            string Startdate;
            string EndDate;
            VIDs = objSOx.Vessel_Id;
            Startdate = objSOx.Start_date;
            EndDate = objSOx.End_date;
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("Vessel_Id");

            //VIDs = VIDs.TrimEnd(']');
            //VIDs = VIDs.TrimStart('[');

            string[] values = VIDs.Split(',');
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            {
                DataRow dr = dtVessel.NewRow();
                dr["Vessel_Id"] = values[i];
                dtVessel.Rows.Add(dr);
            }


            List<KPISOx> NT = new List<KPISOx>();
            try
            {

                DataTable dtnodehierarchy = objKPI.Get_SOx_Average(dtVessel, Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[1];
                KPISOx nt;
                foreach (DataRow dr in dtnodehierarchy.Rows)
                {
                    nt = new KPISOx();
                    nt.RDATE = Convert.ToString(dr["Record_Date"]);
                    nt.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                    nt.Vessel_Id = Convert.ToString(dr["Vessel_Id"]);
                    nt.AVERAGE = Convert.ToString(dr["Average"]);
                    DataTable dt = objEEDIBLL.Get_EEDI(Convert.ToInt32(Convert.ToString(dr["Vessel_Id"])));
                    if (dt.Rows.Count != 0)
                    {
                        nt.EEDI = Math.Round(Convert.ToDecimal(dt.Rows[0]["EEDI_Value"].ToString()), 2).ToString();
                    }
                    else
                        nt.EEDI = "0";
                    NT.Add(nt);
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }


        #region --Generic KPI --



        public List<KPIGeneric> GetMultipleGenericVesselData(KPIGeneric oKPIGeneric)
        {
            //string VID, string KPI_ID, string Interval, string Value_Type, string Startdate, string EndDate
            List<KPIGeneric> NT = new List<KPIGeneric>();

            string VIDs;
            string Startdate;
            string EndDate;
            string KPI_ID;
            string Value_Type;
            string Interval;
            VIDs = oKPIGeneric.Vessel_Id;
            Startdate = oKPIGeneric.Start_date;
            EndDate = oKPIGeneric.End_date;
            KPI_ID = oKPIGeneric.KID;
            Value_Type = oKPIGeneric.Value_Type;
            Interval = oKPIGeneric.Interval;
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("Vessel_Id");
            string[] values = VIDs.Split(',');
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            {
                DataRow dr = dtVessel.NewRow();
                dr["Vessel_Id"] = values[i];
                dtVessel.Rows.Add(dr);
            }

            try
            {

                DataTable dtnodehierarchy = objKPI.Get_Multiple_Generic_Values(dtVessel, int.Parse(KPI_ID), Interval, Value_Type, Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[0];
                KPIGeneric nt;



                if (dtnodehierarchy.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Rows)
                    {
                        nt = new KPIGeneric();
                        nt.Vessel_Id = Convert.ToString(dr["Vessel_ID"]);
                        nt.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);
                        //nt.RDATE = Convert.ToString(dr["Record_Date"]);
                        nt.VALUE = Convert.ToString(dr["Value"]);
                        NT.Add(nt);
                    }
                }
                else
                {
                    nt = new KPIGeneric();
                    nt.RDATE = Startdate;
                    nt.VALUE = "0.00";


                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    nt.RDATE = EndDate;
                    nt.VALUE = "0.00";


                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();

                    NT.Add(nt);
                }



            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        public List<KPIGeneric> GetGenericData(string VID, string KPI_ID, string Interval, string Value_Type, string Startdate, string EndDate)
        {
            List<KPIGeneric> NT = new List<KPIGeneric>();
            try
            {

                DataTable dtnodehierarchy = objKPI.Get_Vessel_KPI_Values(int.Parse(VID), int.Parse(KPI_ID), Interval, Value_Type, Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[0];
                KPIGeneric nt;

                if (dtnodehierarchy.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Rows)
                    {
                        nt = new KPIGeneric();
                        nt.RDATE = Convert.ToString(dr["Record_Date"]);
                        nt.VALUE = Convert.ToString(dr["Value"]);
                        NT.Add(nt);
                    }
                }
                else
                {
                    nt = new KPIGeneric();
                    nt.RDATE = Startdate;
                    nt.VALUE = "0.00";


                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();
                    nt.RDATE = EndDate;
                    nt.VALUE = "0.00";


                    nt.AVERAGE = dtnodehierarchy.Compute("AVG(Value)", "").ToString();

                    NT.Add(nt);
                }



            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }



        public List<KPIGeneric> GetGenericVoyageData(KPIGeneric objKPIGeneric)
        {

            List<KPIGeneric> NT = new List<KPIGeneric>();
            string fDate = "";
            string lDate = "";
            string[] id = Convert.ToString(objKPIGeneric.TID).Trim(',').Split('-');
            var tempId = new List<string>();
            foreach (var s in id)
            {
                if (!string.IsNullOrEmpty(s))
                    tempId.Add(s);
            }
            id = tempId.ToArray(); int c = 0;
            double avg = 0;
            for (int x = 0; x < id.Length; x++)
            {

                c++;

                try
                {
                    DataTable dt1 = BLL_TMSA_PI.GetTelDate(id[x], Convert.ToInt32(objKPIGeneric.Vessel_Id)).Tables[0];

                    if (dt1.Rows.Count > 0)
                    {
                        fDate = dt1.Rows[0][0].ToString();
                        lDate = dt1.Rows[dt1.Rows.Count - 1][0].ToString();
                    }
                    KPIGeneric nt;

                    //foreach (DataRow dr in dtnodehierarchy.Rows)
                    //{
                    //    nt = new TDATE();
                    //    nt.TID = dr["Telegram_Date"].ToString();
                    //    NT.Add(nt);
                    //}
                    DataTable dtH = objKPI.GetVoyageGenericData(id[x], Convert.ToInt32(objKPIGeneric.Vessel_Id), Convert.ToInt32(objKPIGeneric.KID), Convert.ToDateTime(fDate), Convert.ToDateTime(lDate)).Tables[0];
                    foreach (DataRow dr in dtH.Rows)
                    {

                        nt = new KPIGeneric();
                        nt.EFROM = dr["EffectiveFrom"].ToString();
                        nt.ETO = dr["EffectiveTo"].ToString();
                        nt.FPORT = dr["FromPort"].ToString();
                        nt.TPORT = dr["ToPort"].ToString();
                        nt.VALUE = dr["Value"].ToString();
                        avg += Convert.ToDouble(dr["Value"].ToString());
                        nt.PORT = dr["FromPort"].ToString() + "-" + dr["ToPort"].ToString();
                        nt.AVERAGE = "0"; //(avg/c).ToString();
                        NT.Add(nt);
                    }
                    foreach (KPIGeneric n in NT)
                    {
                        n.AVERAGE = (avg / c).ToString();
                    }

                }

                catch (Exception ex)
                {
                    string s = ex.Message;
                }

            }
            return NT;
        }

        #endregion



        #region --Crew Retention--

        /// <summary>
        /// Description: Method to search crew retention rate 
        /// Created By: Bhairab
        /// Created On: 30/05/2016
        /// </summary>

        /// <param name="Crewretention obj"> For particular category retention rate, category will be send a as parameter</param>
        public List<CrewRetention> GetRetentionData(CrewRetention obj)
        {
            string RankIDs = obj.Rank;
            string Year = obj.Year;
            string Category = obj.Category;

            List<CrewRetention> NT = new List<CrewRetention>();
            try
            {
                int iCategory = Convert.ToInt16(Category);
                if (RankIDs == "0")
                    RankIDs = "";
                DataTable dtRetention = objKPI.Search_CrewRetention(RankIDs, Year, iCategory).Tables[0];
                CrewRetention nt;

                if (dtRetention.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRetention.Rows)
                    {
                        nt = new CrewRetention();
                        nt.Quarter = Convert.ToString(dr["Qtr"]);
                        nt.Value = Convert.ToString(dr["KPI_Value"]);
                        nt.Start_date = dr["Qtr_Start"].ToString();
                        nt.End_date = dr["Qtr_End"].ToString();
                        nt.AVERAGE = Convert.ToString(dr["AvgAvailable"]);
                        nt.NTBR = Convert.ToString(dr["NTBR"]);
                        nt.LeftAll = Convert.ToString(dr["LeftAll"]);

                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }


        #endregion


        #region PMSOverdue
        /// <summary>
        /// Description:Method to get vessel wise PMS overdue rate for Critical and non critical jobs
        /// </summary>
        /// <param name="VID"></param>
        /// <param name="Startdate"></param>
        /// <param name="EndDate"></param>
        /// <returns>List of PMS overdue object </returns>


        public List<PMSOverdue> GetPMSOverDueByVessel(string VID, string Startdate, string EndDate)
        {
            List<PMSOverdue> NT = new List<PMSOverdue>();
            try
            {

                DataTable dtnodehierarchy = objKPI.Get_PMS_OverDue_ByVessel(int.Parse(VID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[0];
                PMSOverdue nt;

                if (dtnodehierarchy.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtnodehierarchy.Rows)
                    {
                        nt = new PMSOverdue();
                        nt.MonthYear = Convert.ToString(dr["MonthYear"]);
                        nt.CriticalOverdue = Convert.ToString(dr["CriticalOverdue"]);
                        nt.NonCriticalOverdue = Convert.ToString(dr["NonCriticalOverdue"]);
                        nt.AllOverdue = Convert.ToString(dr["AllOverdue"]);
                        NT.Add(nt);
                    }
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

            return NT;

        }

        /// <summary>
        /// Method to get critical aor non critical jobs overdue rate
        /// </summary>
        /// <param name="KPIID"></param>
        /// <returns></returns>

        public List<PMSOverdue> GetMultipleVesselPMSOverdue(PMSOverdue objPMS)
        {
            string VIDs;
            string EndDate;
            VIDs = objPMS.VesselIDs;
            EndDate = objPMS.End_date;
            DataTable dtVessel = new DataTable();
            dtVessel.Columns.Add("Vessel_Id");
            string[] values = VIDs.Split(',');
            int cnt = values.Length;
            for (int i = 0; i < cnt; i++)
            {
                DataRow dr = dtVessel.NewRow();
                dr["Vessel_Id"] = values[i];
                dtVessel.Rows.Add(dr);
            }
            List<PMSOverdue> NT = new List<PMSOverdue>();
            PMSOverdue nt;
            try
            {
                DataTable dtnodehierarchy = objKPI.Get_PM_OverdueLastMonth(dtVessel, Convert.ToDateTime(EndDate)).Tables[0];
                string[] Pkey_cols = new string[] { "VesselID" };
                string[] Hide_cols = new string[] { "ID", "VesselID" };
                DataTable dt1 = objKPI.PivotTable("KPI", "Value", "", Pkey_cols, Hide_cols, dtnodehierarchy);


                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        nt = new PMSOverdue();
                        nt.Vessel = Convert.ToString(dr["Vessel"]);
                        nt.CriticalOverdue = Convert.ToString(dr["Critical"]);
                        nt.NonCriticalOverdue = Convert.ToString(dr["Non Critical"]);
                        nt.AllOverdue = Convert.ToString(dr["AllOverdue"]);
                        NT.Add(nt);
                    }
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

            return NT;
        }


        #endregion



        /// <summary>
        /// Description: Method to fetch Rank list
        /// Created By: Krishnapriya
        /// Created On: 23/11/2016
        /// </summary>
        public List<KPIRank> GetRankData()
        {
            List<KPIRank> NT = new List<KPIRank>();
            try
            {
                DataTable dt = objKPI.Get_RankList();
                KPIRank nt;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nt = new KPIRank();
                        nt.RANKID = Convert.ToString(dr["ID"]);
                        nt.RANKNAME = Convert.ToString(dr["Rank_Short_Name"]);
                        NT.Add(nt);
                    }
                }

            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;
        }

        /// <summary>
        /// Description: Method to fetch year list
        /// Created By: Krishnapriya
        /// Created On: 23/11/2016
        /// </summary>

        public List<KPIYear> GetYearData()
        {
            List<KPIYear> NT = new List<KPIYear>();
            try
            {
                KPIYear nt;
                DataTable dt = new DataTable();
                int CurrentYear = DateTime.Now.Year;
                int count = 0;
                dt.Columns.Add("Year");
                for (count = CurrentYear; count >= CurrentYear - 10; count--)
                {
                    nt = new KPIYear();
                    nt.YEAR = count.ToString();
                    NT.Add(nt);
                }

            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return NT;
        }

        #region --TMSA Report--

        /// <summary>
        /// Description: Method to get Overall TMSA Report 
        /// Created By: Gargi
        /// Created On: 28/09/2016
        /// </summary>

        public List<REPORT> GetOverallReport(REPORT obj)
        {
            string ElementIDs = obj.ElementID;
            string StageIDs = obj.StageID;
            string LevelIDs = obj.LevelNo;
            string Role = obj.Role;
            string VersionNo = obj.VersionNo;

            List<REPORT> NT = new List<REPORT>();
            try
            {
                int iVersionNo = Convert.ToInt16(VersionNo);

                DataTable dtOverallReport = objREPORT.Search_OverallReport(ElementIDs, StageIDs, LevelIDs, Role, iVersionNo).Tables[0];
                REPORT nt;

                if (dtOverallReport.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtOverallReport.Rows)
                    {
                        nt = new REPORT();
                        nt.ID = Convert.ToString(dr["ID"]);
                        nt.ParentID = Convert.ToString(dr["Parent_ID"]);
                        nt.ElementCode = Convert.ToString(dr["Element_Code"]);
                        nt.StageCode = Convert.ToString(dr["Stage_Code"]);
                        nt.KpiTmsa = dr["KPI_TMSA"].ToString();
                        nt.BestPractices = dr["Best_Practices"].ToString();
                        nt.AuditedProcess = dr["Audited_Process"].ToString();
                        nt.Procedure = dr["Procedure"].ToString();
                        nt.Module = dr["Module"].ToString();
                        nt.KPI = dr["KPI"].ToString();
                        nt.Compliance = dr["Compliance"].ToString();
                        nt.Notes = dr["Notes"].ToString();
                        nt.Edit = dr["Edit"].ToString();

                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        /// <summary>
        /// Description: Method to get Elements 
        /// Created By: Gargi
        /// Created On: 28/09/2016
        /// </summary>
        public List<ELEMENTDATA> GetElementData(string VID)
        {

            List<ELEMENTDATA> NT = new List<ELEMENTDATA>();
            try
            {
                DataTable dt = objREPORT.Get_ElementList(int.Parse(VID));
                ELEMENTDATA nt;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nt = new ELEMENTDATA();
                        nt.ELEMENTID = Convert.ToString(dr["ID"]);
                        nt.ELEMENTCODE = Convert.ToString(dr["Element_Code"]);

                        NT.Add(nt);
                    }
                }
                else
                {
                    //write logic for else
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        /// <summary>
        /// Description: Method to get Stages 
        /// Created By: Gargi
        /// Created On: 28/09/2016
        /// </summary>
        public List<STAGEDATA> GetStageData(string VID)
        {



            List<STAGEDATA> NT = new List<STAGEDATA>();
            try
            {
                DataTable dt = objREPORT.Get_StageList(int.Parse(VID));
                STAGEDATA nt;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nt = new STAGEDATA();
                        nt.STAGEID = Convert.ToString(dr["Stage_ID"]);
                        nt.STAGECODE = Convert.ToString(dr["Stage_Code"]);

                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        /// <summary>
        /// Description: Method to get Levels 
        /// Created By: Gargi
        /// Created On: 28/09/2016
        /// </summary>
        public List<LEVELDATA> GetLevelData(string VID)
        {



            List<LEVELDATA> NT = new List<LEVELDATA>();
            try
            {
                DataTable dt = objREPORT.Get_LevelList(int.Parse(VID));
                LEVELDATA nt;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nt = new LEVELDATA();
                        nt.LEVELID = Convert.ToString(dr["Level_ID"]);
                        nt.LEVELNO = Convert.ToString(dr["Level_No"]);

                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        /// <summary>
        /// Description: Method to get Versions 
        /// Created By: Gargi
        /// Created On: 28/09/2016
        /// </summary>
        /// 
        public List<VERSIONDATA> GetVersionData()
        {



            List<VERSIONDATA> NT = new List<VERSIONDATA>();
            try
            {
                DataTable dt = objREPORT.Get_VersionList();
                VERSIONDATA nt;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nt = new VERSIONDATA();
                        nt.VERSIONNO = Convert.ToString(dr["Version_No"]);
                        nt.VERSIONNAME = Convert.ToString(dr["Version_Name"]);
                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }


        public List<KPIURL> LinkCount(KPIURL obj)
        {
            string ParentID = obj.ParentID;
            string LinkType = obj.LinkType;

            int iParentID = Convert.ToInt16(ParentID);

            List<KPIURL> NT = new List<KPIURL>();
            try
            {
                DataTable dt = objREPORT.GetCount(iParentID, LinkType).Tables[0];
                KPIURL nt;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nt = new KPIURL();
                        nt.Count = Convert.ToInt16(dr["LinkCount"]);

                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }

        public List<REPORT> LinkExists(REPORT obj)
        {
            string ParentID = obj.ParentID;
            string LinkType = obj.LinkType;
            string LinkID = obj.LinkID;
            string DPath = obj.DocPath;
            string Notes = obj.Notes;

            int iParentID = Convert.ToInt16(ParentID);

            List<REPORT> NT = new List<REPORT>();
            try
            {
                DataTable dt = objREPORT.LinkExists(iParentID, LinkType, LinkID, DPath, Notes).Tables[0];
                REPORT nt;

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        nt = new REPORT();
                        nt.LinkExists = Convert.ToString(dr["LinkExists"]);

                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;

        }


        public int SaveData(REPORT obj)
        {

            string ParentID = obj.ParentID;
            string LinkType = obj.LinkType;
            string LinkID = obj.LinkID;
            string DPath = obj.DocPath;
            string Notes = obj.Notes;



            int iParentID = Convert.ToInt16(ParentID);


            try
            {
                return objREPORT.SaveData(iParentID, LinkType, LinkID, DPath, Notes);


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public int UpdateData(REPORT obj)
        {

            string ID = obj.ID;
            string ParentID = obj.ParentID;
            string AuditedProcess = obj.AuditedProcess;
            string Compliance = obj.Compliance;

            if (Compliance == "")
            {
                Compliance = "3";
            }

            int iID = Convert.ToInt16(ID);
            int iParentID = Convert.ToInt16(ParentID);
            int iCompliance = Convert.ToInt16(Compliance);


            try
            {
                return objREPORT.UpdateData(iID, iParentID, AuditedProcess, iCompliance);


            }
            catch (Exception ex)
            {
                throw;
            }

        }



        public int DeleteData(REPORT obj)
        {

            string LinkID = obj.LinkID;


            int iLinkID = Convert.ToInt16(LinkID);


            try
            {
                return objREPORT.DeleteData(iLinkID);


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public List<REPORT> Get_PieChartData_DL(string VID, string VNO)
        {

            List<REPORT> NT = new List<REPORT>();

            try
            {
                DataTable dtPieChartData = objREPORT.Get_PieChartData(int.Parse(VNO), int.Parse(VID));
                REPORT nt;

                if (dtPieChartData.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPieChartData.Rows)
                    {
                        nt = new REPORT();
                        nt.Value = Convert.ToString(dr["Value"]);
                        nt.Compliance = Convert.ToString(dr["Compliance"]);
                        NT.Add(nt);
                    }
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return NT;
        }


        #endregion

        #region Worklist Reports

        /// <summary>
        /// Method to get yearly worklist by TYPE
        /// </summary>
        /// <param name="KPIID"></param>
        /// <returns></returns>

        public Stream GetMultipleVesselWorkList(WorkList objWL)
        {
            string Type;

            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            WorkList nt;
            decimal avgValue = 0;
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetYearWiseWorklistCount(VesselIDs, Years, Type).Tables[0];
                string[] Pkey_cols = new string[] { "Vessel_ID" };
                string[] Hide_cols = new string[] { "ID", "Vessel_ID", "Year" };
                DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                DataRow toInsert = dt1.NewRow();
                toInsert[0] = "AVG";
                dt1.Rows.Add(toInsert);
                int sum = 0;

                foreach (DataRow row in dt1.Rows)
                {
                    if (row[0].ToString() == "AVG")
                    {
                        foreach (DataColumn col in dt1.Columns)
                        {

                            if (col.ColumnName != "VESSEL_Name")
                            {
                                avgValue = dt1.Select().Where(p => p[col.ColumnName] != DBNull.Value).Select(c => Convert.ToDecimal(c[col.ColumnName])).Average();
                                avgValue = Decimal.Round(avgValue, 2);
                                row[col] = avgValue;
                            }
                        }
                    }

                    dt1.AcceptChanges();

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }




            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType =
        "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }


        /// <summary>
        /// Method to get yearly worklist by TYPE
        /// </summary>
        /// <param name="KPIID"></param>
        /// <returns></returns>

        public Stream GetMultipleVesselYearWiseWorkList(WorkList objWL)
        {
            string Type;


            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            WorkList nt;
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetYearWiseWorklistCount(VesselIDs, Years, Type).Tables[0];
                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID", "Vessel_ID", "VESSEL_Name" };
                DataTable dt1 = objKPI.PivotTable("VESSEL_Name", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
                dt1.Columns.Add("AVG");

                foreach (DataRow row in dt1.Rows)
                {
                    decimal NoOfVessel = 0;
                    decimal avgValue = 0;
                    decimal totalValue = 0;
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (col.ColumnName != "YEAR" && col.ColumnName != "AVG")
                        {
                            NoOfVessel = NoOfVessel + 1;
                            totalValue = totalValue + Convert.ToInt32(row[col.ColumnName]);
                        }

                    }

                    avgValue = totalValue / NoOfVessel;
                    avgValue = Decimal.Round(avgValue, 2);

                    row["AVG"] = avgValue;
                    row.AcceptChanges();
                }

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }




            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType =
        "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }








        /// <summary>
        /// Method to get monthly worklist by TYPE
        /// </summary>
        /// <param name="KPIID"></param>
        /// <returns></returns>

        public Stream GetMultipleVesselWorkListMonthly(WorkList objWL)
        {
            string Type;
            decimal avgValue = 0;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            int Year = Convert.ToInt32(objWL.YEAR);
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            WorkList nt;
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetMonthlyWorklistCountByVessel(VesselIDs, Year, Type).Tables[0];
                string[] Pkey_cols = new string[] { "Vessel_ID" };
                string[] Hide_cols = new string[] { "ID", "Vessel_ID", "MONTH" };
                DataTable dt1 = objKPI.PivotTable("MONTH", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                DataRow toInsert = dt1.NewRow();
                toInsert[0] = "AVG";
                dt1.Rows.Add(toInsert);
                int sum = 0;

                foreach (DataRow row in dt1.Rows)
                {
                    if (row[0].ToString() == "AVG")
                    {
                        foreach (DataColumn col in dt1.Columns)
                        {

                            if (col.ColumnName != "VESSEL_Name")
                            {
                                avgValue = dt1.Select().Where(p => p[col.ColumnName] != DBNull.Value).Select(c => Convert.ToDecimal(c[col.ColumnName])).Average();
                                avgValue = Decimal.Round(avgValue, 2);
                                row[col] = avgValue;
                            }
                        }
                    }

                    dt1.AcceptChanges();
                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }




            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType =
        "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }



        /// <summary>
        /// Method to get monthly worklist by TYPE
        /// </summary>
        /// <param name="KPIID"></param>
        /// <returns></returns>

        public Stream GetMonthlyWorklistCountByVessel(WorkList objWL)
        {
            string Type;

            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            int Year = Convert.ToInt32(objWL.YEAR);
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            WorkList nt;
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetMonthlyWorklistCountByVessel(VesselIDs, Year, Type).Tables[0];
                string[] Pkey_cols = new string[] { "MONTH" };
                string[] Hide_cols = new string[] { "ID", "Vessel_ID", "VESSEL_Name" };
                DataTable dt1 = objKPI.PivotTable("VESSEL_Name", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                dt1.Columns.Add("AVG");

                foreach (DataRow row in dt1.Rows)
                {
                    decimal NoOfVessel = 0;
                    decimal avgValue = 0;
                    decimal totalValue = 0;
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (col.ColumnName != "MONTH" && col.ColumnName != "AVG")
                        {
                            NoOfVessel = NoOfVessel + 1;
                            totalValue = totalValue + Convert.ToInt32(row[col.ColumnName]);
                        }

                    }

                    avgValue = totalValue / NoOfVessel;
                    avgValue = Decimal.Round(avgValue, 2);

                    row["AVG"] = avgValue;
                    row.AcceptChanges();
                }


                foreach (DataRow row in dt1.Rows)
                {
                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType =
        "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }






        #endregion
        /// <summary>
        /// Method to get vessel list by user company
        /// </summary>
        /// <param name="UserCompanyID">user company</param>
        /// <returns></returns>
        public List<WorkList> Get_VesselList(string UserCompanyID)
        {
            List<WorkList> Vsl = new List<WorkList>();
            try
            {
                int iCompanyID = Convert.ToInt16(UserCompanyID);
                DataSet dsData = BLL_TMSA_PI.Get_All_Vessels(iCompanyID);
                DataTable dtvessel = dsData.Tables[0];
                foreach (DataRow dr in dtvessel.Rows)
                {
                    WorkList ov = new WorkList();
                    ov.Vessel_Id = Convert.ToString(dr["Vessel_ID"]);

                    ov.Vessel_Name = Convert.ToString(dr["Vessel_Name"]);


                    Vsl.Add(ov);

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }

            return Vsl;
        }


        #region Near Misses Reports

        /// <summary>
        /// Method to get yearly Near Misses count Vessel wise and year wise to bind jqxgrid control
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public Stream GetVesselCountNearMisses(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            decimal avgValue = 0;
            decimal totalValue = 0;

            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetVesselCountNearMisses(VesselIDs, Years, Type).Tables[0];
                string[] Pkey_cols = new string[] { "Vessel_ID" };
                string[] Hide_cols = new string[] { "ID", "Vessel_ID", "Year" };
                DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                DataRow totalInsert = dt1.NewRow();
                totalInsert[0] = "TOTAL";
                dt1.Rows.Add(totalInsert);

                DataRow toInsert = dt1.NewRow();
                toInsert[0] = "AVG";
                dt1.Rows.Add(toInsert);


                foreach (DataRow row in dt1.Rows)
                {
                    if (row[0].ToString() == "TOTAL")
                    {
                        foreach (DataColumn col in dt1.Columns)
                        {
                            if (col.ColumnName != "VESSEL_Name")
                            {
                                totalValue = dt1.Select().Where(p => p[col.ColumnName] != DBNull.Value).Select(c => Convert.ToDecimal(c[col.ColumnName])).Sum();
                                row[col] = totalValue;

                            }
                        }
                    }
                }


                foreach (DataRow row in dt1.Rows)
                {
                    if (row[0].ToString() == "AVG")
                    {
                        foreach (DataColumn col in dt1.Columns)
                        {
                            if (col.ColumnName != "VESSEL_Name")
                            {
                                avgValue = dt1.Select("VESSEL_Name <> 'TOTAL'").Where(p => p[col.ColumnName] != DBNull.Value).Select(c => Convert.ToDecimal(c[col.ColumnName])).Average();
                                row[col] = Decimal.Round(avgValue, 2);

                            }
                        }
                    }


                    dt1.AcceptChanges();

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }



        /// <summary>
        /// Method to get Near Misses for vessels to bind jqx chart control
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public Stream GetChartVesselCountNearMisses(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetVesselCountNearMisses(VesselIDs, Years, Type).Tables[0];
                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID", "Vessel_ID", "VESSEL_Name" };
                DataTable dt1 = objKPI.PivotTable("VESSEL_Name", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
                dt1.Columns.Add("Avg");

                foreach (DataRow row in dt1.Rows)
                {
                    decimal NoOfVessel = 0;
                    decimal avgValue = 0;
                    decimal totalValue = 0;
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (col.ColumnName != "YEAR" && col.ColumnName != "Avg" && col.ColumnName != "Total")
                        {
                            NoOfVessel = NoOfVessel + 1;
                            totalValue = totalValue + Convert.ToInt32(row[col.ColumnName]);
                        }

                    }

                    avgValue = Convert.ToDecimal(totalValue / NoOfVessel);
                    avgValue = Decimal.Round(avgValue, 2);

                    row["Avg"] = avgValue;
                    row.AcceptChanges();
                }

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType =
        "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }


        /// <summary>
        /// Method to get Near Misses per vessel for a particular year to bind pie chart control
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public List<WorkList> GetPerVesselPerYearNearMissesForPieChart(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetVesselCountNearMisses(VesselIDs, Years, Type).Tables[0];
                WorkList nt;

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        nt = new WorkList();
                        nt.Vessel_Name = Convert.ToString(dr["VESSEL_Name"]);
                        nt.VALUE = Convert.ToString(dr["Rec_Count"]);
                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return NT;
        }


        /// <summary>
        /// Method to get Total Near Misses Per Year for all the vessels to bind pie chart control
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public List<WorkList> GetPerYearNearMissesForPieChart(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetVesselCountNearMisses(VesselIDs, Years, Type).Tables[1];
                WorkList nt;

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        nt = new WorkList();
                        nt.YEAR = Convert.ToString(dr["Year"]);
                        nt.VALUE = Convert.ToString(dr["Rec_Count"]);
                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return NT;
        }


        #endregion

        #region Incidents reports

        /// <summary>
        /// Method to get per year Incident count for all Category for all vessels to bind pie chart control
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public List<WorkList> GetCategoryIncidentCountForPieChart(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetIncidentCount(VesselIDs, Years, Type).Tables[0];
                WorkList nt;

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        nt = new WorkList();
                        nt.YEAR = Convert.ToString(dr["Name"]);
                        nt.VALUE = Convert.ToString(dr["Rec_Count"]);
                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return NT;
        }

        /// <summary>
        /// Method to get Incident count for all vessels per year to bind jqx grid/chart
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public Stream GetMultipleVesselIncidentCount(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            decimal avgValue = 0;
            decimal totalValue = 0;
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetMultipleVesselIncidentCount(VesselIDs, Years, Type).Tables[0];

                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID", "Vessel" };
                DataTable dt1 = objKPI.PivotTable("Vessel", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
                dt1.Columns.Add("TOTAL");
                dt1.Columns.Add("AVG");

                foreach (DataRow row in dt1.Rows)
                {
                    totalValue = 0;
                    int NoOfVessel = 0;
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (col.ColumnName != "YEAR" && col.ColumnName != "AVG" && col.ColumnName != "TOTAL")
                        {
                            NoOfVessel = NoOfVessel + 1;
                            totalValue = totalValue + Convert.ToInt32(row[col.ColumnName]);
                        }

                    }

                    avgValue = totalValue / NoOfVessel;
                    avgValue = Decimal.Round(avgValue, 2);
                    row["TOTAL"] = totalValue;
                    row["AVG"] = avgValue;
                    row.AcceptChanges();

                    dt1.AcceptChanges();
                }
                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }


        /// <summary>
        /// Method to get Incident count for all vessels per year to bind jqx grid/chart
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public Stream GetCategoryIncidentCount(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            decimal avgValue = 0;
            decimal totalValue = 0;
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetCategoryIncidentCount(VesselIDs, Years, Type).Tables[0];

                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID", "Vessel" };
                DataTable dt1 = objKPI.PivotTable("Vessel", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
                dt1.Columns.Add("Total");
                dt1.Columns.Add("Avg");

                foreach (DataRow row in dt1.Rows)
                {
                    totalValue = 0;
                    int NoOfVessel = 0;
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (col.ColumnName != "Year" && col.ColumnName != "Avg" && col.ColumnName != "Total")
                        {
                            NoOfVessel = NoOfVessel + 1;
                            totalValue = totalValue + Convert.ToInt32(row[col.ColumnName]);
                        }

                    }

                    avgValue = totalValue / NoOfVessel;
                    avgValue = Decimal.Round(avgValue, 2);


                    row["Total"] = totalValue;

                    row["Avg"] = avgValue;

                    row.AcceptChanges();

                    dt1.AcceptChanges();
                }
                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }
        #endregion


        #region Injury/Death Incident reports
        /// <summary>
        /// Method to get per year Injury/Death Incident count for all Category for all vessels to bind pie chart control
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public List<WorkList> GetInjuryIncidentCountForPieChart(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            string SubType = objWL.SubType;
            List<WorkList> NT = new List<WorkList>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetInjuryIncidentCount(VesselIDs, Years, Type, SubType).Tables[0];
                WorkList nt;

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        nt = new WorkList();
                        nt.YEAR = Convert.ToString(dr["Name"]);
                        nt.VALUE = Convert.ToString(dr["Rec_Count"]);
                        NT.Add(nt);
                    }
                }


            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return NT;
        }

        /// <summary>
        /// Method to get Injury/death Incident count for all vessels per year to bind jqx grid/chart
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public Stream GetCategoryInjuryIncidentCount(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            string SubType = objWL.SubType;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            decimal avgValue = 0;
            decimal totalValue = 0;
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetCategoryInjuryIncidentCount(VesselIDs, Years, Type, SubType).Tables[0];

                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID", "Vessel" };
                DataTable dt1 = objKPI.PivotTable("Vessel", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
                dt1.Columns.Add("Total");
                dt1.Columns.Add("Avg");

                foreach (DataRow row in dt1.Rows)
                {
                    totalValue = 0;
                    int NoOfVessel = 0;
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (col.ColumnName != "Year" && col.ColumnName != "Avg" && col.ColumnName != "Total")
                        {
                            NoOfVessel = NoOfVessel + 1;
                            totalValue = totalValue + Convert.ToInt32(row[col.ColumnName]);
                        }

                    }

                    avgValue = totalValue / NoOfVessel;
                    avgValue = Decimal.Round(avgValue, 2);


                    row["Total"] = totalValue;

                    row["Avg"] = avgValue;

                    row.AcceptChanges();

                    dt1.AcceptChanges();
                }
                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }


        /// <summary>
        /// Method to get Injury/death Incident count for all vessels per year to bind jqx grid/chart
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public Stream GetMultipleVesselInjuryIncidentCount(WorkList objWL)
        {
            string Type;
            Type = objWL.Type;
            string VesselIDs = objWL.Vessel_IDs;
            string Years = objWL.Years;
            string SubType = objWL.SubType;
            List<WorkList> NT = new List<WorkList>();
            var list = new List<Dictionary<string, object>>();
            decimal avgValue = 0;
            decimal totalValue = 0;
            try
            {
                DataTable dtRawdata;
                dtRawdata = objWorklist.GetMultipleVesselInjuryIncidentCount(VesselIDs, Years, Type, SubType).Tables[0];

                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID", "Vessel" };
                DataTable dt1 = objKPI.PivotTable("Vessel", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
                dt1.Columns.Add("TOTAL");
                dt1.Columns.Add("AVG");

                foreach (DataRow row in dt1.Rows)
                {
                    totalValue = 0;
                    int NoOfVessel = 0;
                    foreach (DataColumn col in dt1.Columns)
                    {
                        if (col.ColumnName == "YEAR" || col.ColumnName == "AVG" || col.ColumnName == "TOTAL")
                        {
                            totalValue = totalValue;
                        }
                        else
                        {

                            NoOfVessel = NoOfVessel + 1;
                            totalValue = totalValue + Convert.ToInt32(row[col.ColumnName]);
                        }
                    }

                    avgValue = totalValue / NoOfVessel;
                    avgValue = Decimal.Round(avgValue, 2);
                    row["TOTAL"] = totalValue;
                    row["AVG"] = avgValue;
                    row.AcceptChanges();

                    dt1.AcceptChanges();
                }
                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }

        #endregion

        /// <summary>
        /// Description: Method to fetch year 
        /// Created By: Krishnapriya
        /// Created On: 11/01/2017
        /// </summary>
        /// <param name="noOfYears">It denotes how many years should be returned</param>

        public List<KPIYear> GetYears(string NumOfYears)
        {
            List<KPIYear> NT = new List<KPIYear>();
            try
            {
                KPIYear nt;
                DataTable dt = new DataTable();
                int CurrentYear = DateTime.Now.Year;
                int count = 0;
                int numYear = Convert.ToInt32(NumOfYears);
                dt.Columns.Add("Year");
                for (count = CurrentYear; count >= CurrentYear - numYear; count--)
                {
                    nt = new KPIYear();
                    nt.YEAR = count.ToString();
                    NT.Add(nt);
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return NT;
        }


        #region --Vetting Observation Report--
        /// <summary>
        /// Description: Method to fetch Observations By Vessel Count
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetObservationsByVesselCnt(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();
            try
            {
               // Years = "2017";
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetObservationsByVesselCnt(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID" };
                DataTable dt1 = objKPI.PivotTable("Vessel", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);
               
                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));

        }


        /// <summary>
        /// Description: Method to fetch Observations By Fleet Count
        /// Created By: Krishnapriya
        /// Created On: 01-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetObservationsByFleetCnt(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();

            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetObservationsByFleetCnt(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "FleetCode" };
                DataTable dt1 = objKPI.PivotTable("FleetName", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));

        }

        /// <summary>
        /// Method to fetch Observations By Vessel Count for pie chart
        /// Created By: Krishnapriya
        /// Created On: 02-MAR-2017
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public List<VettingReport> GetObservationsByVesselCntForPieChart(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            List<VettingReport> VTR = new List<VettingReport>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetObservationsByVesselCnt(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];
                VettingReport vtr;

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        vtr = new VettingReport();
                        vtr.Vessel_Name = Convert.ToString(dr["Vessel"]);
                        vtr.Rec_Count = Convert.ToString(dr["Rec_Count"]);
                        VTR.Add(vtr);
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return VTR;
        }

        /// <summary>
        /// Method to fetch Observations By Fleet Count for pie chart
        /// Created By: Krishnapriya
        /// Created On: 02-MAR-2017
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public List<VettingReport> GetObservationsByFleetCntForPieChart(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            List<VettingReport> VTR = new List<VettingReport>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetObservationsByFleetCnt(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];
                VettingReport vtr;

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        vtr = new VettingReport();
                        vtr.Fleet_Name = Convert.ToString(dr["FleetName"]);
                        vtr.Rec_Count = Convert.ToString(dr["Rec_Count"]);
                        VTR.Add(vtr);
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return VTR;
        }    



        
        /// <summary>
        /// Description: Method to fetch Vessel Observation count by categories
        /// Created By: Krishnapriya
        /// Created On: 02-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetVesselObservationCntByCategory(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();

            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetVesselObservationCntByCategory(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "ID" };
                string[] Hide_cols = new string[] { "ID", "Category_ID"};
                DataTable dt1 = objKPI.PivotTable("CategoryName", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));

        }

        /// <summary>
        /// Description: Method to fetch Fleet Observation count by categories
        /// Created By: Krishnapriya
        /// Created On: 02-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetFleetObservationCntByCategory(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();

            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetFleetObservationCntByCategory(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "FleetCode" };
                string[] Hide_cols = new string[] {"ID","FleetCode" };
                DataTable dt1 = objKPI.PivotTable("CategoryName", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));

        }

        /// <summary>
        /// Description: Method to fetch Vetting Type 
        /// Created By: Krishnapriya
        /// Created On: 06-MAR-2017
        /// </summary>
        public List<VettingType> GetVettingType()
        {
            List<VettingType> VT = new List<VettingType>();
            try
            {
                VettingType vt;
                DataTable dtRawdata = new DataTable();
                dtRawdata = objVet.VET_Get_VettingTypeList();

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        vt = new VettingType();
                        vt.Vetting_Type_Name = Convert.ToString(dr["Vetting_Type_Name"]);
                        vt.Vetting_Type_ID = Convert.ToString(dr["Vetting_Type_ID"]);
                        VT.Add(vt);
                    }
                }
            }
            catch(Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return VT;
        }

        /// <summary>
        /// Description: Method to fetch Category
        /// Created By: Krishnapriya
        /// Created On: 06-MAR-2017
        /// </summary>
        public List<Category> GetCategory()
        {
            List<Category> Cat = new List<Category>();
            try
            {
                Category ct;
                DataTable dt = new DataTable();
                dt = objVetIndex.VET_Get_ObservationCategories("Edit");
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ct = new Category();
                        ct.Category_ID = Convert.ToString(dr["OBSCategory_ID"]);
                        ct.Category_Name = Convert.ToString(dr["OBSCategory_Name"]);
                        Cat.Add(ct);
                    }
                }
            }

            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return Cat;
        }

        /// <summary>
        /// Description: Method to fetch Observation Type 
        /// Created By: Krishnapriya
        /// Created On: 06-MAR-2017
        /// </summary>
        public List<ObservationType> GetObservationType()
        {
            List<ObservationType> OT = new List<ObservationType>();
            try
            {
                ObservationType ot;
                DataTable dtRawdata = new DataTable();
                dtRawdata = objVetIndex.VET_Get_ObservationTypeList();

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        ot = new ObservationType();
                        ot.ObservationType_Name = Convert.ToString(dr["ObsTypName"]);
                        ot.ObservationType_ID = Convert.ToString(dr["ID"]);
                        OT.Add(ot);
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return OT;
        }


        /// <summary>
        /// Description: Method to fetch Observation Type 
        /// Created By: Krishnapriya
        /// Created On: 06-MAR-2017
        /// </summary>
        public List<Fleet> GetFleetList(string CompanyID)
        {
            List<Fleet>FT = new List<Fleet>();
            try
            {
                Fleet ft;
                BLL_Infra_VesselLib objVsl = new BLL_Infra_VesselLib();

                DataTable FleetDT = objVsl.GetFleetList(Convert.ToInt32(CompanyID));


                if (FleetDT.Rows.Count > 0)
                {
                    foreach (DataRow dr in FleetDT.Rows)
                    {
                        ft = new Fleet();
                        ft.Flt_Name = Convert.ToString(dr["name"]);
                        ft.Fleet_ID = Convert.ToString(dr["code"]);
                        FT.Add(ft);
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return FT;
        }
       

        #endregion


       
        /// <summary>
        /// Description: Method to fetch Vessel Observation count Year wise
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetVesselObservationsCntYearWise(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();

            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetVesselObservationsCntYearWise(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "VesselID" };
                string[] Hide_cols = new string[] { "VesselID"};
                DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));

        }

        /// <summary>
        /// Description: Method to fetch Fleet Observation count by year wise
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetFleetObservationCntYearWise(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();

            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetFleetObservationCntYearWise(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "FleetCode" };
                string[] Hide_cols = new string[] { "FleetCode" };
                DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }


        /// <summary>
        /// Description: Method to fetch observation by Category count
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetObservationByCategoryCount(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();

            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetObservationByCategoryCount(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "CategoryID","ID", "Year" };
                DataTable dt1 = objKPI.PivotTable("CategoryName", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }

        /// <summary>
        /// Description: Method to fetch observation by Category count for pie chart
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public List<VettingReport> GetObservationByCategoryCountForChart(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            List<VettingReport> VTR = new List<VettingReport>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetObservationByCategoryCountForChart(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];
                VettingReport vtr;

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        vtr = new VettingReport();
                        vtr.CategoryName = Convert.ToString(dr["CategoryName"]);
                        vtr.Rec_Count = Convert.ToString(dr["Rec_Count"]);
                        VTR.Add(vtr);
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return VTR;
        }   


        
         /// <summary>
        /// Description: Method to fetch observation by Category count for multiple year
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetMultipleYearCategoryCount(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();

            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetMultipleYearCategoryCount(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID" };
                DataTable dt1 = objKPI.PivotTable("Category_Name", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }


        /// <summary>
        /// Description: Method to fetch observation by Category count for multiple year to bind chart
        /// Created By: Krishnapriya
        /// Created On: 10-MAR-2017
        /// </summary>
        /// <param name="VettingReport object"></param>
        public Stream GetMultipleYearCategoryCountChart(VettingReport objVTR)
        {
            string VesselIDs = objVTR.Vessel_IDs;
            string Years = objVTR.Years;
            string vettingTypeID = objVTR.VettingTypeID;
            string categoryID = objVTR.CategoryID;
            string observationTypeID = objVTR.ObvTypeID;
            string fleetID = objVTR.FleetID;
            var list = new List<Dictionary<string, object>>();

            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetMultipleYearCategoryCount(VesselIDs, Years, vettingTypeID, categoryID, observationTypeID, fleetID).Tables[0];

                string[] Pkey_cols = new string[] { "Category_Name" };
                string[] Hide_cols = new string[] { "Rec_Count" };
                DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_cols, Hide_cols, dtRawdata);

                foreach (DataRow row in dt1.Rows)
                {

                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }


        #region PMSOverdue
        /// <summary>
        /// Description:Method to get vessel wise PMS overdue rate for Critical and non critical jobs
        /// </summary>
        /// <param name="VID"></param>
        /// <param name="Startdate"></param>
        /// <param name="EndDate"></param>
        /// <returns>List of PMS overdue object </returns>


        public List<KPIGeneric> Vetting_KPI_ByCompany(string KID, string Startdate, string EndDate)
        {
            List<KPIGeneric> NT = new List<KPIGeneric>();
            try
            {
                string Qtr = "";
                DataTable dt1 = objKPI.Get_Vetting_KPI_ByCompany(Qtr,int.Parse(KID), Convert.ToDateTime(Startdate), Convert.ToDateTime(EndDate)).Tables[0];
                KPIGeneric nt;
                //string[] Pkey_cols = new string[] { "KPI_ID" };
                //string[] Hide_cols = new string[] { "ID", "Qtr_Start", "Qtr_End", "KPI_Name", "KPI_ID" };
                //DataTable dt1 = objKPI.PivotTable("Qtr", "KPI_Value", "", Pkey_cols, Hide_cols, dt);

                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        nt = new KPIGeneric();
                        nt.Qtr = Convert.ToString(dr["Qtr"]);
                        nt.VALUE = Convert.ToString(dr["KPI_Value"]);
                        NT.Add(nt);
                    }
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

            return NT;

        }



        /// <summary>
        /// Method to get quarterly kpi value for all 
        /// </summary>
        /// <param name="KPIID"></param>
        /// <returns></returns>

        public List<KPIGeneric> GetMultipleKPIVettingCompany(KPIGeneric objGen)
        {

            List<KPIGeneric> NT = new List<KPIGeneric>();
            KPIGeneric nt;
            try
            {
                DataTable dt1 = objKPI.Get_Vetting_KPI_ByCompany(objGen.Qtr, 0,null,null).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt1.Rows)
                    {
                        nt = new KPIGeneric();
                        nt.KPI_Name = Convert.ToString(dr["Code"]);
                        nt.VALUE = Convert.ToString(dr["KPI_Value"]);
                        nt.GOAL = Convert.ToString(dr["GOAL"]);
                        NT.Add(nt);
                    }
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

            return NT;
        }



        /// <summary>
        /// Method to get PI ist for selected kpi  
        /// </summary>
        /// <param name="KPIID"></param>
        /// <returns></returns>

        public string Get_PI_ListByKPI(string KPI_ID)
        {
            int Kpi = Convert.ToInt16(KPI_ID);
            string Result = "";
            try
            {
                DataTable dt1 = objKPI.Get_PI_ListByKPI_Async(Kpi).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    Result = dt1.Rows[0][0].ToString();
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

            return Result;
        }





        #endregion


        #region Method for Observation By OilMajors and Observation By Risk Level

        /// <summary>
        /// Description : Method for get the Oil Major
        /// Created By : Harshal
        /// Created On : 04/03/2017
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public Stream GetVesselwiseOilMajors(VettingReport objVettingReport)
        {
            string VesselIDs = objVettingReport.Vessel_IDs;
            string Years = objVettingReport.Years;
            string CategoryID = objVettingReport.CategoryID;
            string VettingTypeID = objVettingReport.VettingTypeID;
            string ObservationTypeID = objVettingReport.ObvTypeID;
            string FleetID = objVettingReport.FleetID;
            string OilMajorID = objVettingReport.Oil_ID;

            List<VettingReport> NT = new List<VettingReport>();
            var list = new List<Dictionary<string, object>>();
            try
            {
                DataTable dtData;
                dtData = objVTReports.GetVesselwiseOilMajors(Years, VesselIDs, CategoryID, VettingTypeID, ObservationTypeID, FleetID,OilMajorID).Tables[0];
                string[] Pkey_Cols = new string[] { "year" };
                string[] Hide_cols = new string[] { "ID", "year" };
                DataTable dt1 = objKPI.PivotTable("OilMajorName", "Rec_Count", "", Pkey_Cols, Hide_cols, dtData);
                foreach (DataRow row in dt1.Rows)
                {
                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }

            }

            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 06/03/2017
        /// Description : Load the Oil Major names with count for Vessel on Pie Chart
        /// </summary>
        /// <param name="objVettingReport"></param>
        /// <returns></returns>

        public List<VettingReport> GetOilMajorNameCount(VettingReport objVettingReport)
        {

            string VesselIDs = objVettingReport.Vessel_IDs;
            string Years = objVettingReport.Years;
            string CategoryID = objVettingReport.CategoryID;
            string VettingTypeID = objVettingReport.VettingTypeID;
            string ObservationTypeID = objVettingReport.ObvTypeID;
            string FleetID = objVettingReport.FleetID;
            string OilMajorID = objVettingReport.Oil_ID;

            List<VettingReport> NT = new List<VettingReport>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetOilMajorNameCount(Years, VesselIDs, CategoryID, VettingTypeID, ObservationTypeID, FleetID,OilMajorID).Tables[0];
                VettingReport VR;
                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        VR = new VettingReport();
                        VR.Years = Convert.ToString(dr["OilMajorName"]);
                        VR.Rec_Count = Convert.ToString(dr["Rec_Count"]);
                        NT.Add(VR);
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return NT;
        }

        /// <summary>
        /// Description : Method for get the Oil Major
        /// Created By : Harshal
        /// Created On : 04/03/2017
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>
        public Stream GetOilMajorCountYearwise(VettingReport objVettingReport)
        {
            string VesselIDs = objVettingReport.Vessel_IDs;
            string Years = objVettingReport.Years;
            string CategoryID = objVettingReport.CategoryID;
            string VettingTypeID = objVettingReport.VettingTypeID;
            string ObservationTypeID = objVettingReport.ObvTypeID;
            string FleetID = objVettingReport.FleetID;
            string OilMajorID = objVettingReport.Oil_ID;

            List<VettingReport> NT = new List<VettingReport>();
            var list = new List<Dictionary<string, object>>();
            try
            {
                DataTable dtData;
                dtData = objVTReports.GetOilMajorCountYearwise(Years, VesselIDs, CategoryID, VettingTypeID, ObservationTypeID, FleetID,OilMajorID).Tables[0];
                string[] Pkey_Cols = new string[] { "OilMajorID" };
                string[] Hide_cols = new string[] { "OilMajorID" };

                DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_Cols, Hide_cols, dtData);

                foreach (DataRow row in dt1.Rows)
                {
                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 06/03/2017
        /// Description : Load the Oil Major names with count for Column Chart
        /// </summary>
        /// <param name="objVettingReport"></param>
        /// <returns></returns>

        public List<VettingReport> GetOilMajorNameColumnChart(VettingReport objVettingReport)
        {

            string VesselIDs = objVettingReport.Vessel_IDs;
            string Years = objVettingReport.Years;
            string CategoryID = objVettingReport.CategoryID;
            string VettingTypeID = objVettingReport.VettingTypeID;
            string ObservationTypeID = objVettingReport.ObvTypeID;
            string FleetID = objVettingReport.FleetID;

            List<VettingReport> NT = new List<VettingReport>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetOilMajorNameColumnChart(Years, VesselIDs, CategoryID, VettingTypeID, ObservationTypeID, FleetID).Tables[0];
                VettingReport VR;
                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        VR = new VettingReport();
                        VR.Years = Convert.ToString(dr["Year"]);
                        VR.Rec_Count = Convert.ToString(dr["Rec_Count"]);
                        NT.Add(VR);
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return NT;
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 10-03-2017
        /// Description : To bind the dropdown of OilMajors
        /// </summary>
        /// <returns></returns>
        public List<OilMajors> GetOilMajorDropdown()
        {
            List<OilMajors> ol = new List<OilMajors>();
            try
            {
                OilMajors OL;
                DataTable dtRawdata = new DataTable();
                dtRawdata = objVet.VET_Get_OilMajorList();

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        OL = new OilMajors();
                        OL.OilMajorName = Convert.ToString(dr["Display_Name"]);
                        OL.OilID = Convert.ToString(dr["ID"]);
                        ol.Add(OL);
                    }
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return ol;
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 14-03-2017
        ///  Description : To bind the jqx grid1 with Risk Level Observation
        /// </summary>
        /// <param name="objVettingReport"></param>
        /// <returns></returns>
        public Stream GetRiskLevelObservation(VettingReport objVettingReport)
        {
            string VesselIDs = objVettingReport.Vessel_IDs;
            string Years = objVettingReport.Years;
            string FleetID = objVettingReport.FleetID;
            string Risk_Level = objVettingReport.Risk_Level;
            string VettingTypeID = objVettingReport.VettingTypeID;
            string ObservationTypeID = objVettingReport.ObvTypeID;
            string CategoryID = objVettingReport.CategoryID;

            List<VettingReport> NT = new List<VettingReport>();
            var list = new List<Dictionary<string, object>>();
            try
            {
                DataTable dtData;
                dtData = objVTReports.GetRiskLevelObservation(Years, VesselIDs, FleetID, Risk_Level,VettingTypeID,ObservationTypeID,CategoryID).Tables[0];
                string[] Pkey_Cols = new string[] { "Year" };
                string[] Hide_cols = new string[] { "ID", "Year" };
                DataTable dt1 = objKPI.PivotTable("Risk_Level", "Rec_Count", "", Pkey_Cols, Hide_cols, dtData);
                foreach (DataRow row in dt1.Rows)
                {
                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }
            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 15-03-2017
        /// Description : To bind the pie chart with Risk Level Observation
        /// </summary>
        /// <param name="objVettingReport"></param>
        /// <returns></returns>
        public List<VettingReport> GetRiskLevelObservationPieChart(VettingReport objVettingReport)
        {
            string VesselIDs = objVettingReport.Vessel_IDs;
            string Years = objVettingReport.Years;
            string Risk_Level = objVettingReport.Risk_Level;
            string FleetID = objVettingReport.FleetID;
            string VettingTypeID = objVettingReport.VettingTypeID;
            string ObservationTypeID = objVettingReport.ObvTypeID;
            string CategoryID = objVettingReport.CategoryID;

            List<VettingReport> NT = new List<VettingReport>();
            try
            {
                DataTable dtRawdata;
                dtRawdata = objVTReports.GetRiskLevelObservationPieChart(Years, VesselIDs, FleetID, Risk_Level,VettingTypeID,ObservationTypeID,CategoryID).Tables[0];
                VettingReport VR;

                if (dtRawdata.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtRawdata.Rows)
                    {
                        VR = new VettingReport();
                        VR.Years = Convert.ToString(dr["Risk_Level"]);
                        VR.Rec_Count = Convert.ToString(dr["Rec_Count"]);
                        NT.Add(VR);
                    }
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            return NT;
        }

        /// <summary> 
        /// Created By : Harshal
        /// Created On : 15/03/2017
        /// Description : Method for get the Risk Level Observation Yearwise grid2
        /// </summary>
        /// <param name="objWL"></param>
        /// <returns></returns>

        public Stream GetRiskLevelObservationYearwise(VettingReport objVettingReport)
        {
            string VesselIDs = objVettingReport.Vessel_IDs;
            string Years = objVettingReport.Years;
            string FleetID = objVettingReport.FleetID;
            string Risk_level = objVettingReport.Risk_Level;
            string VettingTypeID = objVettingReport.VettingTypeID;
            string ObservationTypeID = objVettingReport.ObvTypeID;
            string CategoryID = objVettingReport.CategoryID;

            List<VettingReport> NT = new List<VettingReport>();
            var list = new List<Dictionary<string, object>>();
            try
            {
                DataTable dtData;
                dtData = objVTReports.GetRiskLevelObservationgrid(Years, VesselIDs, FleetID, Risk_level,VettingTypeID,ObservationTypeID,CategoryID).Tables[0];
                string[] Pkey_Cols = new string[] { "Vessel_ID", };
                string[] Hide_cols = new string[] { "Vessel_ID" };

                DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_Cols, Hide_cols, dtData);

                foreach (DataRow row in dt1.Rows)
                {
                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }

        /// <summary>
        /// Created By : Harshal
        /// Created On : 15/03/2017
        /// Description : Method for get the Risk Level Observation Yearwise grid3
        /// </summary>
        /// <returns></returns>
        public Stream GetFleetObservationYearwise(VettingReport objVettingReport)
        {
            string VesselIDs = objVettingReport.Vessel_IDs;
            string Years = objVettingReport.Years;
            string FleetID = objVettingReport.FleetID;
            string Risk_level = objVettingReport.Risk_Level;

            List<VettingReport> NT = new List<VettingReport>();
            var list = new List<Dictionary<string, object>>();
            try
            {
                DataTable dtData;
                dtData = objVTReports.GetFleetObservationRiskLevelCntYearwise(Years, VesselIDs, FleetID, Risk_level).Tables[0];
                string[] Pkey_Cols = new string[] { "FleetCode", };
                string[] Hide_cols = new string[] { "FleetCode" };

                DataTable dt1 = objKPI.PivotTable("Year", "Rec_Count", "", Pkey_Cols, Hide_cols, dtData);

                foreach (DataRow row in dt1.Rows)
                {
                    var dict = new Dictionary<string, object>();

                    foreach (DataColumn col in dt1.Columns)
                    {
                        dict[col.ColumnName] = (Convert.ToString(row[col]));
                    }
                    list.Add(dict);
                }

            }
            catch (Exception ex)
            {
                UDFLib.WriteExceptionLog(ex);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string jsonClient = serializer.Serialize(list);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            return new MemoryStream(Encoding.UTF8.GetBytes(jsonClient));
        }


        #endregion





  
   
  

  





    }
}
