using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{
    public partial class View_RollCage_With_Tag
    {
        [Key]
        public long? RowIndex { get; set; }


        public string Chute_No { get; set; }
        public string TagOut_No { get; set; }
        public string LocationType { get; set; }
        public string UDF_1 { get; set; }
        public string FlagScanIN { get; set; }
        public string RollCage_Id { get; set; }
        public string GI_NO { get; set; }
        public string TruckLoad_No { get; set; }
        public string BranchCode { get; set; }
        public string Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string box_no { get; set; }

    }
}













