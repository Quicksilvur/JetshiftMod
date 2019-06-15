using JetshiftMod.NPCs;
using JetshiftMod.NPCs.Collin;
using JetshiftMod.NPCs.Mortalos;
using JetshiftMod.NPCs.CrystalConflict;
using JetshiftMod.NPCs.ShiftFinal;
using JetshiftMod.NPCs.CosmicMystery;
using JetshiftMod.Backgrounds;
using JSExp.Interface;
using JetshiftMod.Interface;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using System;

namespace JetshiftMod
{
	public class JetshiftMod : Mod
	{
		internal static JetshiftMod instance;
		public static Texture2D texture;
		public static float GDDarkness;

		public void Jetshift()
		{
			base.Properties = new ModProperties
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            ModLoader.GetMod("JetshiftMod");
            if (Main.myPlayer != -1 && !Main.gameMenu && Main.musicVolume != 0f)
            {
                if (Main.LocalPlayer.active && Main.player[Main.myPlayer].GetModPlayer<JetshiftModPlayer>(this).ZoneGreatDarkness && !JetshiftGlobalNPC.AnyBossNPCS())
                {
                    music = base.GetSoundSlot(SoundType.Music, "Sounds/Music/VoidBiome");
                    priority = MusicPriority.BiomeHigh;
                }

                if (Main.LocalPlayer.active && Main.player[Main.myPlayer].ZoneDirtLayerHeight && Main.player[Main.myPlayer].GetModPlayer<JetshiftModPlayer>(this).ZoneGreatDarkness && !JetshiftGlobalNPC.AnyBossNPCS())
                {
                    music = base.GetSoundSlot(SoundType.Music, "Sounds/Music/DeepVoidBiome");
                    priority = MusicPriority.BiomeHigh;
                }

                if (Main.LocalPlayer.active && Main.player[Main.myPlayer].ZoneRockLayerHeight && Main.player[Main.myPlayer].GetModPlayer<JetshiftModPlayer>(this).ZoneGreatDarkness && !JetshiftGlobalNPC.AnyBossNPCS())
                {
                    music = base.GetSoundSlot(SoundType.Music, "Sounds/Music/DeepVoidBiome");
                    priority = MusicPriority.BiomeHigh;
                }
            }

            if (JetshiftGlobalNPC.downedCosmicMystery)
            {
                music = base.GetSoundSlot(SoundType.Music, "Sounds/Music/Safe");
            }
        }
		
		public override void Load()
		{
			JetshiftMod.instance = this;
			if (!Main.dedServ)
			{
				base.AddMusicBox(base.GetSoundSlot(SoundType.Music, "Sounds/Music/CharlieMusic"), base.ItemType("Laptop"), base.TileType("CharlieMusicBox"), 0);
				texture = GetTexture("ExtraTextures/UI/ExperienceBar");
				Filters.Scene["JetshiftMod:Backgrounds"] = new Filter(new ScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0f).UseOpacity(0.8f), EffectPriority.High);
				SkyManager.Instance["JetshiftMod:Backgrounds"] = new VoidBgStyleSky();
				Filters.Scene["JetshiftMod:Collin"] = new Filter(new CollinScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.0f, 0.2f).UseOpacity(0.6f), EffectPriority.VeryHigh);
				SkyManager.Instance["JetshiftMod:Collin"] = new CollinSky();
				Filters.Scene["JetshiftMod:CrystalConflict"] = new Filter(new CCScreenShaderData("FilterMiniTower").UseColor(1.0f, 0.0f, 0.0f).UseOpacity(0.6f), EffectPriority.VeryHigh);
				SkyManager.Instance["JetshiftMod:CrystalConflict"] = new CCSky();
				Filters.Scene["JetshiftMod:CosmicMystery"] = new Filter(new CMScreenShaderData("FilterMiniTower").UseColor(1.0f, 1.0f, 1.0f).UseOpacity(0f), EffectPriority.VeryHigh);
				SkyManager.Instance["JetshiftMod:CosmicMystery"] = new CMSky();
				Filters.Scene["JetshiftMod:Mortalos"] = new Filter(new MScreenShaderData("FilterMiniTower").UseColor(0.7f, 0.0f, 0.0f).UseOpacity(0.7f), EffectPriority.VeryHigh);
				SkyManager.Instance["JetshiftMod:Mortalos"] = new MSky();
				Filters.Scene["JetshiftMod:ShiftFinal"] = new Filter(new SFScreenShaderData("FilterMiniTower").UseColor(0.7f, 0.0f, 0.7f).UseOpacity(0.6f), EffectPriority.VeryHigh);
				SkyManager.Instance["JetshiftMod:ShiftFinal"] = new SFSky();
			}
			//Main.logoTexture = base.GetTexture("Logo");
			//Main.logo2Texture = base.GetTexture("Logo2");
		}
		
		public override void PostSetupContent()
		{
			Mod mod = ModLoader.GetMod("JetshiftMod");
			Mod mod2 = ModLoader.GetMod("BossChecklist");
			Mod mod3 = ModLoader.GetMod("CheatSheet");
			Mod mod4 = ModLoader.GetMod("AAMod");
			
			try
			{
				JetshiftGlobalNPC.CalamityInstalled = ((ModLoader.GetMod("CalamityMod") != null) || (ModLoader.GetMod("AAMod") != null));
				JetshiftGlobalNPC.CheatSheetInstalled = (ModLoader.GetMod("CheatSheet") != null);
			}
			catch (Exception ex)
			{
				ErrorLogger.Log("JetshiftMod PostSetupContent Error: " + ex.StackTrace + ex.Message);
			}
			
			if (mod2 != null)
			{
				Mod mod3 = mod2;
				object[] array = new object[5];
				array[0] = "AddBossWithInfo";
				array[1] = "Serperannus";
				array[2] = 5.2f;
				array[3] = new Func<bool>(() => JetshiftGlobalNPC.downedSnakeBoss);
				array[4] = "Use a [i:" + mod.ItemType("SoilBall") + "] in the Underground Jungle";
				mod3.Call(array);
				Mod mod4 = mod2;
				object[] array2 = new object[5];
				array2[0] = "AddBossWithInfo";
				array2[1] = "Torva-mes";
				array2[2] = 5.8f;
				array2[3] = new Func<bool>(() => JetshiftGlobalNPC.downedReaper);
				array2[4] = "Use a [i:" + mod.ItemType("Curse") + "] in the Underworld";
				mod4.Call(array2);
				Mod mod5 = mod2;
				object[] array3 = new object[5];
				array3[0] = "AddBossWithInfo";
				array3[1] = "Collin";
				array3[2] = 4.4f;
				array3[3] = new Func<bool>(() => JetshiftGlobalNPC.downedCollin);
				array3[4] = "Use a [i:" + mod.ItemType("CorruptedShard") + "] in the Corruption/Crimson";
				mod5.Call(array3);
				Mod mod6 = mod2;
				object[] array4 = new object[5];
				array4[0] = "AddBossWithInfo";
				array4[1] = "Wall of Voices";
				array4[2] = 6.1f;
				array4[3] = new Func<bool>(() => JetshiftGlobalNPC.downedWallofVoices);
				array4[4] = "Use a [i:" + mod.ItemType("CursedWall") + "] in the Underworld";
				mod6.Call(array4);
				Mod mod7 = mod2;
				object[] array5 = new object[5];
				array5[0] = "AddBossWithInfo";
				array5[1] = "Arlenon";
				array5[2] = 8.46f;
				array5[3] = new Func<bool>(() => JetshiftGlobalNPC.downedEdrol);
				array5[4] = "Use a [i:" + mod.ItemType("PearlOfTheSky") + "] in Space";
				mod7.Call(array5);
				Mod mod8 = mod2;
				object[] array6 = new object[5];
				array6[0] = "AddBossWithInfo";
				array6[1] = "Athazel";
				array6[2] = 9.67f;
				array6[3] = new Func<bool>(() => JetshiftGlobalNPC.downedDevilmon);
				array6[4] = "Use a [i:" + mod.ItemType("TridentOfDoom") + "] in the Underworld";
				mod8.Call(array6);
				Mod mod9 = mod2;
				object[] array7 = new object[5];
				array7[0] = "AddBossWithInfo";
				array7[1] = "Polypus";
				array7[2] = 12.8f;
				array7[3] = new Func<bool>(() => JetshiftGlobalNPC.downedSeaCreatures);
				array7[4] = "Use a [i:" + mod.ItemType("RottenShrimp") + "] in the Ocean";
				mod9.Call(array7);
				Mod mod10 = mod2;
				object[] array8 = new object[5];
				array8[0] = "AddBossWithInfo";
				array8[1] = "Blizzados";
				array8[2] = 16.1f;
				array8[3] = new Func<bool>(() => JetshiftGlobalNPC.downedIceYeti);
				array8[4] = "Use a [i:" + mod.ItemType("Cryogen") + "] in the Tundra";
				mod10.Call(array8);
				Mod mod11 = mod2;
				object[] array9 = new object[5];
				array9[0] = "AddBossWithInfo";
				array9[1] = "Mortalos";
				array9[2] = 18.6f;
				array9[3] = new Func<bool>(() => JetshiftGlobalNPC.downedMortalos);
				array9[4] = "Use a [i:" + mod.ItemType("TheVoid") + "] in the Underworld, but on a special platform";
				mod11.Call(array9);
				Mod mod12 = mod2;
				object[] array10 = new object[5];
				array10[0] = "AddBossWithInfo";
				array10[1] = "The Annihilation";
				array10[2] = 20.3f;
				array10[3] = new Func<bool>(() => JetshiftGlobalNPC.downedAnnihilation);
				array10[4] = "Use a [i:" + mod.ItemType("PitchBlackness") + "] in The Great Darkness";
				mod12.Call(array10);
				Mod mod13 = mod2;
				object[] array11 = new object[5];
				array11[0] = "AddBossWithInfo";
				array11[1] = "Shift Final";
				array11[2] = 22.7f;
				array11[3] = new Func<bool>(() => JetshiftGlobalNPC.downedShiftFinal);
				array11[4] = "Use a [i:" + mod.ItemType("ArtifactOfTheVoid") + "] in The Great Darkness";
				mod13.Call(array11);
				Mod mod14 = mod2;
				object[] array12 = new object[5];
				array12[0] = "AddBossWithInfo";
				array12[1] = "Crystal Conflict";
				array12[2] = 30.3f;
				array12[3] = new Func<bool>(() => JetshiftGlobalNPC.downedCCP2);
				array12[4] = "Use a [i:" + mod.ItemType("CrystaloftheMultiverse") + "] anywhere";
				mod14.Call(array12);
                Mod mod15 = mod2;
                object[] array13 = new object[5];
                array13[0] = "AddBossWithInfo";
                array13[1] = "???";
                array13[2] = 999f;
                array13[3] = new Func<bool>(() => JetshiftGlobalNPC.downedCosmicMystery);
                array13[4] = "Destroy the mystery";
                mod15.Call(array13);
            }
		}
		
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			//JSExp.Interface.InterfaceHelper.ModifyInterfaceLayers(layers);
            InterfaceHelperExt.ModifyInterfaceLayers(layers);
        }
	}  
}