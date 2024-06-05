namespace Project.Common
{
    public class FilterParameters
    {
        public Guid? ConditionID { get; set; }
        public string StateID { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SearchQuarry { get; set; }
    }
}
