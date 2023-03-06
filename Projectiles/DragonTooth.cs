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
	public class DragonTooth : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Tooth");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
		public override void SetDefaults()
		{
			Projectile.width = 110;
			Projectile.height = 110;
			Projectile.aiStyle = 0;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 124;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
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
            Projectile.velocity.Y = 0;
            Projectile.direction = player.direction * (int)player.gravDir;
            Projectile.velocity.X = Projectile.direction;
            float speed = player.GetAttackSpeed(DamageClass.Melee) / 2;
            if (player.inventory[player.selectedItem].shoot == Projectile.type)
            {
                Projectile.scale = player.inventory[player.selectedItem].scale;
                speed = ((45f / player.inventory[player.selectedItem].useTime) / player.GetAttackSpeed(DamageClass.Melee)) / 2;
                Projectile.width = (int)(110 * Projectile.scale);
                Projectile.height = (int)(110 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
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
            if (Projectile.ai[0] < 30)
            {
                Projectile.ai[1] -= speed;
                player.velocity.X *= 0.98f;
                Projectile.localAI[0] += 0.02f * speed;
            }
            if (Projectile.ai[0] > 30 && Projectile.soundDelay >= 0 && channeling)
            {
                Projectile.soundDelay = -60;
                SoundEngine.PlaySound(SoundID.Item39, Projectile.Center);
            }
            if (channeling && Projectile.ai[0] > 30)
            {
                player.velocity.X *= 0.98f;
                Projectile.ai[0] = 30;
                if (player.velocity.Y * player.gravDir > player.gravity)
                {
                    Projectile.localAI[1] = 1;
                }
                else
                {
                    Projectile.localAI[1] = 0;
                }
            }
            if (!channeling && Projectile.ai[0] < 30)
            {
                Projectile.ai[0] = 30;
            }
            if (Projectile.ai[0] <= 32)
            {
                Projectile.ai[0] += speed;
                Projectile.timeLeft = 122;
            }
            else if (Projectile.soundDelay != -10)
            {
                Projectile.soundDelay = -10;
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
            }
            if (Projectile.timeLeft <= 120)
            {
                if (Projectile.localAI[1] > 0)
                {
                    player.mount.Dismount(player);
                    if (Projectile.ai[1] > 150 && Projectile.localAI[1] < 10)
                    {
                        if (player.velocity.Y == 0)
                        {
                            if (Projectile.ai[0] < 100)
                            {
                                Projectile.localAI[1] = 10;
                                SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                                int damage = Projectile.damage;
                                float knockback = Projectile.knockBack;
                                for (int i = 1; i <= 5 * Projectile.scale; i++)
                                {
                                    Projectile.NewProjectile(Projectile.Center.X + i * 16, Projectile.Center.Y - 60 * player.gravDir, 0.01f * i, 15 * player.gravDir, Mod.Find<ModProjectile>("DragonToothWave").Type, damage, knockback, Projectile.owner);
                                    Projectile.NewProjectile(Projectile.Center.X - i * 16, Projectile.Center.Y - 60 * player.gravDir, -0.01f * i, 15 * player.gravDir, Mod.Find<ModProjectile>("DragonToothWave").Type, damage, knockback, Projectile.owner);
                                }
                                Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y - 60 * player.gravDir, 0, 15 * player.gravDir, Mod.Find<ModProjectile>("DragonToothWave").Type, damage, knockback, Projectile.owner);
                            }
                        }
                        else
                        {
                            Projectile.timeLeft = 110;
                            Projectile.ai[1] = 150;
                        }
                    }
                    else if (Projectile.ai[1] < 160)
                    {
                        Projectile.timeLeft = 110;
                        Projectile.ai[1] += 10 * speed * (Projectile.localAI[0] + 1);
                    }
                    player.fullRotation = (Projectile.ai[1] * 0.00174f * player.direction);
                    player.fullRotationOrigin = player.Center - player.position;
                    if (Projectile.localAI[1] >= 10)
                    {
                        player.velocity.X = 0;
                        player.velocity.Y = 10f * player.gravDir;
                        player.controlJump = false;
                        player.controlLeft = false;
                        player.controlRight = false;
                    }
                }
                else
                {
                    if (Projectile.ai[1] < 180)
                    {
                        Projectile.timeLeft = 70;
                        Projectile.ai[1] += 10 * speed * (Projectile.localAI[0] + 1);
                    }
                }
                if (Projectile.timeLeft == 68 && Projectile.ai[0] < 100)
                {
                    Projectile.ai[0] = 100;
                    Projectile.timeLeft = (int)(68 / (speed * 2));
                    if (player.velocity.Y == 0 && Projectile.localAI[1] == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Trackable, Projectile.Center);
                    }
                }
                if (Projectile.timeLeft <= 1)
                {
                    player.fullRotation = 0;
                }
                if (Projectile.ai[1] > 180)
                {
                    Projectile.ai[1] = 180;
                }
            }
            player.heldProj = Projectile.whoAmI;
            if (Projectile.ai[1] < 180)
            {
                player.itemTime = (int)((45f / (speed * 2)) - ((Projectile.ai[1] / 15f) * 2 / speed));
                player.itemAnimation = (int)((45f / (speed * 2)) - ((Projectile.ai[1] / 15f) * 2 / speed));
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
            /*
            float rot = projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
            Vector2 unit = rot.ToRotationVector2();
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            for (int i = 0; i < 7; i++)
            {
                Dust.NewDustPerfect(vector + unit * i * 32, 20);
            }*/
        }
        public override bool? CanCutTiles()
        {
            return (Projectile.ai[0] > 32 && Projectile.ai[0] < 100);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (Projectile.ai[0] > 32 && Projectile.ai[0] < 100)
            {
                Player player = Main.player[Projectile.owner];
                float rot = Projectile.rotation - 1.57f + (0.785f * player.direction * (int)player.gravDir);
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 140 * Projectile.scale, 40 * Projectile.scale, ref point))
                {
                    return true;
                }
                if (player.velocity.Y * player.gravDir > 3 && Projectile.localAI[1] > 0 && Collision.CheckAABBvAABBCollision(player.Center - new Vector2((player.width / 2) + 2, 0), new Vector2(player.width + 4, ((player.height / 2) + 4) * player.gravDir), targetHitbox.TopLeft(), targetHitbox.Size()))
                {
                    if (player.immuneTime < 6)
                    {
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = 6;
                    }
                    return true;
                }
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = (int)(damage * (Projectile.localAI[0] + 1));
            knockback = knockback * (Projectile.localAI[0] + 1);
            if (target.velocity.Y == 0)
                hitDirection = target.Center.X < Main.player[Projectile.owner].Center.X ? -1 : 1;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * (Projectile.localAI[0] + 1));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (target.knockBackResist > 0)
            {
                if (Projectile.localAI[1] > 0 && Projectile.ai[1] >= 150)
                {
                    target.velocity.Y = (knockback + Math.Abs(player.velocity.Y)) * player.gravDir * target.knockBackResist;
                }
                else if (Projectile.ai[1] < 90 && player.velocity.Y == 0 && target.velocity.Y != 0 && target.velocity.Y > -knockback)
                {
                    target.velocity.Y = -knockback;
                }
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                if (Projectile.localAI[1] > 0 && Projectile.ai[1] >= 150)
                {
                    target.velocity.Y = (Projectile.knockBack + Math.Abs(player.velocity.Y)) * player.gravDir;
                }
                else if (Projectile.ai[1] < 90 && player.velocity.Y == 0 && target.velocity.Y != 0 && target.velocity.Y > -Projectile.knockBack)
                {
                    target.velocity.Y = -Projectile.knockBack;
                }
            }
        }
        public override bool CanHitPvp(Player target)
        {
            if (Projectile.ai[0] > 32 && Projectile.ai[0] < 100)
            {
                return base.CanHitPvp(target);
            }
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (Projectile.ai[0] > 32 && Projectile.ai[0] < 100)
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
