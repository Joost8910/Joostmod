using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
	public class FireBall : ModNPC
    { 
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dead Man's Fire");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.width = 68;
            npc.height = 68;
            npc.defense = 9999;
            npc.lifeMax = 6;
            npc.damage = 50;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.Item74;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.buffImmune[BuffID.OnFire] = true;
            npc.buffImmune[BuffID.Venom] = true;
            npc.buffImmune[BuffID.Poisoned] = true;
            npc.buffImmune[BuffID.Frostburn] = true;
            npc.buffImmune[BuffID.CursedInferno] = true;
            npc.buffImmune[BuffID.Daybreak] = true;
            npc.buffImmune[mod.BuffType("BoneHurt")] = true;
            npc.buffImmune[mod.BuffType("CorruptSoul")] = true;
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            crit = false;
            damage = 1;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
            damage = 1;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            Player P = Main.player[npc.target];
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Fire, -npc.velocity.X / 5, -npc.velocity.Y / 5, 100, default(Color), 2f + (Main.rand.Next(20) * 0.1f)).noGravity = true;
            }
            if (npc.life > 0)
            {
                if (npc.friendly)
                {
                    npc.friendly = false;
                    npc.velocity = npc.velocity.Length() * npc.DirectionTo(P.Center) * 1.15f;
                }
                else
                {
                    npc.friendly = true;
                    npc.velocity *= -1.15f;
                }
            }
            npc.ai[2] = 0;
        }
        public override void AI()
		{
            npc.TargetClosest(true);
            Player P = Main.player[npc.target];
            if (npc.ai[0] < 1)
            {
                npc.velocity = npc.DirectionTo(P.Center) * 8;
                npc.ai[0] = 1;
            }
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, -npc.velocity.X / 5, -npc.velocity.Y / 5, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
            npc.ai[2]++;
            if (npc.ai[2] > 200)
            {
                npc.life = 0;
            }
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            if (npc.life == 1 && !npc.friendly)
            {
                npc.life = 2;
            }
            npc.lifeRegen = 0;
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 100;
            npc.frameCounter++;
            if (npc.frameCounter >= 4)
            {
                npc.frame.Y += frameHeight;
                npc.frameCounter = 0;
            }
            if (npc.frame.Y > frameHeight * 2)
            {
                npc.frame.Y = 0;
            }
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.type == NPCID.BurningSphere)
            {
                return true;
            }
            return base.CanHitNPC(target);
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            if (Main.player[projectile.owner].heldProj != projectile.whoAmI && projectile.friendly)
            {
                return false;
            }
            return base.CanBeHitByProjectile(projectile);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type != NPCID.BurningSphere)
            {
                npc.life = 0;
                Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, npc.damage, 20, npc.target);
            }
            npc.ai[2] = 0;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            npc.life = 0;
            Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.InfernoHostileBlast, 30, 20);
            npc.ai[2] = 0;
        }
        public override bool CheckDead()
        {
            if (npc.friendly)
            {
                Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, npc.damage*5, 20, npc.target);
            }
            else
            {
                Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.InfernoHostileBlast, 25, 20);
            }
            return true;
        }
    }
}


