using SQLite;
using SQLiteNetExtensions.Attributes;
using ForeignKeyAttribute = SQLiteNetExtensions.Attributes.ForeignKeyAttribute;


namespace GenkiHeartChecker.Components
{
    public class DBService
    {
        private SQLiteAsyncConnection _conn;

        public async Task InitDbAsync()
        {
            if (_conn == null)
                return;
            _conn = new SQLiteAsyncConnection(MauiProgram.DbPath, MauiProgram.Flags);
            await _conn.CreateTableAsync<Pet>();
            await _conn.CreateTableAsync<HeartRecord>();

            //await InitDefaultPet();
        }


        //public async Task InitDefaultPet()
        //{
        //    if (await _conn.Table<Pet>().CountAsync() > 0)
        //        return;
        //    await AddorUpdatePet(new Pet() { PetName = "Genki" });
        //}

        public async Task<Pet> AddorUpdatePet(Pet pet)
        {
            if (pet.Id == 0)
            {
                await _conn.InsertAsync(pet);
            }
            else
            {
                await _conn.UpdateAsync(pet);
            }
            return pet;
        }

        public async Task<List<Pet>> GetPets()
        {
            return await _conn.Table<Pet>().ToListAsync();
        }
        public async Task<Pet> GetPet(int Id)
        {
            return await _conn.Table<Pet>().FirstAsync();
        }

        public async Task<List<HeartRecord>> GetHeartRecords(int petId = 0)
        {
            if (petId == 0)
            {
                return await _conn.Table<HeartRecord>().ToListAsync();
            }
            return await _conn.Table<HeartRecord>().Where(w => w.PetId == petId).ToListAsync();
        }

        public async Task<HeartRecord> AddHeartRecord(HeartRecord heartRecord)
        {
            await _conn.InsertAsync(heartRecord);
            return heartRecord;
        }

        public class HeartRecord
        {
            [PrimaryKey, AutoIncrement]
            private int Id { get; set; }
            public int Count { get; set; }
            public DateTime DateTime { get; set; }
                = DateTime.Now;

            [ForeignKey(typeof(Pet))]
            public int PetId { get; set; }
            [OneToMany(CascadeOperations = CascadeOperation.CascadeRead)]
            public Pet Pet { get; set; } = new Pet();
        }

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
}
