using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.Data.Infrastructure;
using System.Data;


namespace SMS.Business.Infrastructure
{
   public class BLL_Infra_Exch_Rate
    {

       public DAL_Infra_Exch_Rate objDAL = new DAL_Infra_Exch_Rate();

       public DataTable SearchExchangeRate(string searchtext, int? CurrPrivious, string sortby, int? sortdirection, int? pagenumber, int? pagesize, ref int isfetchcount)
       {
           return objDAL.SearchExchangeRate(searchtext, CurrPrivious, sortby, sortdirection, pagenumber, pagesize, ref isfetchcount);
       }
       
       
       public DataTable Get_ExchangeRate_List(string CURR_CODE, DateTime? CURR_DATE)
       {
           return objDAL.Get_ExchangeRate_List_DL(CURR_CODE, CURR_DATE);
       }

       public int Edit_ExchangeRate(string CURR_CODE, decimal? EXCH_RATE, DateTime? CURR_DATE, int CREATED_BY)
       {
           return objDAL.Edit_ExchangeRate_DL(CURR_CODE, EXCH_RATE, CURR_DATE, CREATED_BY);
       }

       public int Edit_ExchangeRate(string CURR_CODE, decimal? EXCH_RATE, DateTime? CURR_DATE)
       {
           return objDAL.Edit_ExchangeRate_DL(CURR_CODE, EXCH_RATE, CURR_DATE);
       }
       //public int Insert_ExchangeRate(string CURR_CODE, decimal? EXCH_RATE, DateTime? CURR_DATE, int CREATED_BY, string BaseCurrency)
       //{
       //    return objDAL.Insert_ExchangeRate_DL(CURR_CODE, EXCH_RATE, CURR_DATE, CREATED_BY, BaseCurrency); 
       //}
       public int Insert_ExchangeRate(string CURR_CODE, decimal? EXCH_RATE, DateTime? CURR_DATE)
       {
           return objDAL.Insert_ExchangeRate_DL(CURR_CODE, EXCH_RATE, CURR_DATE);
       }

       public int Delete_ExchangeRate(string CURR_CODE, DateTime? CURR_DATE, int CreatedBy)
       {
           return objDAL.Delete_ExchangeRate_DL(CURR_CODE, CURR_DATE, CreatedBy);
       }
       public int Delete_ExchangeRate(string CURR_CODE,   DateTime? CURR_DATE)
       {
           return objDAL.Delete_ExchangeRate_DL(CURR_CODE,CURR_DATE );
       }

       //Check Duplicate
       public DataTable Check_Exchange_List(string Curr_code, DateTime? Date)
       {
           return objDAL.Check_Exchange_List(Curr_code, Date);
       }
    }
}
