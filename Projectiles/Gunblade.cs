using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Gunblade : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gunblade");
        }
        public override void SetDefaults()
        {
            projectile.width = 84;
            projectile.height = 84;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
            projectile.aiStyle = -1;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.localAI[0] > 5 && projectile.localAI[0] < 10)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (projectile.localAI[0] > 5 && projectile.localAI[0] < 10)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[0] == 0)
            {
                Player player = Main.player[projectile.owner];
                Vector2 unit = projectile.velocity;
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
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = !player.dead && projectile.localAI[0] < 20 && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (projectile.localAI[0] <= 5)
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        Vector2 vector13 = Main.MouseWorld - vector;
                        vector13.Normalize();
                        if (vector13.HasNaNs())
                        {
                            vector13 = Vector2.UnitX * (float)player.direction;
                        }
                        if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                        {
                            projectile.netUpdate = true;
                        }
                        projectile.velocity = vector13;
                        if (projectile.velocity.X < 0)
                        {
                            projectile.direction = -1;
                        }
                        else
                        {
                            projectile.direction = 1;
                        }
                    }
                }
                else if (projectile.localAI[0] < 10)
                {
                    float rot = projectile.velocity.ToRotation();
                    rot -= 15f * 0.0174f * projectile.direction;
                    projectile.velocity = rot.ToRotationVector2();
                }
            }
            else
            {
                projectile.Kill();
            }
            projectile.localAI[0]++;
            if (projectile.localAI[0] == 5)
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
                    Projectile.NewProjectile(projectile.Center, projectile.velocity * vel, type, (int)(projectile.damage * 0.75f) + item.damage, projectile.knockBack + item.knockBack, projectile.owner);
                    Main.PlaySound(2, projectile.Center, 41);
                }
                else
                {
                    Main.PlaySound(SoundLoader.customSoundType, projectile.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileClick"));
                }
            }
            projectile.velocity.Normalize();
            projectile.position = (vector - (projectile.Size / 2f)) + (projectile.velocity * projectile.width * 0.7f);
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 1.57f : 0) + 0.785f;
            projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.ai[0] > 0)
            {
                projectile.velocity.Normalize();
                float shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                for (int i = 0; i < projectile.frame; i++)
                {
                    Vector2 vel = projectile.velocity * (shootSpeed + (i * shootSpeed / 11));
                    Projectile.NewProjectile(vector, vel, (int)projectile.ai[1], projectile.damage, projectile.knockBack / 2f, projectile.owner);
                    Main.PlaySound(2, projectile.Center, 63);
                }
            }
        }
    }
}