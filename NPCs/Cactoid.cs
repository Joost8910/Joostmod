using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs
{
	public class Cactoid : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cactoid");
			Main.npcFrameCount[NPC.type] = 8;
		}
		public override void SetDefaults()
		{
			NPC.width = 24;
			NPC.height = 70;
			NPC.damage = 0;
			NPC.defense = 5;
			if (Main.expertMode)
			{
                if (Main.hardMode)
                {
                    NPC.lifeMax = 300;
                }
                else
                {
                    NPC.lifeMax = 150;
                }
                NPC.defense = 10;
            }
			else
			{
				NPC.lifeMax = 75;
            }
            if (NPC.downedPlantBoss)
            {
                if (Main.expertMode)
                {
                    NPC.lifeMax = 450;
                }
                else
                {
                    NPC.lifeMax = 225;
                }
                NPC.defense = 20;
            }
            if (NPC.downedMoonlord)
            {
                if (Main.expertMode)
                {
                    NPC.lifeMax = 1800;
                }
                else
                {
                    NPC.lifeMax = 900;
                }
                NPC.defense = 30;
            }
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 0, 0, 90);
            NPC.knockBackResist = 0.4f;
			NPC.aiStyle = -1;
			NPC.frameCounter = 0;
			Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("CactoidBanner").Type;  
		}
        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.Cactus, 10);
            if (Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Anniversary").Type, 1);
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == Mod.Find<ModNPC>("Cactus Person").Type)
            {
                return false;
            }
            return base.CanHitNPC(target);
        }
        public override void FindFrame(int frameHeight)
		{
			NPC.spriteDirection = NPC.direction;
			NPC.frameCounter++;
			if(NPC.velocity.X != 0)
			{
				if (NPC.frameCounter >= 15 / (1+Math.Abs(NPC.velocity.X)))
				{
					NPC.frameCounter = 0;	
					NPC.frame.Y = (NPC.frame.Y + 74);		
				}
				if (NPC.frame.Y >= 296)
				{
					NPC.frame.Y = 0;	
				}
			}
			else
			{
				if (NPC.frameCounter >= 6)
				{
					NPC.frameCounter = 0;	
					NPC.frame.Y = (NPC.frame.Y + 74);		
				}
				if (NPC.frame.Y >= 592)
				{
					NPC.frame.Y = 296;	
				}
			}
		}
		 public override void HitEffect(int hitDirection, double damage)
		 {
			NPC.ai[2]++;
            for (int n = 0; n < 200; n++)
            {
                NPC N = Main.npc[n];
                if (N.active && N.Distance(NPC.Center) < 400 && Collision.CanHitLine(NPC.Center, 1, 1, N.Center, 1, 1) && (N.type == Mod.Find<ModNPC>("Cactite").Type || N.type == Mod.Find<ModNPC>("Cactoid").Type))
                {
                    N.target = NPC.target;
                    N.ai[2]++;
                    N.netUpdate = true;
                }
            }
            if (NPC.life <= 0)
			{
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Cactite1"), 1f);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Cactite2"), 1f);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Cactite2"), 1f);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Cactoid1"), 1f);
				Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Cactoid2"), 1f);
			}
            if (NPC.friendly)
            {
                NPC.ai[3] = 45;
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (target.GetModPlayer<JoostPlayer>().cactoidCommendation)
            {
                return false;
            }
            return base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void AI()
		{
            if (!NPC.friendly && (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active))
            {
                NPC.TargetClosest(false);
            }
            Player player = Main.player[NPC.target];
            bool playerCactoid = (player.GetModPlayer<JoostPlayer>().cactoidCommendation || player.HasBuff(Mod.Find<ModBuff>("CactoidFriend").Type));
            bool cactusPersonNear = false;
            int cactusPerson = -1;
            if (!playerCactoid)
            {
                float num = 600f;
                for (int i = 0; i < 255; i++)
                {
                    if (Main.player[i].active && !Main.player[i].dead && !Main.player[i].ghost && (Main.player[i].GetModPlayer<JoostPlayer>().cactoidCommendation || Main.player[i].HasBuff(Mod.Find<ModBuff>("CactoidFriend").Type)))
                    {
                        float num4 = Math.Abs(Main.player[i].Center.X - NPC.Center.X + Math.Abs(Main.player[i].Center.Y - NPC.Center.Y));
                        if (num4 < num)
                        {
                            num = num4;
                            NPC.target = i;
                            playerCactoid = true;
                        }
                    }
                }
                for (int k = 0; k < 200; k++)
                {
                    NPC cactu = Main.npc[k];
                    if (cactu.active && cactu.type == Mod.Find<ModNPC>("Cactus Person").Type && NPC.Distance(cactu.Center) < 800)
                    {
                        cactusPersonNear = true;
                        cactusPerson = cactu.whoAmI;
                        break;
                    }
                }
            }
            if (playerCactoid || cactusPersonNear)
            {
                NPC.friendly = true;
            }
            else
            {
                NPC.friendly = false;
            }
            if (NPC.friendly)
            {
                float idleAccel = 0.05f;
                float viewDist = 600f;
                float chaseAccel = 15;
                float inertia = 25f;
                NPC.dontCountMe = true;
                if (NPC.ai[3] > 0)
                {
                    NPC.dontTakeDamageFromHostiles = true;
                    NPC.ai[3]--;
                }
                else
                {
                    NPC.dontTakeDamageFromHostiles = false;
                }
                NPC.ai[2] = 0;
                if (Main.expertMode)
                {
                    if (Main.hardMode)
                    {
                        NPC.damage = 75;
                    }
                    else
                    {
                        NPC.damage = 50;
                    }
                }
                else
                {
                    NPC.damage = 25;
                }
                if (NPC.downedMoonlord)
                {
                    if (Main.expertMode)
                    {
                        NPC.damage = 150;
                    }
                    else
                    {
                        NPC.damage = 75;
                    }
                }
                if (NPC.localAI[1] % 15 == 0 && NPC.life < NPC.lifeMax)
                {
                    NPC.life++;
                }
                if (NPC.localAI[1] <= 0)
                {
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(NPC.Center, Vector2.Zero, Mod.Find<ModProjectile>("Cactite").Type, NPC.damage, 2, Main.myPlayer, NPC.whoAmI);
                    }
                    NPC.localAI[1] = 40;
                }
                NPC.localAI[1]--;

                int num = 1;
                for (int k = 0; k < NPC.whoAmI; k++)
                {
                    if (Main.npc[k].active && Main.npc[k].target == NPC.target && (Main.npc[k].type == NPC.type || Main.npc[k].type == Mod.Find<ModNPC>("Cactite").Type))
                    {
                        num++;
                        if (num > 40)
                        {
                            num = 0;
                        }
                    }
                }
                if (NPC.velocity.X > 0f)
                {
                    NPC.direction = 1;
                }
                else if (NPC.velocity.X < 0f)
                {
                    NPC.direction = -1;
                }
                else if (NPC.velocity.Y == 0 && playerCactoid && Math.Abs(NPC.Center.X - (player.Center.X - ((10 + num * 40) * player.direction))) > 30)
                {
                    NPC.velocity.Y = -10;
                }
                if (playerCactoid)
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC otherCactoid = Main.npc[k];
                        if (k != NPC.whoAmI && otherCactoid.friendly && otherCactoid.active && otherCactoid.target == NPC.target && (otherCactoid.type == NPC.type || otherCactoid.type == Mod.Find<ModNPC>("Cactoid").Type) && Math.Abs(NPC.position.X - otherCactoid.position.X) + Math.Abs(NPC.position.Y - otherCactoid.position.Y) < NPC.width)
                        {
                            if (NPC.position.X < otherCactoid.position.X)
                            {
                                NPC.velocity.X -= idleAccel;
                            }
                            else
                            {
                                NPC.velocity.X += idleAccel;
                            }
                        }
                    }
                }
                Vector2 targetPos = NPC.position;
                float targetDist = viewDist;
                bool target = false;
                if (player.HasMinionAttackTargetNPC && playerCactoid)
                {
                    NPC N = Main.npc[player.MinionAttackTargetNPC];
                    targetDist = Vector2.Distance(NPC.Center, targetPos);
                    targetPos = N.Center;
                    target = true;
                    NPC.ai[0] = 0;
                }
                else
                {
                    for (int k = 0; k < 200; k++)
                    {
                        NPC N = Main.npc[k];
                        if (N.active && !N.friendly && !N.dontTakeDamage && N.lifeMax > 5 && N.chaseable && !N.immortal)
                        {
                            float distance = Vector2.Distance(NPC.Center, N.Center);
                            if ((distance < targetDist || !target) && Collision.CanHitLine(NPC.position, NPC.width, NPC.height, N.position, N.width, N.height))
                            {
                                targetDist = distance;
                                targetPos = N.Center;
                                target = true;
                                NPC.ai[0] = 0;
                            }
                        }
                    }
                }
                if (Vector2.Distance(player.Center, NPC.Center) > (target ? 1500f : 750f) && playerCactoid)
                {
                    NPC.ai[0] = 1f;
                }
                if (cactusPerson > -1 && Vector2.Distance(Main.npc[cactusPerson].Center, NPC.Center) > 600f && cactusPersonNear && !playerCactoid)
                {
                    NPC.ai[0] = 1f;
                }


                if (target && NPC.ai[0] == 0f)
                {
                    Vector2 direction = targetPos - NPC.Center;
                    direction.Normalize();
                    NPC.direction = direction.X < 0 ? -1 : 1;
                    NPC.velocity.X = (NPC.velocity.X * inertia + direction.X * chaseAccel) / (inertia + 1);
                    if (Math.Abs(NPC.velocity.X) < 0.5f)
                    {
                        NPC.velocity.X = 0;
                    }
                    if (targetPos.Y + 60 < NPC.position.Y && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.Y = -10;
                    }
                }
                else if (playerCactoid)
                {
                    Vector2 center = NPC.Center;
                    Vector2 direction = player.Center - center;
                    if (!Collision.CanHitLine(NPC.Center, 1, 1, player.Center, 1, 1) && direction.Length() >= 200f)
                    {
                        NPC.ai[0] = 1f;
                    }
                    float speed = 6f;
                    if (NPC.ai[0] == 1f)
                    {
                        speed = 15f;
                    }
                    direction.X -= ((10 + num * 40) * player.direction);
                    direction.Y -= 70f;
                    float distanceTo = direction.Length();
                    if (distanceTo > 200f && speed < 9f)
                    {
                        speed = 9f;
                    }
                    if (distanceTo < 200f && NPC.ai[0] == 1f && !Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[0] = 0f;
                    }
                    if (distanceTo > 48f)
                    {
                        direction.Normalize();
                        direction *= speed;
                        float temp = inertia / 2f;
                        NPC.velocity.X = (NPC.velocity.X * temp + direction.X) / (temp + 1);
                    }
                    else
                    {
                        NPC.direction = Main.player[NPC.target].direction;
                        NPC.velocity.X *= (float)Math.Pow(0.9, 40.0 / inertia);
                        if (Math.Abs(NPC.velocity.X) < 0.5f)
                        {
                            NPC.velocity.X = 0;
                        }
                    }
                }
                else if (cactusPerson > -1 && Vector2.Distance(Main.npc[cactusPerson].Center, NPC.Center) > 600f && cactusPersonNear)
                {
                    if (cactusPerson > -1 && NPC.Center.X > Main.npc[cactusPerson].Center.X)
                    {
                        NPC.direction = -1;
                    }
                    else
                    {
                        NPC.direction = 1;
                    }
                    if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                    {
                        NPC.velocity.Y = -7;
                    }
                    NPC.velocity.X = NPC.direction * 5;
                }
                else
                {
                    if (cactusPerson > -1 && cactusPersonNear && Vector2.Distance(Main.npc[cactusPerson].Center, NPC.Center) < 400f)
                    {
                        NPC.ai[0] = 0;
                    }
                    NPC.ai[1] += 1 + Main.rand.Next(5);
                    if (NPC.ai[1] > 900)
                    {
                        if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                NPC.direction *= -1;
                            }
                            else
                            {
                                NPC.velocity.Y = -7;
                            }
                        }
                        NPC.velocity.X = NPC.direction * 2;
                    }
                    if (NPC.ai[1] > 2000 && NPC.velocity.Y == 0)
                    {
                        NPC.ai[1] = 0;
                        NPC.velocity.X = 0f;
                    }
                }
            }
            else
            {
                if (NPC.ai[2] < 1)
                {
                    NPC.aiStyle = -1;
                    NPC.damage = 0;
                    NPC.ai[1] += 1 + Main.rand.Next(5);
                    NPC.netUpdate = true;
                    if (NPC.direction == 0)
                    {
                        NPC.direction = -1;
                    }
                    if (NPC.ai[1] > 900)
                    {
                        if (NPC.velocity.X == 0 && NPC.velocity.Y == 0)
                        {
                            if (Main.rand.Next(4) == 0)
                            {
                                NPC.direction *= -1;
                            }
                            else
                            {
                                NPC.velocity.Y = -10;
                            }
                        }
                        NPC.velocity.X = NPC.direction * 2;
                    }
                    if (NPC.ai[1] > 2000 && NPC.velocity.Y == 0)
                    {
                        NPC.ai[1] = 0;
                        NPC.velocity.X = 0f;
                    }
                }
                else
                {
                    NPC.FaceTarget();
                    NPC.aiStyle = 26;
                    AIType = NPCID.Unicorn;
                    if (Main.expertMode)
                    {
                        if (Main.hardMode)
                        {
                            NPC.damage = 75;
                        }
                        else
                        {
                            NPC.damage = 50;
                        }
                    }
                    else
                    {
                        NPC.damage = 25;
                    }
                    if (NPC.downedMoonlord)
                    {
                        if (Main.expertMode)
                        {
                            NPC.damage = 150;
                        }
                        else
                        {
                            NPC.damage = 75;
                        }
                    }
                    NPC.ai[1] = 1000;
                    NPC.ai[2] = 1;
                    NPC.velocity.X = NPC.velocity.X * 0.99f;
                }
            }
            NPC.netUpdate = true;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Tile tile = Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY];
			return !spawnInfo.Player.ZoneBeach && !spawnInfo.Invasion && !Main.pumpkinMoon && !Main.snowMoon && !Main.eclipse && spawnInfo.SpawnTileY < Main.rockLayer && spawnInfo.Player.ZoneDesert && !spawnInfo.Player.ZoneCorrupt && !spawnInfo.Player.ZoneCrimson && !spawnInfo.Player.ZoneHallow ? 0.1f : 0f;
		}
	}
}

