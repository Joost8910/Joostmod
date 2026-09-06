using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class TrueHallowedFlail : Flail
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Incandescence");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = 15;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 7200;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            outTime = 7;
            throwSpeed = 42f;
            returnSpeed = 56f;
            returnSpeedAfterHeld = 56f;
            swingSpeed = 1.2f;
            swingHitCD = 10;
            chainTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Chain");
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            float num4 = Utils.Remap(Projectile.localAI[2], shineTime, shineTime / 3f, 0f, 1f, true);
            float num5 = Utils.Remap(num4, 0f, 0.3f, 0f, 1f, true) * Utils.Remap(num4, 0.3f, 1f, 1f, 0f, true);
            num5 = 1f - (1f - num5) * (1f - num5);
            float scale = Projectile.scale + num5 * 3;
            hitbox.Width = (int)(50 * scale);
            hitbox.Height = (int)(50 * scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
        }
        public override bool? CanHitNPC(NPC target)
        {
            Rectangle hitbox = Projectile.Hitbox;
            ModifyDamageHitbox(ref hitbox);
            if (JoostFunctions.EllipseCollision(hitbox.TopLeft(), hitbox.Size(), target.position, target.Size) && Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            Rectangle hitbox = Projectile.Hitbox;
            ModifyDamageHitbox(ref hitbox);
            if (JoostFunctions.EllipseCollision(hitbox.TopLeft(), hitbox.Size(), target.position, target.Size) && Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
            {
                return base.CanHitPlayer(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            Rectangle hitbox = Projectile.Hitbox;
            ModifyDamageHitbox(ref hitbox);
            if (JoostFunctions.EllipseCollision(hitbox.TopLeft(), hitbox.Size(), target.position, target.Size) && Collision.CanHitLine(Projectile.Center, 1, 1, target.Center, 1, 1))
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        
        public override void CheckStats(ref float speedMult)
        {
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.shoot == Projectile.type)
            {
                Projectile.scale = player.HeldItem.scale;
                speedMult *= 44f / player.HeldItem.useTime;
            }
            Projectile.width = (int)(30 * Projectile.scale);
            Projectile.height = (int)(30 * Projectile.scale);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Hitbox.ClosestPointInRect(Projectile.Center)
            }, default(int?));
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TrueExcalibur, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Hitbox.ClosestPointInRect(Projectile.Center)
            }, default(int?));
        }
        readonly float shineTime = 18f;
        public override void ReachedPeakEffects()
        {
            //Projectile.localAI[2] = 18;
            SoundEngine.PlaySound(SoundID.Item4.WithPitchOffset(0.25f), Projectile.Center);
        }
        public override void ExtraBehavior(ref bool flag)
        {
            if (Projectile.ai[0] == 1) // Throw
            {
                Projectile.localAI[2] = Projectile.ai[1] * (shineTime / outTime);
            }
            else if (Projectile.localAI[2] > 0)
            {
                Projectile.localAI[2]--;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, false, false);

            Vector2 center = Projectile.Center + Projectile.DirectionFrom(vector2) * 50f;
            Rectangle r = Utils.CenteredRectangle(center, new Vector2(30, 30));

            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            float num = Projectile.DirectionFrom(vector2).SafeNormalize(Vector2.Zero).ToRotation() + 2.355f;
            float num3 = r.Size().Length() / Projectile.Hitbox.Size().Length();

            float num4 = Utils.Remap(Projectile.localAI[2], shineTime, shineTime / 3f, 0f, 1f, true);
            float num5 = Utils.Remap(num4, 0f, 0.3f, 0f, 1f, true) * Utils.Remap(num4, 0.3f, 1f, 1f, 0f, true);
            num5 = 1f - (1f - num5) * (1f - num5);

            Vector2 vector3 = r.Center.ToVector2() + new Vector2(0f, Projectile.gfxOffY);
            Vector2.Lerp(vector2, vector3, 1.1f);
            Texture2D value = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 origin = value.Size() / 2f;
            Color color = new Color(255, 220, 80, 0);
            Color color2 = new Color(255, 112, 243, 0);
            float num6 = num - 0.7853982f;

            for (int i = 0; i < 8; i++)
            {
                Color useColor = i % 2 == 0 ? color : color2;
                Main.EntitySpriteDraw(value, Projectile.Center - Main.screenPosition + new Vector2(0f, 2f), default(Rectangle?), useColor * num5 * 0.65f, num6, origin, new Vector2(num5 * num3 * 1f * num5, num3 * 5f * num5) * Projectile.scale * num3, effects, 0f);

                num6 += (float)(Math.PI / 4);
            }
            r.Offset((int)(0f - Main.screenPosition.X), (int)(0f - Main.screenPosition.Y));
            return base.PreDraw(ref lightColor);
        }

    }
}
