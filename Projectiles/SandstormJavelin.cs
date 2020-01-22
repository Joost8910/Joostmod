using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SandstormJavelin : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Javelin");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            Main.projFrames[projectile.type] = 5;
        }
		public override void SetDefaults()
		{
			projectile.width = 54;
			projectile.height = 54;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.thrown = true;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 16;
            projectile.extraUpdates = 1;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 24;
            height = 24;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            Main.PlaySound(0, projectile.Center);
            if (projectile.ai[0] <= 24)
            {
                projectile.timeLeft = 20;
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.ai[1] > 0)
            {
                Player player = Main.player[projectile.owner];
                float rot = projectile.rotation - 1.57f - (0.785f * projectile.spriteDirection);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = projectile.Center - unit * 38;
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 76 * projectile.scale, 24 * projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override void AI()
        {
	        Player player = Main.player[projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 offset = new Vector2(0, 0);
            float speed = 1f;
            float vel = 1f;
            if (player.inventory[player.selectedItem].shoot == projectile.type)
            {
                speed = (24f / player.inventory[player.selectedItem].useTime) / 2;
                vel = player.inventory[player.selectedItem].shootSpeed * (projectile.localAI[0] + 1);
                projectile.netUpdate = true;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (projectile.ai[1] < 0)
            {
                offset.X = player.direction * -8;
                offset.Y = -12;
            }
            if (Main.myPlayer == projectile.owner && projectile.ai[1] <= 0)
            {
                Vector2 vector13 = Main.MouseWorld - (center + offset);
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * (float)player.direction;
                }
                if (channeling)
                {
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
                    projectile.velocity = vector13;
                }
                else
                {
                    projectile.ai[1] = 1;
                    projectile.velocity = vector13 * vel;
                    projectile.tileCollide = true;
                    projectile.netUpdate = true;
                    Main.PlaySound(42, projectile.Center, 204);
                }
            }
            if (projectile.ai[1] <= 0)
            {
                projectile.spriteDirection = projectile.direction;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f + (0.785f * projectile.direction);
                player.heldProj = projectile.whoAmI;
                player.itemTime = (int)(18 / (speed * 2));
                player.itemAnimation = (int)(18 / (speed * 2));
                if (player.itemTime < 2)
                {
                    player.itemTime = 2;
                }
                if (player.itemAnimation < 2)
                {
                    player.itemAnimation = 2;
                }
                player.ChangeDir(projectile.direction * (int)player.gravDir);
                /*
                double rad = (player.fullRotation - 1.83f) + ((projectile.ai[1] - 20) * 0.0174f * projectile.direction) + 2.355f;
                if (player.gravDir < 0)
                {
                    rad += 3.14f;
                }
                projectile.rotation = (float)rad + 2.355f;
                if (projectile.direction < 0)
                {
                    rad -= 1.045;
                    projectile.rotation = (float)rad - 2.355f;
                }
                double dist = -20 * projectile.scale * projectile.direction;
                */
                projectile.position.X = center.X - projectile.width / 2;
                projectile.position.Y = center.Y - projectile.height / 2;
                projectile.position += offset + projectile.velocity * ((projectile.ai[1] * 0.5f) + 20);
                if (projectile.ai[0] < 24)
                {
                    projectile.ai[1] -= speed;
                    player.velocity.X *= 0.99f;
                    projectile.localAI[0] += 0.02f * speed;
                }
                if (projectile.ai[0] > 24 && projectile.soundDelay >= 0 && channeling)
                {
                    projectile.soundDelay = -60;
                    Main.PlaySound(2, projectile.Center, 39);
                }
                if (channeling && projectile.ai[0] > 24)
                {
                    player.velocity.X *= 0.98f;
                    projectile.ai[0] = 24;
                    if (player.velocity.Y * player.gravDir > player.gravity)
                    {
                        projectile.localAI[1] = 1;
                    }
                    else
                    {
                        projectile.localAI[1] = 0;
                    }
                }
                if (!channeling && projectile.ai[0] < 24)
                {
                    projectile.ai[0] = 24;
                }
                if (projectile.ai[0] <= 25)
                {
                    projectile.ai[0] += speed;
                    projectile.timeLeft = 600;
                }
            }
            else
            {
                Vector2 shoot = (projectile.rotation - 1.57f - (0.785f * projectile.spriteDirection)).ToRotationVector2() * -1;
                if (Main.rand.NextBool(27 - (int)projectile.ai[0]))
                {
                    int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 32, shoot.X * 5, shoot.Y * 5);
                    Main.dust[d].noGravity = true;
                }
                if (projectile.ai[0] > 24)
                {
                    projectile.frameCounter++;
                    if (projectile.frameCounter >= 8)
                    {
                        if (projectile.frame % 2 == 0)
                        {
                            Main.PlaySound(2, projectile.Center, 7);
                        }
                        projectile.frameCounter = 0;
                        projectile.frame = (projectile.frame % 4) + 1;
                    }
                    int minTileX = (int)(projectile.position.X / 16f) - 1;
                    int maxTileX = (int)((projectile.position.X + projectile.width) / 16f) + 1;
                    int minTileY = (int)(projectile.position.Y / 16f) - 1;
                    int maxTileY = (int)((projectile.position.Y + projectile.height) / 16f) + 1;
                    if (minTileX < 0)
                    {
                        minTileX = 0;
                    }
                    if (maxTileX > Main.maxTilesX)
                    {
                        maxTileX = Main.maxTilesX;
                    }
                    if (minTileY < 0)
                    {
                        minTileY = 0;
                    }
                    if (maxTileY > Main.maxTilesY)
                    {
                        maxTileY = Main.maxTilesY;
                    }
                    for (int x = minTileX; x <= maxTileX; x++)
                    {
                        for (int y = minTileY; y <= maxTileY; y++)
                        {
                            if (Main.tile[x, y].active())
                            {
                                shoot = shoot.RotatedByRandom(30 * 0.0174f);
                                if (Main.tile[x, y].type == TileID.Sand)
                                {
                                    WorldGen.KillTile(x, y, false, false, true);
                                    if (!Main.tile[x, y].active() && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
                                    }
                                    Projectile.NewProjectile((x * 16) - 8, (y * 16) - 8, shoot.X * 12, shoot.Y * 12, mod.ProjectileType("SandBlock"), projectile.damage, projectile.knockBack, projectile.owner, 0, -1);
                                }
                                if (Main.tile[x, y].type == TileID.Ebonsand)
                                {
                                    WorldGen.KillTile(x, y, false, false, true);
                                    if (!Main.tile[x, y].active() && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
                                    }
                                    Projectile.NewProjectile((x * 16) - 8, (y * 16) - 8, shoot.X * 12, shoot.Y * 12, mod.ProjectileType("EbonSandBlock"), projectile.damage, projectile.knockBack, projectile.owner, 0, -1);
                                }
                                if (Main.tile[x, y].type == TileID.Pearlsand)
                                {
                                    WorldGen.KillTile(x, y, false, false, true);
                                    if (!Main.tile[x, y].active() && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
                                    }
                                    Projectile.NewProjectile((x * 16) - 8, (y * 16) - 8, shoot.X * 12, shoot.Y * 12, mod.ProjectileType("PearlSandBlock"), projectile.damage, projectile.knockBack, projectile.owner, 0, -1);
                                }
                                if (Main.tile[x, y].type == TileID.Crimsand)
                                {
                                    WorldGen.KillTile(x, y, false, false, true);
                                    if (!Main.tile[x, y].active() && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
                                    }
                                    Projectile.NewProjectile((x * 16) - 8, (y * 16) - 8, shoot.X * 12, shoot.Y * 12, mod.ProjectileType("CrimSandBlock"), projectile.damage, projectile.knockBack, projectile.owner, 0, -1);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override bool? CanCutTiles()
        {
            return projectile.ai[1] > 0;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * (projectile.localAI[0] + 1));
            knockback = knockback * (projectile.localAI[0] + 1);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * (projectile.localAI[0] + 1));
        }
        public override bool CanHitPvp(Player target)
        {
            if (projectile.ai[1] > 0)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.ai[1] > 0)
            {
                return base.CanHitNPC(target);
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
            Color color = lightColor;
            if (projectile.ai[0] >= 25)
            {
                for (int k = 1; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] + drawOrigin - Main.screenPosition;
                    Color color2 = color * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, projectile.frame * (tex.Height / Main.projFrames[projectile.type]), tex.Width, tex.Height / Main.projFrames[projectile.type]));
                    spriteBatch.Draw(tex, drawPos, rect, color2, projectile.oldRot[k], drawOrigin, projectile.scale, effects, 0f);
                }
            }
            //color.A = (byte)projectile.alpha;
            //spriteBatch.Draw(tex, projectile.position + drawOrigin - Main.screenPosition, new Rectangle?(new Rectangle(0, projectile.frame * (tex.Height / Main.projFrames[projectile.type]), tex.Width, tex.Height / Main.projFrames[projectile.type])), color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return true;
        }
    }
}
