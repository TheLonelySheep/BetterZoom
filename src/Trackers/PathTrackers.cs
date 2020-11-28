﻿using BetterZoom.src.UI.UIElements;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using BetterZoom.src.UI;
using Terraria.UI;

namespace BetterZoom.src.Trackers
{
    class PathTrackers
    {
        public static List<PathTrackers> trackers = new List<PathTrackers>();
        public UIImage PTrackerImg;
        public dynamic Connection;
        public Vector2 Position;

        public PathTrackers(Vector2 pos)
        {
            trackers.Add(this);
            Position = pos;
            PTrackerImg = new UIImage(ModContent.GetTexture("BetterZoom/Assets/PathTracker"));
            PTrackerImg.ImageScale = 0.5f;
            
            for (int i = 0; i < trackers.Count; i++)
            {
                switch (CCUI.selectedInterp) 
                {
                    case 0:
                        break;
                    case 1:
                        Connection = new Line(
                            trackers[i].Position - Main.screenPosition,
                            trackers[i].Position - Main.screenPosition,
                            5, Color.Red);
                        break;
                    case 2:
                        Connection = new BezierCurve(
                            trackers[i].Position - Main.screenPosition,
                            trackers[i].Position - Main.screenPosition,
                            5, Color.Red);
                        break;
                    case 3:
                        break;
                }
            }
        }
        public static void FixPosition()
        {
            for (int i = 0; i < trackers.Count; i++)
            {
                trackers[i].PTrackerImg.MarginLeft = trackers[i].Position.X - Main.screenPosition.X - trackers[i].PTrackerImg.Width.Pixels / 2;
                trackers[i].PTrackerImg.MarginTop = trackers[i].Position.Y - Main.screenPosition.Y - trackers[i].PTrackerImg.Height.Pixels / 2;
            }
        }
        public static void FixLinePosition()
        {
            // Fix Line Position
            for (int i = 0; i < trackers.Count; i++)
            {
                if (trackers[i].Connection != null && trackers[i] != null)
                {
                    trackers[i].Connection.StartPoint = trackers[i].Position - Main.screenPosition;

                    if (i + 1 < trackers.Count)
                    {
                        trackers[i].Connection.EndPoint = trackers[i + 1].Position - Main.screenPosition;
                    }
                    else
                    {
                        trackers[i].Connection.EndPoint = trackers[i].Connection.StartPoint;
                    }
                }
            }
        }
        public static void Remove()
        {
            int ID = 0;
            for (int i = 0; i < trackers.Count; i++)
            {
                if (trackers[i].PTrackerImg.IsMouseHovering)
                {
                    if (trackers[i] != null && (ID == 0 || Vector2.Distance(trackers[i].Position, Main.MouseWorld) < Vector2.Distance(trackers[ID].Position, Main.MouseWorld)))
                    {
                        ID = i;
                        trackers[ID].PTrackerImg.Remove();
                        trackers[ID].Connection.Remove();
                        trackers.RemoveAt(ID);
                    }
                }
            }
        }
    }
}
