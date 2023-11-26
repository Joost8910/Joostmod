using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class BoookBulletHell : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boook's Bullet Hell");
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 122;
            Projectile.height = 48;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 16;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Main.myPlayer == Projectile.owner && Main.mouseLeft)
            {
                return base.CanHitNPC(target);
            }
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            if (Main.myPlayer == Projectile.owner && Main.mouseLeft)
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
            Vector2 offSet = new Vector2(46, -6 * Projectile.direction);
            offSet = offSet.RotatedBy(Projectile.rotation + (Projectile.direction == -1 ? 3.14f : 0));
            hitbox.Width = 46;
            hitbox.Height = 46;
            hitbox.X = (int)(Projectile.Center.X + offSet.X - (hitbox.Width / 2));
            hitbox.Y = (int)(Projectile.Center.Y + offSet.Y - (hitbox.Height / 2));
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool channeling = (player.controlUseItem || player.controlUseTile) && player.inventory[player.selectedItem].shoot == Projectile.type && !player.dead && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Vector2 vector13 = Main.MouseWorld - vector;
                    vector13.Normalize();
                    if (vector13.HasNaNs())
                    {
                        vector13 = Vector2.UnitX * (float)player.direction;
                    }
                    if (vector13.X != Projectile.velocity.X || vector13.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = vector13;
                }
            }
            else
            {
                Projectile.Kill();
            }

            Projectile.velocity.Normalize();
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.position = (vector - Projectile.Size / 2f);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            
            if (Main.myPlayer == Projectile.owner && Main.mouseRight && Main.mouseRightRelease)
            {
                Projectile.ai[1] = (Projectile.ai[1] + 1) % 3;
                SoundEngine.PlaySound(SoundID.Item23.WithPitchOffset(Projectile.ai[1] * 0.2f), Projectile.Center);
            }

            if (Main.myPlayer == Projectile.owner && Main.mouseLeft)
            {
                int rate = Math.Max(8 - ((int)Projectile.localAI[0] / 20), 2);
                Projectile.localNPCHitCooldown = rate * 2;
                if (Projectile.localAI[0] < 40 + (Projectile.ai[1] * 40))
                {
                    Projectile.localAI[0] += 0.7f - (Projectile.ai[1] * 0.25f);
                }
                else if (Projectile.localAI[0] >= 41 + (Projectile.ai[1] * 40))
                {
                    Projectile.localAI[0]--;
                }
                Projectile.localAI[1] = (Projectile.localAI[1] + 45f / rate) % 360;

                Projectile.ai[0]--;
                if (Projectile.ai[0] <= 0)
                {
                    Projectile.ai[0] = rate;
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
                        Vector2 offSet = new Vector2(46, -6 * Projectile.direction);
                        offSet = offSet.RotatedBy(Projectile.rotation + (Projectile.direction == -1 ? 3.14f : 0));

                        shootSpeed += item.shootSpeed;
                        Vector2 shootDir = Projectile.velocity * shootSpeed;
                        float spread = Math.Max(1, Projectile.localAI[0] - 40) * 0.4f + Math.Max(1, Projectile.localAI[0] - 80) * 1.2f;
                        shootDir = shootDir.RotatedByRandom(MathHelper.ToRadians(spread));
                        type = item.shoot;
                        if (Projectile.localAI[0] > 1 && item.consumable && ItemLoader.ConsumeAmmo(player.HeldItem, item, player))
                        {
                            player.ConsumeItem(item.type);
                        }

                        int damage = (int)((Projectile.damage + item.damage) * player.bulletDamage);
                        if (ProjectileID.Sets.CultistIsResistantTo[type])
                        {
                            damage = (int)(damage * 0.6f);
                        }
                        float knockback = Projectile.knockBack + item.knockback;
                        Projectile.NewProjectile(Projectile.Center + offSet, shootDir, type, damage, knockback, Projectile.owner);
                        SoundEngine.PlaySound(SoundID.Item41, Projectile.Center);
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundLoader.customSoundType, Projectile.Center, Mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/MissileClick"));
                    }
                }
            }

            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.frame = (int)(Projectile.localAI[1] / 15) % 6;

            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.direction == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / Main.projFrames[Projectile.type]) * 0.5f);
            Rectangle? rect = new Rectangle(0, Projectile.frame * (tex.Height / Main.projFrames[Projectile.type]), tex.Width, (tex.Height / Main.projFrames[Projectile.type]));
  
            Texture2D barrelTex = Mod.Assets.Request<Texture2D>("Projectiles/BoookBulletHell_Barrel").Value;
            Vector2 offSet = new Vector2(46, -6 * Projectile.direction);
            offSet = offSet.RotatedBy(Projectile.rotation + (Projectile.direction == -1 ? 3.14f : 0));
            float rot = Projectile.rotation + MathHelper.ToRadians(Projectile.localAI[1]) * Projectile.direction;
            Main.EntitySpriteDraw(barrelTex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY) + offSet, new Rectangle?(new Rectangle(0, 0, barrelTex.Width, barrelTex.Height)), color, rot, new Vector2(barrelTex.Width / 2, barrelTex.Height / 2), Projectile.scale, effects, 0);

            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);

            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                SpriteEffects effects = SpriteEffects.None;
                Texture2D tex = Mod.Assets.Request<Texture2D>("Projectiles/BoookBulletHell_Gear").Value;
                Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, (tex.Height / 3) * 0.5f);
                Rectangle? rect = new Rectangle(0, (tex.Height / 3) * (int)Projectile.ai[1], tex.Width, tex.Height / 3);
                Vector2 drawPos = new Vector2(Main.player[Projectile.owner].Center.X - 20, Main.player[Projectile.owner].position.Y - 18);
                float scale = 1f;
                Color color = Color.White;
                Main.EntitySpriteDraw(tex, drawPos - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, color, 0, drawOrigin, scale, effects, 0);

                tex = Mod.Assets.Request<Texture2D>("Projectiles/BoookBulletHell_Speedometer").Value;
                drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
                rect = new Rectangle(0, 0, tex.Width, tex.Height);
                drawPos = new Vector2(Main.player[Projectile.owner].Center.X + 20, Main.player[Projectile.owner].position.Y - 18);
                Main.EntitySpriteDraw(tex, drawPos - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, color, 0, drawOrigin, scale, effects, 0);

                tex = Mod.Assets.Request<Texture2D>("Projectiles/BoookBulletHell_Speedometer2").Value;
                float rot = MathHelper.ToRadians(Projectile.localAI[0] * 2.25f);
                Main.EntitySpriteDraw(tex, drawPos - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rect, color, rot, drawOrigin, scale, effects, 0);

            }
        }
    }
}