using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
    public class DoomCannon : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doom Cannon");
			Main.projFrames[Projectile.type] = 12;
		}
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, true);
            if (player.controlUseTile)
            {
                Projectile.ai[0] = 0;
                Projectile.netUpdate = true;
                Projectile.netUpdate2 = true;
                Projectile.Kill();
            }
            bool channeling = player.channel && !player.noItems && !player.CCed;
            if (channeling)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    float scaleFactor6 = 1f;
                    if (player.inventory[player.selectedItem].shoot == Projectile.type)
                    {
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * Projectile.scale;
                    }
                    Vector2 dir = Main.MouseWorld - playerPos;
                    Projectile.direction = dir.X > 0 ? 1 : -1;
                    if (Vector2.Distance(Main.MouseWorld, playerPos) > 40)
                    {
                        dir = Main.MouseWorld - Projectile.Center;
                    }
                    dir.Normalize();
                    if (dir.HasNaNs())
                    {
                        dir = Vector2.UnitX * player.direction;
                    }
                    dir *= scaleFactor6;
                    if (dir.X != Projectile.velocity.X || dir.Y != Projectile.velocity.Y)
                    {
                        Projectile.netUpdate = true;
                    }
                    Projectile.velocity = dir;
                } 
            }
            else
            {
                Projectile.Kill();
            }
            if (Projectile.ai[0] < 60)
            {
                Projectile.frame = 0;
            }
            else if (Projectile.ai[0] < 120)
            {
                Projectile.frame = 1;
            }
            else if (Projectile.ai[0] < 180)
            {
                Projectile.frame = 2;
            }
            else if (Projectile.ai[0] < 240)
            {
                Projectile.frame = 3;
            }
            else if (Projectile.ai[0] < 300)
            {
                Projectile.frame = 4;
            }
            else if (Projectile.ai[0] < 360)
            {
                Projectile.frame = 5;
                player.velocity.X *= 0.99f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.99f;
                }
            }
            else if (Projectile.ai[0] < 420)
            {
                Projectile.frame = 6;
                player.velocity.X *= 0.975f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.985f;
                }
            }
            else if (Projectile.ai[0] < 480)
            {
                Projectile.frame = 7;
                player.velocity.X *= 0.96f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.96f;
                }
            }
            else if (Projectile.ai[0] < 540)
            {
                Projectile.frame = 8;
                player.velocity.X *= 0.945f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.945f;
                }
            }
            else if (Projectile.ai[0] < 600)
            {
                Projectile.frame = 9;
                player.velocity.X *= 0.93f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.93f;
                }
            }
            else if (Projectile.ai[0] < 660)
            {
                Projectile.frame = 10;
                player.velocity.X *= 0.915f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.915f;
                }
            }
            else
            {
                player.velocity.X *= 0.9f;
                if (player.velocity.Y * player.gravDir < 0)
                {
                    player.velocity.Y *= 0.9f;
                }
                Vector2 vel = Vector2.Normalize(Projectile.velocity);
                if (Math.Abs(vel.X) < 0.15f)
                {
                    Projectile.velocity.X = 0;
                }
                if (Math.Abs(vel.Y) < 0.15f)
                {
                    Projectile.velocity.Y = 0;
                }
                Projectile.frame = 11;
                int dust2 = Dust.NewDust(new Vector2(player.Center.X - 4, player.Center.Y + player.height / 2 * player.gravDir), 1, 1, 261, 5, -3 * player.gravDir, 0, default(Color), 1);
                Main.dust[dust2].noGravity = true;
                int dust3 = Dust.NewDust(new Vector2(player.Center.X - 4, player.Center.Y + player.height / 2 * player.gravDir), 1, 1, 261, -5, -3 * player.gravDir, 0, default(Color), 1);
                Main.dust[dust3].noGravity = true;
            }
            if (Projectile.ai[0] <= 660)
            {
                Projectile.ai[0]++;
            }
            if (Projectile.ai[0] == 660)
            {
                SoundEngine.PlaySound(SoundID.Trackable, Projectile.position);
            }
            if (Projectile.ai[0] % 60 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item75, Projectile.position);
                if (Projectile.ai[0] >= 360)
                {
                    SoundEngine.PlaySound(SoundID.Item114, Projectile.position);
                }
            }
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.direction == -1 ? 3.14f : 0);
            Projectile.position = playerPos - Projectile.Size / 2 + new Vector2(-14, 0) + new Vector2((float)Math.Cos(Projectile.rotation - (Math.PI / 2)), (float)Math.Sin(Projectile.rotation - (Math.PI / 2))) * 14;
            Projectile.spriteDirection = Projectile.direction;
            Projectile.timeLeft = 2;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = MathHelper.WrapAngle(Projectile.rotation);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner && !player.dead && !Main.mouseRight && Projectile.ai[0] >= 60)
            {
                SoundEngine.PlaySound(SoundID.Item62, Projectile.position);
                Vector2 pos = Projectile.Center + Projectile.velocity * 26;
                float speed = 10;
                float mult = 1;
                int type = Mod.Find<ModProjectile>("DoomSkull").Type;
                if (Projectile.ai[0] < 120)
                {
                    speed = 10;
                    mult = 1;
                }
                else if (Projectile.ai[0] < 180)
                {
                    speed = 11;
                    mult = 2;
                }
                else if (Projectile.ai[0] < 240)
                {
                    speed = 12;
                    mult = 3;
                }
                else if (Projectile.ai[0] < 300)
                {
                    speed = 13;
                    mult = 4;
                }
                else if (Projectile.ai[0] < 360)
                {
                    speed = 14;
                    mult = 5;
                }
                else if (Projectile.ai[0] < 420)
                {
                    type = Mod.Find<ModProjectile>("DoomSkull2").Type;
                    speed = 15;
                    mult = 6;
                }
                else if (Projectile.ai[0] < 480)
                {
                    type = Mod.Find<ModProjectile>("DoomSkull2").Type;
                    speed = 16;
                    mult = 7;
                }
                else if (Projectile.ai[0] < 540)
                {
                    type = Mod.Find<ModProjectile>("DoomSkull2").Type;
                    speed = 17;
                    mult = 8;
                }
                else if (Projectile.ai[0] < 600)
                {
                    type = Mod.Find<ModProjectile>("DoomSkull2").Type;
                    speed = 18;
                    mult = 9;
                }
                else if (Projectile.ai[0] < 660)
                {
                    type = Mod.Find<ModProjectile>("DoomSkull2").Type;
                    speed = 19;
                    mult = 10;
                }
                else
                {
                    type = Mod.Find<ModProjectile>("DoomSkull3").Type;
                    speed = 7;
                    mult = 11;
                    SoundEngine.PlaySound(SoundID.Item74, Projectile.position);
                    pos = Projectile.Center + Projectile.velocity * 140;
                }
                if (float.IsNaN(Projectile.velocity.X) || float.IsNaN(Projectile.velocity.Y))
                {
                    Projectile.velocity = -Vector2.UnitY;
                }
                Projectile.NewProjectile(pos, Projectile.velocity * speed, type, (int)(Projectile.damage * mult), Projectile.knockBack * mult, Projectile.owner);
            }
        }
    }
}