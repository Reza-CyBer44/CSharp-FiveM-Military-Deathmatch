using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace SupplySystem.Client.net
{

    public class Client : BaseScript
    {
        public bool isoccupiedb = false;
        public bool isnearsupplyb = false;
        public Client()
        {

            Tick += CheckDistance;
            Tick += DrawHelpers;
        }

        public async Task CheckDistance()
        {
            Vector3 plyr = Game.PlayerPed.Position;
            Vehicle[] targets = World.GetAllVehicles();
            foreach(Vehicle vehic in targets)
            {
                if(vehic.Model == VehicleHash.Brickade || vehic.Model == VehicleHash.Cargobob)
                {
                    if (vehic.Health > 0)
                    {
                        Vector3 pos = vehic.Position;
                        float dist = World.GetDistance(plyr, pos);
                        if (dist < 5)
                        {
                            isnearsupplyb = true;
                        }
                        else
                        {
                            isnearsupplyb = false;
                        }
                    }
                }

            }
        }
        public async Task DrawHelpers()
        {
            if(isnearsupplyb)
            {
                if (!IsPedInAnyVehicle(PlayerPedId(), true))
                {
                    if (!isoccupiedb)
                    {
                        SetTextComponentFormat("STRING");
                        AddTextComponentString("Press ~INPUT_SELECT_CHARACTER_FRANKLIN~ For Ammo Or Press ~INPUT_REPLAY_SAVE~ For Health And Armor");
                        DisplayHelpTextFromStringLabel(0, false, true, -1);
                        if (IsControlJustPressed(1, 167))
                        {
                            isoccupiedb = true;
                            uint weaponhash = 0;
                            bool test = GetCurrentPedWeapon(PlayerPedId(), ref weaponhash, true);
                            if (weaponhash == (uint)WeaponHash.Unarmed)
                            {
                                TriggerEvent("chat:addMessage", new
                                {
                                    color = new[] { 255, 0, 0 },
                                    args = new[] { "[Refill System]", $"You Have Nothing in you hand To Fill" }
                                });
                                isoccupiedb = false;
                            }
                            else if (weaponhash == (uint)WeaponHash.SpecialCarbineMk2)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.SpecialCarbineMk2);
                                if (ammo + 30 < 300)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    
                                    await   Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.SpecialCarbineMk2, ammo + 30);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 300)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 30 > 300)
                                {
                                
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await   Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.SpecialCarbineMk2, 300);
                                    isoccupiedb = false;
                                }
                            }
                            else if (weaponhash == (uint)WeaponHash.SpecialCarbine)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.SpecialCarbine);
                                if (ammo + 60 < 300)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.SpecialCarbine, ammo + 60);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 300)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 60 > 300)
                                {

                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.SpecialCarbine, 300);
                                    isoccupiedb = false;
                                }
                            }
                            else if (weaponhash == (uint)WeaponHash.CombatPistol)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.CombatPistol);
                                if (ammo + 16 < 200)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.CombatPistol, ammo + 16);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 200)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 16 > 200)
                                {

                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    
                                    await Delay(2500);
                                    
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.CombatPistol, 200);
                                    isoccupiedb = false;
                                }
                            }
                            else if (weaponhash == (uint)WeaponHash.MicroSMG)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.MicroSMG);
                                if (ammo +30 < 200)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.MicroSMG, ammo + 30);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 200)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 30 > 200)
                                {

                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.MicroSMG, 200);
                                    isoccupiedb = false;
                                }
                            }
                            else if (weaponhash == (uint)WeaponHash.HomingLauncher)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.HomingLauncher);
                                if (ammo + 1 < 15)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.HomingLauncher, ammo + 1);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 15)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 1 > 15)
                                {

                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.HomingLauncher, 15);
                                    isoccupiedb = false;
                                }
                            }
                            else if (weaponhash == (uint)WeaponHash.RPG)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.RPG);
                                if (ammo + 1 < 15)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.RPG, ammo + 1);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 15)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 1 > 15)
                                {

                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.RPG, 15);
                                    isoccupiedb = false;
                                }
                            }
                            else if (weaponhash == (uint)WeaponHash.MachinePistol)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.MachinePistol);
                                if (ammo + 20 < 200)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.MachinePistol, ammo + 20);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 200)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 20 > 200)
                                {

                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.MachinePistol, 200);
                                    isoccupiedb = false;
                                }
                            }
                            else if (weaponhash == (uint)WeaponHash.AssaultShotgun)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.AssaultShotgun);
                                if (ammo + 8 < 100)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.AssaultShotgun, ammo + 8);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 100)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 8 > 100)
                                {

                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.AssaultShotgun, 100);
                                    isoccupiedb = false;
                                }
                            }
                            else if (weaponhash == (uint)WeaponHash.MarksmanRifle)
                            {
                                int ammo = GetAmmoInPedWeapon(PlayerPedId(), (uint)WeaponHash.MarksmanRifle);
                                if (ammo + 8 < 100)
                                {
                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.MarksmanRifle, ammo + 8);
                                    isoccupiedb = false;
                                }
                                else if (ammo == 100)
                                {
                                    TriggerEvent("chat:addMessage", new
                                    {
                                        color = new[] { 255, 0, 0 },
                                        args = new[] { "[Refill System]", $"You Have Max Ammo In Your Current Weapon" }
                                    });
                                    isoccupiedb = false;
                                }
                                else if (ammo + 8 > 100)
                                {

                                    Exports["progressBars"].startUI(2500, "Filling The Ammo Clip");
                                    FreezeEntityPosition(PlayerPedId(), true);
                                    await Delay(2500);
                                    FreezeEntityPosition(PlayerPedId(), false);
                                    SetPedAmmo(PlayerPedId(), (uint)WeaponHash.MarksmanRifle, 100);
                                    isoccupiedb = false;
                                }
                            }
                            else
                            {
                                TriggerEvent("chat:addMessage", new
                                {
                                    color = new[] { 255, 0, 0 },
                                    args = new[] { "[Refill System]", $"Invalid Weapon" }
                                });
                                isoccupiedb = false;
                            }
                        }
                        else if (IsControlJustPressed(1,318))
                        {
                            isoccupiedb = true;
                            Exports["progressBars"].startUI(10000, "Using a Medkit and Armor");
                            FreezeEntityPosition(PlayerPedId(), true);
                            await Delay(10000);
                            FreezeEntityPosition(PlayerPedId(), false);
                            Game.PlayerPed.Health = Game.PlayerPed.MaxHealth;
                            Game.PlayerPed.Armor = 100;
                            isoccupiedb = false;
                        }
                    }
                }
            }


        }


    }
}
