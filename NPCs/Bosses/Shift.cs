using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JetshiftMod;
using JetshiftMod.NPCs;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JetshiftMod.NPCs.Shift
{
	//[AutoloadBossHead]
	public class Shift : ModNPC
	{
		private int form;
		private bool flag;
		private bool flag2;
		private bool flag3;
		private bool flag7;
		private bool flag8;
		private bool flag9;
		private bool headLoose;
		private bool armsLoose;
		private int cannonTimer;
		private int formUpdate;
		private int num;
		private int enrageTimer;
		private int cocoonLife;
		private int picker;
		private int num4;
		private int num5;

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shift, Defender of Terraria");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.dontTakeDamage = false;
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 82000;
			if (Main.expertMode)
			{
				base.npc.lifeMax = 54250;
				if (JetshiftGlobalNPC.nightmareMode)
				{
					base.npc.lifeMax = 82500;
				}
			}
			base.npc.damage = 50;
			base.npc.defense = 30;
			base.npc.knockBackResist = 0f;
			base.npc.width = 128;
			base.npc.height = 128;
			base.npc.value = (float)Item.buyPrice(0, 30, 0, 0);
			base.npc.npcSlots = 200f;
			base.npc.boss = true;
			base.npc.alpha = 255;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit34;
			base.npc.DeathSound = SoundID.NPCDeath56;
			base.npc.scale = 1f;
			this.form = 1;
			for (int i = 0; i < base.npc.buffImmune.Length; i++)
			{
				base.npc.buffImmune[i] = true;
			}
			this.music = (this.music = base.mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Shift"));
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 1f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 2f);
		}
		
		private void Talk(string Words)
		{
			Main.NewText(Words, 232, 217, 229, false);
		}
		
		private void TalkEdgy(string Words)
		{
			Main.NewText(Words, 255, 100, 86, false);
		}
		
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
		}
		
		public override void FindFrame(int frameHeight)
		{
			base.npc.frameCounter++;
			if (base.npc.frameCounter > 3)
			{
				base.npc.frame.Y = base.npc.frame.Y + frameHeight;
				base.npc.frameCounter = 0;
			}
			if (base.npc.frame.Y >= frameHeight * 5 && form == 7)
			{
				base.npc.frame.Y = frameHeight * 5;
				return;
			}
			if (base.npc.frame.Y >= frameHeight * 8)
			{
				base.npc.frame.Y = 0;
				return;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Mod mod = ModLoader.GetMod("JetshiftMod");
			Texture2D texture = null;
			switch (form)
			{
				case 1:
					texture = mod.GetTexture("NPCs/Shift/Shift");
					break;
				case 2:
					texture = mod.GetTexture("NPCs/Shift/Shift_2");
					break;
				case 3:
					texture = mod.GetTexture("NPCs/Shift/Shift_3");
					break;
				case 4:
					texture = mod.GetTexture("NPCs/Shift/Shift_4");
					break;
				case 5:
					texture = mod.GetTexture("NPCs/Shift/Shift_5");
					break;
				case 6:
					texture = mod.GetTexture("NPCs/Shift/Shift_6");
					break;
				case 7:
					texture = mod.GetTexture("NPCs/Shift/Shift_7");
					break;
				case 8:
					texture = mod.GetTexture("NPCs/Shift/Shift_8");
					break;
				case 9:
					texture = mod.GetTexture("NPCs/Shift/Shift_9");
					break;
				case 10:
					texture = mod.GetTexture("NPCs/Shift/Shift_10");
					break;
				case 11:
					texture = mod.GetTexture("NPCs/Shift/Shift_11");
					break;
				case 12:
					texture = mod.GetTexture("NPCs/Shift/Shift_12");
					break;
				case 13:
					texture = mod.GetTexture("NPCs/Shift/Shift_13");
					break;
				case 14:
					texture = mod.GetTexture("NPCs/Shift/Shift_14");
					break;
				
			}
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			BaseDrawing.DrawTexture(spriteBatch, texture, 0, base.npc.Center - new Vector2(56f, 0f), base.npc.width, base.npc.height, base.npc.scale, base.npc.rotation, base.npc.spriteDirection, 5, base.npc.frame, drawColor, false, default(Vector2));
			return true;
		}

		public override void NPCLoot()
		{
			
		}
		
		public override bool CheckActive()
		{
			return true;
		}

		private int[] phaseTransitions = new int[]
		{
			1,
			2,
			1,
			3,
			1,
			4,
			1,
			5,
			6,
			7,
			8,
			9,
			8,
			10,
			8,
			11,
			8,
			12,
			13,
			9999
		};

		public override void AI()
		{
			Vector2 arg_11_0 = base.npc.Center;
			Vector2 vector2 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			Player player = Main.player[base.npc.target];
			base.npc.TargetClosest(true);
			float num1 = player.Center.X - vector2.X;
			float num2 = player.Center.Y - vector2.Y;
			if (num4++ > 300)
			{
				num4 = 0;
				FireProjectileAtPlayer(base.mod.ProjectileType("CCProj"));
			}
			//Form 1 = Idle
			//Form 2 = Idle (Armless)
			//Form 3 = Idle (Headless)
			//Form 4 = Idle (Headless & Armless)
			//Form 5 = Spinning Charge
			//Form 6 = Cannon Phase
			//Form 7 = Enrage Transition
			//Form 8-14 = Enraged Forms
			if (this.armsLoose || this.headLoose || form == 7)
			{
				formUpdate = 0;
			}
			if (formUpdate++ > 600)
			{
				picker++;
				if (phaseTransitions[picker] == 8 && !flag)
				{
					picker = 1;
				}
				if (phaseTransitions[picker] == 15 && flag)
				{
					picker = 8;
				}
				if (phaseTransitions[picker] == 9999)
				{
					picker = 11;
				}
				form = phaseTransitions[picker];
				formUpdate = 0;
			}
			float speedNorm = 4f;
			if (form == 5)
			{
				speedNorm *= 2.5f;
			}
			if (flag)
			{
				speedNorm *= 2f;
			}
			if (num5++ > 360)
			{
				speedNorm *= 10f;
				if (num5 > 390)
				{
					num5 = 0;
				}
			}
			base.npc.velocity = JSHelper.MoveTowardsPlayer(speedNorm, base.npc.velocity.X, base.npc.velocity.Y, player, base.npc.Center, base.npc.direction);
			if (base.npc.life < (int)(base.npc.lifeMax * 0.75) && !flag7)
			{
				Talk("INITIATING PRIMARY ELIMINATION PHASE. TARGET ACQUIRED.");
				flag7 = true;
			}
			if (base.npc.life < (int)(base.npc.lifeMax * 0.5) && !flag8)
			{
				Talk("INITIATING SECONDARY ANNIHILATION PHASE. TARGET LOST.");
				flag8 = true;
			}
			if (base.npc.life < (int)(base.npc.lifeMax * 0.25) && !flag9)
			{
				Talk("WARNING: SEVERE DAMAGE DETECTED. ENGAGING IN RECOVERY SAFETY PROTOCOL.");
				flag9 = true;
			}
			if (form == 6 || form == 13)
			{
				if (form == 6)
				{
					flag3 = true;
				}
				else
				{
					flag3 = false;
				}
				int cannonFire = flag3 ? 120 : 60;
				if (cannonTimer++ > cannonFire)
				{
					Main.PlaySound(base.mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/CCBlast"), (int)base.npc.position.X, (int)base.npc.position.Y);
					FireProjectileAtPlayer(base.mod.ProjectileType("CCBlast"));
					cannonTimer = 0;
					num = 0;
				}
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
			}
			base.npc.rotation = base.npc.velocity.X / 40f;
			if (form == 7)
			{
				base.npc.velocity.X = 0f;
				base.npc.velocity.Y = 0f;
				int num3 = Main.expertMode ? 1620 : 2100;
				if (!flag2)
				{
					cocoonLife = 6000;
					flag2 = true;
				}
				if (cocoonLife <= 0)
				{
					Talk("TRANSFORM PHASE CANCELLED. REGAINING POWER.");
					enrageTimer = 0;
					form = 1;
				}
				enrageTimer++;
				if (enrageTimer > num3 && !flag)
				{
					Main.PlaySound(SoundLoader.customSoundType, (int)Main.player[Main.myPlayer].position.X, (int)Main.player[Main.myPlayer].position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ShiftSpawn"));
					Talk("TRANSFORM COMPLETE. SHIFTING TO SECOND TIER.");
					enrageTimer = 0;
					SpewEnergy();
					flag = true;
					form = 8;
					formUpdate = 0;
					base.npc.damage *= 2;
					base.npc.defense *= 2;
				}
			}
			else
			{
				enrageTimer = 0;
			}
			if ((form == 2 || form == 4 || form == 9 || form == 11) && !this.armsLoose)
			{
				this.armsLoose = true;
				NPC.NewNPC((int)base.npc.Center.X + 8, (int)base.npc.Center.Y, base.mod.NPCType("Shift_ArmLeft"), base.npc.whoAmI, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.Center.X - 8, (int)base.npc.Center.Y, base.mod.NPCType("Shift_ArmRight"), base.npc.whoAmI, 0f, 0f, 0f, 0f, 255);
			}
			if ((form == 3 || form == 4 || form == 10 || form == 11) && !this.headLoose)
			{
				this.headLoose = true;
				int num4 = NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y - 16, base.mod.NPCType("Shift_Head"), base.npc.whoAmI, 0f, 0f, 0f, 0f, 255);
				if (form > 8)
				{
					Main.npc[num4].frame.Y = 24;
				}
			}
			bool flag4 = NPC.AnyNPCs(base.mod.NPCType("Shift_ArmLeft"));
			bool flag5 = NPC.AnyNPCs(base.mod.NPCType("Shift_ArmRight"));
			bool flag6 = NPC.AnyNPCs(base.mod.NPCType("Shift_Head"));
			if (!flag4 && !flag5)
			{
				if (form == 2 || form == 9)
				{
					picker++;
					form = phaseTransitions[picker];
					this.armsLoose = false;
				}
			}
			if (!flag6)
			{
				if (form == 3 || form == 10)
				{
					picker++;
					form = phaseTransitions[picker];
					this.headLoose = false;
				}
			}
			if (!flag4 && !flag5 && !flag6 && (form == 4 || form == 11))
			{
				picker++;
				form = phaseTransitions[picker];
				this.armsLoose = false;
				this.headLoose = false;
			}
			if (base.npc.Center.X < player.Center.X)
			{
				base.npc.direction = 1;
			}
			else
			{
				base.npc.direction = -1;
			}
			base.npc.spriteDirection = base.npc.direction;
		}

		private void FireProjectileAtPlayer(int type)
		{
			float pnum1 = 15f;
			float pnum2 = (float) (Main.player[base.npc.target].position.X + (double) Main.player[base.npc.target].width * 0.5 - npc.position.X) + (float) Main.rand.Next(-50, 51);
			float pnum3 = (float) (Main.player[base.npc.target].position.Y + (double) Main.player[base.npc.target].height * 0.5 - npc.position.Y) + (float) Main.rand.Next(-50, 51);
			float pnum4 = (float) Math.Sqrt((double) pnum2 * (double) pnum2 + (double) pnum3 * (double) pnum3);
			float pnum5 = pnum1 / pnum4;
			float SpeedX = pnum2 * pnum5;
			float SpeedY = pnum3 * pnum5;
			int index = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, SpeedX, SpeedY, type, 80, 0.0f, Main.myPlayer, 0.0f, 0.0f);
			Main.projectile[index].hostile = true;
			Main.projectile[index].friendly = false;

			if (type == base.mod.ProjectileType("CCProj"))
			{
				Vector2 vector = new Vector2(SpeedX, SpeedY).RotatedBy(0.3141592, default(Vector2));
				Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, vector.X, vector.Y, type, 30, 0f, Main.myPlayer, 0f, 0f);
				Vector2 vector2 = new Vector2(SpeedX, SpeedY).RotatedBy(-0.3141592, default(Vector2));
				Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, vector2.X, vector2.Y, type, 30, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			Player player = Main.player[base.npc.target];
			if (player.vortexStealthActive && projectile.ranged)
			{
				crit = false;
			}
		}
		
		private void SpewEnergy()
		{
			for (int j = 0; j < 40; j++)
			{
				int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, new Color(0, 255, 0, 255), 2f);
				Main.dust[num].velocity *= 3f;
				if (Main.rand.Next(2) == 0)
				{
					Main.dust[num].scale = 0.5f;
					Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int k = 0; k < 70; k++)
			{
				int num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, new Color(255, 255, 0, 255), 3f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].velocity *= 5f;
				num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, new Color(255, 255, 0, 255), 2f);
				Main.dust[num2].velocity *= 2f;
			}
		}
		
		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (damage == (double)base.npc.lifeMax)
			{
				this.Talk("BUTCHER FUNCTION DETECTED. ATTACK NEGATED - MAXIMUM DAMAGE.");
				damage = 0.0;
				return false;
			}
			
			if (damage > (double)(base.npc.lifeMax / 4))
			{
				this.Talk("MASSIVE DAMAGE DETECTED. ATTACK NEGATED - MAXIMUM DAMAGE.");
				damage = 0.0;
				return false;
			}

			if (cocoonLife > 0)
			{
				cocoonLife -= (int)damage;
			}
			return true;
		}
		
		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Talk("MISSION FAILED. SHUTTING DOWN.");
				this.SpewEnergy();
			}
			for (int i = 0; i < 2; i++)
			{
				Dust.NewDust(base.npc.position, base.npc.width, base.npc.height, 235, (float)hitDirection, -1f, 0, new Color(0, 127, 0, 255), 1f);
			}
		}
		
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			player.AddBuff(164, (Main.expertMode ? 660 : 300), true);
		}
	}
}
