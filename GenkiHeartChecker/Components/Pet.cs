using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GenkiHeartChecker.Components
{
    public class Pet
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255), Unique]
        public string PetName { get; set; } = "Genki";

        [OneToMany(nameof(HeartRecord), CascadeOperations = CascadeOperation.CascadeRead)]
        public List<HeartRecord> Records { get; set; } = new List<HeartRecord>();
    }
}
