using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class WaterBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Water Ball");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 25;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.scale = 0.2f;
            Projectile.width = 15;
            Projectile.height = 15;
            Projectile.aiStyle = 0;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.alpha = 100;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;
            Projectile.timeLeft = 1200;
            Projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.scale = 0.25f + (Projectile.ai[0] / 25);
            Projectile.width = (int)(60 * Projectile.scale);
            Projectile.height = (int)(60 * Projectile.scale);
            if (player.channel && !player.noItems && !player.CCed && !player.dead && Projectile.ai[1] <= 0)
            {
                Projectile.penetrate = -1;
                Projectile.timeLeft = Projectile.timeLeft <= 1192 ? 1200 : Projectile.timeLeft;
                player.itemTime = 15;
                player.itemAnimation = 15;
                if (Projectile.Center.X > player.Center.X)
                    player.direction = 1;
                if (Projectile.Center.X < player.Center.X)
                    player.direction = -1;
                Vector2 dir = player.DirectionTo(Projectile.Center);
                player.itemRotation = (float)Math.Atan2(dir.Y * player.direction, dir.X * player.direction);
                if (Main.myPlayer == Projectile.owner)
                {

                    float speed = 15f - (Projectile.ai[0] * 0.4f);
                    float dist = Projectile.Distance(Main.MouseWorld);
                    if (dist < 100)
                        speed *= dist / 100f;
                    Projectile.velocity = Projectile.DirectionTo(Main.MouseWorld) * speed;
                    Projectile.netUpdate = true;
                }
                if (Projectile.ai[0] < 25 && Projectile.timeLeft == 1200)
                {
                    int minTileX = (int)(Projectile.position.X / 16f);
                    int maxTileX = (int)((Projectile.position.X + Projectile.width) / 16f);
                    int minTileY = (int)(Projectile.position.Y / 16f);
                    int maxTileY = (int)((Projectile.position.Y + Projectile.height) / 16f);
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
                    for (int i = minTileX; i <= maxTileX && Projectile.localAI[0] < 250; i++)
                    {
                        for (int j = minTileY; j <= maxTileY && Projectile.localAI[0] < 250; j++)
                        {
                            if (Main.tile[i, j].LiquidType == 0)
                            {
                                while (Main.tile[i,j].LiquidAmount > 0)
                                {
                                    Main.tile[i, j].LiquidAmount--;
                                    Projectile.localAI[0]++;
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
                    if (Main.raining && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, new Vector2(Projectile.Center.X + ((Main.screenPosition.Y - Projectile.Center.Y) / 14f * Main.windSpeed * 12f), Main.screenPosition.Y - 20), 1, 1) && Main.screenPosition.Y <= Main.worldSurface * 16.0)
                    {
                        Projectile.localAI[0] += Projectile.width * 2;
                    }
                    if (Projectile.localAI[0] >= 250)
                    {
                        Projectile.localAI[0] -= 250;
                        Projectile.ai[0]++;
                        SoundEngine.PlaySound(SoundID.SplashWeak, Projectile.position);
                    }
                }
                if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    //projectile.position.X -= projectile.velocity.X > 0 ? 4 : (projectile.velocity.X < 0 ? -3 : 0);
                    //projectile.position.Y -= projectile.velocity.Y > 0 ? 4 : (projectile.velocity.X < 0 ? -3 : 0);
                    int left = (int)(Projectile.position.X / 16);
                    int right = (int)((Projectile.position.X + Projectile.width) / 16);
                    int top = (int)((Projectile.position.Y) / 16);
                    int bottom = (int)((Projectile.position.Y + Projectile.height) / 16);


                    if (!Collision.SolidTiles(left - 1, left - 1, top - 1, top - 1))
                    {
                        Projectile.position.X -= 8;
                        Projectile.position.Y -= 8;
                    }
                    else if (!Collision.SolidTiles(right + 1, right + 1, top - 1, top - 1))
                    {
                        Projectile.position.X += 8;
                        Projectile.position.Y -= 8;
                    }
                    else if (!Collision.SolidTiles(left - 1, left - 1, bottom + 1, bottom + 1))
                    {
                        Projectile.position.X -= 8;
                        Projectile.position.Y += 8;
                    }
                    else if (!Collision.SolidTiles(right + 1, right + 1, bottom + 1, bottom + 1))
                    {
                        Projectile.position.X += 8;
                        Projectile.position.Y += 8;
                    }
                    if (!Collision.SolidTiles(left, right, top-1, top-1))
                    {
                        Projectile.position.Y -= 8;
                    }
                    else if (!Collision.SolidTiles(left, right, bottom + 1, bottom + 1))
                    {
                        Projectile.position.Y += 8;
                    }
                    if (!Collision.SolidTiles(left - 1, left - 1, top, bottom))
                    {
                        Projectile.position.X -= 8;
                    }
                    else if (!Collision.SolidTiles(right + 1, right + 1, top, bottom))
                    {
                        Projectile.position.X += 8;
                    }
                }
            }
            else
            {
                Projectile.penetrate = 1;
                Projectile.ai[1] = 1;
                if (Projectile.velocity.Y < 5)
                {
                    Projectile.velocity.Y += 0.025f;
                    if (Main.tile[(int)Projectile.Center.ToTileCoordinates().X, (int)Projectile.Center.ToTileCoordinates().Y].LiquidAmount < 150)
                        Projectile.velocity.Y += 0.125f;
                }
            }
            if (Main.rand.NextBool((int)(3f / Projectile.scale)))
            {
                Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 172, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f, 0, default, 1f + Projectile.ai[0] / 16).noGravity = true;
                if (Main.rand.NextBool(20))
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 172, Projectile.velocity.X * -0.5f, Projectile.velocity.Y * -0.5f, 0, default, 1f + Projectile.ai[0] / 25);
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Splash, Projectile.position);
            int minTileX = (int)(Projectile.position.X / 16f);
            int maxTileX = (int)((Projectile.position.X + Projectile.width) / 16f);
            int minTileY = (int)(Projectile.position.Y / 16f);
            int maxTileY = (int)((Projectile.position.Y + Projectile.height) / 16f);
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
            for (int i = minTileX; i <= maxTileX && Projectile.ai[0] > 0; i++)
            {
                for (int j = minTileY; j <= maxTileY && Projectile.ai[0] > 0; j++)
                {
                    Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X / 2, Projectile.velocity.Y / 2).RotatedByRandom(MathHelper.ToRadians(360));
                    perturbedSpeed *= 1f - (Main.rand.NextFloat() * .3f);
                    Projectile.NewProjectile(i * 16 + 8, j * 16 + 8, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("WaterSplash").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                    Projectile.ai[0]--;
                }
            }
            int x = (int)Projectile.Center.ToTileCoordinates().X;
            int y = (int)Projectile.Center.ToTileCoordinates().Y;
            Main.tile[x, y].LiquidType = 0;
            Main.tile[x, y].LiquidAmount += (byte)((int)Projectile.localAI[0]);
            WorldGen.SquareTileFrame(x, y, true);
            if (Main.netMode == 1)
            {
                NetMessage.sendWater(x, y);
            }
            Vector2 vel = Projectile.velocity;
            vel.Normalize();
            vel *= 15;
            for (int i = 0; i <= (int)(Projectile.scale * 30); i++)
            {
                Dust.NewDustDirect(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 172, vel.X, vel.Y, 0, default, 1f + Projectile.ai[0] / 16).noGravity = true;
                if (Main.rand.NextBool(15))
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, 172, vel.X, vel.Y, 0, default, 1f + Projectile.ai[0] / 16);
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Projectile.ai[1] >= 1)
            {
                float mult = 1f + (Projectile.ai[0] / 5);
                damage = (int)(damage * mult);
                knockback = knockback + mult;
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (Projectile.ai[1] >= 1)
            {
                float mult = 1f + (Projectile.ai[0] / 5);
                damage = (int)(damage * mult);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return Projectile.ai[1] > 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[Projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Color color = lightColor;
            color.A = 100;
            Main.EntitySpriteDraw(tex, drawPosition, rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            for (int k = 0; k < Projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = Projectile.oldPos[k] + (Projectile.Size / 2) - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
                Color color2 = color;
                if (Vector2.Distance(drawPosition, drawPos) < 30)
                {
                    color2.A = (byte)(color2.A * (Vector2.Distance(drawPosition, drawPos)) / 30);
                    color2.R = (byte)(color2.R * (Vector2.Distance(drawPosition, drawPos)) / 30);
                    color2.G = (byte)(color2.G * (Vector2.Distance(drawPosition, drawPos)) / 30);
                    color2.B = (byte)(color2.B * (Vector2.Distance(drawPosition, drawPos)) / 30);
                }
                float scale = ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * Projectile.scale;
                Rectangle? rect2 = new Rectangle?(new Rectangle(0, (TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]) * Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]));
                Main.EntitySpriteDraw(tex, drawPos, rect2, color2, Projectile.rotation, drawOrigin, scale, SpriteEffects.None, 0);
            }
            return false;
        }
    }
}
