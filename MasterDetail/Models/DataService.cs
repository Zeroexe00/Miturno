
namespace MasterDetail.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interface;
    using SQLite;
    using Xamarin.Forms;


    public class DataService
    {
        private SQLiteAsyncConnection connection;

        public DataService()
        {
            this.OpenOrCreateDB();
        }
        private async Task OpenOrCreateDB()
        {
            var databasepath = DependencyService.Get<IPathService>().GetDataBasePath();
            this.connection = new SQLiteAsyncConnection(databasepath);
            await connection.CreateTableAsync<EmpaqueModel>().ConfigureAwait(false);
            await connection.CreateTableAsync<Turnos>().ConfigureAwait(false);
            await connection.CreateTableAsync<TraceabilityWorkShift>().ConfigureAwait(false);
        }

        public async Task Insert<T>(T model)
        {
            await this.connection.InsertAsync(model);
        }
        public async Task Insert<T>(List<T> models)
        {
            await this.connection.InsertAllAsync(models);
        }
        public async Task Update<T>(T model)
        {
            await this.connection.UpdateAsync(model);
        }
         public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }
        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        }
        public async Task<List<Turnos>> GetAllTurnos()
        {
            var query = await this.connection.QueryAsync<Turnos>("Select * from [Turnos]");
            var array = query.ToArray();
            var list = array.Select(p => new Turnos
            {
                Id = p.Id,
                Initiation = p.Initiation,
                Calification = p.Calification,
                Finished = p.Finished,
                MaximunCap = p.MaximunCap,
                TurnDate = p.TurnDate,
                TurnState = p.TurnState,
                WorkingDay = p.WorkingDay,

            }).ToList();
            return list;
        }
        public async Task<EmpaqueModel> GetEmpaque()
        {
            var query = await this.connection.QueryAsync<EmpaqueModel>("Select * from [EmpaqueModel]");
            var array = query.ToArray();
            var empaque = array.FirstOrDefault();
            return empaque;
        }
        public async Task DeleteAll()
        {
            var query = await this.connection.QueryAsync<EmpaqueModel>("delete from [EmpaqueModel]");
        }

    }

}
