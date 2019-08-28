using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JoostMod.Projectiles
{
	public class HomingSoulmass : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Homing Soulmass");
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
		public override void SetDefaults()
		{
			projectile.width = 36;
			projectile.height = 36;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 6000;
			projectile.tileCollide = false;
            projectile.minion = true;
            projectile.alpha = 55;
            projectile.magic = true;
		}
		public override void AI()
		{
			if (projectile.timeLeft % 2 == 0)
			{
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
			double deg = projectile.ai[0];
			double rad = deg * (Math.PI / 180);
            projectile.ai[1]++;
            Player player = Main.player[projectile.owner];
            if (player.gravDir < 0)
            {
                rad += Math.PI; 
            }
            if (projectile.localAI[1] == 0)
            {
                projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * 50) - projectile.width / 2;
                projectile.position.Y = (player.Center.Y - 16*player.gravDir) - (int)(Math.Sin(rad) * 50) - projectile.height / 2;
                projectile.direction = player.direction * (int)player.gravDir;
            }
            projectile.spriteDirection = projectile.direction;
            Vector2 move = Vector2.Zero;
            float distance = 800f;
            bool target = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                Vector2 newMove = npc.Center - projectile.Center;
                float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                if (distanceTo < distance)
                {
                    move = newMove;
                    distance = distanceTo;
                    target = true;
                }
            }
            else for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.active && !npc.dontTakeDamage && npc.lifeMax > 5 && npc.CanBeChasedBy(this, false) && Collision.CanHit(new Vector2(projectile.Center.X, projectile.Center.Y), 1, 1, npc.position, npc.width, npc.height))
                {
                    Vector2 newMove = npc.Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                    }
                }
            }
            projectile.rotation = (float)rad + (projectile.timeLeft * -12 * (float)Math.PI / 180f * projectile.direction);
            if (target && projectile.ai[1] > 90 && Collision.CanHitLine(projectile.Center, 1, 1, move + projectile.Center, 1, 1) && projectile.localAI[1] == 0)
            {
                projectile.velocity = projectile.DirectionTo(move + projectile.Center) * 8;
                projectile.localAI[0] = projectile.velocity.Length();
                Main.PlaySound(2, projectile.Center, 9);
                projectile.localAI[1] = 1;
                projectile.ai[0] = 0;
                projectile.tileCollide = true;
                projectile.timeLeft = 600;
            }
            if (projectile.localAI[1] == 1)
            {
                if (target)
                {
                    if (move.Length() > projectile.localAI[0] && projectile.localAI[0] > 0)
                    {
                        move *= projectile.localAI[0] / move.Length();
                    }
                    float home = 15f;
                    projectile.velocity = ((home - 1f) * projectile.velocity + move) / home;
                }
                if (projectile.velocity.Length() < projectile.localAI[0] && projectile.localAI[0] > 0)
                {
                    projectile.velocity *= (projectile.localAI[0] / projectile.velocity.Length());
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.Kill();
        }
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 12; i++)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92);
                Main.dust[dustIndex].noGravity = true;
            }
            Main.PlaySound(2, projectile.Center, 27);
		}
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 20;
            height = 20;
            return true;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Texture2D tex = Main.projectileTexture[projectile.type];
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            SpriteEffects effects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                effects = SpriteEffects.FlipHorizontally;
            }
            Color color = Color.White;
            color *= 0.9f;
            sb.Draw(tex, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, 0, tex.Width, tex.Height)), color, projectile.rotation, new Vector2(tex.Width / 2, tex.Height / 2), projectile.scale, effects, 0f);
            return false;
        }
    }
}

