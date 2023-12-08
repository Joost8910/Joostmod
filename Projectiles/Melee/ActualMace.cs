using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Melee
{
    public class ActualMace : ModProjectile
    {
        public override string Texture => "JoostMod/Items/Weapons/ActualMace";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Actual Mace");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 46;
            Projectile.height = 46;
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
        float maxCharge = 0.45f;
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
                speed = 45f / player.inventory[player.selectedItem].useTime / player.GetAttackSpeed(DamageClass.Melee) / 2;
                Projectile.width = (int)(46 * Projectile.scale);
                Projectile.height = (int)(46 * Projectile.scale);
                Projectile.netUpdate = true;
            }
            bool channeling = player.channel && !player.noItems && !player.CCed && !player.dead;
            if (channeling && Main.myPlayer == Projectile.owner)
            {
                Vector2 vector13 = Main.MouseWorld - center;
                vector13.Normalize();
                if (vector13.HasNaNs())
                {
                    vector13 = Vector2.UnitX * player.direction;
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
            double rad = player.fullRotation - 1.83f + (Projectile.ai[1] - 15) * 0.0174f * Projectile.direction;
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
            double dist = (-20 * Projectile.scale - 10) * Projectile.direction;
            Projectile.position.X = center.X + -2 * player.direction * Projectile.scale - (int)(Math.Cos(rad - 0.785f) * dist) - Projectile.width / 2;
            Projectile.position.Y = center.Y + (Projectile.ai[1] / 30 - 3) * Projectile.scale - (int)(Math.Sin(rad - 0.785f) * dist) - Projectile.height / 2;
            if (Projectile.ai[1] < 0)
            {
                Projectile.position.Y += player.gravDir * (Projectile.ai[1] / Projectile.scale * 0.15f - 4);
            }
            if (Projectile.ai[0] < 30)
            {
                Projectile.ai[1] -= speed;
                Projectile.localAI[0] += maxCharge / 30 * speed;
            }
            if (Projectile.ai[0] > 30 && Projectile.soundDelay >= 0 && channeling)
            {
                Projectile.soundDelay = -60;
                SoundEngine.PlaySound(SoundID.Item39, Projectile.Center);
            }
            if (channeling && Projectile.ai[0] > 30)
            {
                Projectile.ai[0] = 30;
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
                SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_swing_1").WithVolumeScale(0.9f).WithPitchOffset(0.1f), Projectile.Center); // 214
            }
            if (Projectile.timeLeft <= 120)
            {
                if (Projectile.ai[1] < 180)
                {
                    Projectile.timeLeft = 70;
                    Projectile.ai[1] += 10 * speed * (Projectile.localAI[0] + 1);
                }
                if (Projectile.timeLeft == 68 && Projectile.ai[0] < 100)
                {
                    Projectile.ai[0] = 100;
                    Projectile.timeLeft = (int)(68 / (speed * 2));
                    if (player.velocity.Y == 0)
                    {
                        SoundEngine.PlaySound(new SoundStyle("Terraria/Sounds/Custom/dd2_monk_staff_ground_miss_0").WithVolumeScale(0.8f).WithPitchOffset(0.1f), Projectile.Center); // 210
                    }
                }
                if (Projectile.ai[1] > 180)
                {
                    Projectile.ai[1] = 180;
                }
            }
            player.heldProj = Projectile.whoAmI;
            if (Projectile.ai[1] < 180)
            {
                player.itemTime = (int)(45f / (speed * 2) - Projectile.ai[1] / 15f * 2 / speed);
                player.itemAnimation = (int)(45f / (speed * 2) - Projectile.ai[1] / 15f * 2 / speed);
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
            return Projectile.ai[0] > 32 && Projectile.ai[0] < 100;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[0] > 32 && (Projectile.ai[0] < 100 || player.velocity.Y * player.gravDir > 3))
            {
                float rot = Projectile.rotation - 1.57f + 0.785f * player.direction * (int)player.gravDir;
                Vector2 unit = rot.ToRotationVector2();
                Vector2 vector = player.RotatedRelativePoint(player.position + new Vector2(player.width / 2, 20), true);
                float point = 0f;
                if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 62 * Projectile.scale, 14 * Projectile.scale, ref point))
                {
                    return true;
                }
                if (player.velocity.Y * player.gravDir > 3 && Projectile.ai[1] > 120 && Collision.CheckAABBvAABBCollision(player.Center - new Vector2(player.width / 2 + 2, 0), new Vector2(player.width + 4, (player.height / 2 + 4) * player.gravDir), targetHitbox.TopLeft(), targetHitbox.Size()))
                {
                    player.velocity.Y = -4 * player.gravDir;
                    if (player.immuneTime < 4)
                    {
                        player.immune = true;
                        player.immuneNoBlink = true;
                        player.immuneTime = 4;
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
            {
                hitDirection = target.Center.X < Main.player[Projectile.owner].Center.X ? -1 : 1;
            }
            Player player = Main.player[Projectile.owner];
            if (target.knockBackResist > 0)
            {
                if (Projectile.ai[0] >= 100 && Projectile.ai[1] > 120)
                {
                    if (target.velocity.Y < 0 && Projectile.localAI[0] > maxCharge * 0.9f)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, target.velocity, Mod.Find<ModProjectile>("GrabThrow").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, target.whoAmI);
                    }
                    target.velocity.Y = (knockback + Math.Abs(player.velocity.Y)) * player.gravDir * target.knockBackResist;
                }
                else if (Projectile.ai[1] < 90 && player.velocity.Y == 0 && target.velocity.Y != 0 && target.velocity.Y > -knockback)
                {
                    target.velocity.Y = -knockback * target.knockBackResist;
                }
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage = (int)(damage * (Projectile.localAI[0] + 1));
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                if (player.velocity.Y * player.gravDir > 3 && Projectile.ai[1] > 120)
                {
                    if (target.velocity.Y < 0 && Projectile.localAI[0] > maxCharge * 0.9f)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_OnHit(target), target.Center, target.velocity, Mod.Find<ModProjectile>("GrabThrow").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, -1, target.whoAmI);
                    }
                    target.velocity.Y = (Projectile.knockBack + Math.Abs(player.velocity.Y)) * player.gravDir;
                }
                else if (Projectile.ai[1] < 90 && player.velocity.Y == 0 && target.velocity.Y != 0 && target.velocity.Y > -Projectile.knockBack)
                {
                    target.velocity.Y = -Projectile.knockBack;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Projectile.localAI[0] > maxCharge * 0.9f)
            {
                target.AddBuff(BuffID.Ichor, 600);
            }
            //target.velocity = player.velocity + knockback * (projectile.rotation + (90 - 45 * projectile.direction) * 0.0174f).ToRotationVector2() * target.knockBackResist;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (Projectile.localAI[0] > maxCharge * 0.9f)
            {
                target.AddBuff(BuffID.Ichor, 600);
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            SpriteEffects effects = SpriteEffects.None;
            if (Projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = lightColor;
            Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);
            Rectangle? rect = new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height));
            Player player = Main.player[Projectile.owner];
            if (Projectile.ai[1] > 0 && (Projectile.ai[0] < 100 || player.velocity.Y * player.gravDir > 4))
            {
                for (int k = 1; k < Projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + new Vector2(Projectile.width / 2, Projectile.height / 2);
                    Color color2 = color * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                    Main.EntitySpriteDraw(tex, drawPos, rect, color2, Projectile.oldRot[k], drawOrigin, Projectile.scale, effects, 0);
                }
            }
            //color.A = (byte)projectile.alpha;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition, rect, color, Projectile.rotation, drawOrigin, Projectile.scale, effects, 0);
            return false;
        }
    }
}
