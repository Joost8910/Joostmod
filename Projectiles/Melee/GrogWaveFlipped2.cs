using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class GrogWaveFlipped2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Warhammer of Grognak");
        }
        float mult = 0.85f;
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.timeLeft = 140;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
        }
        public override bool PreAI()
        {
            Projectile.direction = (int)Projectile.ai[0];
            Projectile.scale = Projectile.ai[1] * 0.5f;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.width = (int)(36 * Projectile.scale);
            Projectile.height = (int)(36 * Projectile.scale);
            return base.PreAI();
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 14;
            height = 14;
            fallThrough = true;
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity.Y += Projectile.knockBack * target.knockBackResist * Projectile.ai[1] * mult;
            if (target.knockBackResist > 0)
            {
                target.velocity.X = 0;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y += Projectile.knockBack * Projectile.scale;
            }
        }
        public override void ModifyDamageScaling(ref float damageScale)
        {
            damageScale *= Projectile.ai[1] * mult;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            knockback *= Projectile.ai[1] * mult;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = new Color(90, 255, (int)(51 + Main.DiscoG * 0.75f));
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }
    }
}
