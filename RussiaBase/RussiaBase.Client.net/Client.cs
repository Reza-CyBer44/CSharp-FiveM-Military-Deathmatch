using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using MenuAPI;

namespace RussiaBase.Client.net
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
            EventHandlers["Cyber:SelfHandle:dataString:russ"] += new Action<string>(handle);
            Tick += gatherdata;
            Tick += DrawMarkers;


        }

        private async void CreateMapBlips()
        {

            while (string.IsNullOrEmpty(SelfDatabase))
            {
                await BaseScript.Delay(0);
            }


            if (GetTeam(SelfDatabase) == "1")
            {
                // Add Main Blip for russia
                int mblip = AddBlipForCoord(5277.66f, 5689.83f, 55.49f);
                SetBlipSprite(mblip, 303);
                SetBlipDisplay(mblip, 3);
                SetBlipScale(mblip, 1.5f);
                SetBlipColour(mblip, 35);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("Russia Military AirCraft Base");
                EndTextCommandSetBlipName(mblip);
                //Add Divison Blip
                int dblip = AddBlipForCoord(5193.95f, 5672.31f, 5.98f);
                SetBlipSprite(dblip, 150);
                SetBlipAsShortRange(dblip, true);
                SetBlipDisplay(dblip, 5);
                SetBlipAlpha(dblip, 200);
                SetBlipScale(dblip, 2.0f);
                SetBlipColour(dblip, 1);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("Change Division");
                EndTextCommandSetBlipName(dblip);
                //Add Chopter Blip
                int ablip = AddBlipForCoord(5296.5f, 5624.72f, 5.99f);
                SetBlipSprite(ablip, 64);
                SetBlipAsShortRange(ablip, true);
                SetBlipDisplay(ablip, 5);
                SetBlipAlpha(ablip, 200);
                SetBlipScale(ablip, 2.0f);
                SetBlipColour(ablip, 1);
                SetBlipSecondaryColour(ablip, 0, 0, 0);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("Helicopter");
                EndTextCommandSetBlipName(ablip);
                //Add Jet blip
                int tblip = AddBlipForCoord(5228.3f, 5700.76f, 5.96f);
                SetBlipSprite(tblip, 424);
                SetBlipAsShortRange(tblip, true);
                SetBlipDisplay(tblip, 5);
                SetBlipAlpha(tblip, 200);
                SetBlipScale(tblip, 2.0f);
                SetBlipColour(tblip, 1);
                SetBlipSecondaryColour(tblip, 0, 0, 0);
                BeginTextCommandSetBlipName("STRING");
                AddTextComponentString("Jet");
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

           if (GetTeam(SelfDatabase) == "1")
           {

                //Draw Change Division Marker
                Vector3 dmark = new Vector3(5193.95f, 5672.31f, 5f);
                Vector3 gmark = new Vector3(5193.95f, 5672.31f, 6f);
                World.DrawMarker(MarkerType.HorizontalSplitArrowCircle, dmark, new Vector3(0f, 180f, 0f), new Vector3(0f, 0f, 0f), new Vector3(1.5f, 1.5f, 1), System.Drawing.Color.FromArgb(255, 10, 0), false, true);
                Vector3 plyrcord = GetEntityCoords(GetPlayerPed(GetPlayerIndex()), true);
                float distance = GetDistanceBetweenCoords(gmark.X, gmark.Y, gmark.Z, plyrcord.X, plyrcord.Y, plyrcord.Z, false);

                if (distance < 1)
                {
                    SetTextComponentFormat("STRING");
                    AddTextComponentString("Double Press ~INPUT_TALK~ To Open Menu.");
                    DisplayHelpTextFromStringLabel(0, false, true, -1);

                    if (IsControlJustPressed(1, 46))
                    {
                        outside1 = false;
                        MenuController.DontOpenAnyMenu = false;
                        dmenud.OpenMenu();
                    }
                }
                else
                {
                    outside1 = true;
                    if (outside1 & outside2 & outside3)
                    {
                        MenuController.DontOpenAnyMenu = true;
                        
                    }
                }









                // Draw Jet Spawner
                if (GetDivison(SelfDatabase) == "8" || GetDivison(SelfDatabase) == "6")
                {
                    Vector3 tmark = new Vector3(5228.3f, 5700.76f, 5.96f);
                    World.DrawMarker(MarkerType.PlaneSymbol, tmark, new Vector3(5f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(2.5f, 2.5f, 2.5f), System.Drawing.Color.FromArgb(100, 255, 10, 0), true, true);
                    Vector3 tplyrcord = GetEntityCoords(GetPlayerPed(GetPlayerIndex()), true);
                    float sdistance = GetDistanceBetweenCoords(tmark.X, tmark.Y, tmark.Z, tplyrcord.X, tplyrcord.Y, tplyrcord.Z, true);
                    if (sdistance < 1)
                    {
                        SetTextComponentFormat("STRING");
                        AddTextComponentString("Double Press ~INPUT_TALK~ To Open Spawn Menu.");
                        DisplayHelpTextFromStringLabel(0, false, true, -1);
                        if (IsControlJustPressed(1, 46))
                        {
                            MenuController.DontOpenAnyMenu = false;
                            outside2 = false;
                            tmenut.OpenMenu();


                        }
                    }
                    else
                    {
                        outside2 = true;
                        if (outside1 & outside2 & outside3)
                        {
                            MenuController.DontOpenAnyMenu = true;
                        }

                    }



                }
                // Draw Hellicopter Marker
                if (GetDivison(SelfDatabase) == "8" || GetDivison(SelfDatabase) == "6")
                {
                    Vector3 amark = new Vector3(5296.5f, 5624.72f, 5.99f);
                    World.DrawMarker(MarkerType.HelicopterSymbol, amark, new Vector3(5f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(2.5f, 2.5f, 2.5f), System.Drawing.Color.FromArgb(100,255,10,0), true, true);
                    Vector3 aplyrcord = GetEntityCoords(GetPlayerPed(GetPlayerIndex()), true);
                    float tdistance = GetDistanceBetweenCoords(amark.X, amark.Y, amark.Z, aplyrcord.X, aplyrcord.Y, aplyrcord.Z, true);
                    if (tdistance < 1)
                    {

                        SetTextComponentFormat("STRING");
                        AddTextComponentString("Double Press ~INPUT_TALK~ To Open Spawn Menu.");
                        DisplayHelpTextFromStringLabel(0, false, true, -1);
                        if (IsControlJustPressed(1, 46))
                        {
                            MenuController.DontOpenAnyMenu = false;
                            outside3 = false;                                                                                                                                                                                                                                                           
                            amenua.OpenMenu();                                                                  


                        }
                    }
                    else
                    {
                        outside3 = true;
                        if(outside1 & outside2 & outside3)
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
            TriggerServerEvent("Cyber:SelfCallback:dataString:russ", plyr);
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

                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:1:russ", plyr);
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
                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:2:russ", plyr);
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
                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:3:russ", plyr);
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

                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:4:russ", plyr);
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
                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:5:russ", plyr);
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
                TriggerServerEvent("Cyber:SelfHandle:UpdateDV:6:russ", plyr);
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
                final = "Commadno : Artilerry | Guns : Machine Pistol - RPG | Tools : Parachute - Hand Grenade - Proximity Mines";
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

            RequestModel(0x64DE07A1); //strikeforce hashkey
            RequestModel(0xB39B0AE6); //lazer
            RequestModel(0xFB133A17);  //savage hashkey
            RequestModel(0xFCFCB68B); //cargobob
        }
        public async void Spawned(object plyr)
        {
            while (string.IsNullOrEmpty(SelfDatabase))
            {
                await BaseScript.Delay(0);
            }

            if (GetTeam(SelfDatabase) == "1")
            {
                LoadScene(5217.94f, 5672.68f, 5.98f);
                await BaseScript.Delay(1000);
                int ped = GetPlayerPed(GetPlayerIndex());
                SetPlayerHealthRechargeMultiplier(PlayerId(), 0f);

                // ChangePlayerPed(GetPlayerIndex(), GetHashKey("s_m_y_armymech_01"), false, false);
                SetEntityCoords(ped, 5217.94f, 5672.68f, 5.98f, false, false, false, false);
                uint pedmod = (uint)PedHash.Blackops01SMY;
                RequestModel(pedmod);
                while (!HasModelLoaded(pedmod))
                {
                    await BaseScript.Delay(0);
                }
                SetPlayerModel(PlayerId(), pedmod);
            }
        }
        Menu dmenud = new Menu("Divisions", "CybeR 2020");
        Menu tmenut = new Menu("Jet Spawning", "BOOM");
        Menu amenua = new Menu("Hellicopter Spawning", "");
        

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
                dmenud.MenuSubtitle = "Commando : Rifle";
            }
            if (division == "2")
            {
                dmenud.MenuSubtitle = "Commando : Artilerry";
            }
            if (division == "3")
            {
                dmenud.MenuSubtitle = "Commando : Shotgun";
            }
            if (division == "4")
            {
                dmenud.MenuSubtitle = "Commando : Sniper";
            }
            if (division == "5")
            {
                dmenud.MenuSubtitle = "Pilot : Tank";
            }
            if (division == "6")
            {
                dmenud.MenuSubtitle = "Pilot : AirCraft";
            }
            dmenud.AddMenuItem(new MenuItem("Commando : Rifle", GetDivisionNameAndDetails("1"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            dmenud.AddMenuItem(new MenuItem("Commando : Artillery", GetDivisionNameAndDetails("2"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            dmenud.AddMenuItem(new MenuItem("Commando : Shotgun", GetDivisionNameAndDetails("3"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            dmenud.AddMenuItem(new MenuItem("Commando : Sniper", GetDivisionNameAndDetails("4"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.GUN,
                RightIcon = MenuItem.Icon.AMMO
            });
            dmenud.AddMenuItem(new MenuItem("Pilot : Tank", GetDivisionNameAndDetails("5"))
            {

                Enabled = false,
                LeftIcon = MenuItem.Icon.CAR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            dmenud.AddMenuItem(new MenuItem("Pilot : Aircraft", GetDivisionNameAndDetails("6"))
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.CAR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            dmenud.OnItemSelect += (_menu, _item, _index) =>
            {
                if (_index + 1 == 1)
                {
                    dmenud.MenuSubtitle = "Commando : Rifle";
                }
                if (_index + 1 == 2)
                {
                    dmenud.MenuSubtitle = "Commando : Artilerry";
                }
                if (_index + 1 == 3)
                {
                    dmenud.MenuSubtitle = "Commando : Shotgun";
                }
                if (_index + 1 == 4)
                {
                    dmenud.MenuSubtitle = "Commando : Sniper";
                }
                if (_index + 1 == 5)
                {
                    dmenud.MenuSubtitle = "Pilot : Tank";
                }
                if (_index + 1 == 6)
                {
                    dmenud.MenuSubtitle = "Pilot : AirCraft";
                }
                UpdateDV(plyr, _index + 1);
            };
            
            MenuController.AddMenu(dmenud);
           
            //Divison Menu
            // Tank Menu
            tmenut.AddMenuItem(new MenuItem("Lazer", "Jet")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.CAR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            tmenut.AddMenuItem(new MenuItem("Strike Force", "Stealth Fighter")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.CAR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            tmenut.OnItemSelect += (_menu, _item, _index) =>
            {
                if (_index == 0)
                {

                    var vehicle = CreateVehicle((uint) VehicleHash.Lazer, 5229.25f, 5717.61f, 5.99f, 318.75f, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);

                }
                if (_index == 1)
                {
                    string model = "strikeforce";
                    var hash = GetHashKey(model);
                    var vehicle = CreateVehicle(Convert.ToUInt32(hash), 5229.25f, 5717.61f, 5.99f, 318.75f, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);
                }
            };
            MenuController.AddMenu(tmenut);

            // Tank Menu
            // AirCraft Menu
            amenua.AddMenuItem(new MenuItem("Savage", "Slot 4 | Fighter hellicopter")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.ARMOR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });
            amenua.AddMenuItem(new MenuItem("CargoBob", "Slot 8")
            {

                Enabled = true,
                LeftIcon = MenuItem.Icon.ARMOR,
                RightIcon = MenuItem.Icon.MEDAL_GOLD
            });

            amenua.OnItemSelect += (_menu, _item, _index) =>
            {
                
                if (_index == 0)
                {
                    //   string model = "savage";
                    


                    var vehicle = CreateVehicle(0xFB133A17, 5288.89f, 5617.94f, 6.1f, 271f, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);
                }
                if (_index == 1)
                {

                    
                    //   string model = "cargobob";
                    //   var hash = GetHashKey(model);
                    var vehicle = CreateVehicle((uint)VehicleHash.Cargobob, 5288.89f, 5596.94f, 6.1f,271f, true, false);
                    int playerped = GetPlayerPed(GetPlayerIndex());
                    SetPedIntoVehicle(playerped, vehicle, -1);
                }
            };
            MenuController.AddMenu(amenua);
         
            // AirCraft Menu
        }
















    }
}
