using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using MenuAPI;

namespace Usa.Client.net
{
    public class Client : BaseScript
    {
        public string SelfDatabase;
        public Client()
        {
            RequestModels();
            MenuControllers();
            CreateMapBlips();
            EventHandlers["playerSpawned"] += new Action<dynamic>(Spawned);
            EventHandlers["Cyber:SelfHandle:dataString"] += new Action<string>(handle);
            Tick += gatherdata;
            Tick += DrawMarkers;
           
            
        }
        
        private async void CreateMapBlips()
        {
           
            while(string.IsNullOrEmpty(SelfDatabase))
            {
                await BaseScript.Delay(0);
            }
           
            int blip = AddBlipForCoord(-2341.1f, 3260f, 32.83f);
            SetBlipSprite(blip, 305);
            SetBlipDisplay(blip, 3);
            SetBlipScale(blip, 1.5f);
            SetBlipColour(blip, 26);
            BeginTextCommandSetBlipName("STRING");
            AddTextComponentString("Usa Military Base");
            EndTextCommandSetBlipName(blip);
            if(GetTeam(SelfDatabase) == "0")
            {
               //Add Divison Blip
                int dblip = AddBlipForCoord(-2352.58f, 3258.01f, 32.81f);
                SetBlipSprite(dblip, 175);
                SetBlipAsShortRange(dblip, true);
                SetBlipDisplay(dblip, 5);
                SetBlipAlpha(dblip, 200);
                SetBlipScale(dblip, 2.0f);
                SetBlipColour(dblip, 29);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("Change Division");
                EndTextCommandSetBlipName(dblip);
                //Add Aircraft Blip
                int ablip = AddBlipForCoord(-2138.35f, 3254.35f, 32.81f);
                SetBlipSprite(ablip, 307);
                SetBlipAsShortRange(ablip, true);
                SetBlipDisplay(ablip, 5);
                SetBlipAlpha(ablip, 200);
                SetBlipScale(ablip, 2.0f);
                SetBlipColour(ablip, 29);
                SetBlipSecondaryColour(ablip, 0, 0, 0);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("AirCraft");
                EndTextCommandSetBlipName(ablip);
                //Add tank blip
                int tblip = AddBlipForCoord(-2415.59f, 3302.37f, 32.83f);
                SetBlipSprite(tblip, 563);
                SetBlipAsShortRange(tblip, true);
                SetBlipDisplay(tblip, 5);
                SetBlipAlpha(tblip, 200);
                SetBlipScale(tblip, 2.0f);
                SetBlipColour(tblip, 29);
                SetBlipSecondaryColour(tblip, 0, 0, 0);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("Tank");
                EndTextCommandSetBlipName(tblip);

            }

        }

        bool outside1 = true;
        bool outside2 = true;
        bool outside3 = true;
        public async Task DrawMarkers()
        {
            
            while (string.IsNullOrEmpty(SelfDatabase))
            {
                await BaseScript.Delay(0);
            }
            
            if (GetTeam(SelfDatabase) == "0")
            {

                //Draw Change Division Marker
                Vector3 dmark = new Vector3(-2352.58f, 3258.01f, 31.825f);
                World.DrawMarker(MarkerType.HorizontalSplitArrowCircle, dmark, new Vector3(0f, 180f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0.7f, 0.7f, 1), System.Drawing.Color.FromArgb(0, 0, 0), false, false);
                Vector3 plyrcord = GetEntityCoords(GetPlayerPed(GetPlayerIndex()), true);
                float distance = GetDistanceBetweenCoords(dmark.X, dmark.Y, dmark.Z, plyrcord.X, plyrcord.Y, plyrcord.Z, false);
       
                if (distance < 1)
                {
                    SetTextComponentFormat("STRING");
                    AddTextComponentString("Press ~INPUT_TALK~ To Open Menu.");
                    DisplayHelpTextFromStringLabel(0, false, true, -1);
                    
                    if (IsControlJustPressed(1, 46))
                    {
                        outside1 = false;
                        MenuController.DontOpenAnyMenu = false;
                        dmenu.OpenMenu();
                        

                    }
                }
                if (distance > 1)
                {
                    outside1 = true;
                    if (outside1 && outside2 && outside3)
                    {
                        MenuController.DontOpenAnyMenu = true;
                    }

                }









             // Draw Tank Spawner
                if (GetDivison(SelfDatabase) == "5")
                {
                    Vector3 tmark = new Vector3(-2426.82f, 3300.48f, 32.98f);
                    World.DrawMarker(MarkerType.TruckSymbol, tmark, new Vector3(5f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(1.5f, 1.5f, 1), System.Drawing.Color.FromArgb(100, 0, 102, 255), true, true);
                    Vector3 tplyrcord = GetEntityCoords(GetPlayerPed(GetPlayerIndex()), true);
                    float sdistance = GetDistanceBetweenCoords(tmark.X, tmark.Y, tmark.Z, tplyrcord.X, tplyrcord.Y, tplyrcord.Z, true);
                    if (sdistance < 1)
                    {
                        SetTextComponentFormat("STRING");
                        AddTextComponentString("Press ~INPUT_TALK~ To Open Spawn Menu.");
                        DisplayHelpTextFromStringLabel(0, false, true, -1);
                        if (IsControlJustPressed(1, 46))
                        {
                            outside2 = false;
                            tmenu.OpenMenu();
                            MenuController.DontOpenAnyMenu = false;

                        }
                    }
                    if (sdistance > 1)
                    {
                        outside2 = true;
                        if (outside1 && outside2 && outside3)
                        {
                            MenuController.DontOpenAnyMenu = true;
                        }

                    }



                }
                if (GetDivison(SelfDatabase) == "8" || GetDivison(SelfDatabase) == "6")
                {
                    Vector3 amark = new Vector3(-2112.73f, 3238.85f, 32.5f);
                    World.DrawMarker(MarkerType.PlaneModel, amark, new Vector3(5f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(1.5f, 1.5f, 10), System.Drawing.Color.FromArgb(100,0, 102, 255), true, true);
                    Vector3 aplyrcord = GetEntityCoords(GetPlayerPed(GetPlayerIndex()), true);
                    float tdistance = GetDistanceBetweenCoords(amark.X, amark.Y, amark.Z, aplyrcord.X, aplyrcord.Y, aplyrcord.Z, true);
                    if (tdistance < 1)
                    {

                        SetTextComponentFormat("STRING");
                        AddTextComponentString("Press ~INPUT_TALK~ To Open Spawn Menu.");
                        DisplayHelpTextFromStringLabel(0, false, true, -1);
                        if (IsControlJustPressed(1, 46))
                        {
                            outside3 = false;
                            amenu.OpenMenu();
                            MenuController.DontOpenAnyMenu = false;

                        }
                    }
                    if (tdistance > 1)
                    {
                        outside3 = true;
                        if (outside1 && outside2 && outside3)
                        {
                            MenuController.DontOpenAnyMenu = true;
                        }
                    }


                    
                }

            }
            else
            {
                MenuController.DontOpenAnyMenu = true;
            }
        }
        public void handle(string db)
        {
            SelfDatabase = db;
        }
        public async Task gatherdata()
        {
            int id = GetPlayerIndex();
            Player plyr = new PlayerList()[id];
            TriggerServerEvent("Cyber:SelfCallback:dataString", plyr);
            await BaseScript.Delay(10);
        }
        public string GetDivison(string data)
        {
            string[] row = data.Split(',');
            return row[3];
        }
        public string GetTeam(string data)
        {
          
            string[] row = data.Split(',');
            return row[2];
        }
        public string GetName(string data)
        {
            string[] row = data.Split(',');
            return row[1];
        }
        public void UpdateDV(Player plyr, int dvnum)
        {
            int id = GetPlayerIndex();
            var ped = GetPlayerPed(id);
            if (dvnum == 1)
            {

                foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
                {

                    uint weaponhash = (uint)weapon;
                    SetPedAmmo(ped, weaponhash, 0);
                    if (HasPedGotWeapon(ped, weaponhash, false))
                    {
                        RemoveWeaponFromPed(ped, weaponhash);
                    }
                }
                uint unarmed = (uint)WeaponHash.Unarmed;
                GiveWeaponToPed(ped, unarmed, 1, false, false);
                uint combatpistol = (uint)WeaponHash.CombatPistol;
                uint specialcarbine = (uint)WeaponHash.SpecialCarbine;
                uint parachute = (uint)WeaponHash.Parachute;
                uint grenade = (uint)WeaponHash.Grenade;
                uint specialcarbinesuppresor = (uint)WeaponComponentHash.AtArSupp02;
                uint flashlightar = (uint)WeaponComponentHash.AtArFlsh;
                uint flashlightap = (uint)WeaponComponentHash.AtPiFlsh;
                uint specialcarbineclip = (uint)WeaponComponentHash.SpecialCarbineClip02;
                uint combatpistolsuppresor = (uint)WeaponComponentHash.AtPiSupp;
                uint combatpistolclip = (uint)WeaponComponentHash.CombatPistolClip02;
                GiveWeaponToPed(ped, combatpistol, 60, false, false);
                GiveWeaponToPed(ped, specialcarbine, 300, false, true);
                GiveWeaponToPed(ped, parachute, 1, false, false);
                GiveWeaponToPed(ped, grenade, 10, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Knife, 1, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Flashlight, 1, false, false);
                GiveWeaponComponentToPed(ped, specialcarbine, specialcarbineclip);
                // GiveWeaponComponentToPed(ped, specialcarbine, specialcarbinesuppresor);
                GiveWeaponComponentToPed(ped, specialcarbine, flashlightar);
                GiveWeaponComponentToPed(ped, combatpistol, flashlightap);
                GiveWeaponComponentToPed(ped, combatpistol, combatpistolclip);
               // GiveWeaponComponentToPed(ped, combatpistol, combatpistolsuppresor);

                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:1", plyr);
            }
            if (dvnum == 2)
            {
                foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
                {
                    uint weaponhash = (uint)weapon;
                    SetPedAmmo(ped, weaponhash, 0);
                    if (HasPedGotWeapon(ped, weaponhash, false))
                    {
                        RemoveWeaponFromPed(ped, weaponhash);
                    }
                }
                uint unarmed = (uint)WeaponHash.Unarmed;
                GiveWeaponToPed(ped, unarmed, 1, false, false);
                uint microsmg = (uint)WeaponHash.MicroSMG;
                uint rpg = (uint)WeaponHash.HomingLauncher;
                uint parachute = (uint)WeaponHash.Parachute;
                uint mines = (uint)WeaponHash.ProximityMine;
                uint grenade = (uint)WeaponHash.Grenade;
                uint machineclip = (uint)WeaponComponentHash.MicroSMGClip02;
                uint microsupp = (uint)WeaponComponentHash.AtArSupp02;
                uint flashlightar = (uint)WeaponComponentHash.AtPiFlsh;
                GiveWeaponToPed(ped, microsmg, 200, false, false);
                GiveWeaponToPed(ped, rpg, 12, false, true);
                GiveWeaponToPed(ped, parachute, 1, false, false);
                GiveWeaponToPed(ped, mines, 20, false, false);
                GiveWeaponToPed(ped, grenade, 10, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Knife, 1, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Flashlight, 1, false, false);
                GiveWeaponComponentToPed(ped, microsmg, machineclip);
                uint flashlightap = (uint)WeaponComponentHash.AtPiFlsh;
                uint combatpistolclip = (uint)WeaponComponentHash.CombatPistolClip02;
                uint combatpistol = (uint)WeaponHash.CombatPistol;
                GiveWeaponToPed(ped, combatpistol, 60, false, false);
                GiveWeaponComponentToPed(ped, combatpistol, flashlightap);
                GiveWeaponComponentToPed(ped, combatpistol, combatpistolclip);
                // GiveWeaponComponentToPed(ped, microsmg, microsupp);
                GiveWeaponComponentToPed(ped, microsmg, flashlightar);
                GiveWeaponToPed(ped, (uint)WeaponHash.Knife, 1, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Flashlight, 1, false, false);
                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:2", plyr);
            }
            if (dvnum == 3)
            {
                foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
                {
                    uint weaponhash = (uint)weapon;
                    SetPedAmmo(ped, weaponhash, 0);
                    if (HasPedGotWeapon(ped, weaponhash, false))
                    {
                        RemoveWeaponFromPed(ped, weaponhash);
                    }
                }
                uint unarmed = (uint)WeaponHash.Unarmed;
                GiveWeaponToPed(ped, unarmed, 1, false, false);
                uint shotgun = (uint)WeaponHash.AssaultShotgun;
                uint machinepistol = (uint)WeaponHash.MachinePistol;
                uint parachute = (uint)WeaponHash.Parachute;
                uint grenade = (uint)WeaponHash.Grenade;
                uint flashlightap = (uint)WeaponComponentHash.AtPiFlsh;
                uint combatpistolclip = (uint)WeaponComponentHash.CombatPistolClip02;
                uint combatpistol = (uint)WeaponHash.CombatPistol;
                GiveWeaponToPed(ped, combatpistol, 60, false, false);
                GiveWeaponComponentToPed(ped, combatpistol, flashlightap);
                GiveWeaponComponentToPed(ped, combatpistol, combatpistolclip);
                GiveWeaponToPed(ped, grenade, 10, false, false);
                GiveWeaponToPed(ped, parachute, 1, false, false);
                GiveWeaponToPed(ped, machinepistol, 200, false, false);
                GiveWeaponToPed(ped, shotgun, 80, false, true);
                uint flashlight = (uint)WeaponComponentHash.AtArFlsh;
                uint shotgunsupp = (uint)WeaponComponentHash.AtArSupp;
                uint machineclip = (uint)WeaponComponentHash.MachinePistolClip02;
                GiveWeaponComponentToPed(ped, machinepistol, machineclip);
                uint shotgunclip = (uint)WeaponComponentHash.AssaultShotgunClip01;
                GiveWeaponComponentToPed(ped, shotgun, flashlight);
               // GiveWeaponComponentToPed(ped, shotgun, shotgunsupp);
                GiveWeaponComponentToPed(ped, shotgun, shotgunclip);
                GiveWeaponToPed(ped, (uint)WeaponHash.Knife, 1, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Flashlight, 1, false, false);
                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:3", plyr);
            }
            if (dvnum == 4)
            {

                foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
                {
                    uint weaponhash = (uint)weapon;
                    SetPedAmmo(ped, weaponhash, 0);
                    if (HasPedGotWeapon(ped, weaponhash, false))
                    {
                        RemoveWeaponFromPed(ped, weaponhash);
                    }
                }
                uint unarmed = (uint)WeaponHash.Unarmed;
                GiveWeaponToPed(ped, unarmed, 1, false, false);
                uint combatpistol = (uint)WeaponHash.CombatPistol;
                uint marksmansnip = (uint)WeaponHash.MarksmanRifle;
                uint parachute = (uint)WeaponHash.Parachute;
                uint grenade = (uint)WeaponHash.Grenade;
                GiveWeaponToPed(ped, grenade, 10, false, false);
                GiveWeaponToPed(ped, parachute, 1, false, false);
                GiveWeaponToPed(ped, marksmansnip, 70, false, true);
                GiveWeaponToPed(ped, combatpistol, 120, false, false);
                uint flashlightar = (uint)WeaponComponentHash.AtArFlsh;
                uint flashlightap = (uint)WeaponComponentHash.AtPiFlsh;
                uint combatpistolsuppresor = (uint)WeaponComponentHash.AtPiSupp;
                uint combatpistolclip = (uint)WeaponComponentHash.CombatPistolClip02;
                uint marksmansupp = (uint)WeaponComponentHash.AtArSupp;
                uint marksmanclip = (uint)WeaponComponentHash.MarksmanRifleClip01;
                GiveWeaponComponentToPed(ped, combatpistol, flashlightap);
                GiveWeaponComponentToPed(ped, combatpistol, combatpistolclip);
             //   GiveWeaponComponentToPed(ped, combatpistol, combatpistolsuppresor);
          //      GiveWeaponComponentToPed(ped, marksmansnip, marksmansupp);
                GiveWeaponComponentToPed(ped, marksmansnip, flashlightar);
                GiveWeaponComponentToPed(ped, marksmansnip, marksmanclip);
                GiveWeaponToPed(ped, (uint)WeaponHash.Knife, 1, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Flashlight, 1, false, false);
                //

                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:4", plyr);
            }
            if (dvnum == 5)
            {

                foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
                {
                    uint weaponhash = (uint)weapon;
                    SetPedAmmo(ped, weaponhash, 0);
                    if (HasPedGotWeapon(ped, weaponhash, false))
                    {
                        RemoveWeaponFromPed(ped, weaponhash);
                    }
                }
                uint unarmed = (uint)WeaponHash.Unarmed;
                GiveWeaponToPed(ped, unarmed, 1, false, false);
                uint combatpistol = (uint)WeaponHash.CombatPistol;

                uint grenade = (uint)WeaponHash.Grenade;
                uint flashlightap = (uint)WeaponComponentHash.AtPiFlsh;

                uint combatpistolsuppresor = (uint)WeaponComponentHash.AtPiSupp;
                uint combatpistolclip = (uint)WeaponComponentHash.CombatPistolClip02;
                GiveWeaponToPed(ped, grenade, 10, false, false);

                GiveWeaponToPed(ped, combatpistol, 200, false, false);
                GiveWeaponComponentToPed(ped, combatpistol, flashlightap);
                GiveWeaponComponentToPed(ped, combatpistol, combatpistolclip);
                GiveWeaponComponentToPed(ped, combatpistol, combatpistolsuppresor);
                GiveWeaponToPed(ped, (uint)WeaponHash.Knife, 1, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Flashlight, 1, false, false);
                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:5", plyr);
            }
            if (dvnum == 6)
            {

                foreach (WeaponHash weapon in Enum.GetValues(typeof(WeaponHash)))
                {
                    uint weaponhash = (uint)weapon;
                    SetPedAmmo(ped, weaponhash, 0);
                    if (HasPedGotWeapon(ped, weaponhash, false))
                    {
                        RemoveWeaponFromPed(ped, weaponhash);
                    }
                }
                uint unarmed = (uint)WeaponHash.Unarmed;
                GiveWeaponToPed(ped, unarmed, 1, false, false);
                uint combatpistol = (uint)WeaponHash.CombatPistol;
                uint parachute = (uint)WeaponHash.Parachute;

                uint flashlightap = (uint)WeaponComponentHash.AtPiFlsh;

                uint combatpistolsuppresor = (uint)WeaponComponentHash.AtPiSupp;
                uint combatpistolclip = (uint)WeaponComponentHash.CombatPistolClip02;

                GiveWeaponToPed(ped, parachute, 1, false, false);
                GiveWeaponToPed(ped, combatpistol, 200, false, false);
                GiveWeaponComponentToPed(ped, combatpistol, flashlightap);
                GiveWeaponComponentToPed(ped, combatpistol, combatpistolclip);
                GiveWeaponComponentToPed(ped, combatpistol, combatpistolsuppresor);
                GiveWeaponToPed(ped, (uint)WeaponHash.Knife, 1, false, false);
                GiveWeaponToPed(ped, (uint)WeaponHash.Flashlight, 1, false, false);
                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:6", plyr);
            }

        }
        public string GetDivisionNameAndDetails(string dnum)
        {
            
            string final = "";
            if (dnum == "1")
            {
                final = "Commando : Rifle Gun | Guns : Combat Pistol - Special Carbine | Tools : Parachute - Hand Grenade";
            }
            if (dnum == "2")
            {
                final = "Commadno : Artilerry | Guns : Machine Pistol - Homing Launcher | Tools : Parachute - Hand Grenade - Proximity Mines";
            }
            if (dnum == "3")
            {
                final = "Commadno : ShotGun | Guns : Machine Pistol - Heavy ShotGun | Tools : Parachute - Hand Grenade";
            }
            if (dnum == "4")
            {
                final = "Commando : Sniper | Guns : Combat-Pistol - Marksman Sniper | Tools : Parachute - Hand Grenade";
            }
            if (dnum == "5")
            {
                final = "Pilot : Tank | Guns : Combat Pistol";
            }
            if (dnum == "6")
            {
                final = "Pilot : AirCraft | Guns : Combat Pistol | Tools :  Parachute";
            }
            if (dnum == "7")
            {
                final = "Squad Leader : Ground | Guns :Combat Pistol - Special Carbine MK2 | Tools : Parachute - Flare - Hand Grenade  ";
            }
            if (dnum == "8")
            {
                final = "Squad Leader : AirCrafts | Guns : Combat Pistol | Tools : Parachute";
            }
            return final;
        }
        private void RequestModels()
        {
            
            RequestModel(Convert.ToUInt32(GetHashKey("rhino")));
            RequestModel(Convert.ToUInt32(GetHashKey("hydra")));
            RequestModel(Convert.ToUInt32(GetHashKey("titan")));
            RequestModel(Convert.ToUInt32(GetHashKey("cargobob4")));
            RequestModel((uint)VehicleHash.Insurgent3);
            RequestModel((uint)VehicleHash.Brickade);
        }
        public async void Spawned(object plyr)
        {
            while (string.IsNullOrEmpty(SelfDatabase))
            {
                await BaseScript.Delay(0);
            }

            if (GetTeam(SelfDatabase) == "0")
            {
                LoadScene(-2340.7f, 3259.31f, 33f);
                await BaseScript.Delay(1000);
                int ped = GetPlayerPed(GetPlayerIndex());
                SetPlayerHealthRechargeMultiplier(PlayerId(), 0f);

                // ChangePlayerPed(GetPlayerIndex(), GetHashKey("s_m_y_armymech_01"), false, false);
                SetEntityCoords(ped, -2340.7f, 3259.31f, 33f, false, false, false, false);
                uint pedmod = (uint)PedHash.Marine03SMY;
                RequestModel(pedmod);
                while (!HasModelLoaded(pedmod))
                {
                    await BaseScript.Delay(0);
                }
                SetPlayerModel(PlayerId(), pedmod);
            }
        }
        Menu dmenu = new Menu("Divisions", "CybeR 2020");
        Menu tmenu = new Menu("Tank Spawning", "BOOM");
        Menu amenu = new Menu("AirCraft Spawning", "");


        public async void MenuControllers()
        {
            while (string.IsNullOrEmpty(SelfDatabase))
            {
                await BaseScript.Delay(0);
            }


            int id = GetPlayerIndex();
            Player plyr = new PlayerList()[id];



            //Divison Menu
            MenuController.MenuAlignment = MenuController.MenuAlignmentOption.Right;
            string division = GetDivison(SelfDatabase);
            if (division == "1")
            {
                dmenu.MenuSubtitle = "Commando : Rifle";
            }
            if (division == "2")
            {
                dmenu.MenuSubtitle = "Commando : Artilerry";
            }
            if (division == "3")
            {
                dmenu.MenuSubtitle = "Commando : Shotgun";
            }
            if (division == "4")
            {
                dmenu.MenuSubtitle = "Commando : Sniper";
            }
            if (division == "5")
            {
                dmenu.MenuSubtitle = "Pilot : Tank";
            }
            if (division == "6")
            {
                dmenu.MenuSubtitle = "Pilot : AirCraft";
            }
            dmenu.AddMenuItem(new MenuItem("Commando : Rifle", GetDivisionNameAndDetails("1"))
            {
                
                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            dmenu.AddMenuItem(new MenuItem("Commando : Artillery", GetDivisionNameAndDetails("2"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            dmenu.AddMenuItem(new MenuItem("Commando : Shotgun", GetDivisionNameAndDetails("3"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            dmenu.AddMenuItem(new MenuItem("Commando : Sniper", GetDivisionNameAndDetails("4"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            dmenu.AddMenuItem(new MenuItem("Pilot : Tank", GetDivisionNameAndDetails("5"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.CAR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            dmenu.AddMenuItem(new MenuItem("Pilot : Aircraft", GetDivisionNameAndDetails("6"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.CAR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            dmenu.OnItemSelect += (_menu, _item, _index) =>
            {
                if (_index +1  == 1)
                {
                    dmenu.MenuSubtitle = "Commando : Rifle";
                }
                if (_index + 1 == 2)
                {
                    dmenu.MenuSubtitle = "Commando : Artilerry";
                }
                if (_index + 1 == 3)
                {
                    dmenu.MenuSubtitle = "Commando : Shotgun";
                }
                if (_index + 1 == 4)
                {
                    dmenu.MenuSubtitle = "Commando : Sniper";
                }
                if (_index + 1 == 5)
                {
                    dmenu.MenuSubtitle = "Pilot : Ground";
                }
                if (_index + 1 == 6)
                {
                    dmenu.MenuSubtitle = "Pilot : AirCraft";
                }
                UpdateDV(plyr, _index + 1);
            };
            
            MenuController.AddMenu(dmenu);
            //Divison Menu
            // Tank Menu
            tmenu.AddMenuItem(new MenuItem("Rhino", "Slot 4")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.CAR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            tmenu.AddMenuItem(new MenuItem("Insurgent", "Slot 6")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.CAR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            tmenu.AddMenuItem(new MenuItem("Brickade", "Aircraft Radar. Ammo Supply")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            tmenu.OnItemSelect += (_menu, _item, _index) =>
            {
                if(_index == 0)
                {
                    string model = "rhino";
                    var hash = GetHashKey(model);
                    
                    var vehicle = CreateVehicle(Convert.ToUInt32(hash), -2404.71f, 3308.52f, 32.83f, 325f, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);

                }
                if (_index == 1)
                {

                    var hash = (uint)VehicleHash.Insurgent3;
                    var vehicle = CreateVehicle(Convert.ToUInt32(hash), -2404.71f, 3308.52f, 32.83f, 325f, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);
                }
                if(_index == 2)
                {
                    var hash = (uint)VehicleHash.Brickade;
                    var vehicle = CreateVehicle(Convert.ToUInt32(hash), -2404.71f, 3308.52f, 32.83f, 325f, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);
                }
            };
            MenuController.AddMenu(tmenu);
            
            // Tank Menu
            // AirCraft Menu
            amenu.AddMenuItem(new MenuItem("HydRa", "Slot 2")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.ARMOR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            amenu.AddMenuItem(new MenuItem("Titan", "Slot 10")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.ARMOR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            
            amenu.AddMenuItem(new MenuItem("CarGo", "Slot 2  Makhsoos Haml Tank ")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.ARMOR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            amenu.OnItemSelect += (_menu, _item, _index) =>
            {
                if (_index == 0)
                {
                    string model = "hydra";
                    var hash = GetHashKey(model);
                    

                    var vehicle = CreateVehicle(Convert.ToUInt32(hash), -2141.68f, 3247.13f, 32.81f, 146f, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);

                }
                if (_index == 1)
                {
                    string model = "titan";
                    var hash = GetHashKey(model);


                    var vehicle = CreateVehicle(Convert.ToUInt32(hash), -1843.65f, 2984.08f, 32.81f, 58, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);
                }
                if (_index == 2)
                {
                    
                    string model = "cargobob4";
                    var hash = GetHashKey(model);


                    var vehicle = CreateVehicle(Convert.ToUInt32(hash), -2192.44f, 3176.24f, 32.81f, 325, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);
                }
            };
                MenuController.AddMenu(amenu);
            
            // AirCraft Menu
        }
















    }
}
