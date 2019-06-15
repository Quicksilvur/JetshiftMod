using JetshiftMod.NPCs;
using JetshiftMod.Tiles;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Utilities;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using BaseMod;

namespace JetshiftMod.Worldgen
{
	public class GreatDarkness : MicroBiome
	{
        public override bool Place(Point origin, StructureMap structures)
        {
            //this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

            Mod mod = JetshiftMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
            colorToTile[new Color(51, 51, 51)] = mod.TileType("VoidOre");
			colorToTile[new Color(120, 120, 120)] = mod.TileType("Tenebricite");
            colorToTile[Color.Black] = -1; //don't touch when genning        

            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(150, 150, 150)] = WallID.Stone;
            colorToWall[Color.Black] = -1; //don't touch when genning                

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgen/GreatDarkness"), colorToTile);
			int genX = origin.X - (gen.width / 3);
			int genY = origin.Y - 25;			
            gen.Generate(genX, genY, true, true);
            return true;
        }
	}

	public class GreatDarknessDelete : MicroBiome
	{
		public override bool Place(Point origin, StructureMap structures)
		{
			//this handles generating the actual tiles, but you still need to add things like treegen etc. I know next to nothing about treegen so you're on your own there, lol.

			Mod mod = JetshiftMod.instance;

			Dictionary<Color, int> colorToTile = new Dictionary<Color, int>();
			colorToTile[new Color(51, 51, 51)] = -2;
			colorToTile[new Color(120, 120, 120)] = -2;
			colorToTile[new Color(150, 150, 150)] = -2;
			colorToTile[Color.Black] = -1;

			Dictionary<Color, int> colorToWall = new Dictionary<Color, int>();
			colorToWall[new Color(150, 150, 150)] = -1;
            colorToWall[Color.Black] = -1; //don't touch when genning

			TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgen/GreatDarkness"), colorToTile);
			Point newOrigin = new Point(origin.X, origin.Y - 30);
			int genX = origin.X - (gen.width / 3);
			int genY = origin.Y - 25;		
			gen.Generate(genX, genY, true, true);						

			return true;
		}
	}
}