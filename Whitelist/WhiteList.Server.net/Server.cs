using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using System.IO;

namespace WhiteList.Server.net
{
    public class Server : BaseScript
    {
        public Server()
        {
            EventHandlers["playerConnecting"] += new Action<Player, string, dynamic, dynamic>(OnPlayerConnecting);
            EventHandlers["playerDropped"] += new Action<Player, string>(OnPlayerDropped);
            EventHandlers["SetPlayerID"] += new Action<Player, string>(SetPlayerID);
            EventHandlers["SetPlayerTeam"] += new Action<Player, string>(SetPlayerTeam);
            Tick += CheckPlayerTeam;
        }
        public async Task CheckPlayerTeam()
        {
            string filepath = @".\\WhiteList.txt";
            List<string> lines = File.ReadAllLines(filepath).ToList();
           
            foreach (string line in lines)
            {
                string[] row = line.Split(',');
                if (row[2] == "3")
                {
                    foreach(Player plyr in new PlayerList())
                    {
                        if(plyr.Identifiers["steam"] == row[0])
                        {
                            plyr.TriggerEvent("NeedToChangeTeam", true);
                        }
                    }
                }
                if (row[2] != "3")
                {
                    foreach (Player plyr in new PlayerList())
                    {
                        if (plyr.Identifiers["steam"] == row[0])
                        {
                            plyr.TriggerEvent("NeedToChangeTeam", false);
                        }
                    }
                }
            }
        }
        public void SetPlayerTeam([FromSource]Player plyr,string team)
        {
            string filepath = @".\\WhiteList.txt";
            List<string> lines = File.ReadAllLines(filepath).ToList();
            var steam = plyr.Identifiers["steam"];
            foreach (string line in lines)
            {
                string[] row = line.Split(',');
                if (row[0] == steam)
                {
                    row[2] = team;
                    string final = String.Join(",", row);
                    lines.Remove(line);
                    lines.Add(final);
                    File.WriteAllLines(filepath, lines);
                    break;
                }
            }
        }
        public void SetPlayerID([FromSource]Player plyr,string id)
        {
            string filepath = @".\\WhiteList.txt";
            List<string> lines = File.ReadAllLines(filepath).ToList();
            var steam = plyr.Identifiers["steam"];
            foreach (string line in lines)
            {
                string[] row = line.Split(',');
                if (row[0] == steam)
                {
                    row[4] = id;
                    string final = String.Join(",", row);
                    lines.Remove(line);
                    lines.Add(final);
                    File.WriteAllLines(filepath, lines);
                    break;
                }
            }

        }
        private void OnPlayerDropped([FromSource]Player player, string reason)
        {
           
            string filepath = @".\\WhiteList.txt";
            List<string> lines = File.ReadAllLines(filepath).ToList();
            var steam = player.Identifiers["steam"];
            foreach (string line in lines)
            {
                string[] row = line.Split(',');
                if (row[0] == steam)
                {
                    row[4] = "";
                    string final = String.Join(",", row);
                    lines.Remove(line);
                    lines.Add(final);
                    File.WriteAllLines(filepath, lines);
                    break;
                }
            }
        }
        private void OnPlayerConnecting([FromSource]Player player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            bool whitelisted = IsPlayerWhiteListed(player);
            if(!whitelisted)
            {
                CreatePlayerRow(player);
            }
            
        }
        public void CreatePlayerRow(Player plyr)
        {
            
            string filepath = @".\\WhiteList.txt";
            List<string> lines = File.ReadAllLines(filepath).ToList();
            string steamhex = plyr.Identifiers["steam"];
            string final = steamhex + "," + plyr.Name + "," + "3" + "," + "0" + ",";
            lines.Add(final);
            File.WriteAllLines(filepath, lines);
        }
        private bool IsPlayerWhiteListed(Player plyr)
        {
            bool final = false;
            string filepath = @".\\WhiteList.txt";
            List<string> lines = File.ReadAllLines(filepath).ToList();
            var hex = plyr.Identifiers["steam"];
            foreach(string line in lines)
            {
                string[] row = line.Split(',');
                if(hex == row[0])
                {
                    return true;
                 
                }

            }

            return final;
        }

        public int id(Player plyr)
        {
            int ids = 0;
            for(; ; )
            {
                Player tplyr = new PlayerList()[ids];
                if(tplyr == plyr)
                {
                    return ids;
                }
                else
                {
                   
                    ids++;
                }
            }

        }
    }
}
