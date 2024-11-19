using SQLite;



namespace GenkiHeartChecker.Components
{
    public class DBService
    {
        private SQLiteAsyncConnection _conn;


        public async Task InitDbAsync()
        {
            if (_conn != null)
                return;
            _conn = new SQLiteAsyncConnection(MauiProgram.DbPath, MauiProgram.Flags);
            if (false)
            {
                await _conn.DropTableAsync<Pet>();
                await _conn.DropTableAsync<HeartRecord>();
            }


            await _conn.CreateTableAsync<Pet>();
            await _conn.CreateTableAsync<HeartRecord>();

            await InitDefaultPet();
        }


        public async Task InitDefaultPet()
        {
            if (await _conn.Table<Pet>().CountAsync() > 0)
                return;
            await AddorUpdatePet(new Pet() { PetName = "Genki" });
        }

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
            await InitDbAsync();
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
    }
}
