using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web3_tp_final.Interface
{
    public interface IUserConnectionManager
    {
        void KeepUserConnection(string userId, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(string userId);

        void KeepUserIdentifier(string userId, string connectionId);
        List<string> GetUserIdentifiers(string userId);

    }
}
