using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web3_tp_final.Interface;

namespace web3_tp_final.Hubs
{
    public class UserConnectionManager : IUserConnectionManager
    {
        private static Dictionary<string, List<string>> userConnectionMap = new Dictionary<string, List<string>>();
        private static string userConnectionMapLocker = string.Empty;

        private static Dictionary<string, List<string>> userIdentifierMap = new Dictionary<string, List<string>>();
        private static string userIdentifierMapLocker = string.Empty;

        public List<string> GetUserConnections(string userId)
        {
            var conn = new List<string>();
            lock (userConnectionMapLocker)
            {
                conn = userConnectionMap[userId];
            }

            return conn;
        }

        public List<string> GetUserIdentifiers(string userId)
        {
            var identifier = new List<string>();
            lock (userIdentifierMapLocker)
            {
                identifier = userIdentifierMap[userId];
            }

            return identifier;
        }
        public void KeepUserIdentifier(string userId, string identifier)
        {
            lock (userIdentifierMapLocker)
            {
                if (!userIdentifierMap.ContainsKey(userId))
                {
                    userIdentifierMap[userId] = new List<string>();
                }
                userConnectionMap[userId].Add(identifier);
            }
        }

        public void KeepUserConnection(string userId, string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                if (!userConnectionMap.ContainsKey(userId))
                {
                    userConnectionMap[userId] = new List<string>();
                }
                userConnectionMap[userId].Add(connectionId);
            }
        }

        public void RemoveUserConnection(string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                foreach (var userId in userConnectionMap.Keys)
                {
                    if (userConnectionMap.ContainsKey(userId))
                    {
                        if (userConnectionMap[userId].Contains(connectionId))
                        {
                            userConnectionMap[userId].Remove(connectionId);
                            break;
                        }
                    }
                }
            }
        }
    }
}
