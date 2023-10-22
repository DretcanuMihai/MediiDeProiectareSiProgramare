using System;

namespace model.entities
{
    [Serializable]
    public class Ticket : Entity<int>
    {
        public string BuyerName { get; set; }
        public Festival Festival { get; set; }
        public int NumberOfSpots { get; set; }

        public Ticket(int id, string buyerName, Festival festival, int numberOfSpots) : base(id)
        {
            BuyerName = buyerName;
            Festival = festival;
            NumberOfSpots = numberOfSpots;
        }
    }
}