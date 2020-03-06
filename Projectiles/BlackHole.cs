using JoostMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BlackHole : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Hole");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 360;
            projectile.height = 360;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
		    projectile.alpha = 255;
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            if (player.channel)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    //projectile.position = Main.MouseWorld - projectile.Size / 2f;
                    projectile.velocity = projectile.DirectionTo(Main.MouseWorld) * (projectile.Distance(Main.MouseWorld) < 100 ? projectile.Distance(Main.MouseWorld) / 20f : 5f);
                    projectile.netUpdate = true;
                }
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.ai[0] == 0)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BlackHoleStart"), 0.7f);
            }
            projectile.ai[0]++;
            if (projectile.ai[0] % (48 * 2) == 0)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int)projectile.Center.X, (int)projectile.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/BlackHoleLoop"), 0.7f);
            }
            if (projectile.ai[1] < 12)
            {
                projectile.ai[1] += 0.125f;
            }
            projectile.alpha = 255 - (int)(projectile.ai[1] * 15);
            projectile.direction = 1;
            projectile.rotation += projectile.ai[1] * projectile.direction * 0.5f * 0.0174f;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.direction = (projectile.Center.X < player.Center.X ? -1 : 1);
            player.itemTime = 2;
            player.itemAnimation = 2;
            return false;
        }
        public override void PostAI()
        {
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
                            vel += I.DirectionTo(projectile.Center) * 4f;
                            I.velocity = vel;
                            I.position += I.velocity + projectile.velocity;
                        }
                    }
                }
                for (int n = 0; n < 200; n++)
                {
                    NPC target = Main.npc[n];
                    if (target.Distance(projectile.Center) <= 450 + (target.width > target.height ? target.width : target.height))
                    {
                        if (target.active && target.type != 488)
                        {
                            if (Vector2.Distance(projectile.Center, target.oldPosition + target.Size) < Vector2.Distance(projectile.Center, target.Center))
                            {
                                target.position += target.DirectionTo(projectile.Center) * Vector2.Distance(target.oldPosition, target.position);
                            }
                            
                            Vector2 vel = target.DirectionTo(projectile.Center) * projectile.ai[1] * 0.625f;
                            vel = vel.RotatedBy(90f * -projectile.direction);
                            if (!target.noGravity)
                                vel.Y -= 0.3f;
                            target.velocity = vel;
                            target.position += vel;

                            float dist = 450 * ((float)target.life / (float)target.lifeMax);
                            if (target.Distance(projectile.Center) > dist)
                            {
                                target.position += target.DirectionTo(projectile.Center) * (target.Distance(projectile.Center) - dist);
                            }

                            target.life = (int)(target.life * 0.998f) - (int)(target.lifeMax / 10000f) - (target.lifeMax > 200000 ? 200 : 0);
                            if (target.life < 1 || (target.Distance(projectile.Center) < 50 && target.lifeMax < 75000))
                            {
                                target.life = 0;
                                target.checkDead();
                            }
                            
                            if (target.type == NPCID.WallofFlesh)
                            {
                                target.direction = (Main.player[projectile.owner].Center.X < target.Center.X) ? -1 : 1;
                            }
                        }
                    }
                }
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[projectile.type]) * projectile.frame, tex.Width, tex.Height / Main.projFrames[projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawPosition = projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY);
            spriteBatch.Draw(tex, drawPosition, rect, lightColor * 0.85f * ((255f - projectile.alpha) / 255f), projectile.rotation, drawOrigin, projectile.scale * 2.5f, effects, 0f);
            //spriteBatch.Draw(tex, drawPosition, rect, lightColor * ((255f - projectile.alpha) / 255f), projectile.rotation, drawOrigin, projectile.scale, effects, 0f);
            return true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }

    }
}