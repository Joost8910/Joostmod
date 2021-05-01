using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BoookBulletHell : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boook's Bullet Hell");
            Main.projFrames[projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            projectile.width = 122;
            projectile.height = 48;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ranged = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 16;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Main.myPlayer == projectile.owner && Main.mouseLeft)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Main.myPlayer == projectile.owner && Main.mouseLeft)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage /= 2;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage /= 2;
        }
        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            Vector2 offSet = new Vector2(46, -6 * projectile.direction);
            offSet = offSet.RotatedBy(projectile.rotation + (projectile.direction == -1 ? 3.14f : 0));
            hitbox.Width = 46;
            hitbox.Height = 46;
            hitbox.X = (int)(projectile.Center.X + offSet.X - (hitbox.Width / 2));
            hitbox.Y = (int)(projectile.Center.Y + offSet.Y - (hitbox.Height / 2));
        }
        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = (player.controlUseItem || player.controlUseTile) && player.inventory[player.selectedItem].shoot == projectile.type && !player.dead && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == projectile.owner)
                {
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y)
                    {
                        projectile.netUpdate = true;
                    }
                    projectile.velocity = vector13;
                }
            }
            else
            {
                projectile.Kill();
            }

            projectile.velocity.Normalize();
            projectile.rotation = projectile.velocity.ToRotation() + (projectile.direction == -1 ? 3.14f : 0);
            projectile.position = (vector - projectile.Size / 2f);
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            
            if (Main.myPlayer == projectile.owner && Main.mouseRight && Main.mouseRightRelease)
            {
                projectile.ai[1] = (projectile.ai[1] + 1) % 3;
                Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 23, 1, projectile.ai[1] * 0.2f);
            }

            if (Main.myPlayer == projectile.owner && Main.mouseLeft)
            {
                int rate = Math.Max(8 - ((int)projectile.localAI[0] / 20), 2);
                projectile.localNPCHitCooldown = rate * 2;
                if (projectile.localAI[0] < 40 + (projectile.ai[1] * 40))
                {
                    projectile.localAI[0] += 0.7f - (projectile.ai[1] * 0.25f);
                }
                else if (projectile.localAI[0] >= 41 + (projectile.ai[1] * 40))
                {
                    projectile.localAI[0]--;
                }
                projectile.localAI[1] = (projectile.localAI[1] + 45f / rate) % 360;

                projectile.ai[0]--;
                if (projectile.ai[0] <= 0)
                {
                    projectile.ai[0] = rate;
                    float shootSpeed = player.inventory[player.selectedItem].shootSpeed;
                    int type = 0;
                    Item item = new Item();
                    bool canShoot = false;
                    bool flag = false;
                    for (int i = 54; i < 58; i++)
                    {
                        if (player.inventory[i].ammo == AmmoID.Bullet && player.inventory[i].stack > 0)
                        {
                            item = player.inventory[i];
                            canShoot = true;
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                    {
                        for (int j = 0; j < 54; j++)
                        {
                            if (player.inventory[j].ammo == AmmoID.Bullet && player.inventory[j].stack > 0)
                            {
                                item = player.inventory[j];
                                canShoot = true;
                                break;
                            }
                        }
                    }
                    if (canShoot)
                    {
                        Vector2 offSet = new Vector2(46, -6 * projectile.direction);
                        offSet = offSet.RotatedBy(projectile.rotation + (projectile.direction == -1 ? 3.14f : 0));

                        shootSpeed += item.shootSpeed;
                        Vector2 shootDir = projectile.velocity * shootSpeed;
                        float spread = Math.Max(1, projectile.localAI[0] - 40) * 0.4f + Math.Max(1, projectile.localAI[0] - 80) * 1.2f;
                        shootDir = shootDir.RotatedByRandom(MathHelper.ToRadians(spread));
                        type = item.shoot;
                        if (projectile.localAI[0] > 1 && item.consumable && ItemLoader.ConsumeAmmo(player.HeldItem, item, player))
                        {
                            player.ConsumeItem(item.type);
                        }

                        int damage = (int)((projectile.damage + item.damage) * player.bulletDamage);
                        if (ProjectileID.Sets.Homing[type])
                        {
                            damage = (int)(damage * 0.6f);
                        }
                        float knockBack = projectile.knockBack + item.knockBack;
                        Projectile.NewProjectile(projectile.Center + offSet, shootDir, type, damage, knockBack, projectile.owner);
                        Main.PlaySound(2, projectile.Center, 41);
                    }
                    else
                    {
                        Main.PlaySound(SoundLoader.customSoundType, projectile.Center, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileClick"));
                    }
                }
            }

            projectile.direction = projectile.velocity.X < 0 ? -1 : 1;
            projectile.frame = (int)(projectile.localAI[1] / 15) % 6;

            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle(0, projectile.frame * (tex.Height / Main.projFrames[projectile.type]), tex.Width, (tex.Height / Main.projFrames[projectile.type]));
  
            Texture2D barrelTex = mod.GetTexture("Projectiles/BoookBulletHell_Barrel");
            Vector2 offSet = new Vector2(46, -6 * projectile.direction);
            offSet = offSet.RotatedBy(projectile.rotation + (projectile.direction == -1 ? 3.14f : 0));
            float rot = projectile.rotation + MathHelper.ToRadians(projectile.localAI[1]) * projectile.direction;
            spriteBatch.Draw(barrelTex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY) + offSet, new Rectangle?(new Rectangle(0, 0, barrelTex.Width, barrelTex.Height)), color, rot, new Vector2(barrelTex.Width / 2, barrelTex.Height / 2), projectile.scale, effects, 0f);

            spriteBatch.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rect, color, projectile.rotation, drawOrigin, projectile.scale, effects, 0f);

            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (Main.myPlayer == projectile.owner)
            {
                SpriteEffects effects = SpriteEffects.None;
                Texture2D tex = mod.GetTexture("Projectiles/BoookBulletHell_Gear");
                Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / 3) * 0.5f);
                Rectangle? rect = new Rectangle(0, (tex.Height / 3) * (int)projectile.ai[1], tex.Width, tex.Height / 3);
                Vector2 drawPos = new Vector2(Main.player[projectile.owner].Center.X - 20, Main.player[projectile.owner].position.Y - 18);
                float scale = 1f;
                Color color = Color.White;
                spriteBatch.Draw(tex, drawPos - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rect, color, 0, drawOrigin, scale, effects, 0f);

                tex = mod.GetTexture("Projectiles/BoookBulletHell_Speedometer");
                drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
                rect = new Rectangle(0, 0, tex.Width, tex.Height);
                drawPos = new Vector2(Main.player[projectile.owner].Center.X + 20, Main.player[projectile.owner].position.Y - 18);
                spriteBatch.Draw(tex, drawPos - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rect, color, 0, drawOrigin, scale, effects, 0f);

                tex = mod.GetTexture("Projectiles/BoookBulletHell_Speedometer2");
                float rot = MathHelper.ToRadians(projectile.localAI[0] * 2.25f);
                spriteBatch.Draw(tex, drawPos - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rect, color, rot, drawOrigin, scale, effects, 0f);

            }
        }
    }
}