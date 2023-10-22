using System;

namespace model.entities
{
    [Serializable]
    public class Festival : Entity<int>
    {
        public int ID
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }
        public String ArtistName { get; set; }
        public DateTime Date { get; set; }
        public String Place { get; set; }
        public int AvailableSpots { get; set; }
        public int SoldSpots { get; set; }
        public int RemainingSpots
        {
            get { return AvailableSpots - SoldSpots; }
        }

        public Festival()
        {
            
        }
        public Festival(int id, string artistName, DateTime date, string place, int availableSpots,
            int soldSpots) : base(id)
        {
            ArtistName = artistName;
            Date = date;
            Place = place;
            AvailableSpots = availableSpots;
            SoldSpots = soldSpots;
        }

        public override string ToString()
        {
            return "Festival{" +
                   "id=" + ID +
                   ", artistName='" + ArtistName + '\'' +
                   ", dateTime=" + this.Date +
                   ", place='" + Place + '\'' +
                   ", availableSpots=" + AvailableSpots +
                   ", soldSpots=" + SoldSpots +
                   '}';
        }
    }
}