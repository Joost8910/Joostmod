using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class Naginata : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Naginata");
        }
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 48;
            Projectile.scale = 1f;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 40;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 7;
            Projectile.hide = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 mountedCenter = player.MountedCenter;
            player.direction = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            Projectile.Center = mountedCenter;
            float speed = player.GetAttackSpeed(DamageClass.Melee);
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                speed = 24f / player.itemAnimationMax;
                Projectile.localNPCHitCooldown = (int)(7 / (speed / Projectile.scale));
                Projectile.netUpdate = true;
            }
            /*
            if (player.itemAnimation < player.itemAnimationMax / 2f)
            {
                Projectile.ai[0] -= 1.9f;
            }
            else
            {
                Projectile.ai[0] += 1.9f;
            }
            Projectile.position += Projectile.velocity * Projectile.ai[0];
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
            */
            float num = (float)player.itemAnimation / (float)player.itemAnimationMax;
            float num2 = 1f - num;
            float dir = Projectile.velocity.ToRotation();
            float length = 70f;
            float lengthBase = 16f;
            Vector2 spinningpoint = new Vector2(1f, 0f).RotatedBy((double)(3.14159274f + num2 * 6.28318548f)) * new Vector2(length, 30f * Projectile.direction);
            Projectile.position += spinningpoint.RotatedBy((double)dir) + new Vector2(length + lengthBase, 0f).RotatedBy((double)dir);
            Vector2 target = mountedCenter + spinningpoint.RotatedBy((double)dir) + new Vector2(length + lengthBase + 40f, 0f).RotatedBy((double)dir);
            Projectile.rotation = mountedCenter.AngleTo(target) + 2.355f;
            Projectile.spriteDirection = -Projectile.direction;
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
            if (player.itemAnimation == 0)
            {
                Projectile.Kill();
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
            Vector2 vel = Main.player[Projectile.owner].MountedCenter.DirectionTo(Projectile.Center);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition - vel * 70 * Projectile.scale, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}