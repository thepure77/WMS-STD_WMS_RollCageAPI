using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RollCageDataAccess.Models
{
    public partial class View_CheckTagoutWithRollcage
    {
        [Key]
        public int RowIndex { get; set; }
        public string GoodsIssue_No { get; set; }
        public string TruckLoad_No { get; set; }
        public string Chute_No { get; set; }
        public string TagOut_No { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string LocationType { get; set; }
        public string PlanGoodsIssue_No { get; set; }
        public string BranchCode { get; set; }
        public string FlagScanIN { get; set; }
        public string RollCage_Id { get; set; }
        public string OrderSeq { get; set; }
        public string DropSeq { get; set; }
    }
}
