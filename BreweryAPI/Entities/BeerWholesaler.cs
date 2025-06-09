namespace BreweryAPI.Entities
{
    public class BeerWholesaler
    {
        public virtual Beer Beer { get; set; }
        public int BeerId { get; set; }
        public virtual Wholesaler Wholesaler { get; set; }
        public Guid WholesalerId { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
