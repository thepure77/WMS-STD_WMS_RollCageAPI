using RollCageBusiness;
using System;
using System.Collections.Generic;
using System.Text;

namespace RollCageBusiness.PlanGoodsIssue
{
    public partial class CheckRollcageViewModel : Result
    {
     
        public string RollCage_ID { get; set; }
        public string Chute { get; set; }
        public int? CountTag { get; set; }

    }
}
