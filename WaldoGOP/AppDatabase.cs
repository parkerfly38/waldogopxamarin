using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WaldoGOP.Models;
using SQLite;

namespace WaldoGOP
{
    public class AppDatabase
    {
        readonly SQLiteAsyncConnection database;

        public AppDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Models.AppData>().Wait();
            database.CreateTableAsync<Models.Volunteer>().Wait();
            database.CreateTableAsync<Models.MaineVoter>().Wait();
            database.CreateTableAsync<Models.Event>().Wait();
            database.CreateTableAsync<Models.MyWalkList>().Wait();
        }

        public Task<List<Models.MyWalkList>> GetMyWalkListsAsync()
        {
            return database.Table<Models.MyWalkList>().ToListAsync();
        }

        public void AddWalkLists(List<Models.MyWalkList> walkList)
        {
            database.DropTableAsync<Models.MyWalkList>().Wait();
            database.CreateTableAsync<Models.MyWalkList>().Wait();
            database.InsertAllAsync(walkList).Wait();
        }

        public Task<List<Event>> GetEventDataAsync()
        {
            return database.Table<Event>().ToListAsync();
        }

        public Task<Event> GetEventAsync(int eventId)
        {
            return database.Table<Event>().Where(x => x.eventID == eventId).FirstOrDefaultAsync();
        }

        public void UpdateEvents(List<Event> events)
        {
            database.DropTableAsync<Event>().Wait();
            database.CreateTableAsync<Event>().Wait();
            database.InsertAllAsync(events).Wait();
        }

        public Task<AppData> GetAppDataAsync()
        {
            return database.Table<AppData>().FirstOrDefaultAsync();
        }

        public Task<List<Volunteer>> GetVolunteerDataAsync()
        {
            return database.Table<Volunteer>().ToListAsync();
        }

        public Task<List<MaineVoter>> GetVoterDataAsync()
        {
            return database.Table<MaineVoter>().ToListAsync();
        }

        public Task<MaineVoter> GetVoterByKeyAsync(string voterkey)
        {
            return database.Table<MaineVoter>().Where(x => x.VoterKey == voterkey).FirstOrDefaultAsync();
        }

        public Task<Volunteer> GetVolunteerByIdAsync(int id)
        {
            return database.Table<Volunteer>().Where(x => x.VolunteerID == id).FirstOrDefaultAsync();
        }

        public void UpdateAppData(Models.AppData appdata)
        {
            database.DropTableAsync<AppData>().Wait();
            database.CreateTableAsync<AppData>().Wait();
            database.InsertOrReplaceAsync(appdata).Wait();
        }
    }
}
