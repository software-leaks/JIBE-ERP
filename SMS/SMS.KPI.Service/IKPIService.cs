using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using SMS.KPI.Service.DataContract;
using System.Linq;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

namespace SMS.KPI.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITMSA_Service" in both code and config file together.
    [ServiceContract]
    public interface IKPIService
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVoyageData/VID/{VID}/KPI_ID/{KPI_ID}/Startdate/{Startdate}/EndDate/{EndDate}/telId1/{telId1}/telId2/{telId2}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPICO2> GetVoyageData(string VID, string KPI_ID, string Startdate, string EndDate, string telId1, string telId2);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVoyageDataSOx/VID/{VID}/Startdate/{Startdate}/EndDate/{EndDate}/telId1/{telId1}/telId2/{telId2}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPISOx> GetVoyageDataSOx(string VID, string Startdate, string EndDate, string telId1, string telId2);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVoyageDataNOx/VID/{VID}/Startdate/{Startdate}/EndDate/{EndDate}/telId1/{telId1}/telId2/{telId2}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPINOx> GetVoyageDataNOx(string VID, string Startdate, string EndDate, string telId1, string telId2);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetData/VID/{VID}/KPI_ID/{KPI_ID}/Startdate/{Startdate}/EndDate/{EndDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPICO2> GetData(string VID, string KPI_ID, string Startdate, string EndDate);



        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetDataSOx/VID/{VID}/Startdate/{Startdate}/EndDate/{EndDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPISOx> GetDataSOx(string VID, string Startdate, string EndDate);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetDataNOx/VID/{VID}/Startdate/{Startdate}/EndDate/{EndDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPINOx> GetDataNOx(string VID, string Startdate, string EndDate);




        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetTelDate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPICO2VOYAGE> GetTelDate(TDATE objTDATE);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetTelDateSOx", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPISOxVOYAGE> GetTelDateSOx(TDATE objTDATE);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetTelDateNOx", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPINOxVOYAGE> GetTelDateNOx(TDATE objTDATE);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPICO2> GetMultipleVesselData(KPICO2 objKPICO2);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselDataSOx", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPISOx> GetMultipleVesselDataSOx(KPISOx objKPISOx);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselDataNOx", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPINOx> GetMultipleVesselDataNOx(KPINOx objKPINOx);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselDataSOxInd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPISOx> GetMultipleVesselDataSOxInd(KPISOx objKPISOx);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetGenericData/VID/{VID}/KPI_ID/{KPI_ID}/Interval/{Interval}/ValueType/{ValueType}/Startdate/{Startdate}/EndDate/{EndDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPIGeneric> GetGenericData(string VID, string KPI_ID, string Interval, string ValueType, string Startdate, string EndDate);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetGenericVoyageData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPIGeneric> GetGenericVoyageData(KPIGeneric objKPIGeneric);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleGenericVesselData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPIGeneric> GetMultipleGenericVesselData(KPIGeneric oKPIGeneric);

        /// <summary>
        /// Description: Method to search crew retention rate 
        /// Created By: Bhairab
        /// Created On: 30/05/2016
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetRetentionData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<CrewRetention> GetRetentionData(CrewRetention obj);

        /// <summary>
        /// Description:Method to get vessel wise PMS overdue rate for Critical and non critical jobs
        /// </summary>
        /// <param name="VID"></param>
        /// <param name="Startdate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetPMSOverDueByVessel/VID/{VID}/Startdate/{Startdate}/EndDate/{EndDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<PMSOverdue> GetPMSOverDueByVessel(string VID, string Startdate, string EndDate);


        /// <summary>
        /// Description:Method to get vessel wise PMS overdue rate for all vessels
        /// </summary>
        /// <param name="VID"></param>
        /// <param name="Startdate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselPMSOverdue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        //List<dynamic> GetMultipleVesselPMSOverdue(PMSOverdue objPMS);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselPMSOverdue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<PMSOverdue> GetMultipleVesselPMSOverdue(PMSOverdue obj);


        /// <summary>
        /// Description: Method to fetch Rank list
        /// Created By: Krishnapriya
        /// Created On: 23/11/2016
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetRankData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPIRank> GetRankData();

        /// <summary>
        /// Description: Method to fetch year list
        /// Created By: Krishnapriya
        /// Created On: 23/11/2016
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetYearData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPIYear> GetYearData();



        /// <summary>
        /// Description: Method to search Overall TMSA Report 
        /// Created By: Gargi
        /// Created On: 28/09/2016
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetOverallReport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<REPORT> GetOverallReport(REPORT obj);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetElementData/VID/{VID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<ELEMENTDATA> GetElementData(string VID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetStageData/VID/{VID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<STAGEDATA> GetStageData(string VID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetLevelData/VID/{VID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<LEVELDATA> GetLevelData(string VID);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVersionData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<VERSIONDATA> GetVersionData();


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/LinkCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPIURL> LinkCount(KPIURL obj);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/SaveData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        int SaveData(REPORT obj);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/DeleteData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        int DeleteData(REPORT obj);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        int UpdateData(REPORT obj);


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_PieChartData_DL/VID/{VID}/VNO/{VNO}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<REPORT> Get_PieChartData_DL(string VID, string VNO);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/LinkExists", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<REPORT> LinkExists(REPORT obj);

        /// <summary>
        /// Web method to provide report data for multiple vessel yearly
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselWorkList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetMultipleVesselWorkList(WorkList obj);

        /// <summary>
        /// Web method to provide report data for multiple vessel yearly
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselYearWiseWorkList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetMultipleVesselYearWiseWorkList(WorkList obj);

        /// <summary>
        /// Get all vessel list
        /// </summary>
        /// <param name="UserCompanyID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVesselList/usercompanyid/{UserCompanyID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<WorkList> Get_VesselList(string UserCompanyID);



        /// <summary>
        /// Web method to provide report data for multiple vessel monthly
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMonthlyWorklistCountByVessel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetMonthlyWorklistCountByVessel(WorkList obj);

        /// <summary>
        /// Web method to provide report data for multiple vessel monthly
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselWorkListMonthly", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetMultipleVesselWorkListMonthly(WorkList obj);

        /// <summary>
        /// Get year wise vessel list count to bind gridview for Near Misses Reports
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetVesselCountNearMisses", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetVesselCountNearMisses(WorkList obj);

        /// <summary>
        /// Get year wise vessel list count to bind jqx chart for Near Misses Reports
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetChartVesselCountNearMisses", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetChartVesselCountNearMisses(WorkList obj);


        /// <summary>
        /// Get total Near Misses per year to bind jqx pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetPerVesselPerYearNearMissesForPieChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<WorkList> GetPerVesselPerYearNearMissesForPieChart(WorkList obj);

        
        /// <summary>
        /// Get total Near Misses per year to bind jqx pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetPerYearNearMissesForPieChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<WorkList> GetPerYearNearMissesForPieChart(WorkList obj);

        /// <summary>
        /// Get Incident count to bind jqx pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetCategoryIncidentCountForPieChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<WorkList> GetCategoryIncidentCountForPieChart(WorkList obj);


        /// <summary>
        /// Get Incident count for all vessels per year to bind jqx grid/chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselIncidentCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetMultipleVesselIncidentCount(WorkList obj);


        /// <summary>
        /// Get Incident count for all categories per year to bind jqx grid/chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetCategoryIncidentCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetCategoryIncidentCount(WorkList obj);


        /// <summary>
        /// Get Injury/Death Incident count to bind jqx pie chart/grid
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetInjuryIncidentCountForPieChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<WorkList> GetInjuryIncidentCountForPieChart(WorkList obj);


        /// <summary>
        /// Get Injury/death Incident count for all categories per year to bind jqx grid/chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetCategoryInjuryIncidentCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetCategoryInjuryIncidentCount(WorkList obj);


        /// <summary>
        /// Get Injury/Death Incident count for all vessels per year to bind jqx grid/Chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleVesselInjuryIncidentCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetMultipleVesselInjuryIncidentCount(WorkList obj);

        /// <summary>
        /// Get years list based on the count passed
        /// </summary>
        /// <param name="NumOfYears">Denotes the count of year to be returned</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetYears/NumOfYears/{NumOfYears}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPIYear> GetYears(string NumOfYears);

        /// <summary>
        /// Method to fetch Observations By Vessel Count
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetObservationsByVesselCnt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetObservationsByVesselCnt(VettingReport obj);

        /// <summary>
        /// Method to fetch Observations By Fleet Count
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetObservationsByFleetCnt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetObservationsByFleetCnt(VettingReport obj);

        /// <summary>
        /// Method to fetch Observations By Vessel Count for Pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetObservationsByVesselCntForPieChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<VettingReport> GetObservationsByVesselCntForPieChart(VettingReport obj);

        
        /// <summary>
        /// Method to fetch Observations By Fleet Count for Pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetObservationsByFleetCntForPieChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<VettingReport> GetObservationsByFleetCntForPieChart(VettingReport obj);

        /// <summary>
        /// Method to fetch vessel Observation count By Category
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetVesselObservationCntByCategory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetVesselObservationCntByCategory(VettingReport obj);


        /// <summary>
        /// Method to fetch fleet Observation By Category
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetFleetObservationCntByCategory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetFleetObservationCntByCategory(VettingReport obj);
                
        /// <summary>
        /// Get Vetting Type list
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetVettingType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<VettingType> GetVettingType();
            

        /// <summary>
        /// Get Observation Category list
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetCategory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Category> GetCategory();


        /// <summary>
        /// Get Observation Type list
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetObservationType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<ObservationType> GetObservationType();

        /// <summary>
        /// Get Fleet list
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetFleetList/CompanyID/{CompanyID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<Fleet> GetFleetList(string CompanyID);

        /// <summary>
        /// Get Oil Major Names for Vessel to bind jqx grid/chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetVesselwiseOilMajors", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetVesselwiseOilMajors(VettingReport obj);

        /// <summary>
        /// Get Oil MajorNames with Rec_Count to bind jqx Pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetOilMajorNameCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<VettingReport> GetOilMajorNameCount(VettingReport obj);

        /// <summary>
        /// Get Oil OilMajorNames and Rec_Count Year wise in jqx gird/chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetOilMajorCountYearwise", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetOilMajorCountYearwise(VettingReport obj);
        
        /// <summary>
        /// Get Oil MajorNames with Rec_Count to bind jqx Pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method="POST",UriTemplate="/GetOilMajorNameColumnChart",RequestFormat=WebMessageFormat.Json,ResponseFormat=WebMessageFormat.Json,BodyStyle=WebMessageBodyStyle.Bare)]
        List<VettingReport>GetOilMajorNameColumnChart(VettingReport obj);

        /// <summary>
        /// Method to fetch Vessel Observation count year wise
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetVesselObservationsCntYearWise", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetVesselObservationsCntYearWise(VettingReport obj);


        /// <summary>
        /// Method to fetch fleet Observation count year wise
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetFleetObservationCntYearWise", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetFleetObservationCntYearWise(VettingReport obj);


        /// <summary>
        /// Method to fetch observation by Category count
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetObservationByCategoryCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetObservationByCategoryCount(VettingReport obj);


        /// <summary>
        /// Method to fetch observation by Category count for Pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetObservationByCategoryCountForChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<VettingReport> GetObservationByCategoryCountForChart(VettingReport obj);


        /// <summary>
        /// Method to fetch observation by Category count
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleYearCategoryCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetMultipleYearCategoryCount(VettingReport obj);
               
        /// <summary>
        /// Method to fetch observation by Category count
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleYearCategoryCountChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetMultipleYearCategoryCountChart(VettingReport obj);


        /// <summary>
        /// Description:Method to get vessel wise PMS overdue rate for Critical and non critical jobs
        /// </summary>
        /// <param name="KID"></param>
        /// <param name="Startdate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Vetting_KPI_ByCompany/KID/{KID}/Startdate/{Startdate}/EndDate/{EndDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<KPIGeneric> Vetting_KPI_ByCompany(string KID, string Startdate, string EndDate);

        /// <summary>
        /// Description:Method to get Qurter KPI rate for all vetting KPIs
        /// </summary>
        /// <param name="Qtr"></param>


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMultipleKPIVettingCompany", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedResponse)]
        List<KPIGeneric> GetMultipleKPIVettingCompany(KPIGeneric obj);

       
        /// <summary>
        /// To Bind the Dropdownn of OilMajorNames
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetOilMajorDropdown", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<OilMajors> GetOilMajorDropdown();

        /// <summary>
        /// To bind  the jqx grid with Risk Level Observation
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetRiskLevelObservation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetRiskLevelObservation(VettingReport obj);


        /// <summary>
        /// Get Risk Level Observation with Rec_Count to bind jqx Pie chart
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetRiskLevelObservationPieChart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<VettingReport> GetRiskLevelObservationPieChart(VettingReport obj);

        /// <summary>
        /// To Bind the jqx grid2 Vessel with Risk Level Observation Yearwise
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetRiskLevelObservationYearwise", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetRiskLevelObservationYearwise(VettingReport obj);

        /// <summary>
        /// To Bind the jqx grid3 Risk with Risk Level Observation Yearwise
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetFleetObservationYearwise", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetFleetObservationYearwise(VettingReport obj);



        
        /// <summary>
        /// Description:Method to PI list by KPI as html table
        /// </summary>
        /// <param name="KID"></param>

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Get_PI_ListByKPI/KPI_ID/{KPI_ID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        string Get_PI_ListByKPI(string KPI_ID);
    }            
}