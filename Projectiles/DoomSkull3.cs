using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;

namespace JoostMod.Projectiles
{
    public class DoomSkull3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skull of Destruction");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 250;
            projectile.height = 250;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.ranged = true;
            projectile.timeLeft = 180;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + (projectile.direction == -1 ? 3.14f : 0); ;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 4;
            }
            int minTileX = (int)(projectile.position.X / 16f);
            int maxTileX = (int)((projectile.position.X + projectile.width) / 16f);
            int minTileY = (int)(projectile.position.Y / 16f);
            int maxTileY = (int)((projectile.position.Y + projectile.height) / 16f);
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
            for (int i = minTileX; i <= maxTileX; i++)
            {
                for (int j = minTileY; j <= maxTileY; j++)
                {
                    bool canKillTile = true;
                    if (Main.tile[i, j] != null && Main.tile[i, j].active())
                    {
                        if (Main.tileDungeon[Main.tile[i, j].type] || Main.tile[i, j].type == 88 || Main.tile[i, j].type == 21 || Main.tile[i, j].type == 26 || Main.tile[i, j].type == 107 || Main.tile[i, j].type == 108 || Main.tile[i, j].type == 111 || Main.tile[i, j].type == 226 || Main.tile[i, j].type == 237 || Main.tile[i, j].type == 221 || Main.tile[i, j].type == 222 || Main.tile[i, j].type == 223 || Main.tile[i, j].type == 211)
                        {
                            canKillTile = false;
                        }
                        if (!Main.hardMode && Main.tile[i, j].type == 58)
                        {
                            canKillTile = false;
                        }
                        if (!TileLoader.CanExplode(i, j))
                        {
                            canKillTile = false;
                        }
                        if (canKillTile)
                        {
                            WorldGen.KillTile(i, j, false, false, false);
                            if (!Main.tile[i, j].active() && Main.netMode != 0)
                            {
                                NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
                            }
                        }
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Rectangle? rect = new Rectangle?(new Rectangle(0, (Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]) * projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]));
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            }
            return true;
        }
    }
}

