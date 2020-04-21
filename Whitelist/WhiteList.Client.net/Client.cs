using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
using MenuAPI;
namespace WhiteList.Client.net
{
    public class Client : BaseScript
    {
        bool ntct = false;
        bool ntd = false;
        public Client()
        {
            EventHandlers["NeedToChangeTeam"] += new Action<bool>(ChangeTeam);
            Tick += sendID;
            Tick += Change;
        }
        Menu cmenu = new Menu("Change Team", "Select Your Team");
        private async Task Change()
        {
            if(ntct && !MenuController.IsAnyMenuOpen())
            {
                MenuController.DontOpenAnyMenu = false;
                cmenu.AddMenuItem(new MenuItem("Usa", "Usa Military Force. Best On Foot")
                {
                    Enabled = true,
                    LeftIcon = MenuItem.Icon.AMMO
                });
                cmenu.AddMenuItem(new MenuItem("Russia", "Russia Military Force. Best In Sky")
                {
                    Enabled = true,
                    LeftIcon = MenuItem.Icon.AMMO
                });
                cmenu.OnItemSelect += (_menu, _item, _index) =>
                {
                    if (_index == 0)
                    {
                        TriggerServerEvent("SetPlayerTeam", _index);
                        ntct = false;
                        ntd = true;
                    }
                    if (_index == 1)
                    {
                        TriggerServerEvent("SetPlayerTeam", _index);
                        ntct = false;
                        ntd = true;
                           
                    }
                };
                if(ntd)
                {
                    await Delay(3000);
                    ntd = false;
                    Game.PlayerPed.Kill();
                }
                MenuController.AddMenu(cmenu);
                cmenu.OpenMenu();
            }
            else if (!ntct)
            {
                MenuController.DontOpenAnyMenu = true;
            }
        }
       
        private void ChangeTeam(bool result)
        {
            ntct = result;
        }
        private async Task sendID()
        {
            TriggerServerEvent("SetPlayerID", GetPlayerServerId(PlayerId()).ToString());
            await Delay(2000);
        }
    }
}
