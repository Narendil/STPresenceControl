using STPresenceControl.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace STPresenceControl.Contracts
{
    public interface IDataProvider
    {
        Task LoginAsync(string username, string password);
        Task<List<PresenceControlEntry>> GetPrensenceControlEntriesAsync(DateTime date);
    }
}
