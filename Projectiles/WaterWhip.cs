using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class WaterWhip : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slapping Water Tendril");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.magic = true;
            projectile.ignoreWater = true;
            projectile.alpha = 100;
            projectile.extraUpdates = 1;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 20;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Vector2.Distance(projectile.position, projectile.oldPos[1]) > 20)
            {
                for (int i = 0; i < 5; i++)
                {
                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 33, -oldVelocity.X, -oldVelocity.Y);
                }
                Main.PlaySound(19, projectile.Center, 1);
            }
            projectile.rotation = projectile.velocity.ToRotation();
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projectile.localAI[0] >= 4 && Vector2.Distance(projectile.position, projectile.oldPos[1]) > 5)
            {
                Player player = Main.player[projectile.owner];
                Vector2 start = projectile.oldPos[1] + projectile.Size / 2;
                Vector2 end = projectile.Center;
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 32 * projectile.scale, ref point))
                {
                    return true;
                }
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            float mult = Vector2.Distance(projectile.position, projectile.oldPos[1]) * 0.05f;
            if (mult > 3f)
                mult = 3f;
            damage = (int)(damage * mult);
            knockback = 0;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            float mult = Vector2.Distance(projectile.position, projectile.oldPos[1]) * 0.05f;
            if (mult > 3)
                mult = 3f;
            damage = (int)(damage * mult);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.knockBackResist > 0)
            {
                Vector2 vel = projectile.position - projectile.oldPos[1];
                vel.Normalize();
                float mult = Vector2.Distance(projectile.position, projectile.oldPos[1]) * 0.05f;
                if (mult > 3f)
                    mult = 3f;
                target.velocity = vel * projectile.knockBack * target.knockBackResist * mult;
            }
            Main.PlaySound(19, projectile.Center, 0);
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 33, -target.velocity.X, -target.velocity.Y, 0, default, 2);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (!target.noKnockback)
            {
                Vector2 vel = projectile.position - projectile.oldPos[1];
                vel.Normalize();
                float mult = Vector2.Distance(projectile.position, projectile.oldPos[1]) * 0.05f;
                if (mult > 3)
                    mult = 3;
                target.velocity = vel * projectile.knockBack * mult;
            }
            Main.PlaySound(19, projectile.Center, 0);
            for (int i = 0; i < 12; i++)
            {
                Dust.NewDust(target.position, target.width, target.height, 33, -target.velocity.X, -target.velocity.Y, 0, default, 2);
            }
        }
        int nextProj = -1;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(projectile.localAI[0]);
            writer.Write(projectile.localAI[1]);
            writer.Write(nextProj);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            projectile.localAI[0] = reader.ReadSingle();
            projectile.localAI[1] = reader.ReadSingle();
            nextProj = reader.ReadInt32();
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            int max = 30;
            projectile.scale = 1f - (projectile.ai[0] / max);
            projectile.width = (int)(32 * projectile.scale);
            projectile.height = (int)(32 * projectile.scale);
            bool channeling = player.channel && !player.noItems && !player.CCed;
            int mana = 5;
            if (player.inventory[player.selectedItem].type == mod.ItemType("WaterWhip"))
            {
                mana = player.inventory[player.selectedItem].mana / 2;
            }
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0]++;
            }
            if (channeling && player.CheckMana(mana))
            {
                if (projectile.ai[1] == -1 && projectile.localAI[0] % 40 == 0)
                {
                    player.CheckMana(mana, true);
                }
                projectile.localAI[0]++;
                if (nextProj >= 0 && Main.projectile[nextProj].type != projectile.type && Main.projectile[nextProj].owner != projectile.owner)
                {
                    projectile.localAI[0] = 5;
                }
                if (player.ownedProjectileCounts[projectile.type] < max - 1 && projectile.localAI[0] == 5)
                {
                    nextProj = Projectile.NewProjectile(projectile.Center, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0] + 1, projectile.identity);
                    player.ownedProjectileCounts[projectile.type]++;
                }
                if (projectile.ai[1] != -1)
                {
                    if (Main.projectile[(int)projectile.ai[1]].type == projectile.type && Main.projectile[(int)projectile.ai[1]].owner == projectile.owner)
                    {
                        vector = Main.projectile[(int)projectile.ai[1]].Center;
                    }
                    else
                    {
                        projectile.Kill();
                    }
                }
                if (Main.myPlayer == projectile.owner)
                {
                    float scaleFactor = 20f * projectile.scale;
                    /*
                    for (int i = 0; i < projectile.ai[0]; i++)
                    {
                        scaleFactor += 8f * (1f - (i / max));
                    }
                    */
                    Vector2 dir = Main.MouseWorld - vector;
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * (float)player.direction;
                    }
                    dir *= scaleFactor;
                    if (dir.X != projectile.velocity.X || dir.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    if (projectile.ai[1] == -1)
                    {
                        projectile.velocity = dir;
                    }
                    else
                    {
                        if (Collision.CanHitLine(projectile.position, projectile.width, projectile.height, projectile.Center + dir, 1, 1))
                        {
                            Vector2 move = vector + dir - projectile.Center;
                            if (move.Length() > 0)
                            {
                                move *= scaleFactor / move.Length();
                            }
                            float home = 5 + projectile.ai[0] / 2;
                            projectile.velocity = ((home - 1f) * projectile.velocity + move) / home;
                            if (projectile.velocity.Length() < scaleFactor)
                            {
                                projectile.velocity *= (scaleFactor / projectile.velocity.Length());
                            }
                        }
                        else
                        {
                            projectile.velocity = projectile.oldVelocity;
                        }
                    }
                }
            }
            else
            {
                projectile.Kill();
            }
            projectile.position = (vector) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation();
            //projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            if (projectile.ai[0] == 0)
            {
                player.ChangeDir(projectile.direction);
                player.itemTime = 10;
                player.itemAnimation = 10;
                player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            }
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            Color color = lightColor;
            spriteBatch.Draw(tex, drawPosition, rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return false;
        }
    }
}