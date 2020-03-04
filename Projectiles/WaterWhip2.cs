using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class WaterWhip2 : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grasping Water Tendril");
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
            projectile.rotation = projectile.velocity.ToRotation();
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[1] == 0 && target.knockBackResist > 0 && target.type != NPCID.TargetDummy)
            {
                projectile.localAI[1] = target.whoAmI + 1;
            }
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            int max = 30;
            projectile.scale = 1f - (projectile.ai[0] / max);
            projectile.width = (int)(32 * projectile.scale);
            projectile.height = (int)(32 * projectile.scale);
            bool channeling = player.controlUseTile && !player.noItems && !player.CCed;
            int mana = 5;
            if (player.inventory[player.selectedItem].type == mod.ItemType("WaterWhip"))
            {
                mana = player.inventory[player.selectedItem].mana / 2;
            }
            if (channeling && player.CheckMana(mana))
            {
                if (projectile.ai[1] == 0 && projectile.localAI[0] % 40 == 0)
                {
                    player.CheckMana(mana, true);
                }
                projectile.localAI[0]++;
                if (player.ownedProjectileCounts[projectile.type] < max - 1 && projectile.localAI[0] == 4)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0] + 1, projectile.whoAmI);
                    player.ownedProjectileCounts[projectile.type]++;
                }
                if (projectile.ai[1] != 0)
                {
                    vector = Main.projectile[(int)projectile.ai[1]].Center;
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
                    if (projectile.ai[1] == 0)
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
                            float home = 20f;
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
            for (int i = 0; i < Main.item.Length; i++)
            {
                if (Main.item[i].active)
                {
                    Item I = Main.item[i];
                    if (projectile.Hitbox.Intersects(I.Hitbox))
                    {
                        Vector2 vel = I.DirectionTo(player.Center) * 2;
                        I.velocity = vel;
                        I.position += I.velocity;
                    }
                }
            }
            if (projectile.localAI[1] > 0)
            {
                NPC target = Main.npc[(int)projectile.localAI[1] - 1];
                bool tooClose = player.Distance(target.Center) <= (target.width > target.height ? target.width / 2 : target.height / 2) + 30;
                if (target.active && target.life > 0 && target.knockBackResist > 0 && !target.dontTakeDamage && !target.friendly && target.type != NPCID.TargetDummy)
                {
                    if (tooClose)
                    {
                        projectile.localAI[1] = 0;
                        target.velocity = player.DirectionTo(target.Center) * projectile.knockBack;
                    }
                    else
                    {
                        target.position = projectile.Center - target.Size / 2;
                    }
                }
                else
                {
                    projectile.localAI[1] = 0;
                }
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
            if (projectile.localAI[0] % 5 == 0)
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 33).noGravity = true;
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