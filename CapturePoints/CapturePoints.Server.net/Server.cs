using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.IO;






namespace CapturePoints.Server.net
{
    public class Server : BaseScript
    {
        public Vector3 Capturecord = new Vector3(488.64f, -3345.04f, 7f);
        public string status = "nothing";
        public int usa = 0;
        public int russia = 0;
        public List<Player> russiaC = new List<Player>();
        public List<Player> usaC = new List<Player>();
        public Server()
        {
            EventHandlers["Cyber:EnteredCapturePoint"] += new Action<Player>(EnteredCapture);
            EventHandlers["Cyber:LeavedCapturePoint"] += new Action<Player>(LeavedCapture);
            Tick += SendEvents;
            Tick += HandlePoints;
            
        }
        public async Task HandlePoints()
        {
            await Delay(1000);
            int russiacount = russiaC.Count;
            int usacount = usaC.Count;
            if(russiacount > 0 && usacount == 0)
            {
                status = "red";
                russia++;
            }
            else if(russiacount == 0 && usacount>0)
            {
                status = "blue";
                usa++;
            }
            else if(russiacount > 0 && usacount > 0)
            {
                status = "draw";
            }
            else if(russiacount == 0 && usacount == 0)
            {
                status = "nothing";
            }



        }
        public void LeavedCapture([FromSource]Player plyr)
        {
            string team = GetPlayerTeam(plyr);
            if (team == "russia")
            {
                if (russiaC.Contains(plyr))
                {
                    russiaC.Remove(plyr);
                }
            }
            
            else if (team == "usa")
            {
                if (usaC.Contains(plyr))
                {
                    usaC.Remove(plyr);
                }
            }
        }
        public void EnteredCapture([FromSource]Player plyr)
        {
            string team = GetPlayerTeam(plyr);
            if(team == "russia")
            {
                if (!russiaC.Contains(plyr))
                {
                    russiaC.Add(plyr);
                }
            }
            else if(team == "usa")
            {
                if (!usaC.Contains(plyr))
                {
                    usaC.Add(plyr);
                }
            }
        }
        public string GetPlayerTeam(Player plyr)
        {
            string filepath = @".\\WhiteList.txt";
            List<string> lines = File.ReadAllLines(filepath).ToList();
            var hex = plyr.Identifiers["steam"];
            foreach(string line in lines)
            {
                string[] row = line.Split(',');
                if(row[0] == hex)
                {
                    if(row[2] == "0")
                    {
                        return "usa";
                    }
                    else if(row[2] == "1")
                    {
                        return "russia";
                    }
                }
            }
            return null;
        }
        public async Task SendEvents()
        {
            TriggerClientEvent("Cyber:Usa:Points", usa);
            TriggerClientEvent("Cyber:Russia:Points", russia);
            TriggerClientEvent("Cyber:BlipStatus", status);
        }
    }
}
