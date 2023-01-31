using System;
namespace RollCageBusiness.FilmCutting
{

    public  class FilmCuttingViewModel : Result
    {
        
        public string tag_no { get; set; }
        public string docNo { get; set; }
        public string create_By { get; set; }
        public string product_id { get; set; }
        public string product_name { get; set; }
        public string value { get; set; }
        public string station { get; set; }

    }
}
