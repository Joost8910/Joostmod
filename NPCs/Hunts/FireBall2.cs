using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.NPCs.Hunts
{
	public class FireBall2 : ModNPC
    { 
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dead Man's Fire");
            Main.npcFrameCount[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.friendly = true;
            npc.dontTakeDamageFromHostiles = true;
            npc.width = 68;
            npc.height = 68;
            npc.defense = 9999;
            npc.lifeMax = Main.expertMode ? 12 : 6;
            npc.damage = 50;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.Item74;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0;
            npc.aiStyle = -1;
            npc.noGravity = true;
            npc.noTileCollide = false;
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
            npc.target = player.whoAmI;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
            damage = 1;
            npc.target = projectile.owner;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, npc.velocity.X / 10, npc.velocity.Y / 10, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
            }
            if (npc.life > 0)
            {
                npc.velocity *= -1.15f;
            }
            npc.ai[3]= 1;
        }
        public override void AI()
		{
            if (npc.ai[3] < 1)
            {
                npc.velocity.X = npc.ai[0];
                npc.velocity.Y = npc.ai[1];
                npc.damage = (int)npc.ai[2];
                npc.ai[3]= 1;
                if (Main.netMode != 0)
                {
                    ModPacket packet = mod.GetPacket();
                    packet.Write((byte)JoostModMessageType.NPCpos);
                    packet.Write(npc.whoAmI);
                    packet.WriteVector2(npc.position);
                    packet.WriteVector2(npc.velocity);
                    ModPacket netMessage = packet;
                    netMessage.Send();
                }
                npc.netUpdate = true;
            }
            Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Fire, -npc.velocity.X / 5, -npc.velocity.Y / 5, 100, default(Color), 2f + (Main.rand.Next(20) * 0.1f)).noGravity = true;
            npc.ai[3]++;
            bool colliding = (npc.ai[3] > 2 && (npc.oldVelocity.X != 0 && npc.velocity.X == 0) || (npc.oldVelocity.Y != 0 && npc.velocity.Y == 0));
            for (int i = 0; i < Main.maxNPCs && !colliding; i++)
            {
                NPC target = Main.npc[i];
                if (!target.friendly && !target.dontTakeDamage && npc.Hitbox.Intersects(target.Hitbox))
                {
                    colliding = true;
                    break;
                }
            }
            if (npc.ai[3] > 300 || colliding)
            {
                npc.life = 0;
                npc.HitEffect(0, 0);
                npc.checkDead();
            }
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
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
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return target.whoAmI != (int)npc.target && target.hostile && Main.player[npc.target].hostile;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (!target.friendly)
            {
                return true;
            }
            return base.CanHitNPC(target);
        }
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            if ((player.whoAmI == (int)npc.target) || !(ItemLoader.CanHitPvp(item, player, Main.player[npc.target]) && PlayerHooks.CanHitPvp(player, item, Main.player[npc.target])))
            {
                return false;
            }
            return base.CanBeHitByItem(player, item);
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            if ((projectile.owner == (int)npc.target) || (player.heldProj != projectile.whoAmI) || !(ProjectileLoader.CanHitPvp(projectile, Main.player[npc.target]) && PlayerHooks.CanHitPvpWithProj(projectile, Main.player[npc.target])))
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
                npc.HitEffect(0, 0);
                npc.checkDead();
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            npc.life = 0;
            npc.HitEffect(0, 0);
            npc.checkDead();
        }
        public override bool CheckDead()
        {
            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("FireballExplosion"), npc.damage, 20, (int)npc.target);
            return true;
        }
    }
}


