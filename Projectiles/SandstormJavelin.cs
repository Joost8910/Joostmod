using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class SandstormJavelin : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstorm Javelin");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Projectile.type] = 5;
        }
		public override void SetDefaults()
		{
			Projectile.width = 54;
			Projectile.height = 54;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 16;
            Projectile.extraUpdates = 1;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 24;
            height = 24;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            if (Projectile.ai[0] <= 24)
            {
                Projectile.timeLeft = 20;
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.ai[1] > 0)
            {
                Player player = Main.player[Projectile.owner];
                float rot = Projectile.rotation - 1.57f - (0.785f * Projectile.spriteDirection);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = Projectile.Center - unit * 38;
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 76 * Projectile.scale, 24 * Projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override void AI()
        {
	        Player player = Main.player[Projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.MountedCenter, true);
            Vector2 offset = new Vector2(0, 0);
            float speed = 1f;
            float vel = 1f;
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                speed = (24f / player.inventory[player.selectedItem].useTime) / 2;
                vel = player.inventory[player.selectedItem].shootSpeed * (Projectile.localAI[0] + 1);
                Projectile.netUpdate = true;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (Projectile.ai[1] < 0)
            {
                offset.X = player.direction * -8;
                offset.Y = -12;
            }
            if (Main.myPlayer == Projectile.owner && Projectile.ai[1] <= 0)
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
                        Projectile.direction = (int)player.gravDir;
                        Projectile.netUpdate = true;
                    }
                    else
                    {
                        Projectile.direction = -(int)player.gravDir;
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector13;
                }
                else
                {
                    Projectile.ai[1] = 1;
                    Projectile.velocity = vector13 * vel;
                    Projectile.tileCollide = true;
                    Projectile.netUpdate = true;
                    SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                }
            }
            if (Projectile.ai[1] <= 0)
            {
                Projectile.spriteDirection = Projectile.direction;
                Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f + (0.785f * Projectile.direction);
                player.heldProj = Projectile.whoAmI;
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
                player.ChangeDir(Projectile.direction * (int)player.gravDir);
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
                Projectile.position.X = center.X - Projectile.width / 2;
                Projectile.position.Y = center.Y - Projectile.height / 2;
                Projectile.position += offset + Projectile.velocity * ((Projectile.ai[1] * 0.5f) + 20);
                if (Projectile.ai[0] < 24)
                {
                    Projectile.ai[1] -= speed;
                    player.velocity.X *= 0.99f;
                    Projectile.localAI[0] += 0.02f * speed;
                }
                if (Projectile.ai[0] > 24 && Projectile.soundDelay >= 0 && channeling)
                {
                    Projectile.soundDelay = -60;
                    SoundEngine.PlaySound(SoundID.Item39, Projectile.Center);
                }
                if (channeling && Projectile.ai[0] > 24)
                {
                    player.velocity.X *= 0.98f;
                    Projectile.ai[0] = 24;
                    if (player.velocity.Y * player.gravDir > player.gravity)
                    {
                        Projectile.localAI[1] = 1;
                    }
                    else
                    {
                        Projectile.localAI[1] = 0;
                    }
                }
                if (!channeling && Projectile.ai[0] < 24)
                {
                    Projectile.ai[0] = 24;
                }
                if (Projectile.ai[0] <= 25)
                {
                    Projectile.ai[0] += speed;
                    Projectile.timeLeft = 600;
                }
            }
            else
            {
                Vector2 shoot = (Projectile.rotation - 1.57f - (0.785f * Projectile.spriteDirection)).ToRotationVector2() * -1;
                if (Main.rand.NextBool(27 - (int)Projectile.ai[0]))
                {
                    int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 32, shoot.X * 5, shoot.Y * 5);
                    Main.dust[d].noGravity = true;
                }
                if (Projectile.ai[0] > 24)
                {
                    Projectile.frameCounter++;
                    if (Projectile.frameCounter >= 8)
                    {
                        if (Projectile.frame % 2 == 0)
                        {
                            SoundEngine.PlaySound(SoundID.Item7, Projectile.Center);
                        }
                        Projectile.frameCounter = 0;
                        Projectile.frame = (Projectile.frame % 4) + 1;
                    }
                    int minTileX = (int)(Projectile.position.X / 16f) - 1;
                    int maxTileX = (int)((Projectile.position.X + Projectile.width) / 16f) + 1;
                    int minTileY = (int)(Projectile.position.Y / 16f) - 1;
                    int maxTileY = (int)((Projectile.position.Y + Projectile.height) / 16f) + 1;
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
                            if (Main.tile[x, y].HasTile)
                            {
                                shoot = shoot.RotatedByRandom(30 * 0.0174f);
                                if (Main.tile[x, y].TileType == TileID.Sand)
                                {
                                    WorldGen.KillTile(x, y, false, false, true);
                                    if (!Main.tile[x, y].HasTile && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
                                    }
                                    Projectile.NewProjectile((x * 16) - 8, (y * 16) - 8, shoot.X * 12, shoot.Y * 12, Mod.Find<ModProjectile>("SandBlock").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, -1);
                                }
                                if (Main.tile[x, y].TileType == TileID.Ebonsand)
                                {
                                    WorldGen.KillTile(x, y, false, false, true);
                                    if (!Main.tile[x, y].HasTile && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
                                    }
                                    Projectile.NewProjectile((x * 16) - 8, (y * 16) - 8, shoot.X * 12, shoot.Y * 12, Mod.Find<ModProjectile>("EbonSandBlock").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, -1);
                                }
                                if (Main.tile[x, y].TileType == TileID.Pearlsand)
                                {
                                    WorldGen.KillTile(x, y, false, false, true);
                                    if (!Main.tile[x, y].HasTile && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
                                    }
                                    Projectile.NewProjectile((x * 16) - 8, (y * 16) - 8, shoot.X * 12, shoot.Y * 12, Mod.Find<ModProjectile>("PearlSandBlock").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, -1);
                                }
                                if (Main.tile[x, y].TileType == TileID.Crimsand)
                                {
                                    WorldGen.KillTile(x, y, false, false, true);
                                    if (!Main.tile[x, y].HasTile && Main.netMode != 0)
                                    {
                                        NetMessage.SendData(17, -1, -1, null, 0, (float)x, (float)y, 0f, 0, 0, 0);
                                    }
                                    Projectile.NewProjectile((x * 16) - 8, (y * 16) - 8, shoot.X * 12, shoot.Y * 12, Mod.Find<ModProjectile>("CrimSandBlock").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0, -1);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override bool? CanCutTiles()
        {
            return Projectile.ai[1] > 0;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * (Projectile.localAI[0] + 1));
            knockback = knockback * (Projectile.localAI[0] + 1);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * (Projectile.localAI[0] + 1));
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.ai[1] > 0)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[1] > 0)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[Projectile.type]) * 0.5f);
            Color color = lightColor;
            if (Projectile.ai[0] >= 25)
            {
                for (int k = 1; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] + drawOrigin - Main.screenPosition;
                    Color color2 = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, Projectile.frame * (tex.Height / Main.projFrames[Projectile.type]), tex.Width, tex.Height / Main.projFrames[Projectile.type]));
                    Main.EntitySpriteDraw(tex, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
                }
            }
            //color.A = (byte)projectile.alpha;
            //spriteBatch.Draw(tex, projectile.position + drawOrigin - Main.screenPosition, new Rectangle?(new Rectangle(0, projectile.frame * (tex.Height / Main.projFrames[projectile.type]), tex.Width, tex.Height / Main.projFrames[projectile.type])), color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return true;
        }
    }
}
