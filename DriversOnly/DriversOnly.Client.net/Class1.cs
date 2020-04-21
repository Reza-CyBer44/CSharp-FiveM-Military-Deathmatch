using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace DriversOnly.Client.net
{
    public class Class1 : BaseScript
    {
        public string SelfDatabase;
        public bool checkspam = false;
        public Class1()
        {
            EventHandlers["Cyber:SelfHandle:dataString"] += new Action<string>(handle);
            Tick += VehicleChecker;
        }
        public void handle(string db)
        {
            SelfDatabase = db;
        }
        public async Task VehicleChecker()
        {
            if (!IsStringNullOrEmpty(SelfDatabase))
            {
                int ped = PlayerPedId();
                if (IsPedInAnyVehicle(ped, false))
                {
                    int vehicle = GetVehiclePedIsIn(ped, false);
                    if (IsVehicleModel(vehicle, (uint)VehicleHash.Rhino) || IsVehicleModel(vehicle, (uint)VehicleHash.Barracks3) || IsVehicleModel(vehicle, (uint)VehicleHash.Insurgent3))
                    {
                        if (GetDivison(SelfDatabase) != "5")
                        {
                            FreezeEntityPosition(vehicle, true);
                            if (!checkspam)
                            {
                                TriggerEvent("chat:addMessage", new
                                {
                                    color = new[] {255, 255,0 },
                                    multiline = true,
                                    args = new[] { "Vehicle Control System", "You Cant Ride This vehicle" }
                                });
                                checkspam = true;
                            }                            
                        }
                        else
                        {
                            FreezeEntityPosition(vehicle, false);
                        }
                    }
                    if (IsVehicleModel(vehicle, (uint)VehicleHash.Lazer) || IsVehicleModel(vehicle, (uint)VehicleHash.Hydra) || IsVehicleModel(vehicle, (uint)VehicleHash.Titan) || IsVehicleModel(vehicle, (uint)VehicleHash.Cargobob) || IsVehicleModel(vehicle, (uint)VehicleHash.Cargobob4) || IsVehicleModel(vehicle, 0x64DE07A1))
                    {
                        if (GetDivison(SelfDatabase) != "6" && GetDivison(SelfDatabase) != "8")
                        {
                            FreezeEntityPosition(vehicle, true);
                            if (!checkspam)
                            {
                                TriggerEvent("chat:addMessage", new
                                {
                                    color = new[] {255, 255, 0 },
                                    multiline = true,
                                    args = new[] { "Vehicle Control System","You Cant Ride This vehicle" }
                                });
                                checkspam = true;

                            }
                        }
                        else
                        {
                            FreezeEntityPosition(vehicle, false);
                        }
                    }

                }
                else
                {
                    checkspam = false;
                }




            }
            await Delay(0);
        }

        public string GetDivison(string data)
        {

            string[] row = data.Split(',');
            return row[3];
        }

    }
}
