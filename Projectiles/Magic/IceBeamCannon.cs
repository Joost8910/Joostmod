using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Magic
{
    public class IceBeamCannon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Beam");
            Main.projFrames[Projectile.type] = 13;
        }
        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 44;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            //projectile.light = 0.50f;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.coldDamage = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
            if (channeling)
            {
                if (Projectile.ai[0] % 8 < 1 && Projectile.ai[0] < 48 && !player.CheckMana(player.inventory[player.selectedItem].mana, true))
                {
                    Projectile.Kill();
                }
                Projectile.ai[0]++;
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                    }
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * player.direction;
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector13;
                }
            }
            else
            {
                Projectile.Kill();
            }
            if (Projectile.ai[0] == 1)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/ChargingStart"), Projectile.Center);
            }
            if (Projectile.ai[0] == 25)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/ChargingStart2"), Projectile.Center);
            }
            if (Projectile.ai[0] == 48)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/ChargingLoop"), Projectile.Center);
            }
            if (Projectile.ai[0] == 63)
            {
                Projectile.ai[0] = 48;
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/ChargingLoop"), Projectile.Center);
            }
            if (Projectile.ai[0] % 4 == 0 && Projectile.ai[0] < 48)
            {
                Projectile.frame = (Projectile.frame + 1) % 12;
            }
            if (Projectile.ai[0] >= 48 && Projectile.ai[0] % 4 == 0)
            {
                Projectile.frame = Projectile.frame % 3 + 10;
            }
            float light = (Projectile.ai[0] > 48 ? 48 : Projectile.ai[0]) / 48f;
            Lighting.AddLight(Projectile.Center, light * 0.5f, light, light);


            Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f + Vector2.Normalize(Projectile.velocity) * 24;
            Projectile.rotation = Projectile.velocity.ToRotation() + num;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));

            return false;
        }

        public override void Kill(int timeLeft)
        {
            var source = Projectile.GetSource_Death();
            // Here you can use projectile.ai[0] to calculate how much time has passed, like the following
            if (Projectile.ai[0] < 48)
            {
                Vector2 pos = Projectile.Center;
                Vector2 dir = Vector2.Normalize(Projectile.velocity);
                if (float.IsNaN(dir.X) || float.IsNaN(dir.Y))
                {
                    dir = -Vector2.UnitY;
                }
                Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 8, dir.Y * 8, ModContent.ProjectileType<IceBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1);
                Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 8, dir.Y * 8, ModContent.ProjectileType<IceBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 8, dir.Y * 8, ModContent.ProjectileType<IceBeam>(), Projectile.damage, Projectile.knockBack, Projectile.owner, -1);
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/IceBeam"), Projectile.Center);
            }
            else
            {
                Vector2 pos = Projectile.Center;
                Vector2 dir = Vector2.Normalize(Projectile.velocity);
                if (float.IsNaN(dir.X) || float.IsNaN(dir.Y))
                {
                    dir = -Vector2.UnitY;
                }
                Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 10, dir.Y * 10, ModContent.ProjectileType<IceBeamCharged>(), Projectile.damage * 8, Projectile.knockBack * 8, Projectile.owner, 1);
                Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 10, dir.Y * 10, ModContent.ProjectileType<IceBeamCharged>(), Projectile.damage * 8, Projectile.knockBack * 8, Projectile.owner);
                Projectile.NewProjectile(source, pos.X, pos.Y, dir.X * 10, dir.Y * 10, ModContent.ProjectileType<IceBeamCharged>(), Projectile.damage * 8, Projectile.knockBack * 8, Projectile.owner, -1);
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/IceBeamCharged"), Projectile.Center);

            }

        }
    }
}