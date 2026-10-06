using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using System;
using Terraria.GameContent.Drawing;
using Terraria.Audio;

namespace JoostMod.Projectiles.Melee
{
    public abstract class HammerEcho : ModProjectile
    {
        //public override string Texture => "JoostMod/Projectiles/Melee/HammerEcho";

        protected int dustId = -1;
        protected int maxTime = 25;
        protected Color echoColor = Color.White;
        protected float recursionDamageScaling = 0.5f;
        protected float recursionSizeScaling = 0.5f;
        protected float critSizeScale = 1.5f;

        private ref float MaxScale => ref Projectile.ai[0];
        private ref float IsCrit => ref Projectile.ai[1];
        private ref float Recursion => ref Projectile.ai[2];
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(Projectile.width * Projectile.scale);
            hitbox.Height = (int)(Projectile.height * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Rectangle hitbox = Projectile.Hitbox;
            ModifyDamageHitbox(ref hitbox);
            if (JoostFunctions.EllipseCollision(hitbox.TopLeft(), hitbox.Size(), targetHitbox.TopLeft(), targetHitbox.Size()) && Collision.CanHitLine(Projectile.Center, 1, 1, targetHitbox.Center(), 1, 1))
            {
                return base.Colliding(projHitbox, targetHitbox);
            }
            return false;
        }

        public virtual void HitEffects(Entity target)
        {

        }
        private void RecursionEcho(Vector2 pos, Entity victim)
        {
            if (Recursion > 0)
            {
                Projectile P = Projectile.NewProjectileDirect(Projectile.GetSource_OnHit(victim), pos, Vector2.Zero, Projectile.type, (int)(Projectile.damage * recursionDamageScaling), 0, Projectile.owner, MaxScale * recursionSizeScaling, IsCrit, Recursion - 1);
                if (victim.GetType() == typeof(NPC))
                {
                    P.localNPCImmunity[victim.whoAmI] = -1;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            HitEffects(target);
            RecursionEcho(target.Hitbox.ClosestPointInRect(Projectile.Center), target);
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            HitEffects(target);
            RecursionEcho(target.Hitbox.ClosestPointInRect(Projectile.Center), target);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.DamageVariationScale *= 0;
            modifiers.ScalingArmorPenetration += 1f;
            modifiers.HitDirectionOverride = Math.Sign(target.Center.X - Projectile.Center.X);
            if (IsCrit > 0)
            {
                modifiers.CritDamage -= 1f;
                modifiers.SetCrit();
            }
            else
            {
                modifiers.DisableCrit();
            }
        }
        public override void AI()
        {
            if (Projectile.timeLeft == maxTime)
            {
                Projectile.spriteDirection = Projectile.direction;
                Projectile.scale = 0;
                if (IsCrit > 0)
                {
                    MaxScale *= critSizeScale;
                }
            }

            //Projectile.scale += (MaxScale / maxTime);
            Projectile.scale = Utils.Remap((maxTime - Projectile.timeLeft), 0, maxTime, Projectile.scale * 0.8f, MaxScale);
            //Main.NewText(Projectile.scale);


            /*Rectangle hitbox = Projectile.Hitbox;
            hitbox.Width = (int)(Projectile.width * Projectile.scale);
            hitbox.Height = (int)(Projectile.height * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
            int d = Dust.NewDust(
                         hitbox.TopLeft(),
                         hitbox.Width,
                         hitbox.Height,
                         dustId, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default,
                         Projectile.scale
                         );
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 0.1f;*/

        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;

            Color color = echoColor;

            if (Projectile.timeLeft < 10)
            {
                color *= Projectile.timeLeft / 10f;
            }
            float scale = Projectile.scale;//(MaxScale / maxTime) * (maxTime - Projectile.timeLeft);

            Vector2 drawOrigin = new Vector2(tex.Width / 2, tex.Height / 2);
            Rectangle? drawRect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), drawRect, color, Projectile.rotation, drawOrigin, scale, effects);

            return false;
        }
    }
}

