using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class GilgBusterLimitBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilgamesh's Buster Sword");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 120;
            Projectile.height = 120;
            Projectile.aiStyle = 27;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.alpha = 25;
            Projectile.light = 0.7f;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 30;
            height = 30;
            return true;
        }
        public override void AI()
        {
            if (Projectile.timeLeft % 5 == 0)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 56);
                Main.dust[dust].noGravity = true;
            }
            Projectile.direction = 1;
            if (Projectile.velocity.X < 0)
            {
                Projectile.direction = -1;
            }
            Projectile.spriteDirection = Projectile.direction;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction < 0 ? 3.14f : 0);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.velocity *= 0;
            }
            target.immuneTime = 1;
            Projectile.position = target.Center - Projectile.Size / 2;
            Projectile.Kill();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                float Speed = Main.rand.Next(1, 8) * 0.1f;
                float randRot = Main.rand.Next(360);
                Vector2 vel = new Vector2((float)(Math.Cos(randRot) * Speed * -1), (float)(Math.Sin(randRot) * Speed * -1));
                int type = ModContent.ProjectileType<GilgBusterLimitSlash>();
                Projectile.NewProjectile(Projectile.GetSource_Death(), Projectile.Center, vel, type, 10, 0, Main.myPlayer, i);
            }
            for (int i = 0; i < 30; i++)
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 56);
                Main.dust[dust].noGravity = true;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, Projectile.height * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, tex.Height / Main.projFrames[Projectile.type] * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(tex, drawPos, rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            }
            Vector2 drawPosition = Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
            Main.EntitySpriteDraw(tex, drawPosition, rect, Color.White, Projectile.rotation, drawOrigin, 1f, effects, 0);
            return false;
        }
    }
}

