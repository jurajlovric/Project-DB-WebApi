namespace Project.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public string StateID { get; set; }
        public Guid? ConditionID { get; set; }
    }
}
