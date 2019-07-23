using Microsoft.Xna.Framework;
using System;
using Terraria;
using JetshiftMod;
using JetshiftMod.NPCs;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace JetshiftMod.NPCs
{
	public class Quaker : ModNPC
	{
		private bool flag;
		private bool flag2;
		private int earthquakeTimer = -1;
		private int frameUpdate;
		private int wallTimer;
		private int spawnedTimer;
		
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Quaker");
			Main.npcFrameCount[base.npc.type] = 16;
		}

		public override void SetDefaults()
		{
			base.npc.damage = 190;
			base.npc.npcSlots = 12f;
			base.npc.width = 54;
			base.npc.height = 88;
			base.npc.defense = 40;
			base.npc.lifeMax = 5600;
			base.npc.aiStyle = -1;
			base.npc.takenDamageMultiplier = 1.4f;
			this.aiType = -1;
			this.animationType = 10;
			base.npc.knockBackResist = 0f;
			base.npc.boss = false;
			base.npc.value = (float)Item.buyPrice(0, 1, 0, 0);
			base.npc.alpha = 0;
			for (int i = 0; i < base.npc.buffImmune.Length; i++)
			{
				base.npc.buffImmune[i] = true;
			}
			base.npc.behindTiles = false;
			base.npc.noGravity = false;
			base.npc.noTileCollide = false;
			base.npc.HitSound = SoundID.NPCHit41;
			base.npc.DeathSound = SoundID.NPCDeath44;
			base.npc.netAlways = true;
		}

		public override void FindFrame(int frameHeight) //Frame counter
		{
			Player player2 = Main.player[base.npc.target];
			frameUpdate++;
			if (frameUpdate == 5)
			{
				base.npc.frame.Y = base.npc.frame.Y + frameHeight;
				frameUpdate = 0;
			}
			if (!flag && !flag2 && base.npc.frame.Y >= frameHeight * 7)
			{
				base.npc.frame.Y = 0;
				return;
			}
			if (flag)
			{
				base.npc.frame.Y = frameHeight * 7;
			}
			if (flag2)
			{
				if (JSHelper.WithinRange(base.npc.velocity.Y, -0.1f, 0.1f))
				{
					if (base.npc.frame.Y >= frameHeight * 14)
					{
						base.npc.frame.Y = frameHeight * 14;
						earthquakeTimer++;
						if (earthquakeTimer == 1)
						{
							Main.PlaySound(SoundID.NPCDeath14, (int)base.npc.Center.X, (int)base.npc.Center.Y);
							for (int j = 0; j < 20; j++)
							{
								int num7 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 2f);
								Main.dust[num7].velocity *= 14f;
								if (Main.rand.Next(2) == 0)
								{
									Main.dust[num7].scale = 0.5f;
									Main.dust[num7].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
								}
							}
						}
						if (earthquakeTimer < 240)
						{
							JetshiftGlobalNPC.isQuakerQuaking = false;
							if (JSHelper.WithinRange(earthquakeTimer, 0, 3))
							{
								JetshiftGlobalNPC.isQuakerQuaking = true;
							}
							int xCheck = (int)player2.Center.X / 16;
							int yCheck = (int)player2.Center.Y / 16;
							int c = -1;
							for (int a = xCheck - 2; a <= xCheck + 2; a++)
							{
								for (int b = yCheck; b <= yCheck + 2; b++)
								{
									if (WorldGen.SolidTile2(a, b))
									{
										c = b;
										break;
									}
								}
							}
							if (Vector2.Distance(player2.Center, base.npc.Center) < 800f && c != -1 && JSHelper.WithinRange(earthquakeTimer, 0, 3))
							{
								player2.velocity.Y -= 6f;
								player2.AddBuff(156, 180, true);
							}
							return;
						}
						earthquakeTimer = -120;
						base.npc.velocity.X = 1.54f;
						flag2 = false;
						return;
					}
					base.npc.frame.Y = base.npc.frame.Y + frameHeight;
					if (base.npc.frame.Y > frameHeight * 14)
					{
						base.npc.frame.Y = frameHeight * 14;
					}
					frameUpdate = 0;
				}
				else
				{
					base.npc.frame.Y = frameHeight * 9;
					earthquakeTimer = 0;
				}
			}
		}

		public override void AI()
		{
			JetshiftGlobalNPC.quakerPosition = base.npc.position;
			spawnedTimer++;
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (base.npc.Center.X > player.Center.X)
			{
				base.npc.direction = 1;
			}
			else
			{
				base.npc.direction = -1;
			}
			base.npc.spriteDirection = base.npc.direction;
			base.npc.timeLeft = 999999;
			if (JSHelper.WithinRange(base.npc.velocity.Y, -0.1f, 0.1f) && flag)
			{
				flag2 = true;
				flag = false;
			}
			if (Vector2.Distance(player.Center, base.npc.Center) < 400f && Main.rand.Next(1,400) == 200 && !flag && !flag2)
			{
				base.npc.velocity.Y -= 9f;
				flag = true;
			}
			if (JSHelper.WithinRange(base.npc.velocity.X, -0.1f, 0.1f) && JSHelper.WithinRange(base.npc.velocity.Y, -0.1f, 0.1f) && !flag && !flag2)
			{
				int xCheck2 = (int)base.npc.Center.X / 16;
				int yCheck2 = (int)base.npc.Center.Y / 16;
				int c2 = -1;
				for (int a2 = xCheck2 - 4; a2 <= xCheck2 + 4; a2++)
				{
					for (int b2 = yCheck2; b2 <= yCheck2 + 4; b2++)
					{
						if (WorldGen.SolidTile2(a2, b2))
						{
							c2 = b2;
							break;
						}
					}
				}
				if (base.npc.Center.X < player.Center.X)
				{
					base.npc.velocity.X = 1.5f;
				}
				if (base.npc.Center.X > player.Center.X)
				{
					base.npc.velocity.X = -1.5f;
				}
				if (wallTimer++ > 50 && c2 != -1)
				{
					base.npc.velocity.Y -= 9f;
					flag = true;
					wallTimer = 0;
				}
			}
			if (!flag)
			{
				if (base.npc.Center.X < player.Center.X)
				{
					base.npc.velocity.X = 1.5f;
				}
				if (base.npc.Center.X > player.Center.X)
				{
					base.npc.velocity.X = -1.5f;
				}
			}
			if (flag2)
			{
				base.npc.velocity.X = 0f;
			}
		}

		private void Talk(string Words)
		{
			Main.NewText(Words, 255, 200, 0, false);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.playerSafe || !Main.LocalPlayer.GetModPlayer<JetshiftModPlayer>(mod).ZoneGreatDarkness)
			{
				return 0f;
			}
			if (Main.LocalPlayer.GetModPlayer<JetshiftModPlayer>(mod).ZoneGreatDarkness && Main.player[Main.myPlayer].ZoneDirtLayerHeight && !NPC.AnyNPCs(base.mod.NPCType("Quaker")))
			{
				return 0.3f;
			}
			return 0f;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = 7400;
			base.npc.damage = 245;
			if (JetshiftGlobalNPC.nightmareMode)
			{
				base.npc.lifeMax = 9000;
				base.npc.damage = 300;
			}
		}
	}
}
