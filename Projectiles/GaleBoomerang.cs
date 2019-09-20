using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class GaleBoomerang : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gale Boomerang");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            Main.projFrames[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 64;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 1800;
			projectile.usesIDStaticNPCImmunity = true;
			projectile.idStaticNPCHitCooldown = 9;
			aiType = ProjectileID.Bullet;
		}
		public override void AI()
        {
            Player player = Main.player[projectile.owner];
            projectile.rotation = 0;
			if (projectile.timeLeft % 5 == 0)
			{
				Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 7);
				int num1 = Dust.NewDust(
				projectile.position,
				projectile.width,
				projectile.height,
				51, //Dust ID
				Main.rand.Next(5) - 2,
				Main.rand.Next(5) - 2,
				100, //alpha goes from 0 to 255
				default(Color),
				1f
				);
			Main.dust[num1].noGravity = true;
			}
			/*if (projectile.timeLeft <= 540 && projectile.timeLeft > 495)
			{
				projectile.velocity.Y = projectile.velocity.Y * 0.95f;
				projectile.velocity.X = projectile.velocity.X * 0.95f;
			}*/
			for(int i = 0; i < Main.item.Length; i++)
				{
					if(Main.item[i].active)
					{
						Item I = Main.item[i];
						if(projectile.Hitbox.Intersects(I.Hitbox))
						{
							I.velocity = I.DirectionTo(projectile.Center)*5;
							I.position += I.velocity;
						}
					}
				}
			if (projectile.timeLeft <= 1695)
			{
				projectile.aiStyle = 3;
				projectile.tileCollide = false;
			}
			projectile.frameCounter++;
			if (projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 6;
			}
            for (int n = 0; n < 200; n++)
            {
                NPC target = Main.npc[n];
                if (projectile.Colliding(projectile.getRect(), target.getRect()))
                {
                    bool tooClose = player.Distance(projectile.Center) < 80 && player.Distance(projectile.Center) < player.Distance(projectile.oldPosition + projectile.Size / 2);
                    if (target.active && !target.friendly && !target.dontTakeDamage && target.type != 488 && !target.boss && target.knockBackResist > 0)
                    {
                        if (target.knockBackResist > 1f - projectile.knockBack / 10)
                        {
                            if (tooClose)
                            {
                                target.velocity = new Vector2(projectile.knockBack * (target.Center.X < player.Center.X ? -0.5f : 0.5f), projectile.knockBack * (player.Center.Y < projectile.Center.Y ? 1 : -1));
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
                                target.velocity = new Vector2(projectile.knockBack * target.knockBackResist * (target.Center.X < player.Center.X ? -0.5f : 0.5f), projectile.knockBack * target.knockBackResist * projectile.velocity.Length() * (player.Center.Y < projectile.Center.Y ? 1 : -1));
                            }
                            else
                            {
                                target.velocity = projectile.velocity * target.knockBackResist;
                                target.velocity.Y -= target.noGravity || projectile.aiStyle == 3 || player.Distance(projectile.Center) < 80 ? 0 : 0.4f * target.knockBackResist;
                            }
                        }
                    }
                }
            }
		}
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
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
                if (target.knockBackResist > 1f - projectile.knockBack / 10)
                {
                    if (tooClose)
                    {
                        target.velocity = new Vector2(projectile.knockBack * (target.Center.X < player.Center.X ? -0.5f : 0.5f), projectile.knockBack * (player.Center.Y < projectile.Center.Y ? 1 : -1));
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
                        target.velocity = new Vector2(projectile.knockBack * target.knockBackResist * (target.Center.X < player.Center.X ? -0.5f : 0.5f), projectile.knockBack * target.knockBackResist * projectile.velocity.Length() * (player.Center.Y < projectile.Center.Y ? 1 : -1));
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
			if (projectile.velocity.X != oldVelocity.X)
			{
				projectile.velocity.X = -oldVelocity.X;
			}
			if (projectile.velocity.Y != oldVelocity.Y)
			{
				projectile.velocity.Y = -oldVelocity.Y;
			}
			return false;
		}

	}
}

