using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{
    public partial class tmp_RollCageOrder
    {
        [Key]
        public Guid Temp_Index { get; set; }

        public string RollCage_ID { get; set; }

        public Guid? Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public string Location_Type { get; set; }

        public string TagOut_No { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }
    }
}
