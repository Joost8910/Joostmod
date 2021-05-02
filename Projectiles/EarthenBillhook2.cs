using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class EarthenBillhook2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Billhook");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			projectile.width = 124;
			projectile.height = 122;
			projectile.aiStyle = 19;
            projectile.aiStyle = 0;
            projectile.timeLeft = 124;
            projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.ownerHitCheck = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = -1;
            projectile.extraUpdates = 1;
		}
		
		public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
            float speed = player.meleeSpeed / 2;
            if (player.inventory[player.selectedItem].shoot == mod.ProjectileType("EarthenBillhook"))
            {
                projectile.scale = player.inventory[player.selectedItem].scale;
                speed = ((36f / player.inventory[player.selectedItem].useTime) / player.meleeSpeed) / 2;
                projectile.width = (int)(46 * projectile.scale);
                projectile.height = (int)(46 * projectile.scale);
                projectile.netUpdate = true;
            }
            projectile.velocity.Y = 0;
            projectile.direction = player.direction * (int)player.gravDir;
            projectile.velocity.X = projectile.direction;
            bool channeling = player.channel && player.active && !player.dead && !player.noItems && !player.CCed;
            if (channeling && Main.myPlayer == projectile.owner)
            {
                Vector2 vector13 = Main.MouseWorld - center;
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * (float)player.direction;
                }
                if (vector13.X > 0)
                {
                    projectile.direction = (int)player.gravDir;
                    projectile.netUpdate = true;
                }
                else
                {
                    projectile.direction = -(int)player.gravDir;
                    projectile.netUpdate = true;
                }
            }
            player.ChangeDir(projectile.direction * (int)player.gravDir);
            projectile.spriteDirection = projectile.direction;
            double rad = (player.fullRotation - 1.83f) + ((projectile.ai[1] - 20) * 0.0174f * projectile.direction);
            if (player.gravDir < 0)
            {
                rad += 3.14f;
            }
            projectile.rotation = (float)rad;
            if (projectile.direction == -1)
            {
                rad -= 1.045;
                projectile.rotation = (float)rad - 1.57f;
            }
            double dist = -70 * projectile.scale * projectile.direction;
            projectile.position.X = center.X + (0 * player.direction) - (int)(Math.Cos(rad - 0.785f) * dist) - (projectile.width / 2);
            projectile.position.Y = center.Y + (0) - (int)(Math.Sin(rad - 0.785f) * dist) - (projectile.height / 2);
            if (projectile.ai[1] < 0)
            {
                projectile.position.Y += player.gravDir * ((projectile.ai[1] / projectile.scale * 0.15f) - 4);
            }
            if (projectile.ai[0] < 10)
            {
                projectile.ai[0] += speed;
                projectile.ai[1] -= speed;
                projectile.timeLeft = 122;
            }
            if (projectile.timeLeft <= 120)
            {
                if (projectile.timeLeft == 120)
                {
                    Main.PlaySound(2, projectile.Center, 18);
                }
                if (projectile.ai[1] < 180)
                {
                    projectile.timeLeft = 70;
                    projectile.ai[1] += 12 * speed;
                }
                if (projectile.ai[1] > 180)
                {
                    projectile.ai[1] = 180;
                    if (projectile.ai[0] < 100)
                    {
                        projectile.ai[0] = 100;
                        projectile.timeLeft = (int)(36 / (speed * 2));
                        bool foundTile = false;
                        Vector2 pos = new Vector2(center.X + 120 * projectile.scale * projectile.direction * player.gravDir, player.position.Y + player.height);
                        float velY = -9;
                        if (player.velocity.Y == 0)
                        {
                            if (player.gravDir > 0)
                            {
                                for (int i = pos.ToTileCoordinates().Y; i < pos.ToTileCoordinates().Y + 16; i++)
                                {
                                    if (Main.tile[pos.ToTileCoordinates().X, i].active() && Main.tileSolid[Main.tile[pos.ToTileCoordinates().X, i].type])
                                    {
                                        foundTile = true;
                                        velY -= Math.Abs(pos.ToTileCoordinates().Y - i) * 0.3f;
                                        pos.Y = i * 16;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = pos.ToTileCoordinates().Y; i > pos.ToTileCoordinates().Y - 16; i--)
                                {
                                    if (Main.tile[pos.ToTileCoordinates().X, i].active() && Main.tileSolid[Main.tile[pos.ToTileCoordinates().X, i].type])
                                    {
                                        foundTile = true;
                                        pos.Y = (i * 16) - 16;
                                        break;
                                    }
                                }
                            }
                        }
                        if (foundTile)
                        {
                            Main.PlaySound(42, pos, 210);
                            for (int d = 0; d < 15; d++)
                            {
                                Dust.NewDust(new Vector2(pos.X - 20, pos.Y), 40, 10, 1, 0, -4 * player.gravDir, 0, default, 1);
                            }
                            if (Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer == projectile.owner)
                            {
                                Projectile.NewProjectile(pos, new Vector2(0, velY * player.gravDir), mod.ProjectileType("Boulder"), projectile.damage, projectile.knockBack, projectile.owner);
                            }
                        }
                        else
                        {
                            Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 7, 1.2f, -0.3f);
                        }
                    }
                }
                if (projectile.ai[0] == 100 && Main.myPlayer == projectile.owner && Main.mouseLeft)
                {
                    projectile.Kill();
                }
            }
            player.heldProj = projectile.whoAmI;
            if (projectile.ai[1] < 180)
            {
                player.itemTime = (int)((36f / (speed * 2)) - ((projectile.ai[1] / 15f) * 2 / speed));
                player.itemAnimation = (int)((36f / (speed * 2)) - ((projectile.ai[1] / 15f) * 2 / speed));
                if (player.itemTime < 2)
                {
                    player.itemTime = 2;
                }
                if (player.itemAnimation < 2)
                {
                    player.itemAnimation = 2;
                }
            }
            else
            {
                player.itemTime = 2;
                player.itemAnimation = 2;
            }
            player.ChangeDir(projectile.direction * (int)player.gravDir);
        }
        public override bool? CanCutTiles()
        {
            return (projectile.ai[0] >= 10 && projectile.ai[0] < 100);
        }
        public override bool CanHitPvp(Player target)
        {
            if (projectile.ai[0] >= 10 && projectile.ai[0] < 100)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[0] >= 10 && projectile.ai[0] < 100)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[0] >= 10 && projectile.ai[0] < 100)
            {
                Player player = Main.player[projectile.owner];
                float rot = projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 160 * projectile.scale, 32 * projectile.scale, ref point))
                {
                    return true;
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
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor;
            if (projectile.ai[1] > 0 && projectile.ai[0] < 100)
            {
                for (int k = 1; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + new Vector2(projectile.width / 2, projectile.height / 2);
                    Color color2 = color * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
                    spriteBatch.Draw(tex, drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
                }
            }
            //color.A = (byte)projectile.alpha;
            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }
    }
}