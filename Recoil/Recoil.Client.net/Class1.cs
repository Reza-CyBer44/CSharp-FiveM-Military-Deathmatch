using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Recoil.Client.net
{
    public class Class1 : BaseScript
    {

        public Class1()
        {
            Tick += FirstPerson;
              Tick += Shooting;
             Tick += DisableHUD;
             Tick += Recoil;
        }
        public async Task FirstPerson()
        {

            if (IsPlayerFreeAiming(PlayerId()))
            {
                if (GetFollowPedCamViewMode() != 4)
                {
                    SetFollowPedCamViewMode(4);
                }
            }

        }
        public async Task DisableHUD()
        {
            int id = PlayerId();
            int ped = GetPlayerPed(id);
            uint wep = 0;
            var gotwep = GetCurrentPedWeapon(ped, ref wep, true);
            if (wep == (uint)WeaponHash.Grenade || wep == (uint)WeaponHash.Flare || wep == (uint)WeaponHash.ProximityMine || wep == (uint)WeaponHash.MarksmanRifle)
            {

            }
            else if (IsPedInFlyingVehicle(GetPlayerPed(PlayerId())) & GetFollowPedCamViewMode() == 4)
            {

            }
            else
            {
                HideHudComponentThisFrame(14);
            }

        }
        public async Task Shooting()
        {
            if (IsPedShooting(GetPlayerPed(PlayerId())) && GetFollowPedCamViewMode() != 4)
            {
                SetFollowPedCamViewMode(4);
            }


        }
        public async Task Recoil()
        {
            int ID = PlayerId();
            int ped = GetPlayerPed(ID);
            if (IsPedShooting(ped) && !IsPedDoingDriveby(ped))
            {

                uint wep = 0;
                var gotwep = GetCurrentPedWeapon(ped, ref wep, true);
                if (wep == (uint)WeaponHash.MachinePistol || wep == (uint)WeaponHash.APPistol)                             //Spray Pistols
                {

                    float p = GetGameplayCamRelativePitch();
                    SetGameplayCamRelativePitch(p + 0.1f, 0.2f);
                }
                else if (wep == (uint)WeaponHash.CombatPistol || wep == (uint)WeaponHash.HeavyPistol || wep == (uint)WeaponHash.MarksmanPistol || wep == (uint)WeaponHash.Pistol || wep == (uint)WeaponHash.Pistol50 || wep == (uint)WeaponHash.PistolMk2 || wep == (uint)WeaponHash.SNSPistol || wep == (uint)WeaponHash.VintagePistol)                        //Single Shot Pistols
                {
                    float p = GetGameplayCamRelativePitch();

                    SetGameplayCamRelativePitch(p + 0.5f, 0.2f);
                }
                else if (wep == (uint)WeaponHash.SpecialCarbine || wep == (uint)WeaponHash.AssaultRifle || wep == (uint)WeaponHash.AssaultSMG || wep == (uint)WeaponHash.CarbineRifle || wep == (uint)WeaponHash.AdvancedRifle || wep == (uint)WeaponHash.CompactRifle)                       //ARS
                {
                    float p = GetGameplayCamRelativePitch();
                    SetGameplayCamRelativePitch(p + 0.4f, 0.2f);
                }
                else if (wep == (uint)WeaponHash.SMG || wep == (uint)WeaponHash.MiniSMG || wep == (uint)WeaponHash.CombatPDW || wep == (uint)WeaponHash.Gusenberg || wep == (uint)WeaponHash.CombatMG || wep == (uint)WeaponHash.MG || wep == (uint)WeaponHash.MiniSMG)                       //SMG
                {
                    float p = GetGameplayCamRelativePitch();
                    SetGameplayCamRelativePitch(p + 0.3f, 0.2f);
                }
                else if (wep == (uint)WeaponHash.RPG || wep == (uint)WeaponHash.Railgun || wep == (uint)WeaponHash.Minigun || wep == (uint)WeaponHash.HomingLauncher || wep == (uint)WeaponHash.GrenadeLauncher || wep == (uint)WeaponHash.CompactGrenadeLauncher || wep == (uint)WeaponHash.Firework)                          // BOMBS
                {
                    float p = GetGameplayCamRelativePitch();
                    SetGameplayCamRelativePitch(p + 4.7f, 0.2f);
                }
                else if (wep == (uint)WeaponHash.HeavyShotgun || wep == (uint)WeaponHash.AssaultShotgun || wep == (uint)WeaponHash.SweeperShotgun || wep == (uint)WeaponHash.BullpupShotgun || wep == (uint)WeaponHash.DoubleBarrelShotgun || wep == (uint)WeaponHash.PumpShotgun || wep == (uint)WeaponHash.SawnOffShotgun)               //Shotguns
                {
                    float p = GetGameplayCamRelativePitch();
                    SetGameplayCamRelativePitch(p + 0.3f, 0.2f);
                }
                else if (wep == (uint)WeaponHash.MarksmanRifle || wep == (uint)WeaponHash.HeavySniper || wep == (uint)WeaponHash.SniperRifle)               //Snipers
                {
                    float p = GetGameplayCamRelativePitch();
                    SetGameplayCamRelativePitch(p + 1.5f, 0.2f);
                }
                else if (!IsPedInAnyVehicle(GetPlayerPed(PlayerId()), false))
                {
                    float p = GetGameplayCamRelativePitch();
                    SetGameplayCamRelativePitch(p + 0.2f, 0.2f);
                }

            }




        }

    }
}
