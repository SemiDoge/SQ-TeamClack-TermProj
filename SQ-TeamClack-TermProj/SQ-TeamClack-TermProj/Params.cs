namespace SQ_TeamClack_TermProj
{
    public struct contractParams
    {
        public ulong orderID { get; set; }
        public string clientName { get; set; }
        public string orderDate { get; set; }
        public int jobType { get; set; }
        public int quantity { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public int vanType { get; set; }
        public int duration { get; set; }
        public bool MARKEDFORACTION { get; set; }
    }

    public struct carrierParams
    {
        public string carrierName { get; set; }
        public string cityName { get; set; }
        public double FTLA { get; set; }
        public double LTLA { get; set; }
        public double FTLRate { get; set; }
        public double LTLRate { get; set; }
        public double reefCharge { get; set; }
    }

    public struct routeParams
    {
        public string destination { get; set; }
        public int distance { get; set; }
        public double time { get; set; }
        public string west { get; set; }
        public string east { get; set; }
    }
}