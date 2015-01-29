using SSQLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SteamServerQueryPing
{
    class Program
    {
        static void Main(string[] args)
        {
            string address;
            int ssqport = -1, loopMaxCount = -1, loopCount = 0;
            SSQL server = null;
            IPAddress ip;
            IPEndPoint ipEndpoint;
            ServerInfo temp = null;
            DateTime tempDate, currentLoopStart;
            TimeSpan elapsed = new TimeSpan();
            try
            {
                address = args[0];
                ip = IPAddress.Parse(address);
                ssqport = Int32.Parse(args[1]);
                if (args.Length >= 3) Int32.TryParse(args[2], out loopMaxCount);
                ipEndpoint = new IPEndPoint(ip, ssqport);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Invalid parameters, expect the following:");
                Console.Out.WriteLine("[Server IP Address] [Steam Server Query Port] [Times To Loop]");
                Console.Out.WriteLine("[Server IP Address] must be a valid string representation for the IPv4 address of the server.");
                Console.Out.WriteLine("[Steam Server Query Port] must be a valid string representation for the Steam Server Query Port of the server (usually game port + 1.");
                Console.Out.WriteLine("[Times To Loop] is optional, will be used as the number of times to ping the server. Will ping indefinately if omitted or a negitive integer is given.");
                Console.Out.WriteLine("");
                Console.Out.WriteLine(ex.Message);
                Console.Out.WriteLine(ex.StackTrace.ToString());
                /*
                Console.Out.WriteLine("Press enter to continue/close.");
                Console.In.ReadLine();
                //*/
                return;
            }

            while (loopMaxCount != loopCount++)
            {
                currentLoopStart = DateTime.UtcNow;
                if (server == null)
                {
                    server = new SSQL(ipEndpoint);
                }

                try
                {
                    tempDate = DateTime.UtcNow;
                    temp = server.Server();
                    elapsed = DateTime.UtcNow.Subtract(tempDate);
                }
                catch
                {
                    temp = null;
                }
                if (temp == null)
                {
                    Console.Out.WriteLine(DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToLongTimeString() + ": Server Failed To Respond");
                }
                else
                {
                    Console.Out.WriteLine(DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToLongTimeString() + ": " + temp.Name + ", " + " " + temp.Game + ", " + temp.Map + ", " + temp.IP + ":" + temp.Port + ", " + temp.PlayerCount + "/" + temp.MaxPlayers + ", " + temp.Version + ", " + elapsed.TotalMilliseconds + "ms");
                }
                System.Threading.Thread.Sleep(1000 - DateTime.UtcNow.Subtract(currentLoopStart).Milliseconds);
            }

        }
    }
}
