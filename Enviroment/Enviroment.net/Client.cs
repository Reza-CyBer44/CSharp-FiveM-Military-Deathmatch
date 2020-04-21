using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace Enviroment.net
{
    public class Client : BaseScript
    {
        public Client()
        {
            Tick += Time;
            Tick += BlackOut;
        }
        public async Task Time()
        {
           NetworkOverrideClockTime(0, 0, 0);
            await BaseScript.Delay(1);
        }
        public async Task BlackOut()
        {
            int ID = PlayerId();
            int ped = GetPlayerPed(ID);
            Vector3 cord = GetEntityCoords(ped, true);
            float distance = GetDistanceBetweenCoords(cord.X, cord.Y, cord.Z, -2256.58f, 282.49f, 174.47f, false);
            float distance2 = GetDistanceBetweenCoords(cord.X, cord.Y, cord.Z, 488.64f, -3345.04f, 7f, false);
            if(distance<2000 || distance2 <2000)
            {
                SetBlackout(true);
            }
            else
            {
                SetBlackout(false);
            }


        }
       
    }
}
