using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class PetEyeball : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pet Eyeball");
            Main.projFrames[projectile.type] = 3;
		}
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.melee = true;
            projectile.ignoreWater = false;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
        }
        public override bool? CanHitNPC(NPC target)
		{
			return !target.friendly && projectile.ai[1] >= 10;
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.Center.X > Main.player[projectile.owner].Center.X)
            {
                hitDirection = 1;
            }
            else
            {
                hitDirection = -1;
            }
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor = 1f;
                    if (player.inventory[player.selectedItem].shoot == projectile.type)
                    {
                        scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                    }
                    Vector2 dir = Main.MouseWorld - projectile.Center;
                    dir = dir.RotatedByRandom(MathHelper.ToRadians(45));
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * (float)player.direction;
                    }
                    if (projectile.Distance(Main.MouseWorld) < 72)
                    {
                        scaleFactor = projectile.Distance(Main.MouseWorld) / 4f;
                    }
                    if (dir.X * scaleFactor != projectile.velocity.X || dir.Y * scaleFactor != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = dir * scaleFactor;
                }
                if (Collision.SolidCollision(projectile.Center + new Vector2(-4, -4), 8, 8))
                {
                    projectile.velocity = projectile.DirectionTo(playerPos) * 9;
                    projectile.position += projectile.velocity;
                }
            }
            else
            {
                projectile.velocity = projectile.DirectionTo(playerPos) * 18;
                projectile.tileCollide = false;
                if (projectile.Distance(playerPos) < 20 || projectile.Distance(playerPos) > 800)
                {
                    projectile.Kill();
                }
            }
        
            if (projectile.ai[1] == 0)
            {
                projectile.ai[0] = 0;
            }
            projectile.ai[1] = projectile.ai[1] < 15 ? projectile.ai[1]+1 : 0;
            if (projectile.ai[1] < 5)
            {
                projectile.frame = 0;
            }
            else if (projectile.ai[1] < 10)
            {
                projectile.frame = 1;
            }
            else
            {
                projectile.frame = 2;
            }
            if (projectile.Distance(playerPos) > 250)
            {
                projectile.position = playerPos + (projectile.DirectionFrom(playerPos) * 250) - projectile.Size / 2f;
            }
            if (projectile.Center.X < playerPos.X)
            {
                projectile.direction = -1;
            }
            else
            {
                projectile.direction = 1;
            }
            for (int i = 0; i < Main.item.Length; i++)
            {
                if (Main.item[i].active)
                {
                    Item I = Main.item[i];
                    if (projectile.Hitbox.Intersects(I.Hitbox))
                    {
                        I.velocity = projectile.velocity;
                        I.position = projectile.Center - (I.Size / 2);
                    }
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            projectile.spriteDirection = projectile.direction;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 10;
            player.itemAnimation = 10;
            Vector2 direction = player.DirectionTo(projectile.Center);
            player.itemRotation = (float)Math.Atan2((double)(direction.Y * (float)projectile.direction), (double)(direction.X * (float)projectile.direction));
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModContent.GetTexture("JoostMod/Projectiles/PetEyeball_Chain");

            Vector2 position = projectile.Center;
            Player player = Main.player[projectile.owner];
            Vector2 mountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1f, SpriteEffects.None, 0.0f);
                }
            }

            return true;
        }
    }
}