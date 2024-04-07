using System;
using JoostMod.Buffs;
using JoostMod.Projectiles.Magic;
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
            Main.npcFrameCount[NPC.type] = 3;
        }
        public override void SetDefaults()
        {
            NPC.friendly = true;
            NPC.dontTakeDamageFromHostiles = true;
            NPC.width = 68;
            NPC.height = 68;
            NPC.defense = 9999;
            NPC.lifeMax = Main.expertMode ? 12 : 6;
            NPC.damage = 50;
            NPC.HitSound = SoundID.NPCHit3;
            NPC.DeathSound = SoundID.Item74;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.knockBackResist = 0;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Frostburn] = true;
            NPC.buffImmune[BuffID.CursedInferno] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[ModContent.BuffType<BoneHurt>()] = true;
            NPC.buffImmune[ModContent.BuffType<CorruptSoul>()] = true;
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            crit = false;
            damage = 1;
            NPC.target = player.whoAmI;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
            damage = 1;
            NPC.target = projectile.owner;
        }
        public override bool PreKill()
        {
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X / 10, NPC.velocity.Y / 10, 100, default(Color), 0.8f + (Main.rand.Next(3) * 0.1f));
            }
            if (NPC.life > 0)
            {
                NPC.velocity *= -1.15f;
            }
            NPC.ai[3]= 1;
        }
        public override void AI()
		{
            if (NPC.ai[3] < 1)
            {
                NPC.velocity.X = NPC.ai[0];
                NPC.velocity.Y = NPC.ai[1];
                NPC.damage = (int)NPC.ai[2];
                NPC.ai[3]= 1;
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    ModPacket packet = Mod.GetPacket();
                    packet.Write((byte)JoostModMessageType.NPCpos);
                    packet.Write(NPC.whoAmI);
                    packet.WriteVector2(NPC.position);
                    packet.WriteVector2(NPC.velocity);
                    ModPacket netMessage = packet;
                    netMessage.Send();
                }
                NPC.netUpdate = true;
            }
            Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Torch, -NPC.velocity.X / 5, -NPC.velocity.Y / 5, 100, default(Color), 2f + (Main.rand.Next(20) * 0.1f)).noGravity = true;
            NPC.ai[3]++;
            bool colliding = (NPC.ai[3] > 2 && (NPC.oldVelocity.X != 0 && NPC.velocity.X == 0) || (NPC.oldVelocity.Y != 0 && NPC.velocity.Y == 0));
            for (int i = 0; i < Main.maxNPCs && !colliding; i++)
            {
                NPC target = Main.npc[i];
                if (!target.friendly && !target.dontTakeDamage && NPC.Hitbox.Intersects(target.Hitbox))
                {
                    colliding = true;
                    break;
                }
            }
            if (NPC.ai[3] > 300 || colliding)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 0);
                NPC.checkDead();
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
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
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            return target.whoAmI != (int)NPC.target && target.hostile && Main.player[NPC.target].hostile;
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
            if ((player.whoAmI == (int)NPC.target) || !(ItemLoader.CanHitPvp(item, player, Main.player[NPC.target]) && PlayerLoader.CanHitPvp(player, item, Main.player[NPC.target])))
            {
                return false;
            }
            return base.CanBeHitByItem(player, item);
        }
        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            if ((projectile.owner == (int)NPC.target) || (player.heldProj != projectile.whoAmI) || !(ProjectileLoader.CanHitPvp(projectile, Main.player[NPC.target]) && PlayerLoader.CanHitPvpWithProj(projectile, Main.player[NPC.target])))
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
                NPC.HitEffect(0, 0);
                NPC.checkDead();
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            NPC.life = 0;
            NPC.HitEffect(0, 0);
            NPC.checkDead();
        }
        public override bool CheckDead()
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<FireballExplosion>(), NPC.damage, 20, (int)NPC.target);
            return true;
        }
    }
}


