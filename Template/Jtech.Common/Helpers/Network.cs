using Microsoft.AspNetCore.Http;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.Helpers
{
    public static class Network
    {
        private const string NullIpAddress = "::1";

        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        public static IPAddress[] GetIPAddr(string hostName)
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addr = ipEntry.AddressList;
            return addr;
        }

        public static bool IsLocal(this HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress.IsSet())
            {
                //We have a remote address set up
                return connection.LocalIpAddress.IsSet()
                    //Is local is same as remote, then we are local
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    //else we are remote if the remote IP address is not a loopback address
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }

            return true;
        }
        private static bool IsSet(this IPAddress address)
        {
            return address != null && address.ToString() != NullIpAddress;
        }

    }
}
