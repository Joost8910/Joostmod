using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using JoostMod.Items.Legendaries;

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
			NPC.width = 60;
			NPC.height = 60;
			NPC.damage = 22;
			NPC.defense = 5;
			NPC.lifeMax = 1300;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.value = 0;
			NPC.knockBackResist = 0.1f;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
            NPC.netAlways = true;
		}
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.7f * bossLifeScale + 1);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return !Main.dayTime && spawnInfo.SpawnTileY <= Main.worldSurface && !JoostWorld.downedICU && JoostWorld.activeQuest.Contains(NPC.type) && !NPC.AnyNPCs(NPC.type) ? 0.15f : 0f;
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return NPC.ai[0] > 0 && base.CanHitPlayer(target, ref cooldownSlot);
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<EvilStone>(), 100));
        }
        public override void OnKill()
        {
            JoostWorld.downedICU = true;
            CommonCode.DropItemForEachInteractingPlayerOnThePlayer(NPC, ModContent.ItemType<Items.Quest.ICU>(), Main.rand, 1, 1, 1, false);
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (item.CountsAsClass(DamageClass.Melee))
            {
                crit = true;
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.CountsAsClass(DamageClass.Melee) && Main.player[projectile.owner].heldProj == projectile.whoAmI)
            {
                crit = true;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            NPC.ai[0]++;
            if (Main.netMode != NetmodeID.Server && NPC.life <= 0)
            {
                var sauce = NPC.GetSource_Death();
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("ICU1").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("ICU2").Type);
                Gore.NewGore(sauce, NPC.position, NPC.velocity, Mod.Find<ModGore>("ICU2").Type);
            }
        }
        public override void AI()
		{
            Player P = Main.player[NPC.target];
            Vector2 posOff = new Vector2(NPC.ai[2], NPC.ai[3]);
            Vector2 targetPos = P.Center + posOff;
            int ai = 60;
            float Speed = 10f;
            if (NPC.life < (int)(NPC.lifeMax * 0.5f))
            {
                ai = 30;
                Speed = 15f;
            }
            if (!Main.expertMode)
            {
                ai = (int)(ai*1.5f);
            }
            if (Vector2.Distance(NPC.Center, P.Center) > 2500 || NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(false);
                P = Main.player[NPC.target];
                if (!P.active || P.dead || Vector2.Distance(NPC.Center, P.Center) > 2500)
                {
                    NPC.ai[0] = 0;
                }
            }
            if (NPC.ai[0] < 1)
            {
                NPC.velocity *= 0;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = -60;
                NPC.life = NPC.life < NPC.lifeMax ? NPC.life + 1 + (int)((float)NPC.lifeMax * 0.001f) : NPC.lifeMax;
                NPC.rotation += 15 * 0.0174f;
                if (Collision.CanHitLine(NPC.Center, 1, 1, P.Center, 1, 1) && Vector2.Distance(NPC.Center, P.Center) < 400)
                {
                    NPC.ai[0]++;
                }   
            }
            else
            {
                posOff = new Vector2(NPC.ai[2], NPC.ai[3]);
                targetPos = P.Center + posOff;
                if (targetPos.Y > NPC.Center.Y)
                {
                    bool platform = false;
                    for (int i = (int)(NPC.position.X / 16); i < (int)((NPC.position.X + NPC.width) / 16); i++)
                    {
                        platform = false;
                        int j = (int)((NPC.position.Y + NPC.height + 1) / 16);
                        if (Main.tileSolidTop[Main.tile[i, j].TileType])
                        {
                            platform = true;
                        }
                    }
                    if (platform)
                    {
                        NPC.position.Y++;
                    }
                }
                NPC.velocity = NPC.DirectionTo(targetPos) * Speed;
                int dir = Main.rand.Next(2);
                if (Main.expertMode && (Math.Abs(P.Center.X - NPC.Center.X) < 20 || Math.Abs(P.Center.Y - NPC.Center.Y) < 20))
                {
                    dir = 1;
                }
                NPC.ai[1]++;
                if (NPC.ai[1] + dir >= ai)
                {
                    NPC.velocity *= 0;
                    int damage = 12;
                    int type = 83;
                    if (NPC.ai[1] > ai+15)
                    {
                        SoundEngine.PlaySound(SoundID.Item11, NPC.position);
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {
                            var source = NPC.GetSource_FromAI();
                            Projectile.NewProjectile(source, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(NPC.rotation) * Speed) * -1), (float)((Math.Sin(NPC.rotation) * Speed) * -1), type, damage, 0.8f, Main.myPlayer);
                            Projectile.NewProjectile(source, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(NPC.rotation + 1.57f) * Speed) * -1), (float)((Math.Sin(NPC.rotation + 1.57f) * Speed) * -1), type, damage, 0.8f, Main.myPlayer);
                            Projectile.NewProjectile(source, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(NPC.rotation + 3.14f) * Speed) * -1), (float)((Math.Sin(NPC.rotation + 3.14f) * Speed) * -1), type, damage, 0.8f, Main.myPlayer);
                            Projectile.NewProjectile(source, NPC.Center.X, NPC.Center.Y, (float)((Math.Cos(NPC.rotation + 4.71f) * Speed) * -1), (float)((Math.Sin(NPC.rotation + 4.71f) * Speed) * -1), type, damage, 0.8f, Main.myPlayer);
                        }
                        NPC.ai[1] = 0;
                        NPC.ai[2] = Main.rand.Next(-1, 1) * (ai + 50 + Main.rand.Next(ai * 2));
                        NPC.ai[3] = Main.rand.Next(-1, 1) * (ai + 50 + Main.rand.Next(ai * 2));
                        if (NPC.ai[2] == 0 && NPC.ai[3] == 0)
                        {
                            if (Main.rand.NextBool(2))
                            {
                                NPC.ai[2] = ai + 150;
                            }
                            else
                            {
                                NPC.ai[3] = ai + 150;
                            }
                            if (Main.rand.NextBool(2))
                            {
                                NPC.ai[2] *= -1;
                                NPC.ai[3] *= -1;
                            }
                        }
                    }
                }
                else
                {
                    NPC.rotation = (float)(NPC.ai[1] * (Math.PI/180) * 45);
                }
            }
            NPC.netUpdate = true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}

