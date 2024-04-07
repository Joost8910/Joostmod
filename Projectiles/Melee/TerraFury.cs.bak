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
    public class TerraFury : Flail
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Fury");
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
            outTime = 15;
            throwSpeed = 26f;
            returnSpeed = 28f;
            returnSpeedAfterHeld = 32f;
            chainTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Chain");
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(42 * Projectile.scale);
            hitbox.Height = (int)(42 * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
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
                    Projectile.NewProjectile(Projectile.GetSource_ItemUse(player.HeldItem), Projectile.Center, Projectile.velocity, offType, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    player.ownedProjectileCounts[offType]++;
                }
            }
        }

        float beamDamageMult = 1f;
        int type = ModContent.ProjectileType<TerraFuryBeam>();
        public override void SwingEffects()
        {
            if (Main.myPlayer == Projectile.owner && Projectile.localAI[1] >= 30)
            {
                Projectile.localAI[1] -= 24;
                int damage = (int)(Projectile.damage * beamDamageMult);
                float kb = Projectile.knockBack * 0.5f;
                float shootSpeed = 26f;
                Vector2 dir = Projectile.Center.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX * Projectile.direction);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir * shootSpeed, type, damage, kb, Projectile.owner);
            }
        }
        public override void ReachedPeakEffects()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                int damage = (int)(Projectile.damage * beamDamageMult);
                float kb = Projectile.knockBack * 0.5f;
                float shootSpeed = 26f;
                Vector2 dir = Projectile.velocity;
                dir.Normalize();
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir * shootSpeed, type, damage, kb, Projectile.owner);
            }
        }
        public override void ExtraBehavior(ref bool flag)
        {
            if (Projectile.ai[0] == 6)
            {
                flag = false;
                float maxDist = 600f;
                NPC nPC = null;
                if (Projectile.owner == Main.myPlayer)
                {
                    if (Projectile.localAI[0] * swingSpeed >= 360f)
                    {
                        Projectile.localAI[0] = 0;
                        for (int i = 0; i < 200; i++)
                        {
                            NPC nPC2 = Main.npc[i];
                            if (nPC2.CanBeChasedBy(Projectile, false))
                            {
                                float dist = Projectile.Distance(nPC2.Center);
                                if (dist < maxDist && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                                {
                                    nPC = nPC2;
                                    maxDist = dist;
                                }
                            }
                        }
                    }
                    if (nPC != null)
                    {
                        Projectile.localAI[0] = 0f;
                        float shootSpeed = 24f;
                        Vector2 center = Projectile.Center;
                        Vector2 velocity = center.DirectionTo(nPC.Center) * shootSpeed;
                        int damage = (int)(Projectile.damage * beamDamageMult);
                        float kb = Projectile.knockBack * 0.5f;
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), center, velocity, type, damage, kb, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
    }
    public class TerraFury2 : Flail
    {
        public override string Texture => "JoostMod/Projectiles/Melee/TerraFury";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Fury");
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
            outTime = 15;
            throwSpeed = 26f;
            returnSpeed = 28f;
            returnSpeedAfterHeld = 32f;
            chainTex = (Texture2D)ModContent.Request<Texture2D>($"{Texture}_Chain");
            isOffhand = true;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Width = (int)(42 * Projectile.scale);
            hitbox.Height = (int)(42 * Projectile.scale);
            hitbox.X -= (hitbox.Width - Projectile.width) / 2;
            hitbox.Y -= (hitbox.Height - Projectile.height) / 2;
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
                    Projectile.NewProjectile(Projectile.GetSource_ItemUse(player.HeldItem), Projectile.Center, Projectile.velocity, offType, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    player.ownedProjectileCounts[offType]++;
                }
            }
        }
        float damageMult = 0.75f;
        public override void ModifyDamageScaling(ref float damageScale)
        {
            damageScale *= damageMult;
        }

        int type = ModContent.ProjectileType<TerraFuryBeam>();
        public override void SwingEffects()
        {
            if (Main.myPlayer == Projectile.owner && Projectile.localAI[1] >= 30)
            {
                Projectile.localAI[1] -= 24;
                int damage = (int)(Projectile.damage * damageMult);
                float kb = Projectile.knockBack * 0.5f;
                float shootSpeed = 26f;
                Vector2 dir = Projectile.Center.DirectionTo(Main.MouseWorld).SafeNormalize(Vector2.UnitX * Projectile.direction);
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir * shootSpeed, type, damage, kb, Projectile.owner);
            }
        }
        public override void ReachedPeakEffects()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                int damage = (int)(Projectile.damage * damageMult);
                float kb = Projectile.knockBack * 0.5f;
                float shootSpeed = 26f;
                Vector2 dir = Projectile.velocity;
                dir.Normalize();
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, dir * shootSpeed, type, damage, kb, Projectile.owner);
            }
        }
        public override void ExtraBehavior(ref bool flag)
        {
            if (Projectile.ai[0] == 6)
            {
                flag = false;
                float maxDist = 600f;
                NPC nPC = null;
                if (Projectile.owner == Main.myPlayer)
                {
                    if (Projectile.localAI[0] * swingSpeed >= 360f)
                    {
                        Projectile.localAI[0] = 0;
                        for (int i = 0; i < 200; i++)
                        {
                            NPC nPC2 = Main.npc[i];
                            if (nPC2.CanBeChasedBy(Projectile, false))
                            {
                                float dist = Projectile.Distance(nPC2.Center);
                                if (dist < maxDist && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                                {
                                    nPC = nPC2;
                                    maxDist = dist;
                                }
                            }
                        }
                    }
                    if (nPC != null)
                    {
                        Projectile.localAI[0] = 0f;
                        float shootSpeed = 24f;
                        Vector2 center = Projectile.Center;
                        Vector2 velocity = center.DirectionTo(nPC.Center) * shootSpeed;
                        int damage = (int)(Projectile.damage * damageMult);
                        float kb = Projectile.knockBack * 0.5f;
                        Projectile.NewProjectile(Projectile.GetSource_FromAI(), center, velocity, type, damage, kb, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
    }
}

