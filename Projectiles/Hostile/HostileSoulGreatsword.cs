using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hostile
{
    public class HostileSoulGreatsword : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Greatsword");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 160;
            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 50;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
        }
        public override void AI()
        {
            NPC host = Main.npc[(int)Projectile.ai[0]];
            Projectile.direction = host.direction;
            Projectile.spriteDirection = host.spriteDirection;
            double rad = host.rotation - 2.355f + Projectile.ai[1] * 0.0174f * Projectile.direction + (Projectile.direction == 1 ? 0 : -1.57f);
            Projectile.rotation = (float)rad;
            double dist = -122 * Projectile.direction;
            Projectile.position.X = host.Center.X + 40 * host.direction - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = host.Center.Y + -16 - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            Projectile.scale = 1f;
            if (!host.active || host.type != Mod.Find<ModNPC>("Spectre").Type)
            {
                Projectile.Kill();
            }
            if (Projectile.timeLeft > 25)
            {
                Projectile.alpha -= 8;
            }
            if (Projectile.timeLeft == 25)
            {
                SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
            }
            if (Projectile.timeLeft == 11)
            {
                SoundEngine.PlaySound(new("Terraria/Sounds/Custom/dd2_etherian_portal_dryad_touch"), Projectile.Center); // 186
            }
            if (Projectile.timeLeft <= 10)
            {
                if (Projectile.ai[1] < 180)
                {
                    Projectile.timeLeft = 10;
                    Projectile.ai[1] += 15;
                }
                else
                {
                    Projectile.alpha += 16;
                }
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.timeLeft <= 10 && (Projectile.timeLeft > 4 || Projectile.localAI[1] > 0))
            {
                NPC host = Main.npc[(int)Projectile.ai[0]];
                float rot = Projectile.rotation - 1.57f + 0.785f * host.direction;
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = Projectile.Center + unit * -Projectile.width / 2;
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 186 * Projectile.scale, 52 * Projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        /*
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            if (projectile.ai[1] < 45)
            {
                hitbox.Width = 220;
                hitbox.Height = 50;
                hitbox.Y += 55;
                hitbox.X -= 30;
            }
            else if (projectile.ai[1] < 90)
            {
                hitbox.Width = 160;
                hitbox.Height = 160;
            }
            else if (projectile.ai[1] < 135)
            {
                hitbox.Width = 50;
                hitbox.Height = 220;
                hitbox.X += 55;
                hitbox.Y -= 30;
            }
            else if (projectile.ai[1] < 180)
            {
                hitbox.Width = 160;
                hitbox.Height = 160;
            }
            else
            {
                hitbox.Width = 220;
                hitbox.Height = 50;
                hitbox.Y += 55;
                hitbox.X -= 30;
            }
        }*/
        public override bool CanHitPlayer(Player target)
        {
            return Projectile.timeLeft <= 10 && Projectile.timeLeft > 4;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            if (Projectile.alpha < 100)
            {
                Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
                for (int k = 0; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                    Color color2 = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type] * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                    Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
                }
            }
            Color color = Color.White * (1f - Projectile.alpha / 255f);
            color.A = 150;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);
            return false;
        }
    }
}
