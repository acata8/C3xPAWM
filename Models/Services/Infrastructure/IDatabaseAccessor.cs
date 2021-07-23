using System;
using System.Data;
using System.Threading.Tasks;

namespace C3xPAWM.Models.Services.Infrastructure
{
    public interface IDatabaseAccessor
    {
        Task<DataSet> BasicQueryAsync(string q, string citta);
        Task<DataSet> BasicQueryAsync(string q);
    }
}