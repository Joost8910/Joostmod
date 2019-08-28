using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class IceBeamCannon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
            Main.projFrames[projectile.type] = 13;
        }
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
            projectile.light = 0.50f;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 10;
            projectile.coldDamage = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Main.myPlayer == projectile.owner)
            {
                bool channeling = player.channel && !player.noItems && !player.CCed;
                if (channeling)
                {
                    if (projectile.ai[0] % 8 < 1 && projectile.ai[0] < 48 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                    {
                        projectile.Kill();
                    }
                    projectile.ai[0]++;
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    }
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector13;
                }
                else
                {
                    projectile.Kill();
                }
                if (projectile.ai[0] == 1)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargingStart"));
                }
                if (projectile.ai[0] == 25)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargingStart2"));
                }
                if (projectile.ai[0] == 48)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargingLoop"));
                }
                if (projectile.ai[0] == 63)
                {
                    projectile.ai[0] = 48;
                    Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/ChargingLoop"));
                }
                if ((projectile.ai[0] % 4) == 0 && projectile.ai[0] < 48)
                {
                    projectile.frame = (projectile.frame + 1) % 12;
                }
                if (projectile.ai[0] >= 48 && (projectile.ai[0] % 4) == 0)
                {
                    projectile.frame = (projectile.frame % 3) + 10;
                }

            }

            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f + Vector2.Normalize(projectile.velocity) * 24;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));

            return false;
        }

        public override void Kill(int timeLeft)
        {
            // Here you can use projectile.ai[0] to calculate how much time has passed, like the following
            if (projectile.ai[0] < 48)
            {
                Vector2 pos = projectile.Center;
                Vector2 dir = Vector2.Normalize(projectile.velocity);
                if (float.IsNaN(dir.X) || float.IsNaN(dir.Y))
                {
                    dir = -Vector2.UnitY;
                }
                Projectile.NewProjectile(pos.X, pos.Y, dir.X * 8, dir.Y * 8, mod.ProjectileType("IceBeam"),
                        projectile.damage, projectile.knockBack, projectile.owner);
                Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeam"));
            }
            else
            {
                Vector2 pos = projectile.Center;
                Vector2 dir = Vector2.Normalize(projectile.velocity);
                if (float.IsNaN(dir.X) || float.IsNaN(dir.Y))
                {
                    dir = -Vector2.UnitY;
                }
                Projectile.NewProjectile(pos.X, pos.Y, dir.X * 10, dir.Y * 10, mod.ProjectileType("IceBeamCharged"),
                        projectile.damage * 8, projectile.knockBack * 8, projectile.owner);
                Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/IceBeamCharged"));

            }

        }
    }
}