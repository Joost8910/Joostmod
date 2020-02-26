using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class Swirlwind : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hurricane");
		}
        public override void SetDefaults()
        {
            projectile.width = 360;
            projectile.height = 360;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.minion = true;
		    projectile.alpha = 255;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 16;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            int mana = 10;
            if (player.inventory[player.selectedItem].shoot == projectile.type)
            {
                mana = player.inventory[player.selectedItem].mana / 2;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed && player.CheckMana(mana, projectile.ai[0] % 16 == 0);
            if (channeling)
            {
                if (projectile.ai[0] < 1)
                {
                    if (Main.myPlayer == projectile.owner)
                    {
                        Vector2 vector13 = Main.MouseWorld - vector;
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
                }
                projectile.ai[0]++;
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.ai[0] % (int)(20 - projectile.ai[1]) <= 0)
            {
                Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 1, 0.9f, -0.6f);
            }
            if (projectile.ai[1] < 12)
            {
                projectile.ai[1] += 0.25f;
            }
            projectile.alpha = 255 - (int)(projectile.ai[1] * 12);
            projectile.localNPCHitCooldown = 28 - (int)projectile.ai[1];
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.velocity.X = projectile.direction * 8;
            projectile.velocity.Y = 0;
            projectile.rotation += projectile.ai[1] * projectile.direction * 0.666f * 0.0174f;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            if (projectile.ai[1] >= 12)
            {
                for (int i = 0; i < Main.item.Length; i++)
                {
                    if (Main.item[i].active)
                    {
                        Item I = Main.item[i];
                        if (projectile.Hitbox.Intersects(I.Hitbox))
                        {
                            Vector2 vel = I.DirectionTo(projectile.Center) * projectile.ai[1] * 0.3125f;
                            vel = vel.RotatedBy(90f * -projectile.direction);
                            vel += I.DirectionTo(projectile.Center) * 2f;
                            I.velocity = vel;
                            I.position += I.velocity + player.velocity;
                        }
                    }
                }
                for (int n = 0; n < 200; n++)
                {
                    NPC target = Main.npc[n];
                    if (target.Distance(projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                    {
                        bool tooClose = player.Distance(target.Center) <= (target.width > target.height ? target.width : target.height) + 40;
                        if (target.active && !target.friendly && !target.dontTakeDamage && target.type != 488 && !target.boss && target.knockBackResist > 0)
                        {
                            Vector2 vel = target.DirectionTo(projectile.Center) * projectile.ai[1] * 0.625f;
                            vel = vel.RotatedBy(90f * -projectile.direction);
                            if (tooClose)
                            {
                                vel -= target.DirectionTo(projectile.Center) * 4f;
                            }
                            else
                            {
                                vel += target.DirectionTo(projectile.Center) * 4f;
                            }
                            if (!target.noGravity)
                                vel.Y -= 0.3f;
                            target.velocity = vel;
                        }
                    }
                }
            }
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = ModContent.GetTexture("JoostMod/Projectiles/WindWheel");
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPosition, rect, lightColor, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.Distance(projectile.Center) < 30)
                crit = true;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (target.Distance(projectile.Center) < 30)
                crit = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (target.Distance(projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                return base.CanHitNPC(target);
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (target.Distance(projectile.Center) <= 180 + (target.width > target.height ? target.width : target.height))
                return base.CanHitPvp(target);
            return false;
        }

    }
}