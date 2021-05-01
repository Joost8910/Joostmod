using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
    [AutoloadBossHead]
    public class ICU : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("ICU");
		}
		public override void SetDefaults()
		{
			npc.width = 60;
			npc.height = 60;
			npc.damage = 22;
			npc.defense = 5;
			npc.lifeMax = 1300;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = 0;
			npc.knockBackResist = 0.1f;
			npc.aiStyle = -1;
			npc.noGravity = true;
            npc.netAlways = true;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !Main.dayTime && spawnInfo.spawnTileY <= Main.worldSurface && !JoostWorld.downedICU && JoostWorld.activeQuest.Contains(npc.type) && !NPC.AnyNPCs(npc.type) ? 0.15f : 0f;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return npc.ai[0] > 0 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void NPCLoot()
		{
            JoostWorld.downedICU = true;
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("ICU"), 1, false);
            if (Main.expertMode && Main.rand.Next(100) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EvilStone"), 1);
            }
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (item.melee)
            {
                crit = true;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.melee && Main.player[projectile.owner].heldProj == projectile.whoAmI)
            {
                crit = true;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.ai[0]++;
            if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ICU2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ICU1"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ICU2"), 1f);
			}

		}
        public override void AI()
		{
            Player P = Main.player[npc.target];
            Vector2 posOff = new Vector2(npc.ai[2], npc.ai[3]);
            Vector2 targetPos = P.Center + posOff;
            int ai = 60;
            float Speed = 10f;
            if (npc.life < (int)(npc.lifeMax * 0.5f))
            {
                ai = 30;
                Speed = 15f;
            }
            if (!Main.expertMode)
            {
                ai = (int)(ai*1.5f);
            }
            if (Vector2.Distance(npc.Center, P.Center) > 2500 || npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(false);
                P = Main.player[npc.target];
                if (!P.active || P.dead || Vector2.Distance(npc.Center, P.Center) > 2500)
                {
                    npc.ai[0] = 0;
                }
            }
            if (npc.ai[0] < 1)
            {
                npc.velocity *= 0;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = -60;
                npc.life = npc.life < npc.lifeMax ? npc.life + 1 + (int)((float)npc.lifeMax * 0.001f) : npc.lifeMax;
                npc.rotation += 15 * 0.0174f;
                if (Collision.CanHitLine(npc.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(npc.Center, P.Center) < 400)
                {
                    npc.ai[0]++;
                }   
            }
            else
            {
                posOff = new Vector2(npc.ai[2], npc.ai[3]);
                targetPos = P.Center + posOff;
                if (targetPos.Y > npc.Center.Y)
                {
                    bool platform = false;
                    for (int i = (int)(npc.position.X / 16); i < (int)((npc.position.X + npc.width) / 16); i++)
                    {
                        platform = false;
                        int j = (int)((npc.position.Y + npc.height + 1) / 16);
                        if (Main.tileSolidTop[Main.tile[i, j].type])
                        {
                            platform = true;
                        }
                    }
                    if (platform)
                    {
                        npc.position.Y++;
                    }
                }
                npc.velocity = npc.DirectionTo(targetPos) * Speed;
                int dir = Main.rand.Next(2);
                if (Main.expertMode && (Math.Abs(P.Center.X - npc.Center.X) < 20 || Math.Abs(P.Center.Y - npc.Center.Y) < 20))
                {
                    dir = 1;
                }
                npc.ai[1]++;
                if (npc.ai[1] + dir >= ai)
                {
                    npc.velocity *= 0;
                    int damage = 12;
                    int type = 83;
                    if (npc.ai[1] > ai+15)
                    {
                        Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 11);
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(npc.rotation) * Speed) * -1), (float)((Math.Sin(npc.rotation) * Speed) * -1), type, damage, 0.8f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(npc.rotation + 1.57f) * Speed) * -1), (float)((Math.Sin(npc.rotation + 1.57f) * Speed) * -1), type, damage, 0.8f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(npc.rotation + 3.14f) * Speed) * -1), (float)((Math.Sin(npc.rotation + 3.14f) * Speed) * -1), type, damage, 0.8f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, (float)((Math.Cos(npc.rotation + 4.71f) * Speed) * -1), (float)((Math.Sin(npc.rotation + 4.71f) * Speed) * -1), type, damage, 0.8f, Main.myPlayer);
                        }
                        npc.ai[1] = 0;
                        npc.ai[2] = Main.rand.Next(-1, 1) * (ai + 50 + Main.rand.Next(ai * 2));
                        npc.ai[3] = Main.rand.Next(-1, 1) * (ai + 50 + Main.rand.Next(ai * 2));
                        if (npc.ai[2] == 0 && npc.ai[3] == 0)
                        {
                            if (Main.rand.Next(2) == 0)
                            {
                                npc.ai[2] = ai + 150;
                            }
                            else
                            {
                                npc.ai[3] = ai + 150;
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                npc.ai[2] *= -1;
                                npc.ai[3] *= -1;
                            }
                        }
                    }
                }
                else
                {
                    npc.rotation = (float)(npc.ai[1] * (Math.PI/180) * 45);
                }
            }
            npc.netUpdate = true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
}

