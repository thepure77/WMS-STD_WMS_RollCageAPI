using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RollCageDataAccess.Models
{

    public partial class ms_Staging_location
    {
        [Key]
        public Guid? Location_Index { get; set; }
        
        public Guid? LocationType_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public string RollCage_Id { get; set; }

    }
}
