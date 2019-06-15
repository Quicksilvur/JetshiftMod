using JetshiftMod.NPCs;
using JetshiftMod.Tiles;
using JetshiftMod.Worldgen;
using JSExp.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World;
using Terraria.World.Generation;
using Terraria.UI;
using BaseMod;

namespace JetshiftMod
{
	public class JetshiftWorld : ModWorld
	{	
		public static int SlaughterDMG = 0; //Initialising the Slaughter weapon damage.
		public int meteorSaveAs = 0;
		public int savedmeteorPos;
		public static int greatDarknessTiles;
		public static int mortalTiles;
		public static bool spawnVoidMeteor;
		public static double PlayerExp;
		public static ulong PlayerExpLevel;
		public static ulong PlayerExpRequired;
		public static ulong PlayerExpDiffRange;
		public static ulong PlayerExpPrevLevel;
		
		private static FieldInfo mHInfo;
		private static FieldInfo _itemIconCacheTimeInfo;

		public static void InitializeUI()
		{
			mHInfo = typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);
			_itemIconCacheTimeInfo = typeof(Main).GetField("_itemIconCacheTime", BindingFlags.NonPublic | BindingFlags.Static);
		}

		public override void Initialize()
		{
			JetshiftGlobalNPC.CCFormCountdown = 0;
			JetshiftGlobalNPC.downedSnakeBoss = false;
			JetshiftGlobalNPC.downedReaper = false;
			JetshiftGlobalNPC.downedCollin = false;
			JetshiftGlobalNPC.downedWallofVoices = false;
			JetshiftGlobalNPC.downedEdrol = false;
			JetshiftGlobalNPC.downedDevilmon = false;
			JetshiftGlobalNPC.downedSeaCreatures = false;
			JetshiftGlobalNPC.downedML = false;
			JetshiftGlobalNPC.downedIceYeti = false;
			JetshiftGlobalNPC.downedMortalos = false;
			JetshiftGlobalNPC.downedShiftFinal = false;
			JetshiftGlobalNPC.downedAnnihilation = false;
			JetshiftGlobalNPC.downedCCP1 = false;
			JetshiftGlobalNPC.downedCCP2 = false;
			JetshiftGlobalNPC.downedCosmicMystery = false;
			JetshiftGlobalNPC.nightmareMode = false;
			JetshiftGlobalNPC.meteorPosition = 0;
			JetshiftGlobalNPC.meteorPositionY = 0;
			JetshiftGlobalNPC.meteorLife = 9999;
			JetshiftGlobalNPC.meteorMoveTimer = 18000;
			JetshiftWorld.spawnVoidMeteor = false;
			JetshiftWorld.PlayerExp = 0;
			JetshiftWorld.PlayerExpLevel = 0;
			JetshiftWorld.PlayerExpRequired = 600;
			JetshiftWorld.PlayerExpDiffRange = 600;
			JetshiftWorld.PlayerExpPrevLevel = 0;
		}
		
		public static double meteorP = JetshiftGlobalNPC.meteorPosition;

		public override TagCompound Save()
		{
			List<string> list = new List<string>();
			if (JetshiftGlobalNPC.downedSnakeBoss)
			{
				list.Add("serperannus");
			}
			if (JetshiftGlobalNPC.downedReaper)
			{
				list.Add("reaper");
			}
			if (JetshiftGlobalNPC.downedCollin)
			{
				list.Add("collin");
			}
			if (JetshiftGlobalNPC.downedWallofVoices)
			{
				list.Add("wallOfVoices");
			}
			if (JetshiftGlobalNPC.downedEdrol)
			{
				list.Add("kingEdrol");
			}
			if (JetshiftGlobalNPC.downedDevilmon)
			{
				list.Add("devilmon");
			}
			if (JetshiftGlobalNPC.downedSeaCreatures)
			{
				list.Add("seaCreatures");
			}
			if (JetshiftGlobalNPC.downedIceYeti)
			{
				list.Add("blizzados");
			}
			if (JetshiftGlobalNPC.downedMortalos)
			{
				list.Add("mortalos");
			}
			if (JetshiftGlobalNPC.downedShiftFinal)
			{
				list.Add("shiftFinal");
			}
			if (JetshiftGlobalNPC.downedCCP1)
			{
				list.Add("crystal1");
			}
			if (JetshiftGlobalNPC.downedCCP2)
			{
				list.Add("crystal2");
			}
			if (JetshiftGlobalNPC.downedML)
			{
				list.Add("moonLord");
			}
			if (JetshiftGlobalNPC.downedStark)
			{
				list.Add("starkEnergy");
			}
			if (JetshiftWorld.spawnVoidMeteor)
			{
				list.Add("voidMeteor");
			}
			if (JetshiftGlobalNPC.nightmareMode)
			{
				list.Add("nightmareMode");
			}
			if (JetshiftGlobalNPC.downedAnnihilation)
			{
				list.Add("annihilation");
			}
            if (JetshiftGlobalNPC.downedCosmicMystery)
            {
                list.Add("cosmicMystery");
            }
            //JetshiftGlobalNPC.meteorPosition = this.meteorSaveAs;
            TagCompound tag = new TagCompound();
			tag.Add("downed", list);

			int meteorPosition = (int)Math.Round(JetshiftGlobalNPC.meteorPosition);
			tag.Add("meteorPosition", meteorPosition);
			int meteorPositionY = (int)Math.Round(JetshiftGlobalNPC.meteorPositionY);
			tag.Add("meteorPositionY", meteorPositionY);
			int meteorLife = (int)Math.Round(JetshiftGlobalNPC.meteorLife);
			tag.Add("meteorLife", meteorLife);
			int meteorMoveTimer = (int)(JetshiftGlobalNPC.meteorMoveTimer);
			tag.Add("meteorMoveTimer", meteorMoveTimer);
			double experienceInWorld = (double)JetshiftWorld.PlayerExp;
			tag.Add("experienceP", experienceInWorld);
			ulong experienceInWorldX = (ulong)JetshiftWorld.PlayerExpLevel;
			tag.Add("experienceX", experienceInWorldX);
			ulong experienceInWorldL = (ulong)JetshiftWorld.PlayerExpRequired;
			tag.Add("experienceL", experienceInWorldL);
			ulong experienceInWorldD = (ulong)JetshiftWorld.PlayerExpDiffRange;
			tag.Add("experienceD", experienceInWorldD);
			ulong experienceInWorldPL = (ulong)JetshiftWorld.PlayerExpPrevLevel;
			tag.Add("experiencePL", experienceInWorldPL);
			long experienceInWorldEP1 = (long)JetshiftGlobalNPC.experiHelp;
			tag.Add("experienceEP1", experienceInWorldEP1);
			long experienceInWorldEP2 = (long)JetshiftGlobalNPC.experiHelp2;
			tag.Add("experienceEP2", experienceInWorldEP2);
			float experienceInWorldEP3 = (float)JSExp.Interface.InterfaceHelper.experiHelp3;
			tag.Add("experienceEP3", experienceInWorldEP3);
			return tag;
		}
		
		public override void Load(TagCompound tag)
		{
			IList<string> list = tag.GetList<string>("downed");
			JetshiftGlobalNPC.downedSnakeBoss = list.Contains("serperannus");
			JetshiftGlobalNPC.downedReaper = list.Contains("reaper");
			JetshiftGlobalNPC.downedCollin = list.Contains("collin");
			JetshiftGlobalNPC.downedWallofVoices = list.Contains("wallOfVoices");
			JetshiftGlobalNPC.downedEdrol = list.Contains("kingEdrol");
			JetshiftGlobalNPC.downedDevilmon = list.Contains("devilmon");
			JetshiftGlobalNPC.downedSeaCreatures = list.Contains("seaCreatures");
			JetshiftGlobalNPC.downedIceYeti = list.Contains("blizzados");
			JetshiftGlobalNPC.downedMortalos = list.Contains("mortalos");
			JetshiftGlobalNPC.downedShiftFinal = list.Contains("shiftFinal");
			JetshiftGlobalNPC.downedCCP1 = list.Contains("crystal1");
			JetshiftGlobalNPC.downedCCP2 = list.Contains("crystal2");
			JetshiftGlobalNPC.downedCosmicMystery = list.Contains("cosmicMystery");
			JetshiftGlobalNPC.downedAnnihilation = list.Contains("annihilation");
			JetshiftWorld.spawnVoidMeteor = list.Contains("voidMeteor");
			JetshiftGlobalNPC.nightmareMode = list.Contains("nightmareMode");
			IList<int> intList = tag.GetList<int>("");
			int meteorPosition = tag.Get<int>("meteorPosition");
			int meteorPositionY = tag.Get<int>("meteorPositionY");
			int meteorLife = tag.Get<int>("meteorLife");
			int meteorMoveTimer = tag.Get<int>("meteorMoveTimer");
			JetshiftWorld.PlayerExp = tag.Get<double>("experienceP");
			JetshiftWorld.PlayerExpLevel = tag.Get<ulong>("experienceX");
			JetshiftWorld.PlayerExpRequired = tag.Get<ulong>("experienceL");
			JetshiftWorld.PlayerExpDiffRange = tag.Get<ulong>("experienceD");
			JetshiftWorld.PlayerExpPrevLevel = tag.Get<ulong>("experiencePL");
            JetshiftGlobalNPC.experiHelp = tag.Get<long>("experienceEP1");
            JetshiftGlobalNPC.experiHelp2 = tag.Get<long>("experienceEP2");
			JSExp.Interface.InterfaceHelper.experiHelp3 = tag.Get<float>("experienceEP3");
			if (meteorPosition > 0)
			{
				NPC.NewNPC(meteorPosition, meteorPositionY, mod.NPCType("MysteriousMeteor"), 0, 0f, 0f, 0f, 0f, 0);
			}
			JetshiftGlobalNPC.meteorMoveTimer = tag.Get<int>("meteorMoveTimer");
			JetshiftGlobalNPC.downedML = list.Contains("moonLord");
		}
		
		public override void PostUpdate()
		{
			if (JetshiftGlobalNPC.downedSnakeBoss)
			{
				SlaughterDMG = 150;
			}
			
			if (JetshiftGlobalNPC.downedReaper)
			{
				SlaughterDMG = 300;
			}
			
			if (JetshiftGlobalNPC.downedCollin)
			{
				SlaughterDMG = 425;
			}
			
			if (JetshiftGlobalNPC.downedWallofVoices)
			{
				SlaughterDMG = 600;
			}
			
			if (JetshiftGlobalNPC.downedEdrol)
			{
				SlaughterDMG = 850;
			}
			
			if (JetshiftGlobalNPC.downedDevilmon)
			{
				SlaughterDMG = 1000;
			}
			
			if (JetshiftGlobalNPC.downedSeaCreatures)
			{
				SlaughterDMG = 2000;
			}
			
			if (JetshiftGlobalNPC.downedIceYeti)
			{
				SlaughterDMG = 3000;
			}
			
			if (JetshiftGlobalNPC.downedMortalos)
			{
				SlaughterDMG = 5000;
			}
			
			if (JetshiftGlobalNPC.downedShiftFinal)
			{
				SlaughterDMG = 7500;
			}

			if (JetshiftGlobalNPC.downedCCP2)
			{
				SlaughterDMG = 10000;
			}

			this.meteorSaveAs = (int)JetshiftGlobalNPC.meteorPosition;
            PlayerExpLevel = JSHelper.ClampULong(PlayerExpLevel, 0, 100);
			
			for(int k = 0; k < Main.maxTilesX * Main.maxTilesY * 1E-05 * Main.worldRate; k++)
			{
				int x = WorldGen.genRand.Next(10, Main.maxTilesX - 10);
				int y = WorldGen.genRand.Next(10, Main.maxTilesY - 1);
				if(Main.tile[x, y] != null && Main.tile[x, y].liquid <= 32 && Main.tile[x, y].nactive())
				{
					UpdateTile(x, y);
				}
			}
		}
		
		public override void ResetNearbyTileEffects()
		{
			JetshiftWorld.greatDarknessTiles = 0;
			JetshiftWorld.mortalTiles = 0;
		}

		public override void TileCountsAvailable(int[] tileCounts)
		{
			JetshiftWorld.greatDarknessTiles = tileCounts[base.mod.TileType("VoidOre")] + tileCounts[base.mod.TileType("VoidSnow")] + tileCounts[base.mod.TileType("VoidIce")] + tileCounts[base.mod.TileType("VoidStoneBlock")];
			JetshiftWorld.mortalTiles = tileCounts[base.mod.TileType("HellBrickBlock")];
		}

		public static bool ModContentGenerated = false;

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            int shiniesIndex1 = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));

            tasks.Insert(shiniesIndex1 + 1, new PassLegacy("Great Darkness", delegate (GenerationProgress progress)
            {
				GreatDarknessGen(progress);
            }));
            
            ModContentGenerated = true;
        }

		private int greatDarknessSide = 0;
		private Vector2 greatDarknessPos = new Vector2(0, 0);

		private void GreatDarknessGen(GenerationProgress progress)
        {
            greatDarknessSide = ((Main.dungeonX > Main.maxTilesX / 2) ? (-1) : (1));
			switch (Main.maxTilesX)
			{
				case 4200:
            		greatDarknessPos.X = Main.maxTilesX / 3f;
					break;
				case 6400:
					greatDarknessPos.X = Main.maxTilesX / 2.7f;
					break;
				case 8400:
					greatDarknessPos.X = Main.maxTilesX / 2.4f;
					break;
				default:
					greatDarknessPos.X = Main.maxTilesX / 3f;
					break;
			}
            int j = (int)WorldGen.worldSurfaceLow - 30;
            while (Main.tile[(int)(greatDarknessPos.X), j] != null && !Main.tile[(int)(greatDarknessPos.X), j].active())
            {
                j++;
            }
            for (int l = (int)(greatDarknessPos.X) - 25; l < (int)(greatDarknessPos.X) + 25; l++)
            {
                for (int m = j - 6; m < j + 90; m++)
                {
                    if (Main.tile[l, m] != null && Main.tile[l, m].active())
                    {
                        int type = Main.tile[l, m].type;
                        if (type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Sunplate)
                        {
                            j++;
                            if (!Main.tile[l, m].active())
                            {
                                j++;
                            }
                        }
                    }
                }
            }

            greatDarknessPos.Y = j - 10f;
			GreatDarknessH();
            progress.Message = "Shrouding with darkness";
        }

		public void GreatDarknessH()
        {
            Point origin = new Point ((int)greatDarknessPos.X, (int)greatDarknessPos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            GreatDarkness biome = new GreatDarkness();
            GreatDarknessDelete delete = new GreatDarknessDelete();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
        }

		private void UpdateTile(int x1, int y2)
        {
			if (!JetshiftGlobalNPC.downedML)
			{
				return;
			}
            Tile tile = Main.tile[x1, y2];
            if (!tile.inActive() && ((tile.type == base.mod.TileType("VoidOre")) || (tile.type == base.mod.TileType("VoidSnow")) || (tile.type == base.mod.TileType("VoidIce")) || (tile.type == base.mod.TileType("VoidStoneBlock")) || (tile.type == base.mod.TileType("Tenebricite")))) // && WorldGen.genRand.Next(1) == 0)
            {
                bool flag = true;
                while (flag)
                {
                    flag = false;
                    int toX = x1 + WorldGen.genRand.Next(-8, 7);
                    int toY = y2 + WorldGen.genRand.Next(-8, 7);
                    bool tileChanged = false;
                    int targetType = Main.tile[toX, toY].type;
                    if ((targetType == 0 || targetType == 2) && !JetshiftGlobalNPC.downedShiftFinal)
                    {
                        Main.tile[toX, toY].type = (ushort)base.mod.TileType("VoidOre"); //6
                        tileChanged = true;
                    }
					if ((targetType == 0 || targetType == 2) && !JetshiftGlobalNPC.downedShiftFinal)
                    {
                        Main.tile[toX, toY].type = (ushort)base.mod.TileType("VoidOre"); //6
                        tileChanged = true;
                    }
					if ((targetType == 6 || targetType == 7 || targetType == 8 || targetType == 9 || targetType == 22 || targetType == 204) && !JetshiftGlobalNPC.downedShiftFinal)
                    {
                        Main.tile[toX, toY].type = (ushort)base.mod.TileType("Tenebricite"); //6
                        tileChanged = true;
                    }
					if ((targetType == 147) && !JetshiftGlobalNPC.downedShiftFinal)
                    {
                        Main.tile[toX, toY].type = (ushort)base.mod.TileType("VoidSnow"); //6
						tileChanged = true;
                    }
					if ((targetType == 1 || targetType == 25 || targetType == 203) && !JetshiftGlobalNPC.downedShiftFinal)
                    {
                        Main.tile[toX, toY].type = (ushort)base.mod.TileType("VoidStoneBlock"); //6
						tileChanged = true;
                    }
					if ((targetType == 161 || targetType == 162 || targetType == 163 || targetType == 164 || targetType == 200) && !JetshiftGlobalNPC.downedShiftFinal)
                    {
                        Main.tile[toX, toY].type = (ushort)base.mod.TileType("VoidIce"); //6
                        tileChanged = true;
                    }
					if (targetType == (ushort)base.mod.TileType("VoidOre") && JetshiftGlobalNPC.downedShiftFinal)
					{
						Main.tile[toX, toY].type = 0;
						tileChanged = true;
					}
					if (targetType == (ushort)base.mod.TileType("VoidSnow") && JetshiftGlobalNPC.downedShiftFinal)
					{
						Main.tile[toX, toY].type = 147;
						tileChanged = true;
					}
					if (targetType == (ushort)base.mod.TileType("VoidIce") && JetshiftGlobalNPC.downedShiftFinal)
					{
						Main.tile[toX, toY].type = 161;
						tileChanged = true;
					}
					if (targetType == (ushort)base.mod.TileType("VoidStoneBlock") && JetshiftGlobalNPC.downedShiftFinal)
					{
						Main.tile[toX, toY].type = 1;
						tileChanged = true;
					}
                    if (tileChanged)
                    {
						flag = true;
                        WorldGen.SquareTileFrame(toX, toY, true);
                        NetMessage.SendTileSquare(-1, toX, toY, 1);
                    }
                }
            }
        }
	}
}