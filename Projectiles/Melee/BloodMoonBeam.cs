using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using System;
using Terraria.GameContent.Drawing;

namespace JoostMod.Projectiles.Melee
{
    public class BloodMoonBeam : ModProjectile
    {
        private ref float SpinRot => ref Projectile.ai[0];
        private ref float Scale => ref Projectile.ai[1];
        private ref float SpinSpeed => ref Projectile.ai[2];

        private readonly int dustId = DustID.CrimsonTorch;
        private readonly int maxTime = 25;
        private readonly float maxScale = 3.5f;
        private readonly Vector3 lightColor = new Vector3(0.93f, 0.07f, 0.21f);
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = maxTime / 2 + 1;
            Projectile.timeLeft = maxTime;
            Projectile.tileCollide = false;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(Projectile.width * Projectile.scale);
            hitbox.Height = (int)(Projectile.height * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
        }
        private void HitEffects(Entity target)
        {
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustDirect(target.Hitbox.ClosestPointInRect(Projectile.Center), 0, 0, DustID.RedTorch).noGravity = true;
                float rot = MathHelper.ToRadians(i * (360f / 8));
                Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(Projectile.Center), DustID.PortalBoltTrail, rot.ToRotationVector2() * 3, 0, Color.Red, 2f).noGravity = true;
            }
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
        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, lightColor);
            if (Projectile.timeLeft == maxTime)
            {
                Projectile.direction = Math.Sign(SpinRot);
                Projectile.spriteDirection = Projectile.direction;
                Projectile.scale = Scale;
                Projectile.alpha = 55;
                SpinSpeed = 1f;
            }

            Projectile.scale += ((maxScale - 1f) / maxTime) * Scale;
            SpinRot += MathHelper.ToRadians(13) * SpinSpeed * Projectile.spriteDirection;
            Projectile.rotation = SpinRot;
            SpinSpeed += 0.01f;
            Projectile.alpha += (200 / maxTime);

            Rectangle hitbox = Projectile.Hitbox;
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
            Main.dust[d].velocity *= 0.1f;

        }

    }
}

