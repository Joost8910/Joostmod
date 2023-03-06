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
	public class EarthenBillhook2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Earthen Billhook");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			Projectile.width = 124;
			Projectile.height = 122;
			Projectile.aiStyle = 19;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 124;
            Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.ownerHitCheck = true;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = -1;
            Projectile.extraUpdates = 1;
		}
		
		public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 center = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
            float speed = player.GetAttackSpeed(DamageClass.Melee) / 2;
            if (player.inventory[player.selectedItem].shoot == Mod.Find<ModProjectile>("EarthenBillhook").Type)
            {
                Projectile.scale = player.inventory[player.selectedItem].scale;
                speed = ((36f / player.inventory[player.selectedItem].useTime) / player.GetAttackSpeed(DamageClass.Melee)) / 2;
                Projectile.width = (int)(46 * Projectile.scale);
                Projectile.height = (int)(46 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            Projectile.velocity.Y = 0;
            Projectile.direction = player.direction * (int)player.gravDir;
            Projectile.velocity.X = Projectile.direction;
            bool channeling = player.channel && player.active && !player.dead && !player.noItems && !player.CCed;
            if (channeling && Main.myPlayer == Projectile.owner)
            {
                Vector2 vector13 = Main.MouseWorld - center;
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * (float)player.direction;
                }
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
            }
            player.ChangeDir(Projectile.direction * (int)player.gravDir);
            Projectile.spriteDirection = Projectile.direction;
            double rad = (player.fullRotation - 1.83f) + ((Projectile.ai[1] - 20) * 0.0174f * Projectile.direction);
            if (player.gravDir < 0)
            {
                rad += 3.14f;
            }
            Projectile.rotation = (float)rad;
            if (Projectile.direction == -1)
            {
                rad -= 1.045;
                Projectile.rotation = (float)rad - 1.57f;
            }
            double dist = -70 * Projectile.scale * Projectile.direction;
            Projectile.position.X = center.X + (0 * player.direction) - (int)(Math.Cos(rad - 0.785f) * dist) - (Projectile.width / 2);
            Projectile.position.Y = center.Y + (0) - (int)(Math.Sin(rad - 0.785f) * dist) - (Projectile.height / 2);
            if (Projectile.ai[1] < 0)
            {
                Projectile.position.Y += player.gravDir * ((Projectile.ai[1] / Projectile.scale * 0.15f) - 4);
            }
            if (Projectile.ai[0] < 10)
            {
                Projectile.ai[0] += speed;
                Projectile.ai[1] -= speed;
                Projectile.timeLeft = 122;
            }
            if (Projectile.timeLeft <= 120)
            {
                if (Projectile.timeLeft == 120)
                {
                    SoundEngine.PlaySound(SoundID.Item18, Projectile.Center);
                }
                if (Projectile.ai[1] < 180)
                {
                    Projectile.timeLeft = 70;
                    Projectile.ai[1] += 12 * speed;
                }
                if (Projectile.ai[1] > 180)
                {
                    Projectile.ai[1] = 180;
                    if (Projectile.ai[0] < 100)
                    {
                        Projectile.ai[0] = 100;
                        Projectile.timeLeft = (int)(36 / (speed * 2));
                        bool foundTile = false;
                        Vector2 pos = new Vector2(center.X + 120 * Projectile.scale * Projectile.direction * player.gravDir, player.position.Y + player.height);
                        float velY = -9;
                        if (player.velocity.Y == 0)
                        {
                            if (player.gravDir > 0)
                            {
                                for (int i = pos.ToTileCoordinates().Y; i < pos.ToTileCoordinates().Y + 16; i++)
                                {
                                    if (Main.tile[pos.ToTileCoordinates().X, i].HasTile && Main.tileSolid[Main.tile[pos.ToTileCoordinates().X, i].TileType])
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
                                    if (Main.tile[pos.ToTileCoordinates().X, i].HasTile && Main.tileSolid[Main.tile[pos.ToTileCoordinates().X, i].TileType])
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
                            SoundEngine.PlaySound(SoundID.Trackable, pos);
                            for (int d = 0; d < 15; d++)
                            {
                                Dust.NewDust(new Vector2(pos.X - 20, pos.Y), 40, 10, 1, 0, -4 * player.gravDir, 0, default, 1);
                            }
                            if (Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer == Projectile.owner)
                            {
                                Projectile.NewProjectile(pos, new Vector2(0, velY * player.gravDir), Mod.Find<ModProjectile>("Boulder").Type, Projectile.damage, Projectile.knockBack, Projectile.owner);
                            }
                        }
                        else
                        {
                            SoundEngine.PlaySound(SoundID.Item7.WithVolumeScale(1.2f).WithPitchOffset(-0.3f), Projectile.Center);
                        }
                    }
                }
                if (Projectile.ai[0] == 100 && Main.myPlayer == Projectile.owner && Main.mouseLeft)
                {
                    Projectile.Kill();
                }
            }
            player.heldProj = Projectile.whoAmI;
            if (Projectile.ai[1] < 180)
            {
                player.itemTime = (int)((36f / (speed * 2)) - ((Projectile.ai[1] / 15f) * 2 / speed));
                player.itemAnimation = (int)((36f / (speed * 2)) - ((Projectile.ai[1] / 15f) * 2 / speed));
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
            player.ChangeDir(Projectile.direction * (int)player.gravDir);
        }
        public override bool? CanCutTiles()
        {
            return (Projectile.ai[0] >= 10 && Projectile.ai[0] < 100);
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.ai[0] >= 10 && Projectile.ai[0] < 100)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] >= 10 && Projectile.ai[0] < 100)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.ai[0] >= 10 && Projectile.ai[0] < 100)
            {
                Player player = Main.player[Projectile.owner];
                float rot = Projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 160 * Projectile.scale, 32 * Projectile.scale, ref point))
                {
                    return true;
                }
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
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Color color = lightColor;
            if (Projectile.ai[1] > 0 && Projectile.ai[0] < 100)
            {
                for (int k = 1; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                    Color color2 = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
                    spriteBatch.Draw(tex, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0f);
                }
            }
            //color.A = (byte)projectile.alpha;
            spriteBatch.Draw(tex, Projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0f);
            return false;
        }
    }
}