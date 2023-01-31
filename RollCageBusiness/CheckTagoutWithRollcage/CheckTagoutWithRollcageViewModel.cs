using System;
using System.Collections.Generic;
using System.Text;

namespace RollCageBusiness.RollCage
{
    public class CheckTagoutWithRollcageViewModel
    {
        public long RowIndex { get; set; }
        public string GoodsIssue_No { get; set; }
        public string TruckLoad_No { get; set; }
        public string Chute_No { get; set; }
        public string TagOut_No { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string LocationType { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string FlagScanIN { get; set; }
        public string RollCage_Id { get; set; }

    }

    public class SearchCheckTagoutWithRollcage
    {
        public string goodsIssue_No { get; set; }
        public string locationType { get; set; }
        public bool isScan { get; set; }
    }



    public class ResultCheckTagoutWithRollcage
    {
        public string goodsIssue_no { get; set; }
        public string truck_no { get; set; }
        public string location_type { get; set; }
        public string chute { get; set; }
        public string tag { get; set; }
        public string productid { get; set; }
        public string product_name { get; set; }
        public string flag_scan { get; set; }
        public string rollcage { get; set; }
        public string branch_code { get; set; }
        public string drop_order_seq { get; set; }
        public string plangoodsissue_no { get; set; }

    }

    public class actionResultViewModel : Result
    {
        public IList<ResultCheckTagoutWithRollcage> itemsCheckout { get; set; }
    }
}
