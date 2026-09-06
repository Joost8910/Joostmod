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
    public class TerraFury : Flail
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Fury");
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
            Projectile.timeLeft = 9600;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            outTime = 10;
            throwSpeed = 34f;
            returnSpeed = 36f;
            returnSpeedAfterHeld = 40f;
            chainTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Chain");
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            float num5 = Utils.Remap(Projectile.localAI[2], shineTime / 3, shineTime, 0f, 1f, true);
            float scale = Projectile.scale + num5 * 6;
            hitbox.Width = (int)(42 * scale);
            hitbox.Height = (int)(42 * scale);
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
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TerraBlade, new ParticleOrchestraSettings
            {
                PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox)
            }, default(int?));
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            if (Projectile.ai[0] <= 0 && num > 0) // Swing
            {
                FireBeam(num);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.TerraBlade, new ParticleOrchestraSettings
            {
                PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox)
            }, default(int?));
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            if (Projectile.ai[0] <= 0 && num > 0) // Swing
            {
                FireBeam(num);
            }
        }
        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                int offType = ModContent.ProjectileType<TerraFury2>();
                bool offControl = Main.mouseRight;
                if (offControl && player.ownedProjectileCounts[offType] == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item1, player.Center);
                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Projectile.Center, Projectile.velocity, offType, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    player.ownedProjectileCounts[offType]++;
                }
            }
        }

        readonly float beamDamageMult = 1f;
        readonly int type = ModContent.ProjectileType<TerraFuryBeam>();
        readonly int maxBeams = 4;
        readonly float shineTime = 30f;
        private int BeamFreq()
        {
            return (int)(30f / Main.player[Projectile.owner].GetAttackSpeed(DamageClass.Melee));
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
            if ((int)Projectile.localAI[0] <= BeamFreq() * maxBeams)
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
            SoundEngine.PlaySound(SoundID.Item4.WithPitchOffset(0.25f).WithVolumeScale(0.7f), Projectile.Center);

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
                if (num >= maxBeams && (int)Projectile.localAI[0] == BeamFreq() * (maxBeams + 1))
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
            Rectangle r = Utils.CenteredRectangle(center, new Vector2(40, 40));

            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            float num = Projectile.DirectionFrom(vector2).SafeNormalize(Vector2.Zero).ToRotation() + 2.355f;
            float num3 = r.Size().Length() / Projectile.Hitbox.Size().Length();

            float num5 = Utils.Remap(Projectile.localAI[2], shineTime / 3, shineTime, 0f, 1f, true);

            Vector2 vector3 = r.Center.ToVector2() + new Vector2(0f, Projectile.gfxOffY);
            Vector2.Lerp(vector2, vector3, 1.1f);
            Texture2D value = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 origin = value.Size() / 2f;
            Color color = new Color(83, 255, 40, 0);
            float num6 = num - 0.7853982f;

            
            for (int i = 0; i < 8; i++)
            {
                Main.EntitySpriteDraw(value, Projectile.Center - Main.screenPosition, default(Rectangle?), color * num5, num6, origin, new Vector2(num5 * num3, num3) * Projectile.scale * num3, effects, 0f);

                Main.EntitySpriteDraw(value, Projectile.Center - Main.screenPosition + new Vector2(0f, 2f), default(Rectangle?), color * num5 * 0.65f, num6, origin, new Vector2(num5 * num3 * 1f * num5, num3 * 4f * num5) * Projectile.scale * num3, effects, 0f);

                num6 += (float)(Math.PI / 4);
            }
            r.Offset((int)(0f - Main.screenPosition.X), (int)(0f - Main.screenPosition.Y));
            return base.PreDraw(ref lightColor);
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
    public class TerraFury2 : Flail
    {
        public override string Texture => "JoostMod/Projectiles/Melee/TerraFury";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Fury");
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
            Projectile.timeLeft = 9600;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            outTime = 10;
            throwSpeed = 30f;
            returnSpeed = 32f;
            returnSpeedAfterHeld = 34f;
            chainTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Chain");
            isOffhand = true;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            float num5 = Utils.Remap(Projectile.localAI[2], shineTime / 3, shineTime, 0f, 1f, true);
            float scale = Projectile.scale + num5 * 6;
            hitbox.Width = (int)(42 * scale);
            hitbox.Height = (int)(42 * scale);
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
        public override void PostAI()
        {
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                int offType = ModContent.ProjectileType<TerraFury>();
                bool offControl = Main.mouseLeft;
                if (offControl && player.ownedProjectileCounts[offType] == 0)
                {
                    SoundEngine.PlaySound(SoundID.Item1, player.Center);
                    Projectile.NewProjectile(player.GetSource_ItemUse(player.HeldItem), Projectile.Center, Projectile.velocity, offType, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    player.ownedProjectileCounts[offType]++;
                }
            }
        }
        readonly float damageMult = 0.75f;
        readonly float beamDamageMult = 0.75f;
        readonly int type = ModContent.ProjectileType<TerraFuryBeam>();
        readonly int maxBeams = 4;
        readonly float shineTime = 24f;
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SourceDamage *= damageMult;
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.SourceDamage *= damageMult;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            if (Projectile.ai[0] <= 0 && num > 0) // Swing
            {
                FireBeam(num);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int num = Math.Min(maxBeams, (int)Projectile.localAI[0] / BeamFreq());
            if (Projectile.ai[0] <= 0 && num > 0) // Swing
            {
                FireBeam(num);
            }
        }
        private int BeamFreq()
        {
            return (int)(30f / Main.player[Projectile.owner].GetAttackSpeed(DamageClass.Melee));
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
            if ((int)Projectile.localAI[0] <= BeamFreq() * maxBeams)
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
            SoundEngine.PlaySound(SoundID.Item4.WithPitchOffset(0.25f).WithVolumeScale(0.7f), Projectile.Center);

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
                if (num >= maxBeams && (int)Projectile.localAI[0] == BeamFreq() * (maxBeams + 1))
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
            Rectangle r = Utils.CenteredRectangle(center, new Vector2(40, 40));

            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            float num = Projectile.DirectionFrom(vector2).SafeNormalize(Vector2.Zero).ToRotation() + 2.355f;
            float num3 = r.Size().Length() / Projectile.Hitbox.Size().Length();

            float num5 = Utils.Remap(Projectile.localAI[2], shineTime / 3, shineTime, 0f, 1f, true);

            Vector2 vector3 = r.Center.ToVector2() + new Vector2(0f, Projectile.gfxOffY);
            Vector2.Lerp(vector2, vector3, 1.1f);
            Texture2D value = TextureAssets.Extra[ExtrasID.SharpTears].Value;
            Vector2 origin = value.Size() / 2f;
            Color color = new Color(83, 255, 40, 0);
            float num6 = num - 0.7853982f;


            for (int i = 0; i < 8; i++)
            {
                Main.EntitySpriteDraw(value, Projectile.Center - Main.screenPosition, default(Rectangle?), color * num5, num6, origin, new Vector2(num5 * num3, num3) * Projectile.scale * num3, effects, 0f);

                Main.EntitySpriteDraw(value, Projectile.Center - Main.screenPosition + new Vector2(0f, 2f), default(Rectangle?), color * num5 * 0.65f, num6, origin, new Vector2(num5 * num3 * 1f * num5, num3 * 4f * num5) * Projectile.scale * num3, effects, 0f);

                num6 += (float)(Math.PI / 4);
            }
            r.Offset((int)(0f - Main.screenPosition.X), (int)(0f - Main.screenPosition.Y));
            return base.PreDraw(ref lightColor);
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

