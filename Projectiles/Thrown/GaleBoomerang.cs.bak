using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles.Thrown
{
    public class GaleBoomerang : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gale Boomerang");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1800;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 9;
            AIType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation = 0;
            if (Projectile.timeLeft % 5 == 0)
            {
                SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
                int num1 = Dust.NewDust(
                Projectile.position,
                Projectile.width,
                Projectile.height,
                51, //Dust ID
                Main.rand.Next(5) - 2,
                Main.rand.Next(5) - 2,
                100, //alpha goes from 0 to 255
                default,
                1f
                );
                Main.dust[num1].noGravity = true;
            }
            /*if (projectile.timeLeft <= 540 && projectile.timeLeft > 495)
			{
				projectile.velocity.Y = projectile.velocity.Y * 0.95f;
				projectile.velocity.X = projectile.velocity.X * 0.95f;
			}*/
            for (int i = 0; i < Main.item.Length; i++)
            {
                if (Main.item[i].active)
                {
                    Item I = Main.item[i];
                    if (Projectile.Hitbox.Intersects(I.Hitbox))
                    {
                        I.velocity = I.DirectionTo(Projectile.Center) * 5;
                        I.position += I.velocity;
                    }
                }
            }
            if (Projectile.timeLeft <= 1695)
            {
                Projectile.aiStyle = 3;
                Projectile.tileCollide = false;
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = (Projectile.frame + 1) % 6;
            }
            for (int n = 0; n < 200; n++)
            {
                NPC target = Main.npc[n];
                if (Projectile.Colliding(Projectile.getRect(), target.getRect()))
                {
                    bool tooClose = player.Distance(Projectile.Center) < 80 && player.Distance(Projectile.Center) < player.Distance(Projectile.oldPosition + Projectile.Size / 2);
                    if (target.active && !target.friendly && !target.dontTakeDamage && target.type != 488 && !target.boss && target.knockBackResist > 0)
                    {
                        if (target.knockBackResist > 1f - Projectile.knockBack / 10)
                        {
                            if (tooClose)
                            {
                                target.velocity = new Vector2(Projectile.knockBack * (target.Center.X < player.Center.X ? -0.5f : 0.5f), Projectile.knockBack * (player.Center.Y < Projectile.Center.Y ? 1 : -1));
                            }
                            else
                            {
                                target.velocity = Projectile.velocity;
                                target.velocity.Y -= target.noGravity || Projectile.aiStyle == 3 || player.Distance(Projectile.Center) < 80 ? 0 : 0.4f;
                            }
                        }
                        else
                        {
                            if (tooClose)
                            {
                                target.velocity = new Vector2(Projectile.knockBack * target.knockBackResist * (target.Center.X < player.Center.X ? -0.5f : 0.5f), Projectile.knockBack * target.knockBackResist * Projectile.velocity.Length() * (player.Center.Y < Projectile.Center.Y ? 1 : -1));
                            }
                            else
                            {
                                target.velocity = Projectile.velocity * target.knockBackResist;
                                target.velocity.Y -= target.noGravity || Projectile.aiStyle == 3 || player.Distance(Projectile.Center) < 80 ? 0 : 0.4f * target.knockBackResist;
                            }
                        }
                    }
                }
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 20;
            height = 20;
            return true;
        }
        /*
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            Player player = Main.player[projectile.owner];
            bool tooClose = player.Distance(projectile.Center) < 80 && player.Distance(projectile.Center) < player.Distance(projectile.oldPosition + projectile.Size / 2);
            if (target.active && !target.friendly && !target.dontTakeDamage && target.type != 488 && !target.boss && target.knockBackResist > 0)
            {
                if (target.knockBackResist > 1f - projectile.knockback / 10)
                {
                    if (tooClose)
                    {
                        target.velocity = new Vector2(projectile.knockback * (target.Center.X < player.Center.X ? -0.5f : 0.5f), projectile.knockback * (player.Center.Y < projectile.Center.Y ? 1 : -1));
                    }
                    else
                    {
                        target.velocity = projectile.velocity;
                        target.velocity.Y -= target.noGravity || projectile.aiStyle == 3 || player.Distance(projectile.Center) < 80 ? 0 : 0.4f;
                    }
                }
                else
                {
                    if (tooClose)
                    {
                        target.velocity = new Vector2(projectile.knockback * target.knockBackResist * (target.Center.X < player.Center.X ? -0.5f : 0.5f), projectile.knockback * target.knockBackResist * projectile.velocity.Length() * (player.Center.Y < projectile.Center.Y ? 1 : -1));
                    }
                    else
                    {
                        target.velocity = projectile.velocity * target.knockBackResist;
                        target.velocity.Y -= target.noGravity || projectile.aiStyle == 3 || player.Distance(projectile.Center) < 80 ? 0 : 0.4f * target.knockBackResist;
                    }
                }
            }
		}
        */
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

    }
}

