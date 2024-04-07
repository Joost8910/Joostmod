using JoostMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BlackHole : ModProjectile
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Hole");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 360;
            Projectile.height = 360;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
		    Projectile.alpha = 255;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            if (player.channel)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    //projectile.position = Main.MouseWorld - projectile.Size / 2f;
                    Projectile.velocity = Projectile.DirectionTo(Main.MouseWorld) * (Projectile.Distance(Main.MouseWorld) < 100 ? Projectile.Distance(Main.MouseWorld) / 20f : 5f);
                    Projectile.netUpdate = true;
                }
            }
            else
            {
                Projectile.Kill();
            }
            if (Projectile.ai[0] == 0)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/BlackHoleStart").WithVolumeScale(0.7f), Projectile.Center);
            }
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 88 && (Projectile.ai[0] - 8) % (20 * 2) == 0)
            {
                SoundEngine.PlaySound(new SoundStyle("JoostMod/Sounds/Custom/BlackHoleLoop").WithVolumeScale(0.7f), Projectile.Center);
            }
            if (Projectile.ai[1] < 12)
            {
                Projectile.ai[1] += 0.125f;
            }
            Projectile.alpha = 255 - (int)(Projectile.ai[1] * 15);
            Projectile.direction = 1;
            Projectile.rotation += Projectile.ai[1] * Projectile.direction * 0.5f * 0.0174f;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.direction = (Projectile.Center.X < player.Center.X ? -1 : 1);
            player.itemTime = 2;
            player.itemAnimation = 2;
            return false;
        }
        public override void PostAI()
        {
            if (Projectile.ai[1] >= 12)
            {
                for (int i = 0; i < Main.item.Length; i++)
                {
                    if (Main.item[i].active)
                    {
                        Item I = Main.item[i];
                        if (Projectile.Distance(I.Center) <= 450)
                        {
                            Vector2 vel = I.DirectionTo(Projectile.Center) * Projectile.ai[1] * 0.3125f;
                            vel = vel.RotatedBy(90f * -Projectile.direction);
                            vel += I.DirectionTo(Projectile.Center) * 4f;
                            I.velocity = vel;
                            I.position += I.velocity + Projectile.velocity;
                        }
                    }
                }
                for (int n = 0; n < 200; n++)
                {
                    NPC target = Main.npc[n];
                    if (target.Distance(Projectile.Center) <= 450 + (target.width > target.height ? target.width : target.height))
                    {
                        if (target.active && target.type != 488)
                        {
                            if (Vector2.Distance(Projectile.Center, target.oldPosition + target.Size) < Vector2.Distance(Projectile.Center, target.Center))
                            {
                                target.position += target.DirectionTo(Projectile.Center) * Vector2.Distance(target.oldPosition, target.position);
                            }
                            
                            Vector2 vel = target.DirectionTo(Projectile.Center) * Projectile.ai[1] * 0.625f;
                            vel = vel.RotatedBy(90f * -Projectile.direction);
                            if (!target.noGravity)
                                vel.Y -= 0.3f;
                            target.velocity = vel;
                            target.position += vel;

                            float dist = 450 * ((float)target.life / (float)target.lifeMax);
                            if (target.Distance(Projectile.Center) > dist)
                            {
                                target.position += target.DirectionTo(Projectile.Center) * (target.Distance(Projectile.Center) - dist);
                            }

                            target.life = (int)(target.life * 0.998f) - (int)(target.lifeMax / 10000f) - (target.lifeMax > 200000 ? 200 : 0);
                            if (target.life < 1 || (target.Distance(Projectile.Center) < 50 && target.lifeMax < 75000))
                            {
                                target.life = 0;
                                target.checkDead();
                            }
                            
                            if (target.type == NPCID.WallofFlesh)
                            {
                                target.direction = (Main.player[Projectile.owner].Center.X < target.Center.X) ? -1 : 1;
                            }
                        }
                    }
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[Projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, (tex.Height / Main.projFrames[Projectile.type]) * Projectile.frame, tex.Width, tex.Height / Main.projFrames[Projectile.type]));
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Vector2 drawPosition = Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY);
            Main.EntitySpriteDraw(tex, drawPosition, rect, lightColor * 0.85f * ((255f - Projectile.alpha) / 255f), Projectile.rotation, drawOrigin, Projectile.scale * 2.5f, effects, 0);
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