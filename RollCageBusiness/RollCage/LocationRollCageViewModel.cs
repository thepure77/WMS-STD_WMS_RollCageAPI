using System;
using System.ComponentModel.DataAnnotations;

namespace RollCageBusiness.RollCage
{

    public  class LocationRollCageViewModel
    {

        [Key]
        public Guid location_Index { get; set; }

        public string location_Id { get; set; }

        public string location_Name { get; set; }

        public Guid location_Index_To { get; set; }

        public string location_Id_To { get; set; }

        public string location_Name_To { get; set; }

        public Guid locationType_Index { get; set; }

        public string locationType_Id { get; set; }

        public string locationType_Name { get; set; }

        public Guid? rollCage_Index { get; set; }

        public string rollCage_Id { get; set; }
        public string goodsIssue_No { get; set; }

        public string rollCage_Name { get; set; }

        public int? rollCage_Status { get; set; }

        public string create_By { get; set; } // addnew

        public string qrcode { get; set; } // addnew

        public string chute_Id { get; set; } // addnew

    }
}
