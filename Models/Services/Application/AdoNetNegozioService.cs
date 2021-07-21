using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using C3xPAWM.Models.Services.Infrastructure;
using C3xPAWM.Models.ViewModel;

namespace C3xPAWM.Models.Services.Application
{
    public class AdoNetNegoziService : INegoziService
    {
        private readonly IDatabaseAccessor db;

        public AdoNetNegoziService(IDatabaseAccessor db)
        {
            this.db = db;

        }

        public async Task<List<NegozioViewModel>> getNegozi()
        {
            string q = "SELECT nome, citta, via, telefono, tipologia FROM Negozi";
            DataSet dataSet = await db.BasicQueryAsync(q);
            var dataTable = dataSet.Tables[0];
            var listaNegozi = new List<NegozioViewModel>();
            foreach(DataRow negozioRow in dataTable.Rows){
                NegozioViewModel negozio = NegozioViewModel.FromDataRow(negozioRow);
                listaNegozi.Add(negozio);
            }
            return listaNegozi;
        }

        public List<NegozioViewModel> getNegoziByCitta(string citta)
        {
            //string q = "SELECT nome, citta, via, telefono, tipologia FROM Negozi WHERE citta = @citta";
            
            throw new System.NotImplementedException();
        }
    }
}