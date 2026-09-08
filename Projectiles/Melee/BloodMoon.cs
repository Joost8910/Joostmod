using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class BloodMoon : Flail
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Moon");
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
            outTime = 16;
            throwSpeed = 18f;
            returnSpeed = 16f;
            returnSpeedAfterHeld = 20f;
            swingHitCD = 15;
            swingSpeed = 0.8f;
            chainTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Chain");
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(38 * Projectile.scale);
            hitbox.Height = (int)(38 * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
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
        {/*
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.NightsEdge, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Hitbox.ClosestPointInRect(Projectile.Center)
            }, default(int?));*/
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustDirect(target.Hitbox.ClosestPointInRect(Projectile.Center), 0, 0, DustID.RedTorch).noGravity = true;
                float rot = MathHelper.ToRadians(i * (360f / 8));
                Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(Projectile.Center), DustID.PortalBoltTrail, rot.ToRotationVector2() * 3, 0, Color.Red, 2f).noGravity = true;
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {/*
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.NightsEdge, new ParticleOrchestraSettings
            {
                PositionInWorld = target.Hitbox.ClosestPointInRect(Projectile.Center)
            }, default(int?));*/
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDustDirect(target.Hitbox.ClosestPointInRect(Projectile.Center), 0, 0, DustID.RedTorch).noGravity = true;
                float rot = MathHelper.ToRadians(i * (360f / 8));
                Dust.NewDustPerfect(target.Hitbox.ClosestPointInRect(Projectile.Center), DustID.PortalBoltTrail, rot.ToRotationVector2() * 3, 0, Color.Red, 2f).noGravity = true;
            }
        }

        readonly float beamDamageMult = 1.25f;
        readonly int type = ModContent.ProjectileType<BloodMoonBeam>();
        private int BeamFreq()
        {
            float speed = 44f / Main.player[Projectile.owner].itemAnimationMax;
            return (int)(33f / speed);
        }
        private void FireBeam(int num)
        {
            Player player = Main.player[Projectile.owner];
            int damage = (int)(Projectile.damage * beamDamageMult);
            float kb = Projectile.knockBack * 0.5f;
            float rot = Math.Abs(Projectile.rotation) * Projectile.direction;
            if (num > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, type, damage, kb, Projectile.owner, rot, Projectile.scale);
                }
                SoundEngine.PlaySound(SoundID.Item20.WithPitchOffset(0.2f), Projectile.Center);
            }
            Projectile.ai[2] = 0;
        }
        public override void SwingEffects()
        {
            Projectile.ai[2]++;
            if ((int)Projectile.ai[2] <= BeamFreq())
            {
                if ((int)Projectile.ai[2] % BeamFreq() == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20.WithVolumeScale(0.8f), Projectile.Center);
                }
            }
        }
        public override void ReachedPeakEffects()
        {
            int num = Math.Min(1, (int)Projectile.ai[2] / BeamFreq());
            FireBeam(num);
        }
        public override void DoDust(bool doFastThrowDust)
        {
            int num = Math.Min(1, (int)Projectile.ai[2] / BeamFreq());
            if (Projectile.ai[2] >= BeamFreq() && Projectile.ai[2] % 3 == 0)
            {
                Player player = Main.player[Projectile.owner];

                Dust.NewDustDirect(
                         Projectile.position,
                         Projectile.width,
                         Projectile.height,
                         DustID.CrimsonTorch, //Dust ID
                         Projectile.velocity.X,
                         Projectile.velocity.Y,
                         100, //alpha goes from 0 to 255
                         default,
                         1f
                         ).noGravity = true;
            }
        }
        public override void ExtraBehavior(ref bool flag)
        {
            int num = Math.Min(1, (int)Projectile.ai[2] / BeamFreq());
            if (Projectile.ai[0] == 6) // Held in place
            {
                Projectile.ai[2]++;
                if (num >= 1 && (int)Projectile.ai[2] == BeamFreq() * 2)
                {
                    FireBeam(num);
                }
                if ((int)Projectile.ai[2] % BeamFreq() == 0 && Projectile.ai[2] > 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20.WithVolumeScale(0.8f), Projectile.Center);
                }
            }
            if (Projectile.ai[0] == 5 || Projectile.ai[0] == 4) //Hitting Tile or returning after held
            {
                FireBeam(num);
            }
        }
        public override bool PreDrawExtras()
        {
            int num = Math.Min(1, (int)Projectile.ai[2] / BeamFreq());
            Player player = Main.player[Projectile.owner];
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection < 0)
            {
                effects = SpriteEffects.FlipVertically;
            }
            Color color = Color.White * 0.5f;
            if (num > 0)
            {
                Texture2D orbTex = ModContent.Request<Texture2D>($"{Texture}Beam").Value;
                Main.EntitySpriteDraw(orbTex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, orbTex.Width, orbTex.Height)), color, Projectile.rotation, new Vector2(orbTex.Width / 2, orbTex.Height / 2), Projectile.scale, effects, 0);
            }
            return base.PreDrawExtras();
        }
    }
}
