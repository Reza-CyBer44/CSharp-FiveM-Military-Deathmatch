using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Blips.Client.net
{
    public class Client : BaseScript
    {
        public List<string> Database;
        public List<int> blips = new List<int>();
        public string SelfDatabase = null;
        public Client()
        {
            EventHandlers["Cyber:SelfHandle:dataString"] += new Action<string>(Handle);
            EventHandlers["Cyber:Client:GetBlipData"] += new Action<string>(HandleDB);
            
                Tick += DrawBlips;
        }
        public async Task DrawBlips()
        {
           
            while (IsStringNullOrEmpty(SelfDatabase))
            {
                await Delay(0);
            }
            while (Database.Count < 0)
            {
                await Delay(0);
            }
            if (blips != null)
            {
                foreach (int blip in blips.ToList())
                {
                    if (DoesBlipExist(blip))
                    {
                        int safeblip = blip;
                        RemoveBlip(ref safeblip);
                        blips.Remove(blip);
                    }

                }
            }
            if (Team(SelfDatabase) == "0")
            {
                
                foreach (Player plyr in new PlayerList())
                {
                    int serverid = plyr.ServerId;
                    if (serverid != GetPlayerServerId(PlayerId()))
                    {
                        foreach (string line in Database)
                        {
                            string[] row = line.Split(',');
                            if (row[4] != serverid.ToString())
                            {
                                if (Team(line) == "0")
                                {
                                    Vector3 pos = GetEntityCoords(plyr.Character.Handle, true);
                                    Blip blip = World.CreateBlip(pos);
                                    SetBlipSprite(blip.Handle, 1);
                                    ShowHeadingIndicatorOnBlip(blip.Handle, true);
                                    if (plyr.Character.IsInVehicle())
                                    {
                                        ShowHeadingIndicatorOnBlip(blip.Handle, false);
                                        Vehicle vehc = plyr.Character.CurrentVehicle;
                                        if (vehc.Model == VehicleHash.Rhino)
                                        {
                                            blip.Sprite = BlipSprite.Tank;

                                        }
                                        if (vehc.Model == VehicleHash.Insurgent2)
                                        {
                                            blip.Sprite = BlipSprite.GunCar;
                                        }
                                        if (vehc.Model == VehicleHash.Barracks3)
                                        {
                                            SetBlipSprite(blip.Handle, 67);
                                        }
                                        if (vehc.Model == VehicleHash.Cargobob || vehc.Model == VehicleHash.Cargobob2 || vehc.Model == VehicleHash.Cargobob3 || vehc.Model == VehicleHash.Cargobob4)
                                        {
                                            SetBlipSprite(blip.Handle, 481);
                                        }
                                        if (vehc.Model == VehicleHash.Titan)
                                        {
                                            SetBlipSprite(blip.Handle, 583);
                                        }
                                        if (vehc.Model == VehicleHash.Lazer || vehc.Model == VehicleHash.Hydra)
                                        {
                                            SetBlipSprite(blip.Handle, 585);
                                        }
                                        if (vehc.Model == 0x64DE07A1)
                                        {
                                            SetBlipSprite(blip.Handle, 573);
                                        }
                                        if (vehc.Model == VehicleHash.Savage)
                                        {
                                            SetBlipSprite(blip.Handle, 602);
                                        }

                                    }
                                    SetBlipDisplay(blip.Handle, 2);
                                    
                                    blip.Name = ICname(line);
                                    blip.Scale = 0.5f;
                                    blip.Alpha = 200;
                                    blip.Color = BlipColor.Blue;
                                    blip.Rotation = (int)GetEntityHeading(plyr.Character.Handle);
                                    blips.Add(blip.Handle);
                                }
                            }
                        }
                    }


                }






            }
            else if (Team(SelfDatabase) == "1")
            {
                
                foreach (Player plyr in new PlayerList())
                {
                    int serverid = plyr.ServerId;
                    if (serverid != GetPlayerServerId(PlayerId()))
                    {
                        foreach (string line in Database)
                        {
                            string[] row = line.Split(',');
                            if (row[4] == serverid.ToString())
                            {
                                if (Team(line) == "1")
                                {
                                    Vector3 pos = GetEntityCoords(plyr.Character.Handle, true);
                                    Blip blip = World.CreateBlip(pos);
                                    SetBlipSprite(blip.Handle, 1);

                                    if (plyr.Character.IsInVehicle())
                                    {
                                        ShowHeadingIndicatorOnBlip(blip.Handle, false);
                                        Vehicle vehc = plyr.Character.CurrentVehicle;
                                        if (vehc.Model == VehicleHash.Rhino)
                                        {
                                            blip.Sprite = BlipSprite.Tank;

                                        }
                                        if (vehc.Model == VehicleHash.Insurgent2)
                                        {
                                            blip.Sprite = BlipSprite.GunCar;
                                        }
                                        if (vehc.Model == VehicleHash.Barracks3)
                                        {
                                            SetBlipSprite(blip.Handle, 67);
                                        }
                                        if (vehc.Model == VehicleHash.Cargobob || vehc.Model == VehicleHash.Cargobob2 || vehc.Model == VehicleHash.Cargobob3 || vehc.Model == VehicleHash.Cargobob4)
                                        {
                                            SetBlipSprite(blip.Handle, 481);
                                        }
                                        if (vehc.Model == VehicleHash.Titan)
                                        {
                                            SetBlipSprite(blip.Handle, 583);
                                        }
                                        if (vehc.Model == VehicleHash.Lazer || vehc.Model == VehicleHash.Hydra)
                                        {
                                            SetBlipSprite(blip.Handle, 585);
                                        }
                                        if (vehc.Model == 0x64DE07A1)
                                        {
                                            SetBlipSprite(blip.Handle, 573);
                                        }
                                        if (vehc.Model == VehicleHash.Savage)
                                        {
                                            SetBlipSprite(blip.Handle, 602);
                                        }

                                    }
                                    SetBlipDisplay(blip.Handle, 2);

                                    blip.Name = ICname(line);
                                    blip.Scale = 0.5f;
                                    blip.Alpha = 200;
                                    blip.Color = BlipColor.Red;
                                    blip.Rotation = Ceil(GetEntityHeading(plyr.Character.Handle));
                                    blips.Add(blip.Handle);
                                }
                            }
                        }
                    }


                }

            }









        }












        public void HandleDB(string single)
        {
            string[] result = single.Split('-');
            List<string> lines = new List<string>(result); 
            foreach (string line in lines.ToList())
            {
                string[] row = line.Split(',');
                if (String.IsNullOrWhiteSpace(row[4]))
                {
                    lines.Remove(line);
                }
            }
            
            Database = lines;
        }
        public void Handle(string db)
        {
            SelfDatabase = db;
        }
        public string Team(string data)
        {
            string[] row = data.Split(',');
            return row[2];
        }
        public string ICname(string data)
        {
            string[] row = data.Split(',');
            return row[1];
        }
    }
}
