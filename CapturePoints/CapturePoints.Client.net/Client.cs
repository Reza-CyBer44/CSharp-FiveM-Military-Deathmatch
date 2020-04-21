using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;
namespace CapturePoints.Client.net
{
    public class Client : BaseScript
    {
        Vector3 Capturecord = new Vector3(488.64f, -3345.04f, 5.12f);
        public string status = "";
        int savedblip = 0;
        public int usa = 0;
        public int russia = 0;
        System.Drawing.Color DrawColor = System.Drawing.Color.FromArgb(0, 0, 0);
        public Client()
        {
            EventHandlers["Cyber:BlipStatus"] += new Action<string>(UpdateStatus);
            EventHandlers["Cyber:Russia:Points"] += new Action<int>(RussiaPoint);
            EventHandlers["Cyber:Usa:Points"] += new Action<int>(UsaPoint);
            Tick += DrawBlipAndMarker;
            Tick += CaptureStatus;
            Tick += IsCapturing;

        }
        public bool RecentlyEntered = false;
        public async Task IsCapturing()
        {
           if (status == "red")
            {
                DrawColor = System.Drawing.Color.FromArgb(150,255, 0, 0);
            }
            else if (status == "blue")
            {
                DrawColor = System.Drawing.Color.FromArgb(150,0, 0, 255);
            }
            else if (status == "nothing")
            {
                DrawColor = System.Drawing.Color.FromArgb(150,0, 0, 0);
            }
            else if (status == "draw")
            {
                DrawColor = System.Drawing.Color.FromArgb(150,255, 255, 255);
            }
            Vector3 plyrcord = GetEntityCoords(GetPlayerPed(GetPlayerIndex()), true);
            float distance = GetDistanceBetweenCoords(Capturecord.X,Capturecord.Y,Capturecord.Z, plyrcord.X, plyrcord.Y, plyrcord.Z, true);
            if (distance < 5 && !IsPedDeadOrDying(GetPlayerPed(GetPlayerIndex()), true) && !IsPedInAnyVehicle(GetPlayerPed(GetPlayerIndex()), true))
            {
                TriggerServerEvent("Cyber:EnteredCapturePoint");
                RecentlyEntered = true;
            }
            else if (distance > 5)
            {
                TriggerServerEvent("Cyber:LeavedCapturePoint");
                RecentlyEntered = false;
            }
        }
        public void RussiaPoint(int point)
        {
            russia = point;
        }
        public void UsaPoint(int point)
        {
            usa = point;
        }
        public async Task CaptureStatus()
        {
            // Draw Status Rect
            CitizenFX.Core.UI.Rectangle rect = new CitizenFX.Core.UI.Rectangle();
            rect.Position = new System.Drawing.PointF(200.0f, 30);
            rect.Size = new System.Drawing.SizeF(50.0f, 50.0f);
            
            if (status == "red")
            {
                rect.Color = System.Drawing.Color.FromArgb(150,255, 0, 0);
            }
            else if (status == "blue")
            {
                rect.Color = System.Drawing.Color.FromArgb(150,0, 0, 255);
            }
            else if (status == "nothing")
            {
                rect.Color = System.Drawing.Color.FromArgb(150,0, 0, 0);
            }
            else if (status == "draw")
            {
                rect.Color = System.Drawing.Color.FromArgb(150,255, 255, 255);
            }
            rect.Draw();
            //Draw Points
            CitizenFX.Core.UI.Text usat = new CitizenFX.Core.UI.Text("Usa: " + usa.ToString(), new System.Drawing.PointF(20, 30), 0.5f);
            usat.Centered = false;
            usat.Color = System.Drawing.Color.FromArgb(0, 0, 255);
            CitizenFX.Core.UI.Text russiat = new CitizenFX.Core.UI.Text("Russia: " + russia.ToString(), new System.Drawing.PointF(20, 50), 0.5f);
            russiat.Centered = false;
            russiat.Color = System.Drawing.Color.FromArgb(255, 0, 0);
            usat.Draw();
            russiat.Draw();








        }
        public async Task DrawBlipAndMarker()
        {
            if(savedblip != 0)
            {
                RemoveBlip(ref savedblip);
            }
            if(!IsStringNullOrEmpty(status))
            {
                Blip blip = World.CreateBlip(Capturecord);
                SetBlipSprite(blip.Handle, 177);
                SetBlipDisplay(blip.Handle, 6);
                blip.Scale = 1.5f;
                World.DrawMarker(MarkerType.HorizontalCircleSkinny, Capturecord, new Vector3(0, 0, 0), new Vector3(0, 0, 0), new Vector3(10, 10, 2), DrawColor, false, true);
                if(status == "red")
                {
                    blip.Name = "Capture Point";
                    blip.Color = BlipColor.Red;
                }
                else if(status == "blue")
                {
                    blip.Name = "Capture Point";
                    blip.Color = BlipColor.Blue;
                }
                else if(status == "nothing")
                {
                    blip.Name = "Capture Point";
                    blip.Color = BlipColor.White;
                }
                else if(status == "draw")
                {
                    blip.Name = "Capture Point";
                    blip.Color = BlipColor.Yellow;
                }
                savedblip = blip.Handle;
            }
        }
        private void UpdateStatus(string stat)
        {
            status = stat;
        }


    }
}
