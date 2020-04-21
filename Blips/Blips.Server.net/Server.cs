using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.IO;

namespace Blips.Server.net
{
    public class Server : BaseScript
    {
        public Server()
        {
            Tick += SendDataList;
        }
        public async Task SendDataList()
        {
            string filepath = @".\\WhiteList.txt";
            List<string> lines = File.ReadAllLines(filepath).ToList();
            string final = string.Join("-", lines);
            TriggerClientEvent("Cyber:Client:GetBlipData", final);
            await Delay(0);

        }



    }



}
