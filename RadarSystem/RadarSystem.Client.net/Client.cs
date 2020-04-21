using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace RadarSystem.Client.net
{
    
    public class Client : BaseScript
    {
        public List<string> Database;
        public List<int> blips = new List<int>();
        public string SelfDatabase = null;
        public bool showradar = false;
        public bool isStartingRadar = false;
        public bool isStopingRadar = false;
        public Client()
        {
            EventHandlers["Cyber:SelfHandle:dataString"] += new Action<string>(Handle);
            EventHandlers["Cyber:Client:GetBlipData"] += new Action<string>(HandleDB);
            Tick += DrawBlip;
            Tick += AdjustR;
            Tick += CheckInput;
        }
        public int R = 0;
        public async Task CheckInput()
        {
            while(IsStringNullOrEmpty(SelfDatabase))
            {
                await Delay(0);
            }
            if(Divison(SelfDatabase) == "5")
            {
                if(Game.PlayerPed.IsInVehicle())
                {
                    Vehicle veh = Game.PlayerPed.CurrentVehicle;
                    if(veh.Model == VehicleHash.Brickade && veh.Driver == Game.PlayerPed)
                    {
                        if(!showradar)
                        {
                            if (!isStartingRadar)
                            {
                                SetTextComponentFormat("STRING");
                                AddTextComponentString("Press ~INPUT_REPLAY_SAVE~ To initiate Radar System.");
                                DisplayHelpTextFromStringLabel(0, false, true, -1);
                                if(IsControlJustPressed(1, 318))
                                {
                                    isStartingRadar = true;
                                    FreezeEntityPosition(veh.Handle, true);
                                    Exports["progressBars"].startUI(10000, "Initiating Radar System");
                                    await Delay(10000);
                                    isStartingRadar = false;
                                    showradar = true;
                                }
                            }
                        }
                        if(showradar)
                        {
                            if(!isStopingRadar)
                            {
                                SetTextComponentFormat("STRING");
                                AddTextComponentString("Press ~INPUT_REPLAY_SAVE~ To Stop Radar.");
                                DisplayHelpTextFromStringLabel(0, false, true, -1);
                                if (IsControlJustPressed(1, 318))
                                {
                                    isStopingRadar = true;
                                    showradar = false;
                                    Exports["progressBars"].startUI(10000, "Stopping Radar System");
                                    await Delay(10000);
                                    isStopingRadar = false;
                                    FreezeEntityPosition(veh.Handle, false);
                                    
                                }


                            }
                        }


                    }
                }
                else
                {
                    showradar = false;
                }









            }




        }


        public async Task AdjustR()
        {
            if (showradar)
            {
                if (R > 330)
                {
                    R = 0;
                }
                else
                {
                    R = R + 5;
                }
            }
        }
        public async Task DrawBlip()
        {
            while (IsStringNullOrEmpty(SelfDatabase))
            {
                await Delay(0);
            }
            while (Database == null)
            {
                await Delay(0);
            }
            foreach (int blip in blips.ToList())
            {
                int safeblip = blip;
                RemoveBlip(ref safeblip);
                blips.Remove(blip);
            }
            if (showradar)
            {
                Blip testing = World.CreateBlip(Game.PlayerPed.Position, 3000);
                SetBlipSprite(testing.Handle, 1);
                SetBlipColour(testing.Handle, 26);
                testing.Alpha = 100;
                SetBlipDisplay(testing.Handle, 4);
                blips.Add(testing.Handle);
                // Routing Blip
                Blip radar = World.CreateBlip(Game.PlayerPed.Position, R);
                SetBlipSprite(radar.Handle, 9);
                SetBlipColour(radar.Handle, 6);
                radar.Alpha = 150;
                SetBlipDisplay(radar.Handle, 4);
                blips.Add(radar.Handle);
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
                                if (row[4] == serverid.ToString())
                                {
                                    if (Team(line) == "1")
                                    {

                                        Vector3 pos = GetEntityCoords(plyr.Character.Handle, true);
                                        Vector3 radarpos = Game.PlayerPed.Position;
                                        if (GetDistanceBetweenCoords(pos.X, pos.Y, pos.Z, radarpos.X, radarpos.Y, radarpos.Z, false) < 330)
                                        {


                                            if (plyr.Character.IsInVehicle())
                                            {
                                                Vehicle vehc = plyr.Character.CurrentVehicle;
                                                if (vehc.Model == VehicleHash.Cargobob || vehc.Model == VehicleHash.Cargobob2 || vehc.Model == VehicleHash.Cargobob3 || vehc.Model == VehicleHash.Cargobob4 || vehc.Model == VehicleHash.Titan || vehc.Model == VehicleHash.Lazer || vehc.Model == VehicleHash.Hydra || vehc.Model == 0x64DE07A1 || vehc.Model == VehicleHash.Savage)
                                                {
                                                    Blip blip = World.CreateBlip(pos);
                                                    SetBlipSprite(blip.Handle, 1);
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
                                                    SetBlipDisplay(blip.Handle, 2);
                                                    blip.Name = ICname(line);
                                                    blip.Scale = 0.5f;
                                                    blip.Alpha = 200;
                                                    blip.Color = BlipColor.Red;
                                                    blip.Rotation = (int)GetEntityHeading(plyr.Character.Handle);
                                                    ShowHeadingIndicatorOnBlip(blip.Handle, true);
                                                    blips.Add(blip.Handle);
                                                }

                                            }

                                        }
                                    }
                                }
                            }
                        }


                    }
                }
                if (Team(SelfDatabase) == "1")
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
                                    if (Team(line) == "0")
                                    {

                                        Vector3 pos = GetEntityCoords(plyr.Character.Handle, true);
                                        Vector3 radarpos = Game.PlayerPed.Position;
                                        if (GetDistanceBetweenCoords(pos.X, pos.Y, pos.Z, radarpos.X, radarpos.Y, radarpos.Z, false) < 330)
                                        {


                                            if (plyr.Character.IsInVehicle())
                                            {
                                                Vehicle vehc = plyr.Character.CurrentVehicle;
                                                if (vehc.Model == VehicleHash.Cargobob || vehc.Model == VehicleHash.Cargobob2 || vehc.Model == VehicleHash.Cargobob3 || vehc.Model == VehicleHash.Cargobob4 || vehc.Model == VehicleHash.Titan || vehc.Model == VehicleHash.Lazer || vehc.Model == VehicleHash.Hydra || vehc.Model == 0x64DE07A1 || vehc.Model == VehicleHash.Savage)
                                                {
                                                    Blip blip = World.CreateBlip(pos);
                                                    SetBlipSprite(blip.Handle, 1);
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
                                                    SetBlipDisplay(blip.Handle, 2);
                                                    blip.Name = ICname(line);
                                                    blip.Scale = 0.5f;
                                                    blip.Alpha = 200;
                                                    blip.Color = BlipColor.Red;
                                                    blip.Rotation = (int)GetEntityHeading(plyr.Character.Handle);
                                                    ShowHeadingIndicatorOnBlip(blip.Handle, true);
                                                    blips.Add(blip.Handle);
                                                }

                                            }

                                        }
                                    }
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
        public string Divison(string data)
        {
            string[] row = data.Split(',');
            return row[3];
        }
        public string ICname(string data)
        {
            string[] row = data.Split(',');
            return row[1];
        }
    }
}
