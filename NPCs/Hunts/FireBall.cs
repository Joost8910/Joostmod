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
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.width = 68;
            NPC.height = 68;
            NPC.defense = 9999;
            NPC.lifeMax = 6;
            NPC.damage = 50;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.Item74;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.knockBackResist = 0;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("BoneHurt").Type] = true;
            NPC.buffImmune[Mod.Find<ModBuff>("CorruptSoul").Type] = true;
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
        public override bool PreKill()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            Player P = Main.player[NPC.target];
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Torch, -NPC.velocity.X / 5, -NPC.velocity.Y / 5, 100, default(Color), 2f + (Main.rand.Next(20) * 0.1f)).noGravity = true;
            }
            if (NPC.life > 0)
            {
                if (NPC.friendly)
                {
                    NPC.friendly = false;
                    NPC.velocity = NPC.velocity.Length() * NPC.DirectionTo(P.Center) * 1.15f;
                }
                else
                {
                    NPC.friendly = true;
                    NPC.velocity *= -1.15f;
                }
            }
            NPC.ai[2] = 0;
        }
        public override void AI()
		{
            NPC.TargetClosest(true);
            Player P = Main.player[NPC.target];
            if (NPC.ai[0] < 1)
            {
                NPC.velocity = NPC.DirectionTo(P.Center) * 8;
                NPC.ai[0] = 1;
            }
            Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, -NPC.velocity.X / 5, -NPC.velocity.Y / 5, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
            NPC.ai[2]++;
            if (NPC.ai[2] > 200)
            {
                NPC.life = 0;
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
            if (NPC.life == 1 && !NPC.friendly)
            {
                NPC.life = 2;
            }
            NPC.lifeRegen = 0;
        }
        public override void FindFrame(int frameHeight)
        {
            frameHeight = 100;
            NPC.frameCounter++;
            if (NPC.frameCounter >= 4)
            {
                NPC.frame.Y += frameHeight;
                NPC.frameCounter = 0;
            }
            if (NPC.frame.Y > frameHeight * 2)
            {
                NPC.frame.Y = 0;
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
                NPC.life = 0;
                Projectile.NewProjectile(NPC.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, NPC.damage, 20, NPC.target);
            }
            NPC.ai[2] = 0;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            NPC.life = 0;
            Projectile.NewProjectile(NPC.Center, Vector2.Zero, ProjectileID.InfernoHostileBlast, 30, 20);
            NPC.ai[2] = 0;
        }
        public override bool CheckDead()
        {
            if (NPC.friendly)
            {
                Projectile.NewProjectile(NPC.Center, Vector2.Zero, ProjectileID.InfernoFriendlyBlast, NPC.damage*5, 20, NPC.target);
            }
            else
            {
                Projectile.NewProjectile(NPC.Center, Vector2.Zero, ProjectileID.InfernoHostileBlast, 25, 20);
            }
            return true;
        }
    }
}


