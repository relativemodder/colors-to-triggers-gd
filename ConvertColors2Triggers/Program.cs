using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryDashAPI;
using GeometryDashAPI.Levels;

namespace ConvertColors2Triggers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GeometryDashAPI.Data.LocalLevels localLevels = new GeometryDashAPI.Data.LocalLevels();
            Console.Write("Enter level name:");
            string levelName = Console.ReadLine();
            GeometryDashAPI.Data.Models.LevelCreatorModel levelModel = localLevels.GetLevelByName(levelName);
            if(levelModel == null)
            {
                Console.WriteLine("Level not found!");
                Console.ReadLine();
                return;
            }
            Level level = new Level(levelModel.LevelString);
            List<Color> colors = level.Colors.ToList();
            float y = 0.0f;
            foreach(Color color in colors)
            {
                Console.WriteLine($"{color.ID} - {color.Blending}");
                GeometryDashAPI.Levels.GameObjects.Triggers.ColorTrigger colorTrigger = new GeometryDashAPI.Levels.GameObjects.Triggers.ColorTrigger();
                colorTrigger.Blending = color.Blending;
                colorTrigger.Red = color.Red;
                colorTrigger.Green = color.Green;
                colorTrigger.Blue = color.Blue;
                colorTrigger.ColorID = color.ID;
                colorTrigger.TargetChannelID = color.TargetChannelID;
                colorTrigger.Opacity = color.Opacity;
                colorTrigger.ColorHSV = color.ColorHSV;
                colorTrigger.PositionY = y;
                y += 10;
                level.AddBlock(colorTrigger);
            }
            localLevels.GetLevelByName(levelName).LevelString = level.ToString();
            localLevels.Save();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
