using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace RollCageDataAccess.Models
{
    public partial class View_DockLocationRollCage
    {
        [Key]
        public string Ref_Document_No { get; set; }
        public string Appointment_Id { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid Location_Index { get; set; }

        public Guid Dock_Index { get; set; }
        public Guid? CheckIn_Index { get; set; }
        public Guid? CheckOut_Index { get; set; }

       
    }
}
