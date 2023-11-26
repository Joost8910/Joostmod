using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EarthenBillhook : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Billhook");
		}
		public override void SetDefaults()
		{
			Projectile.width = 46;
			Projectile.height = 46;
			Projectile.aiStyle = 19;
			Projectile.timeLeft = 90;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 25;
            Projectile.extraUpdates = 1;
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];
            if ((player.itemAnimation < player.itemAnimationMax / 2) && target.Distance(player.Center + player.velocity) > 60 + knockback + target.width / 2)
            {
                hitDirection = -Projectile.direction;
                knockback *= 0.6f;
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
            float speed = player.GetAttackSpeed(DamageClass.Melee) / 2;
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                Projectile.scale = player.inventory[player.selectedItem].scale;
                speed = (((36f / player.inventory[player.selectedItem].useTime) / player.GetAttackSpeed(DamageClass.Melee)) / 2) * Projectile.scale;
                Projectile.localNPCHitCooldown = (int)(25 / (speed / Projectile.scale) * 0.667f);
                Projectile.width = (int)(46 * Projectile.scale);
                Projectile.height = (int)(46 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            Projectile.spriteDirection = Projectile.direction * (int)player.gravDir;
            player.direction = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            Projectile.position.X = center.X - (float)(Projectile.width / 2);
            Projectile.position.Y = center.Y - (float)(Projectile.height / 2);
            Vector2 vel = Projectile.velocity;
            vel.Normalize();
            Projectile.position += vel * 7 * Projectile.ai[1];
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 3f;
                Projectile.netUpdate = true;
            }
            if (player.itemAnimation < player.itemAnimationMax / 2)
            {
                Projectile.ai[1] -= speed;
                if (player.itemAnimation > player.itemAnimationMax / 3)
                    Projectile.velocity = Projectile.velocity.RotatedBy(2 * 0.0174f * player.direction * player.gravDir);
            }
            else
            {
                Projectile.ai[1] += speed;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == Mod.Find<ModProjectile>("Boulder").Type && Projectile.Distance(p.Center) < 40)
                    {
                        p.velocity = Projectile.velocity * 2f;
                        p.damage = (int)(Projectile.damage * 3f);
                        p.knockback = Projectile.knockBack * 3f;
                        p.owner = Projectile.owner;
                        p.netUpdate = true;
                        if (p.timeLeft <= 500)
                        {
                            SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.25f), p.Center);
                            p.timeLeft = 540;
                        }
                        break;
                    }
                }
            }
            if (player.itemAnimation == 0)
            {
                Projectile.Kill();
            }
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 2.355f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor;
            Vector2 vel = Projectile.velocity;
            vel.Normalize();
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition - vel * 60 * Projectile.scale, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}