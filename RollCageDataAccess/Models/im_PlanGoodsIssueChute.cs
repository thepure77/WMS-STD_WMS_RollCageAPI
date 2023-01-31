using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{


    public partial class im_PlanGoodsIssueChute
    {

        [Key]
        public int RowIndex { get; set; }

        public Guid PlanGoodsIssue_Index { get; set; }

        public string PlanGoodsIssue_No { get; set; }

        public string Chute_No { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }


    }
}
