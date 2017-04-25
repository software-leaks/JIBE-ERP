using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SMS.Data.Infrastructure;
namespace SMS.Business.Infrastructure
{
   public class BLL_Infra_ExchangeRate
    {
       DAL_Infra_ExchangeRate objcr = new DAL_Infra_ExchangeRate();
       
        public BLL_Infra_ExchangeRate()
        {
           
        }

        #region exchange rate methods
        public DataSet OCAP_Exch_Rate_fill(bool showall)
        {
           
               
                return objcr.OCAP_Exch_Rate_fill_DL(showall);
      
        }


        public DataSet OCAP_Exch_Rate_fillHistory(int currCode)
        {
          
                return objcr.OCAP_Exch_Rate_fillHistory_DL(currCode);
          
        }

        public int OCAP_Exch_Rate_Del(int id)
        {
           
                return objcr.OCAP_Exch_Rate_Del_DL(id);
         
        }

        public int OCAP_Exch_Rate_Edit(int id, int Currency_Type, float Exch_rate, string date, int Base_curr)
        {
           
                return objcr.OCAP_Exch_Rate_Edit_DL(id, Currency_Type, Exch_rate, date, Base_curr);
         
        }

        public DataSet OCAP_Exch_Rate_fill_dropdown()
        {
          
                return objcr.OCAP_Exch_Rate_fill_dropdown_DL();
          
        }


        public int OCAP_Exch_Rate_INS(int Currency_code, float Exch_rate, string date, int Base_curr)
        {
           
                return objcr.OCAP_Exch_Rate_INS_DL(Currency_code, Exch_rate, date, Base_curr);
       
        }

        public int ACC_Ins_Exchrate_XML(string exch)
        {
          
                return objcr.ACC_Ins_Exchrate_XML_DL(exch);
         
        }

        public decimal ACC_Get_ExchRate_By_Code(string Code)
        {
            return objcr.ACC_Get_ExchRate_By_Code_DL(Code);
        }

        #endregion  

    }
}
