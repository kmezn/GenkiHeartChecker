using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GenkiHeartChecker.Components
{
    public class HeartRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime DateTime { get; set; }
            = DateTime.Now;

        [ForeignKey(typeof(Pet))]
        public int PetId { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
        public Pet Pet { get; set; } = new Pet();
    }
}
