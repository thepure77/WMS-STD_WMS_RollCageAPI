using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RollCageBusiness.RollCage
{

    public  class GoodsIssueTruckloadRollCageViewModel
    {
        public string truckLoad_No { get; set; }

        public string RollCage_Id { get; set; }

        public string Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string pick_location { get; set; }
        public string location_rollcage { get; set; }
        public string Dock { get; set; }

        public Guid truckLoad_Index { get; set; }

        public Guid planGoodsIssue_Index { get; set; }

        public string planGoodsIssue_No { get; set; }

        public Guid shipTo_Index { get; set; }

        public string shipTo_Id { get; set; }

        public string branch_Name { get; set; }

        public string branch_Code { get; set; }

        public string shipTo_Address { get; set; }

        public string goodsIssue_No { get; set; }

        public Guid? route_Index { get; set; }

        public string route_Id { get; set; }

        public string route_Name { get; set; }

        public Guid? round_Index { get; set; }

        public string round_Id { get; set; }

        public string round_Name { get; set; }

        public Guid goodsIssueItemLocation_Index { get; set; }

        public Guid tagOut_Index { get; set; }

        public string tagOut_No { get; set; }

        public int? tagOut_Status { get; set; }

        public string tagOutRef_No4 { get; set; }
        public string FlagScanIN { get; set; }

        public string tagOutRef_No5 { get; set; }
        public string mess { get; set; }
        public string Status { get; set; }

        public int? countScanBOX { get; set; }

        public int? totalBOX { get; set; }

        public int? countTmpRollcageOrder { get; set; }

        public string chuteId { get; set; }

        public List<ScanSummaryViewModel> lstScanSummary { get; set; }

    }

    public class ScanSummaryViewModel
    {

        public int isComplete { get; set; }
        public string planGoodsIssue_No { get; set; }
        public string truckLoad_No { get; set; }
        public string route_Id { get; set; }
        public string shipTo_Id { get; set; }
        public string branch_Name { get; set; }
        public string shipTo_Address { get; set; }
        public string goodsIssue_No { get; set; }
        public string round_Name { get; set; }
        public int? countScanBOX { get; set; }
        public int? totalBOX { get; set; }
        
    }
}
