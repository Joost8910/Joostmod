using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class SearingBow : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Searing Bow");
            Main.projFrames[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.ignoreWater = true;
        }
        public override bool? CanHitNPC(NPC target)
		{
			return false;
		}
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
            float speed = 25;
            float shootSpeed = 13f;

            if (player.inventory[player.selectedItem].shoot == projectile.type)
            {
                shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                speed = player.inventory[player.selectedItem].useTime;
                projectile.netUpdate = true;
            }
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
            }
            projectile.position = new Vector2(vector.X + projectile.velocity.X * 6, vector.Y + projectile.velocity.Y * 12) - projectile.Size / 2f;
            projectile.position.Y += player.gravDir * 6;
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            if (player.gravDir < 0)
            {
                projectile.rotation += 3.14f;
                projectile.spriteDirection = -projectile.direction;
            }
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            player.itemTime = 2;
            player.itemAnimation = 2;
            projectile.frame = (projectile.ai[1] > 0 || projectile.localAI[0] >= speed * 0.75f) ? 1 : 0;
            if (projectile.localAI[0] > speed * 0.75f || projectile.localAI[0] <= speed * 0.25f)
            {
                player.bodyFrame.Y = player.bodyFrame.Height * 10;
            }
            else if (projectile.localAI[0] > speed * 0.5f)
            {
                player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            }
            else if (projectile.localAI[0] > speed * 0.25f)
            {
                player.bodyFrame.Y = player.bodyFrame.Height;
            }
            if (projectile.ai[1] < 5 && projectile.localAI[0] < speed)
            {
                projectile.localAI[0]++;
            }
            if (player.noItems || player.CCed || player.dead || !player.active || (projectile.ai[1] <= 0 && !player.controlUseItem && !player.controlUseTile))
            {
                projectile.Kill();
            }
            else
            {
                Item item = new Item();
                bool canShoot = false;
                bool flag = false;
                for (int i = 54; i < 58; i++)
                {
                    if (player.inventory[i].ammo == AmmoID.Arrow && player.inventory[i].stack > 0)
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
                        if (player.inventory[j].ammo == AmmoID.Arrow && player.inventory[j].stack > 0)
                        {
                            item = player.inventory[j];
                            canShoot = true;
                            break;
                        }
                    }
                }
                if (projectile.ai[1] < 5 && (player.controlUseTile || projectile.ai[1] < 1) && canShoot)
                {
                    if (projectile.localAI[0] >= (projectile.ai[1] < 1 ? speed : speed * 0.75f))
                    {
                        projectile.ai[0] = item.shoot;
                        if (projectile.ai[0] == ProjectileID.WoodenArrowFriendly)
                        {
                            projectile.ai[0] = mod.ProjectileType("BlazingArrow");
                        }
                        projectile.localAI[0] = 0;
                        projectile.ai[1]++;
                        Main.PlaySound(SoundID.Item17, projectile.Center);
                    }
                }
                else if (!(player.controlUseTile && player.controlUseItem) && projectile.ai[1] > 0)
                {
                    if (canShoot)
                    {
                        shootSpeed += item.shootSpeed;
                        float rotation = MathHelper.ToRadians(22.5f);
                        Vector2 vel = projectile.velocity * shootSpeed;
                        for (int i = 0; i < projectile.ai[1]; i++)
                        {
                            if (item.consumable)
                            {
                                player.ConsumeItem(item.type);
                            }
                            Vector2 perturbedSpeed = (projectile.ai[1] <= 1) ? vel : vel.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (projectile.ai[1] - 1)));
                            Projectile.NewProjectile(projectile.Center, perturbedSpeed, (int)projectile.ai[0], (projectile.damage + item.damage), projectile.knockBack + item.knockBack, projectile.owner);
                        }
                        //Projectile.NewProjectile(projectile.Center, projectile.velocity * shootSpeed, (int)projectile.ai[0], (projectile.damage + item.damage), projectile.knockBack + item.knockBack, projectile.owner);
                        Main.PlaySound(SoundID.Item5, projectile.Center);
                        projectile.ai[1] = 0;
                    }
                    else
                    {
                        Main.PlaySound(SoundID.Item7, projectile.Center);
                    }
                }
            }
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle(0, projectile.frame * (tex.Height / Main.projFrames[projectile.type]), tex.Width, (tex.Height / Main.projFrames[projectile.type]));
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, rect, lightColor, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            if (projectile.ai[0] != 0)
            {
                Main.instance.LoadProjectile((int)(projectile.ai[0]));
                Texture2D arrowTex = Main.projectileTexture[(int)(projectile.ai[0])];
                int frames = (Main.projFrames[(int)(projectile.ai[0])] < 1) ? 1 : Main.projFrames[(int)(projectile.ai[0])];
                Vector2 arrowOrigin = new Vector2(arrowTex.Width * 0.5f, (arrowTex.Height / frames) * 0.5f);
                Rectangle? arrowRect = new Rectangle(0, 0, arrowTex.Width, (arrowTex.Height / frames));
                float rotOff = MathHelper.ToRadians(22.5f);
                Vector2 vel = projectile.velocity;
                vel.Normalize();
                for (int i = 0; i < projectile.ai[1]; i++)
                {
                    Vector2 dir = (projectile.ai[1] <= 1) ? vel : vel.RotatedBy(MathHelper.Lerp(-rotOff, rotOff, i / (projectile.ai[1] - 1)));
                    float rot = dir.ToRotation() + 1.57f;
                    Vector2 offset = (dir * arrowOrigin.Y) - (vel * drawOrigin.X);
                    spriteBatch.Draw(arrowTex, projectile.Center + offset - Main.screenPosition, arrowRect, lightColor, rot, arrowOrigin, projectile.scale, effects, 0f);
                }
            }
            return false;
        }
    }
}