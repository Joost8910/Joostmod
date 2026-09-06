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
    public class TrueBloodMoon : Flail
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Blood Moon");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = ProjAIStyleID.Flail;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 7200;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            outTime = 12;
            throwSpeed = 24f;
            returnSpeed = 24f;
            returnSpeedAfterHeld = 28f;
            swingHitCD = 13;
            swingSpeed = 0.9f;
            chainTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Chain");
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(46 * Projectile.scale);
            hitbox.Height = (int)(46 * Projectile.scale);
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
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            if (Projectile.ai[0] <= 0 && num > 0) // Swing
            {
                FireBeam(num);
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
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            if (Projectile.ai[0] <= 0 && num > 0) // Swing
            {
                FireBeam(num);
            }
        }
        readonly float beamDamageMult = 1f;
        readonly int type = ModContent.ProjectileType<TrueBloodMoonBeam>();
        readonly int maxBeams = 4;
        private int BeamFreq()
        {
            return (int)(33f / Main.player[Projectile.owner].GetAttackSpeed(DamageClass.Melee));
        }
        private void FireBeam(int num)
        {
            Player player = Main.player[Projectile.owner];
            int damage = (int)(Projectile.damage * beamDamageMult);
            float kb = Projectile.knockBack * 0.5f;
            float rot = (float)(Math.PI * ((Projectile.localAI[0] + Projectile.ai[1]) / 11f) * player.direction * player.gravDir);
            if (num > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, type, damage, kb, Projectile.owner, rot);
                    rot += (MathHelper.ToRadians(360f / num));
                }
                SoundEngine.PlaySound(SoundID.Item20.WithPitchOffset(0.2f), Projectile.Center);
            }
            Projectile.localAI[0] = 0;
        }
        public override void SwingEffects()
        {
            Projectile.localAI[0]++;
            if ((int)Projectile.localAI[0] <= maxBeams * BeamFreq())
            {
                if ((int)Projectile.localAI[0] % BeamFreq() == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20.WithVolumeScale(0.8f), Projectile.Center);
                }
            }
        }
        public override void ReachedPeakEffects()
        {
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            FireBeam(num);
        }
        public override void DoDust(bool doFastThrowDust)
        {
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            if (Projectile.localAI[0] >= BeamFreq() && Projectile.localAI[0] % 3 == 0)
            {
                Player player = Main.player[Projectile.owner];
                float rot = (float)(Math.PI * ((Projectile.localAI[0] + Projectile.ai[1]) / 11f) * player.direction * player.gravDir);

                for (int i = 0; i < num; i++)
                {
                    Vector2 offset = rot.ToRotationVector2() * 24;
                    Dust.NewDustDirect(
                             Projectile.position + offset,
                             Projectile.width,
                             Projectile.height,
                             DustID.CursedTorch, //Dust ID
                             Projectile.velocity.X,
                             Projectile.velocity.Y,
                             100, //alpha goes from 0 to 255
                             default,
                             1f
                             ).noGravity = true;
                    rot += (MathHelper.ToRadians(360f / num));
                }
            }
        }
        public override void ExtraBehavior(ref bool flag)
        {
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            if (Projectile.ai[0] == 6) // Held in place
            {
                if (num >= 4 && (int)Projectile.localAI[0] == BeamFreq() * (maxBeams + 1))
                {
                    FireBeam(num);
                }
                if ((int)Projectile.localAI[0] % BeamFreq() == 0 && Projectile.localAI[0] > 0)
                {
                    SoundEngine.PlaySound(SoundID.Item20.WithVolumeScale(0.8f), Projectile.Center);
                }
            }
            if (Projectile.ai[0] == 5 || Projectile.ai[0] == 4) //Hitting Tile or returning after held
            {
                FireBeam(num);
            }
        }
        public override void PostDraw(Color lightColor)
        {
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            Player player = Main.player[Projectile.owner];
            float rot = (float)(Math.PI * ((Projectile.localAI[0] + Projectile.ai[1]) / 11f) * player.direction * player.gravDir);
            SpriteEffects effects = SpriteEffects.None;
            if (rot < 0)
            {
                effects = SpriteEffects.FlipVertically;
            }
            Color color = Color.White * 0.5f;
            if (num > 0)
            {
                for (int i = 0; i < num; i++)
                {
                    Texture2D orbTex = ModContent.Request<Texture2D>($"{Texture}Beam").Value;
                    Vector2 offset = rot.ToRotationVector2() * 24;
                    Main.EntitySpriteDraw(orbTex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) + offset, new Rectangle?(new Rectangle(0, 0, orbTex.Width, orbTex.Height)), color, rot + (float)Math.PI, new Vector2(orbTex.Width / 2, orbTex.Height / 2), Projectile.scale * 0.5f, effects, 0);
                    rot += (MathHelper.ToRadians(360f / num));
                }
            }
        }
    }
}
