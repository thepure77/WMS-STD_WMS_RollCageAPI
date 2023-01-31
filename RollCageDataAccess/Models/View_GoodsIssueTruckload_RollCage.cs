using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{
    public partial class View_GoodsIssueTruckload_RollCage
    {
        [Key]
        public long? RowIndex { get; set; }

        public string TruckLoad_No { get; set; }

        public Guid TruckLoad_Index { get; set; }

        public Guid PlanGoodsIssue_Index { get; set; }


        public string PlanGoodsIssue_No { get; set; }

        public Guid ShipTo_Index { get; set; }


        public string ShipTo_Id { get; set; }


        public string Branch_Name { get; set; }


        public string Branch_Code { get; set; }

 
        public string ShipTo_Address { get; set; }


        public string GoodsIssue_No { get; set; }

        public Guid? Route_Index { get; set; }


        public string Route_Id { get; set; }


        public string Route_Name { get; set; }

        public Guid? Round_Index { get; set; }


        public string Round_Id { get; set; }


        public string Round_Name { get; set; }

        public Guid GoodsIssueItemLocation_Index { get; set; }

        public Guid TagOut_Index { get; set; }


        public string TagOut_No { get; set; }

        public int? TagOut_Status { get; set; }

        public string TagOutRef_No4 { get; set; }

        public string TagOutRef_No5 { get; set; }

        public int? CountScanBOX { get; set; }

        public int? TotalBOX { get; set; }

        public string Chute_Id { get; set; }
    }
}
