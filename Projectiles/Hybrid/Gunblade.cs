using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Hybrid
{
    public class Gunblade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gunblade");
        }
        public override void SetDefaults()
        {
            Projectile.width = 84;
            Projectile.height = 84;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.CountsAsClass(DamageClass.Melee);
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.aiStyle = -1;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.localAI[0] > 5 && Projectile.localAI[0] < 10)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.localAI[0] > 5 && Projectile.localAI[0] < 10)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.ai[0] == 0)
            {
                Player player = Main.player[Projectile.owner];
                Vector2 unit = Projectile.velocity;
                Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 118, 16, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = !player.dead && Projectile.localAI[0] < 20 && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Projectile.localAI[0] <= 5)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Vector2 vector13 = Main.MouseWorld - vector;
                        vector13.Normalize();
                        if (vector13.HasNaNs())
                        {
                            vector13 = Vector2.UnitX * player.direction;
                        }
                        if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                        {
                            Projectile.netUpdate = true;
                        }
                        Projectile.velocity = vector13;
                        if (Projectile.velocity.X < 0)
                        {
                            Projectile.direction = -1;
                        }
                        else
                        {
                            Projectile.direction = 1;
                        }
                    }
                }
                else if (Projectile.localAI[0] < 10)
                {
                    float rot = Projectile.velocity.ToRotation();
                    rot -= 15f * 0.0174f * Projectile.direction;
                    Projectile.velocity = rot.ToRotationVector2();
                }
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.localAI[0]++;
            if (Projectile.localAI[0] == 5)
            {
                float vel = 16;
                int type = 0;
                Item item = new Item();
                bool canShoot = false;
                bool flag = false;
                for (int i = 54; i < 58; i++)
                {
                    if (player.inventory[i].ammo == AmmoID.Bullet && player.inventory[i].stack > 0)
                    {
                        item = player.inventory[i];
                        canShoot = true;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    for (int j = 0; j < 54; j++)
                    {
                        if (player.inventory[j].ammo == AmmoID.Bullet && player.inventory[j].stack > 0)
                        {
                            item = player.inventory[j];
                            canShoot = true;
                            break;
                        }
                    }
                }
                if (canShoot)
                {
                    vel += item.shootSpeed;
                    type = item.shoot;
                    if (item.consumable)
                    {
                        player.ConsumeItem(item.type);
                    }
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Projectile.velocity * vel, type, (int)(Projectile.damage * 0.75f) + item.damage, Projectile.knockBack + item.knockBack, Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item41, Projectile.Center);
                }
                {
                    SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/MissileClick"), Projectile.Center);
                }
            }
            Projectile.velocity.Normalize();
            Projectile.position = vector - Projectile.Size / 2f + Projectile.velocity * Projectile.width * 0.7f;
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 1.57f : 0) + 0.785f;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * Projectile.direction), (double)(Projectile.velocity.X * Projectile.direction));
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (Projectile.ai[0] > 0)
            {
                Projectile.velocity.Normalize();
                float shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                for (int i = 0; i < Projectile.frame; i++)
                {
                    Vector2 vel = Projectile.velocity * (shootSpeed + i * shootSpeed / 11);
                    Projectile.NewProjectile(Projectile.GetSource_Death(), vector, vel, (int)Projectile.ai[1], Projectile.damage, Projectile.knockBack / 2f, Projectile.owner);
                    SoundEngine.PlaySound(SoundID.Item63, Projectile.Center);
                }
            }
        }
    }
}