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
    public class BrokenHammer : ModProjectile
    {
        int startup = 15;
        int active = 10;
        int endlag = 15;
        float dmgMult = 1.3f;
        //float projMult = 1.2f;
        //float shockwaveMult = 3.5f;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Warhammer of Grognak");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 17;
            Projectile.ownerHitCheck = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            Player player = Main.player[Projectile.owner];
            return !target.friendly && (Projectile.ai[0] == 1 && Projectile.ai[1] >= startup && (Projectile.ai[1] < startup + active || Projectile.localAI[1] == 1) || Projectile.ai[0] == 2 && Projectile.ai[1] < 226 || player.velocity.Y * player.gravDir > 9);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (target.knockBackResist > 0 && player.velocity.Y * player.gravDir > 1)
            {
                target.velocity.Y = (hit.Knockback + Math.Abs(player.velocity.Y)) * player.gravDir * target.knockBackResist;
            }
        }
        public override bool CanHitPvp(Player target)
        {
            Player player = Main.player[Projectile.owner];
            return Projectile.ai[0] == 1 && Projectile.ai[1] >= startup && (Projectile.ai[1] < startup + active || Projectile.localAI[1] == 1) || Projectile.ai[0] == 2 && Projectile.ai[1] < 226 || player.velocity.Y * player.gravDir > 9;
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)/* tModPorter Note: Removed. Use OnHitPlayer and check info.PvP */
        {
            Player player = Main.player[Projectile.owner];
            if (!target.noKnockback)
            {
                target.velocity.Y = (Projectile.knockBack + Math.Abs(player.velocity.Y)) * player.gravDir;
            }
        }
        private Rectangle HeadBox()
        {
            Vector2 origin = Main.player[Projectile.owner].MountedCenter;
            int length = 70;
            double rotate = Projectile.rotation + 1.566 + 2.349 * Projectile.direction;
            Vector2 vect = new Vector2((float)Math.Cos(rotate) * length * Projectile.scale, (float)Math.Sin(rotate) * length * Projectile.scale);
            Rectangle r = new Rectangle(0, 0, 34, 34);
            Vector2 pos = origin + vect - r.Size() / 2;
            r.X = (int)pos.X;
            r.Y = (int)pos.Y;
            return r;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];
            float rot = Projectile.rotation + 1.566f + 2.349f * Projectile.direction;
            Vector2 unit = rot.ToRotationVector2();
            Vector2 vector = player.MountedCenter;
            float point = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), vector, vector + unit * 90 * Projectile.scale, 30 * Projectile.scale, ref point))
            {
                return true;
            }
            return false;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Rectangle head = HeadBox();
            if (target.Hitbox.Intersects(head))
            {
                modifiers.SourceDamage *= dmgMult;
                //Dust.NewDustDirect(head.TopLeft(), head.Width, head.Height, DustID.TreasureSparkle, Projectile.direction * -2, -4);
                Dust.NewDustDirect(head.TopLeft(), head.Width, head.Height, DustID.TreasureSparkle, Projectile.direction, -5);
                Dust.NewDustDirect(head.TopLeft(), head.Width, head.Height, DustID.TreasureSparkle, Projectile.direction * 2, -4);
            }
        }
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)/* tModPorter Note: Removed. Use ModifyHitPlayer and check modifiers.PvP */
        {
            Rectangle head = HeadBox();
            if (target.Hitbox.Intersects(head))
            {
                modifiers.SourceDamage *= dmgMult;
                //Dust.NewDustDirect(head.TopLeft(), head.Width, head.Height, DustID.TreasureSparkle, Projectile.direction * -2, -4);
                Dust.NewDustDirect(head.TopLeft(), head.Width, head.Height, DustID.TreasureSparkle, Projectile.direction, -5);
                Dust.NewDustDirect(head.TopLeft(), head.Width, head.Height, DustID.TreasureSparkle, Projectile.direction * 2, -4);
            }
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            JoostPlayer modPlayer = player.GetModPlayer<JoostPlayer>();
            Projectile.scale = player.inventory[player.selectedItem].scale;
            Projectile.width = (int)(64 * Projectile.scale);
            Projectile.height = (int)(64 * Projectile.scale);

            if (Projectile.ai[0] == 0)
            {
                Projectile.ai[1] = 0;
                Projectile.ai[0] = 1;
            }
            var source = Projectile.GetSource_FromAI();
            if (!player.noItems && !player.CCed && !player.dead)
            {
                float scaleFactor6 = 1f;
                if (player.inventory[player.selectedItem].shoot == Projectile.type)
                {
                    scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                }
                Projectile.velocity.Y = 0;
                Projectile.direction = Projectile.velocity.X > 0 ? 1 : -1;
                Projectile.velocity.X = Projectile.direction;
                //Projectile.direction *= (int)player.gravDir;
            }
            else
            {
                player.fullRotation = 0f;
                Projectile.Kill();
            }
            if (Projectile.ai[0] == 1)
            {
                float speed = player.GetAttackSpeed(DamageClass.Melee);

                Projectile.ai[1] += speed;
                float rad = 0;
                if (player.gravDir < 0)
                {
                    rad += (float)(Math.PI + (Math.PI / 2 * Projectile.direction));
                }
                if (Projectile.ai[1] < startup)
                {
                    Projectile.rotation = player.fullRotation + (startup - Projectile.ai[1]) * (float)(Math.PI / 180) * 5 * Projectile.direction * (int)player.gravDir + rad;
                }
                if (Projectile.ai[1] >= startup && (Projectile.ai[1] < startup + active || Projectile.localAI[1] == 1))
                {
                    if (Projectile.localAI[0] == 0)
                    {
                        SoundEngine.PlaySound(SoundID.Item1.WithVolumeScale(1.2f).WithPitchOffset(-0.3f), Projectile.Center);
                        Projectile.localAI[0] = 1;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            Vector2 dir = Main.MouseWorld - player.Center;
                            dir.Normalize();
                            if (dir.HasNaNs())
                            {
                                dir = Vector2.UnitX * player.direction;
                            }
                            if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                            {
                                Projectile.netUpdate = true;
                            }
                            Projectile.direction = dir.X > 0 ? 1 : -1;
                            Projectile.velocity.X = Projectile.direction;
                        }
                        if (player.velocity.Y != 0 && !player.mount.Active)
                        {
                            Projectile.localNPCHitCooldown = active;
                            Projectile.localAI[1] = 1;
                        }
                    }
                    if (Projectile.localAI[1] == 1)
                    {
                        player.fullRotation = (Projectile.ai[1] - startup) * (float)(Math.PI/180) * player.direction * player.gravDir * (360 / (active + endlag));
                        player.mount.Dismount(player);
                    }
                    if (Projectile.ai[1] < startup + active)
                    {
                        Projectile.rotation = player.fullRotation + (Projectile.ai[1] - startup) * (float)(Math.PI/180) * Projectile.direction * (int)player.gravDir * (180 / active) + rad;
                    }
                    else
                    {
                        Projectile.rotation = player.fullRotation + 180 * (float)(Math.PI/180) * Projectile.direction * (int)player.gravDir + rad;
                    }
                }
                if (Projectile.ai[1] >= startup + active + endlag)
                {
                    Projectile.Kill();
                }
                Projectile.timeLeft = 2;
                if (Projectile.ai[1] < startup)
                {
                    player.itemTime = (int)(40 * speed);
                    player.itemAnimation = (int)(40 * speed);
                }
                else if (Projectile.ai[1] <= startup + active)
                {
                    player.itemTime = (int)(40 * speed - (Projectile.ai[1] - startup) * speed * 4);
                    player.itemAnimation = (int)(40 * speed - (Projectile.ai[1] - startup) * speed * 4);
                }
                else
                {
                    player.itemTime = 2;
                    player.itemAnimation = 2;
                }
                player.itemRotation = Projectile.rotation - player.fullRotation;
                if (player.itemTime < 2)
                {
                    player.itemTime = 2;
                }
                if (player.itemAnimation < 2)
                {
                    player.itemAnimation = 2;
                }
                player.itemRotation = 0;
            }
            Projectile.spriteDirection = Projectile.direction * (int)player.gravDir;
            player.fullRotationOrigin = player.Center - player.position;
            int length = 50;
            //float r = Projectile.rotation * 180 / (float)Math.PI;
            //Main.NewText(r, Color.Teal);

            double rotate = (Projectile.rotation + 1.566 + 2.349 * Projectile.direction);
            Vector2 vect = new Vector2((float)Math.Cos(rotate) * length * Projectile.scale, (float)Math.Sin(rotate) * length * Projectile.scale);
            //vect.X *= (int)player.gravDir;

            //r = vect.ToRotation() * 180 / (float)Math.PI;
            //Main.NewText(r, Color.Purple);

            Projectile.position = player.MountedCenter - Projectile.Size / 2f + vect;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
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
            float rot = Projectile.rotation;
            if (Main.player[Projectile.owner].gravDir < 0)
            {
                rot -= (float)(Math.PI/2 * Projectile.direction);
            }
            Color color = lightColor;
            Main.EntitySpriteDraw(tex, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, rot, new Vector2(tex.Width / 2, tex.Height / 2), Projectile.scale, effects, 0);

            //Rectangle head = HeadBox();
            //Main.EntitySpriteDraw(tex, head.Center() - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Rectangle?(head), Color.Red, 0, head.Size() / 2, Projectile.scale, effects, 0);

            return false;
        }
        public override void OnKill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.localAI[1] == 1)
            {
                player.fullRotation = 0;
            }
        }
    }
}