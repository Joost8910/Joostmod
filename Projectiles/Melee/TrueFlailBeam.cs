using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;

namespace JoostMod.Projectiles.Melee
{
    public abstract class TrueFlailBeam : ModProjectile
    {
        protected int activeTime = 180;
        protected int dustId = -1;
        protected int startDist = 24;
        protected int maxDist = 180;
        protected float orbitalSpeedMult = 1f;
        protected Vector3 lightColor = Vector3.Zero;

        private ref float OrbitAngle => ref Projectile.ai[0];
        private ref float Scale => ref Projectile.ai[1];
        private ref float SpinSpeed => ref Projectile.ai[2];
        private ref float OriginX => ref Projectile.localAI[0];
        private ref float OriginY => ref Projectile.localAI[1];

        public virtual void HitEffects(Entity target)
        {

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            HitEffects(target);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            HitEffects(target);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.HitDirectionOverride = Projectile.direction;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(Projectile.width * Projectile.scale);
            hitbox.Height = (int)(Projectile.height * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
        }
        public void DoDust()
        {
            if (dustId > 0)
            {
                int d = Dust.NewDust(
                             Projectile.position,
                             Projectile.width,
                             Projectile.height,
                             dustId, //Dust ID
                             Projectile.velocity.X,
                             Projectile.velocity.Y,
                             100, //alpha goes from 0 to 255
                             default,
                             Projectile.scale + 1f
                             );

                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.1f;
            }
        }

        public override void AI()
        {
            if (Projectile.timeLeft % 2 == 0)
            {
                DoDust();
            }
            Lighting.AddLight(Projectile.Center, lightColor);

            if (Projectile.timeLeft == activeTime)
            {
                OriginX = Projectile.Center.X;
                OriginY = Projectile.Center.Y;
                Projectile.direction = Math.Sign(OrbitAngle);
                Projectile.spriteDirection = Projectile.direction;
                //Main.NewText(Projectile.ai[0]);
                SpinSpeed = orbitalSpeedMult;
                Projectile.scale = 0.5f * Scale;
            }
            float updateScale = 1f / (Projectile.extraUpdates + 1);
            Vector2 origin = new Vector2(OriginX, OriginY);
            double rad = OrbitAngle;
            double dist = (((activeTime - Projectile.timeLeft) / (double)activeTime) * 
                (maxDist - (Projectile.timeLeft)) + startDist) * Scale;
            Projectile.position.X = origin.X - (int)(Math.Cos(rad) * dist) - Projectile.width / 2;
            Projectile.position.Y = origin.Y - (int)(Math.Sin(rad) * dist) - Projectile.height / 2;
            Projectile.rotation = Projectile.spriteDirection > 0 ? (float)rad : (float)(rad + Math.PI);
            OrbitAngle += MathHelper.ToRadians(11) * Projectile.spriteDirection * SpinSpeed * updateScale;

            Projectile.scale += 0.01f * updateScale;
            SpinSpeed += 0.01f * orbitalSpeedMult;
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                int dust = Dust.NewDust(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         dustId, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default,
                         1f
                         );

                Main.dust[dust].noGravity = true;
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

            Color color = Color.White;

            if (Projectile.timeLeft < 20)
            {
                color *= Projectile.timeLeft / 20f;
            }

            Vector2 drawOrigin = new Vector2(tex.Width / 2, tex.Height / 2);
            Rectangle? drawRect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
            {
                float scale = Projectile.scale * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                Main.EntitySpriteDraw(tex, drawPos, drawRect, color, Projectile.oldRot[k], drawOrigin, scale, effects);
            }

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), drawRect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects);

            return false;
        }
    }
}
