using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class EarthenBillhook : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Earthen Billhook");
        }
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 90;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 18;
            Projectile.extraUpdates = 1;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            //Main.NewText(modifiers.GetKnockback(Projectile.knockBack));
            if (player.itemAnimation < player.itemAnimationMax / 2 && target.Distance(player.Center + player.velocity) > 60 + target.width / 2 && modifiers.GetKnockback(Projectile.knockBack) > 2.2)
            {
                modifiers.HitDirectionOverride = -Projectile.direction;
                modifiers.Knockback *= 0.6f;
            }
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 mountedCenter = player.MountedCenter;
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                Projectile.scale = player.inventory[player.selectedItem].scale;
                Projectile.width = (int)(50 * Projectile.scale);
                Projectile.height = (int)(50 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            Projectile.localNPCHitCooldown = player.itemAnimationMax;
            Projectile.spriteDirection = Projectile.direction * (int)player.gravDir;
            player.direction = Projectile.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            Projectile.Center = mountedCenter;
            /*
            Projectile.position = mountedCenter - Projectile.Size / 2;
            float stabMult = 7f;
            Vector2 vel = Projectile.velocity;
            vel.Normalize();
            Projectile.position += vel * speed * Projectile.ai[1];
            if (Projectile.ai[1] == 0f)
            {
                Projectile.ai[1] = 3f;
                Projectile.netUpdate = true;
            }
            */

            float num = (float)player.itemAnimation / (float)player.itemAnimationMax;
            float num2 = 1f - num;
            float dir = Projectile.velocity.ToRotation();
            float length = 62f * Projectile.scale;
            float lengthBase = 22f * Projectile.scale;
            Vector2 spinningpoint = new Vector2(1f, 0f).RotatedBy((double)(3.14159274f + num2 * 6.28318548f)) * new Vector2(length, 30f * Projectile.direction * Projectile.scale);
            Projectile.position += spinningpoint.RotatedBy((double)dir) + new Vector2(length + lengthBase, 0f).RotatedBy((double)dir);
            //Vector2 target = mountedCenter + spinningpoint.RotatedBy((double)dir) + new Vector2(length + lengthBase + 40f, 0f).RotatedBy((double)dir);
            Projectile.rotation = mountedCenter.AngleTo(Projectile.Center) + 2.355f;

            if (player.itemAnimation >= player.itemAnimationMax * 0.4f)
            {
                //Projectile.ai[1] += stabMult;
                foreach(Projectile p in Main.ActiveProjectiles)
                {
                    if (p.active && Projectile.Distance(p.Center) < 55 * Projectile.scale)
                    {
                        if (p.type == ModContent.ProjectileType<Boulder>() || p.type == ProjectileID.Boulder || p.type == ProjectileID.BouncyBoulder || p.type == ProjectileID.LifeCrystalBoulder || p.type == ProjectileID.MoonBoulder|| p.type == ProjectileID.MiniBoulder)
                        {
                            p.velocity = Projectile.velocity * 2f;
                            if (p.type == ModContent.ProjectileType<Boulder>())
                            {
                                p.damage = (int)(Projectile.damage * 3f);
                                p.knockBack = Projectile.knockBack * 3f;
                                p.owner = Projectile.owner;
                                if (p.timeLeft <= 500)
                                {
                                    SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.25f), p.Center);
                                    p.timeLeft = 540;
                                }
                            }
                            else
                            {
                                if (p.hostile)
                                {
                                    SoundEngine.PlaySound(SoundID.Tink.WithPitchOffset(-0.25f), p.Center);
                                    p.hostile = false;
                                }
                            }
                            p.netUpdate = true;
                            break; 
                        }
                    }
                }
            }/*
            else
            {
                Projectile.ai[1] -= stabMult;
                if (player.itemAnimation > player.itemAnimationMax / 3)
                    Projectile.velocity = Projectile.velocity.RotatedBy(2 * 0.0174f * player.direction * player.gravDir);
            }
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 2.355f;
            */

            if (player.itemAnimation == 0)
            {
                Projectile.Kill();
            }
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= 1.57f;
            }
        }
        /*
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 mountedCenter = player.MountedCenter;
            float rot = Projectile.DirectionFrom(mountedCenter).ToRotation();
            Vector2 unit = rot.ToRotationVector2();
            float point = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), mountedCenter, Projectile.Center, 25 * Projectile.scale, ref point))
            {
                return true;
            }

            return base.Colliding(projHitbox, targetHitbox);
        }
        */
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor;
            Vector2 mountedCenter = Main.player[Projectile.owner].MountedCenter;
            Vector2 dir = Projectile.DirectionTo(mountedCenter);
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + dir * 60 * Projectile.scale, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}