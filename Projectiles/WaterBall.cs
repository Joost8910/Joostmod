using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class WaterBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Ball");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.scale = 0.25f;
            projectile.width = 15;
            projectile.height = 15;
            projectile.aiStyle = 0;
            projectile.penetrate = 2;
            projectile.tileCollide = true;
            projectile.alpha = 100;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.timeLeft = 600;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.scale = 0.25f + (projectile.ai[0] / 16);
            projectile.width = (int)(60 * projectile.scale);
            projectile.height = (int)(60 * projectile.scale);
            if (player.channel && !player.noItems && !player.CCed && projectile.ai[1] <= 0)
            {
                projectile.timeLeft = projectile.timeLeft <= 596 ? 600 : projectile.timeLeft;
                player.itemTime = 15;
                player.itemAnimation = 15;
                if (Main.myPlayer == projectile.owner)
                {
                    float speed = 15f - (projectile.ai[0] * 0.5f);
                    float dist = projectile.Distance(Main.MouseWorld);
                    if (dist < 100)
                        speed *= dist / 100f;
                    projectile.velocity = projectile.DirectionTo(Main.MouseWorld) * speed;
                    projectile.netUpdate = true;
                }
                if (projectile.ai[0] < 16 && projectile.timeLeft == 600)
                {
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
                    for (int i = minTileX; i <= maxTileX && projectile.localAI[0] < 150; i++)
                    {
                        for (int j = minTileY; j <= maxTileY && projectile.localAI[0] < 150; j++)
                        {
                            if (Main.tile[i, j].liquidType() == 0)
                            {
                                while (Main.tile[i,j].liquid > 0)
                                {
                                    Main.tile[i, j].liquid--;
                                    projectile.localAI[0]++;
                                }
                                //projectile.localAI[0] += Main.tile[i, j].liquid;
                                //Main.tile[i, j].liquid = 0;
                                WorldGen.SquareTileFrame(i, j, false);
                                if (Main.netMode == 1)
                                {
                                    NetMessage.sendWater(i, j);
                                }
                            }
                        }
                    }
                    if (projectile.localAI[0] >= 150)
                    {
                        projectile.localAI[0] -= 150;
                        projectile.ai[0]++;
                        Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 1);
                    }
                }
            }
            else
            {
                projectile.ai[1] = 1;
                if (projectile.velocity.Y < 10)
                {
                    projectile.velocity.Y += 0.05f;
                    if (Main.tile[(int)projectile.Center.ToTileCoordinates().X, (int)projectile.Center.ToTileCoordinates().Y].liquid < 150)
                        projectile.velocity.Y += 0.25f;
                }
            }
            for (int i = 0; i < (int)(projectile.scale * 8f); i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 172, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, default, 1f + projectile.ai[0] / 16).noGravity = true;
                if (Main.rand.NextBool(10))
                    Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 172, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 0, default, 1f + projectile.ai[0] / 16);
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 0);
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
            for (int i = minTileX; i <= maxTileX && projectile.ai[0] > 0; i++)
            {
                for (int j = minTileY; j <= maxTileY && projectile.ai[0] > 0; j++)
                {
                    Vector2 perturbedSpeed = new Vector2(projectile.velocity.X / 2, projectile.velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(360));
                    perturbedSpeed *= 1f - (Main.rand.NextFloat() * .3f);
                    Projectile.NewProjectile(i * 16, j * 16, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("WaterSplash"), projectile.damage, projectile.knockBack, projectile.owner);
                    projectile.ai[0]--;
                }
            }
            /*
            for (int i = 0; i < projectile.ai[0]; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X / 2, projectile.velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(360));
                perturbedSpeed *= 1f - (Main.rand.NextFloat() * .3f);
                Projectile.NewProjectile(projectile.Center.X + perturbedSpeed.X, projectile.Center.Y + perturbedSpeed.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("WaterSplash"), projectile.damage, projectile.knockBack, projectile.owner);
            }
            */
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            float mult = 1f + (projectile.ai[0] / 16);
            damage = (int)(damage * mult);
            knockback = knockback + mult;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            float mult = 1f + (projectile.ai[0] / 16);
            damage = (int)(damage * mult);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return projectile.ai[1] > 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            Color color = lightColor;
            color.A = 100;
            spriteBatch.Draw(tex, drawPosition, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }
    }
}
