using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{
    public partial class View_GoodsIssueItemLocation_tag
    {
        [Key]
        public long? RowIndex { get; set; }


        public Guid GoodsIssueItemLocation_Index { get; set; }


        public string GoodsIssue_No { get; set; }


        public string Tag_No { get; set; }

        public string Create_By { get; set; }


        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }

        public string Cancel_By { get; set; }


        public string DocumentRef_No1 { get; set; }

        public string DocumentRef_No2 { get; set; }
    }
}
