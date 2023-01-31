using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace RollCageDataAccess.Models
{

    public partial class View_LocationRollCage
    {
        [Key]
        public Guid Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public string Ref_No1 { get; set; }

        public string Ref_No2 { get; set; }

        public Guid LocationType_Index { get; set; }

        public string LocationType_Id { get; set; }

        public string LocationType_Name { get; set; }

        public Guid? RollCage_Index { get; set; }

        public string RollCage_Id { get; set; }

        public string RollCage_Name { get; set; }

        public int? RollCage_Status { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

    }
}
